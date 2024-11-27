using System;
using System.Numerics;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimAndShoot : MonoBehaviour
{

    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    private GameObject bulletInst;
    public GameObject[] allObjects;
    public GameObject nearestObject;
    private UnityEngine.Vector2 worldMousePos, worldEnemyPos;
    private UnityEngine.Vector2 direction;
    float distance, nearestDistance;
    public static bool autoShoot = false;

    void Start(){
        InvokeRepeating("HandleGunShooting", 2f, 2f);

    }

    void Update()
    {
        if(!autoShoot){
            HandleGunRotation();
        }
        Flip();
        
    }

    private void HandleGunRotation(){
        if(!autoShoot){
            //rotate the gun towards the mouse position
            worldMousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            direction = (worldMousePos - (UnityEngine.Vector2)gun.transform.position).normalized;
            gun.transform.right = direction;
        }else{
            direction = ((UnityEngine.Vector2)nearestObject.transform.position - (UnityEngine.Vector2)gun.transform.position).normalized;
            gun.transform.right = direction;
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
        if(!PlayerScript.gamePause){
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

                if(autoShoot){
                    HandleGunRotation();
                }
            }


            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);  
        }

    }


}
