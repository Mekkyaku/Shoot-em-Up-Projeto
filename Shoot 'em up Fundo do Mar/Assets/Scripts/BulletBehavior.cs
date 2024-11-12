using Unity.VisualScripting;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private LayerMask whatDestroysBullet;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDestroyTime();
        SetStraightVelocity();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        //is the collision within the whatDestroysBullet layerMask
        if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0){
            //spawn particles
            
            //play soundfx

            //screenshake

            //damage enemy
            
            //destroy the bullet
            Destroy(gameObject);
        }
    }

    private void SetStraightVelocity(){
        rb.linearVelocity = transform.right * speed;
    }

    private void SetDestroyTime(){
        Destroy(gameObject, destroyTime);
    }
}
