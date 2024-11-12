using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float horizontal, vertical;
    private float speed = 5f;
    public static bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private LayerMask groundLayer;

    // Start() roda antes da PRIMEIRA execução do Update()
    void Start()
    {
       
    }

    // Update é mais utilizado para inputs, pois roda a cada frame.
    void Update()
    {
       horizontal = Input.GetAxisRaw("Horizontal");
       vertical = Input.GetAxisRaw("Vertical");
       Debug.Log("Player: " + transform.position);
       Flip();

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

    /*private bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    */

}
