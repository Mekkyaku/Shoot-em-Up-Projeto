using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class EmpilhadeiraScript : MonoBehaviour
{
    Rigidbody2D rb;
    float timeLeft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * 5;
        timeLeft = 2;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0){
            Destroy(gameObject);
        }    
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 6){
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            //AMOUNT OF DAMAGE TO THE ENEMY
            enemy.healthPoints--;
            Debug.Log("Colidiu");
        }
    }



}
