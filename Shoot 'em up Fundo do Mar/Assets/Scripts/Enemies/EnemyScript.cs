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
    [SerializeField] GameObject xp;
    private Collider2D playerCol;
    public int healthPoints = 2;
    private bool isFacingRight;
    

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
            Instantiate(xp, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void WalkIntoPlayer(){
        if(player && !PlayerScript.gamePause){
            worldPlayerPos = player.transform.position;
            direction = (worldPlayerPos - (UnityEngine.Vector2)this.transform.position).normalized;
            rb.linearVelocity = direction * speed;

            if(direction.x > 0 && isFacingRight || direction.x < 0 && !isFacingRight){
                isFacingRight = !isFacingRight;
                UnityEngine.Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }else{
            rb.linearVelocity = new UnityEngine.Vector2(0, 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        
        if(collision.gameObject.CompareTag("Player")){
            PlayerScript.healthPoints--;
            

        }
    }
}
