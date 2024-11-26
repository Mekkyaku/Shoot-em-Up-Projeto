using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region Variáveis físicas
        private float horizontal, vertical;
        private float speed = 4f;
        public static bool isFacingRight = true;

        [SerializeField] private Rigidbody2D rb;
        public static int healthPoints = 3;
    #endregion
    #region Utilidades do jogo
        private UnityEngine.Vector3 offset = new UnityEngine.Vector3(0, 2f, 0);
        public static bool gamePause = false;
    #endregion
    #region Escolha de personagem
        public static int character;
        private SpriteRenderer sprRenderer;
        private Sprite spriteC;
    #endregion

    // Start() roda antes da PRIMEIRA execução do Update()
    void Start()
    {
        character = PlayerPrefs.GetInt("character");
        sprRenderer = GetComponent<SpriteRenderer>();
        ChosenCharacter();
        Debug.Log(character);
    }

    void OnGUI(){
        if(gameObject){
            UnityEngine.Vector3 worldPlayerPos = Camera.main.WorldToScreenPoint(transform.position + offset);
            worldPlayerPos.y = Screen.height - worldPlayerPos.y;
            GUI.Label(new Rect(worldPlayerPos.x - 20, worldPlayerPos.y + 20, 100, 20), "HP: " + healthPoints);
        }
    }

    // Update é mais utilizado para inputs, pois roda a cada frame.
    void Update()
    {
        if(healthPoints > 0){
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
        }else{
            if(!gamePause){
                Destroy(gameObject);
                gamePause = true;
            }
        }
        
    }

    //FixedUpdate é mais utilizado para física, pois roda a cada 0.02s
    private void FixedUpdate(){
        if(!gamePause){
            rb.linearVelocity = new UnityEngine.Vector2(horizontal * speed, vertical * speed);
        }
    }

    private void Flip(){
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f){
            isFacingRight = !isFacingRight;
            UnityEngine.Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

     private void OnCollisionEnter2D(Collision2D collision){
        
        if(collision.gameObject.CompareTag("Enemy")){
            healthPoints--;
            

        }
    }

    private void ChosenCharacter(){
        switch(character){
            case 1:
                spriteC = Resources.Load<Sprite>("Sprites/Construtor");
                sprRenderer.sprite = spriteC;
                break;
            case 2:
                spriteC = Resources.Load<Sprite>("Sprites/Natureza");
                sprRenderer.sprite = spriteC;
                break;
            case 3:
                spriteC = Resources.Load<Sprite>("Sprites/Nerd");
                sprRenderer.sprite = spriteC;
                break;
            case 4:
                spriteC = Resources.Load<Sprite>("Sprites/Domador");
                sprRenderer.sprite = spriteC;
                break;
            case 5:
                spriteC = Resources.Load<Sprite>("Sprites/Encantador");
                sprRenderer.sprite = spriteC;
                break;
        }

        if(spriteC){
            Debug.Log(spriteC);
            Debug.Log("Sprite carregada com sucesso!");
        }else{
            Debug.Log("Sprite não foi carregada!");
        }
    }

    /*private bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    */

}
