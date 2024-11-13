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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start(){
        player = GameObject.FindWithTag("Player");
    }
    void Update(){
        WalkIntoPlayer();
    }

    void WalkIntoPlayer(){
        worldPlayerPos = player.transform.position;
        direction = (worldPlayerPos - (UnityEngine.Vector2)this.transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

}
