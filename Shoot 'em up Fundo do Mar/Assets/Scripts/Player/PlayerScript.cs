using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    #region Physics variables
        private float horizontal, vertical;
        private float speed = 4f;
        public static bool isFacingRight = true;

        [SerializeField] private Rigidbody2D rb;
    #endregion
    #region Game utility
        private UnityEngine.Vector3 offset = new UnityEngine.Vector3(0, 2f, 0);
        public static bool gamePause;
        public static float xp, xpMax;
        public static int level;
        public static int healthPoints;
    #endregion
    #region Escolha de personagem
        [SerializeField] GameObject gun;
        [SerializeField] GameObject bullet;
        [SerializeField] GameObject UI, lvlUp;
        public static int character;
        private SpriteRenderer sprRenderer, sprRendererW, sprRendererB;
        private Image[] sprRendererUI;
        private Sprite spriteC, spriteW;
    #endregion

    // Start() roda uma vez e antes da PRIMEIRA execução do Update()
    void Start()
    {
        gamePause = false;
        xp = 0; xpMax = 5; level = 0;
        healthPoints = 3;
        #region GETTING ALL SPRITES AND SETTING THEM
            character = PlayerPrefs.GetInt("character");
            sprRenderer = GetComponent<SpriteRenderer>();
            sprRendererW = gun.GetComponent<SpriteRenderer>();
            sprRendererB = bullet.GetComponent<SpriteRenderer>();
            sprRendererUI = UI.GetComponentsInChildren<Image>();
        #endregion
        ChosenCharacter();
        Debug.Log(character);
    }

    //DEBUG ONLY
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
            #region SHOOT AND MOVEMENT
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
                }
            #endregion
        }else{
            //PLAYER'S DEATH
            if(!gamePause){
                Destroy(gameObject);
                gamePause = true;
                SceneManager.LoadSceneAsync(0);
            }
        }
        
        #region UI Control
            if(UI.activeSelf == true){
                gamePause = false;
            }else{
                gamePause = true;
            }
        #endregion

        LevelUp();
        //Debug.Log("XP: " + xp);
        //Debug.Log("XP Máximo: " + xpMax);
    }

    //FixedUpdate é mais utilizado para física, pois roda a cada 0.02s
    private void FixedUpdate(){
        if(!gamePause){
            rb.linearVelocity = new UnityEngine.Vector2(horizontal * speed, vertical * speed);
        }else{
            rb.linearVelocity = new UnityEngine.Vector2(0, 0);
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

    private void ChosenCharacter(){
        //Probably i'll change that, maybe switch all this code to AimAndShoot instead of letting it here
        switch(character){
            //Constructor case
            case 1:
                spriteC = Resources.Load<Sprite>("Sprites/Constructor");
                sprRenderer.sprite = spriteC;
                spriteW = Resources.Load<Sprite>("Sprites/Weapons/InGame/Brick");
                sprRendererW.sprite = spriteW;
                //Here we use the same sprite than the weapon cause its the same. 
                //Maybe when throwing the brick it disappear for a second of the hand of the player then it appears again.
                sprRendererB.sprite = spriteW;
                //Here we need to add a condition that compares the order of the chosen ability to add its image in order on the HUD. sprRendererUI.length maybe
                sprRendererUI[1].sprite = Resources.Load<Sprite>("Sprites/Weapons/Frames/BrickFrame");
                break;

            //Nature case
            case 2:
                spriteC = Resources.Load<Sprite>("Sprites/Nature");
                sprRenderer.sprite = spriteC;
                break;

            //Nerd case
            case 3:
                spriteC = Resources.Load<Sprite>("Sprites/Nerd");
                sprRenderer.sprite = spriteC;
                break;

            //Handler case
            case 4:
                spriteC = Resources.Load<Sprite>("Sprites/Handler");
                sprRenderer.sprite = spriteC;
                break;

            //Enchanter case
            case 5:
                spriteC = Resources.Load<Sprite>("Sprites/Enchanter");
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

    private void LevelUp(){
        if(xp >= xpMax){
            xp = 0;
            xpMax = xpMax * 1.5f;
            gamePause = true;
            lvlUp.SetActive(true);
            UI.SetActive(false);
        }
    }

    /*private bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    */

}