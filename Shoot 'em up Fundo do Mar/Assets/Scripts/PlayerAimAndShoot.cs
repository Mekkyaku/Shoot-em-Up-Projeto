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
    private UnityEngine.Vector2 worldMousePos;
    private UnityEngine.Vector2 direction;

    void Update()
    {
        HandleGunRotation();
        Flip();
        HandleGunShooting();
    }

    private void HandleGunRotation(){
        //rotate the gun towards the mouse position
        worldMousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldMousePos - (UnityEngine.Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;
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

        Debug.Log(gun.transform.right);
    }

    private void HandleGunShooting(){
        if (Mouse.current.leftButton.wasPressedThisFrame){
            //spawn bullet
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);
        }
    }
}
