using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    //All enemies walks into player and do damage
    private float speed = 2f;
    private Rigidbody2D rb ;
    private Vector2 worldPlayerPos;
    private Vector2 direction;
    GameObject player;
    private Collider2D playerCol;
    public int healthPoints = 2;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start(){
        player = GameObject.FindWithTag("Player");
    }
    void Update(){
        WalkIntoPlayer();
        if(healthPoints <= 0){
            Destroy(gameObject);
        }
    }

    void WalkIntoPlayer(){
        if(!PlayerScript.gamePause){
            worldPlayerPos = player.transform.position;
            direction = (worldPlayerPos - (UnityEngine.Vector2)this.transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }else{
            rb.linearVelocity = new UnityEngine.Vector2(0, 0);
        }
    }

}
