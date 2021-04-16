using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    Rigidbody2D myRigidbody;
    Animator anim;
    Vector2 characterDirection = new Vector2(0, -1); //Character direction
    Vector2 bulletDirection = new Vector2(0, -1); //shoot where your aim at (facing)
    Vector2 stoneDirection = new Vector2(0, -1);
    public ShopManager shopManager;


    [Header("Character properties")]
    [SerializeField] public float speed = 300.0f; //Character moving
    public bool isCharacterMoving = false;

    [Header("Ammo system")]
    [SerializeField] public float ammoRange = 10f; //bullet range
    public GameObject ammo;
    public int currentAmmo = 0;
    public GameObject stone;
    private float shootrate=0.2f;
    private float nextShootTime;
    public bool hasUnlimitBullet = false;

    [Header("Scale system")]
    [SerializeField] private int changeScalePickUp = 1;
    [SerializeField] private int currentChangeScale = 0;
    [SerializeField] private float changeScaleTimer = 0f;

    [Header("Door system")]
    [SerializeField] private GameObject ButtonDoor;
        
    [Header("Other")]
    [SerializeField] private Text ammoText;
    [SerializeField] private int npcCount = 0;

    private int keyNumber = 0;

    [Header("Player hurt effect")]
    private SpriteRenderer sr;
    private Color original;
    private float effectTimer = 0.2f;

     //sound effect
    [SerializeField] AudioSource pickUpAudio;
    [SerializeField] AudioSource saveAudio;
    




    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentAmmo = 0;
        currentChangeScale = 0;
        gameManager = GameManager.instance;
        ButtonDoor = GameObject.FindGameObjectWithTag("ButtonDoor");
        sr = GetComponent<SpriteRenderer>();
        original = sr.color;
        npcCount = GameObject.FindGameObjectsWithTag("NPC").Length; //find how many npc in the current level

    }

    private void Update()
    {
        attack();
        IsWin(npcCount);
        reduceScale();
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            OnRotate();
        }
        OnMove();

        if (changeScaleTimer > 0)
        {
            changeScaleTimer -= Time.fixedDeltaTime;
        }
        if (changeScaleTimer <= 0)
        {

            transform.localScale = new Vector2(1.0f, 0.77f);

        }

        isMoving();
        if (isCharacterMoving == true)
        {
            gameManager.enegryChange();
        }
        else if (isCharacterMoving == false)

        {
            gameManager.enegryback();
        }

        saveMode();

        if(hasUnlimitBullet)
        {
            currentAmmo = 500;
            gameManager.getAmmo(currentAmmo);
        }
        else
        gameManager.getAmmo(currentAmmo);


        if (keyNumber <= 0)
        {
            gameManager.hasKey = false;
        }
      

    }

    void FixedUpdate()
    {

        Animate();
        Movement();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AmmoPickUp"))
        {
            currentAmmo += 5;
            Destroy(collision.gameObject);
            pickUpAudio.Play();
        }//AmmoPickUp
        else if (collision.gameObject.CompareTag("ChangeScale"))
        {
            currentChangeScale += changeScalePickUp;
            Destroy(collision.gameObject);
            pickUpAudio.Play();

        }//ScaleChangePickUp
        else if (collision.gameObject.CompareTag("hpLoose") && gameManager.isProtect == false)
        {
            gameManager.HPLose();
            //hurtEffect(effectTimer);
        }
        else if (collision.gameObject.CompareTag("HPadd"))
        {
            gameManager.hpAdd();
            Destroy(collision.gameObject);
            pickUpAudio.Play();

        }
        else if (collision.gameObject.CompareTag("coin"))
        {
            gameManager.coinCollect();
            Destroy(collision.gameObject);
            pickUpAudio.Play();

        }
        else if (collision.gameObject.CompareTag("diamond"))
        {
            gameManager.diamondCollect();
            Destroy(collision.gameObject);
            pickUpAudio.Play();

        }
        else if (collision.gameObject.CompareTag("Door"))
        {
            gameManager.inDoorArea = true;
            if (gameManager.inDoorArea == true && gameManager.hasKey == true)
           {
                keyNumber -= 1;
                Destroy(collision.gameObject);
           }
        }
        else if (collision.gameObject.CompareTag("key"))
        {
            keyNumber += 1;
            gameManager.hasKey = true;
            Destroy(collision.gameObject);
            pickUpAudio.Play();
        }
        else if (collision.gameObject.CompareTag("DoorButton"))
        {
            Destroy(ButtonDoor);
        }
        else if (collision.gameObject.CompareTag("food"))
        {
            gameManager.enegryAdd();
            Destroy(collision.gameObject);
            pickUpAudio.Play();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            gameManager.inDoorArea = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            Destroy(collision.gameObject);
            //collision.gameObject.SetActive(false);
            npcCount--;
            saveAudio.Play(); 

        }
    }

    public void hurtEffect(float time)
    {
        sr.color = Color.red;
        Invoke("resetEffect", time);
    }
    private void resetEffect()
    {
        sr.color = original;
    }


    //character movement
    public void OnMove() 
     {
            characterDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
	 } //bullet move dir

    private void isMoving()
    {

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            isCharacterMoving = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0)
        {
            isCharacterMoving = false;
        }

    }
    private void saveMode()
    {
        if (gameManager.saveMode==true)
        {
            speed = 100;
        }
        else if (gameManager.saveMode == false)
        {

            speed = 300;
        }
    }

    //bullet move dir
    public void OnRotate()
    {
         
        bulletDirection= new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        stoneDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }

    private void Animate()
    {
         anim.SetFloat("Hor", characterDirection.x);
         anim.SetFloat("Ver", characterDirection.y);
         anim.SetFloat("Magnitude", myRigidbody.velocity.magnitude);
    }

    private void attack()
    {
        if (Input.GetButtonDown("Fire1")&&currentAmmo > 0 &&gameManager.canShoot == true&& nextShootTime < Time.time)
         {
            currentAmmo--;
            GameObject bulletRing = Instantiate(ammo, myRigidbody.position, Quaternion.identity);
            bulletRing.GetComponent<Rigidbody2D>().AddForce(bulletDirection * ammoRange, ForceMode2D.Impulse);
            nextShootTime = Time.time +shootrate;
            //shootAudio.Play();
        }
         else if (Input.GetButtonDown("Fire2"))
        {
            //Instantiate(stone, myRigidbody.position, Quaternion.identity);
            GameObject stoneCircle = Instantiate(stone, myRigidbody.position, Quaternion.identity);
            stoneCircle.GetComponent<Rigidbody2D>().AddForce(bulletDirection * ammoRange, ForceMode2D.Impulse);
        }
    }

    private void reduceScale()
    {
         if (Input.GetKeyDown(KeyCode.E) && currentChangeScale > 0)
         {
            currentChangeScale--;

            transform.localScale = new Vector2(0.5f, 0.5f);
            changeScaleTimer = 10f;
             
         }
            
    }

    public void IsWin(int npcCount)
    {

        if (npcCount == 0) //if the number of npc in current level is 0
        {
            gameManager.isWin = true;
        }
    }

    private void Movement()
    {
       
         Vector2 pos = new Vector2();
         pos += (characterDirection.normalized*speed)*Time.fixedDeltaTime;
         myRigidbody.velocity = pos;
            
	}

    public void SetUnlimitBullet()
    {
        hasUnlimitBullet = true;
    }
}

