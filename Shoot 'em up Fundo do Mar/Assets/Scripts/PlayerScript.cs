using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float horizontal, vertical;
    private float speed = 5f;
    public static bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    public static int healthPoints = 3;
    private UnityEngine.Vector3 offset = new UnityEngine.Vector3(0, 2f, 0);
    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private LayerMask groundLayer;

    // Start() roda antes da PRIMEIRA execução do Update()
    void Start()
    {
    }

    void OnGUI(){
        UnityEngine.Vector3 worldPlayerPos = Camera.main.WorldToScreenPoint(transform.position + offset);
        worldPlayerPos.y = Screen.height - worldPlayerPos.y;
        GUI.Label(new Rect(worldPlayerPos.x - 20, worldPlayerPos.y + 20, 100, 20), "HP: " + healthPoints);
    }

    // Update é mais utilizado para inputs, pois roda a cada frame.
    void Update()
    {
       horizontal = Input.GetAxisRaw("Horizontal");
       vertical = Input.GetAxisRaw("Vertical");
       Flip();
       if(Input.GetButtonDown("Fire1")){
            if(!PlayerAimAndShoot.autoShoot){
                PlayerAimAndShoot.autoShoot = true;
            }
            else{
                PlayerAimAndShoot.autoShoot = false;
            }
            Debug.Log(PlayerAimAndShoot.autoShoot);
        }

        if(healthPoints <= 0){
            Destroy(gameObject);
        }
        
    }

    //FixedUpdate é mais utilizado para física, pois roda a cada 0.02s
    private void FixedUpdate(){
        rb.linearVelocity = new UnityEngine.Vector2(horizontal * speed, vertical * speed);
    }

    private void Flip(){
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f){
            isFacingRight = !isFacingRight;
            UnityEngine.Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

     private void OnTriggerEnter2D(Collider2D collision){
        
        if(collision.gameObject.CompareTag("Enemy")){
            healthPoints--;
            

        }
    }

    /*private bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    */

}
