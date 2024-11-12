using System;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.U2D.IK;

public class PlayerHeadScript : MonoBehaviour
{




    /*Se o mouseposition > playerposition {
        scale = 1
        rotation = entre 90 e -90
    }else{
        scale = -1
        rotation = entre -90 e 90
    }
    */

    Camera cam;
    GameObject player;
    float playerposx, playerposy, modulo, soma;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Posição do mouse em pixels
        Vector3 mousepos = Input.mousePosition;

        // Converte a posição do mouse para coordenadas do mundo
        Vector3 worldMousePos = cam.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, 0));
        playerposx = player.transform.position.x;
        playerposy = player.transform.position.y;

        Vector3 direcao = worldMousePos - player.transform.position;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
        Flip();
    }

    void Flip(){
        if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270){
            transform.localScale = new Vector3(1,-1,1);
        }else{
            transform.localScale = new Vector3(1,1,1);
        }
    }
}
