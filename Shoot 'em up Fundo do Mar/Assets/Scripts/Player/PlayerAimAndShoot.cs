using System;
using System.Linq;
using System.Numerics;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimAndShoot : MonoBehaviour
{

    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject forklift;
    [SerializeField] private Transform bulletSpawnPoint;

    private GameObject bulletInst;
    public GameObject[] allObjects;
    public GameObject nearestObject;
    private UnityEngine.Vector2 worldMousePos;
    public static UnityEngine.Vector2 direction;
    float distance, nearestDistance;
    public static bool autoShoot = false;
    public static bool[] Abilities;
    float timeFL, timeBullet;

    void Start(){
        Abilities = new bool[5];

    }

    void Update()
    {
        if(!PlayerScript.gamePause){
            
            Flip();
            HandleGunRotation();
            HandleGunShooting();

            if(Abilities[1]){
                timeFL -= Time.deltaTime;
                if(timeFL <= 0){
                    UnityEngine.Vector2 offset = -direction * 3;
                    Instantiate(forklift, new UnityEngine.Vector2(transform.position.x + offset.x, transform.position.y + offset.y), gun.transform.rotation);
                    timeFL = 10;
                }
            }
        }
        
    }

    private void HandleGunRotation(){
        if(!autoShoot){
            //rotate the gun towards the mouse position
            worldMousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            direction = (worldMousePos - (UnityEngine.Vector2)gun.transform.position).normalized;
            gun.transform.right = direction;
        }else{
            if(nearestObject){
                direction = ((UnityEngine.Vector2)nearestObject.transform.position - (UnityEngine.Vector2)gun.transform.position).normalized;
                gun.transform.right = direction;
            }
        }
        
    }

    void Flip(){
        if(PlayerScript.isFacingRight){
            if(gun.transform.right.x > 0){
                gun.transform.localScale = new UnityEngine.Vector3(0.5f,0.5f,0.5f);
            }else{
                gun.transform.localScale = new UnityEngine.Vector3(0.5f,-0.5f,0.5f);
            }
        }else{
            if(gun.transform.right.x < 0){
                gun.transform.localScale = new UnityEngine.Vector3(-0.5f,-0.5f,0.5f);
            }else{
                gun.transform.localScale = new UnityEngine.Vector3(-0.5f,0.5f,0.5f);
            }
        }

    }

    private void HandleGunShooting(){
        allObjects = GameObject.FindGameObjectsWithTag("Enemy");
        nearestDistance = 10000;
        if(allObjects.Length > 0){
            for (int i = 0; i < allObjects.Length; i++){
                distance = UnityEngine.Vector3.Distance(this.transform.position, allObjects[i].transform.position);

                if(distance < nearestDistance){
                    nearestObject = allObjects[i];
                    nearestDistance = distance;
                }
            }

            
        }

        timeBullet -= Time.deltaTime;
        GameObject bullet = ObjectPool.instance.GetPooledObject();
        if(timeBullet <= 0){
            if(bullet != null){
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = gun.transform.rotation;
                bullet.SetActive(true);
                timeBullet = 2;
            }
        }

    }

}
