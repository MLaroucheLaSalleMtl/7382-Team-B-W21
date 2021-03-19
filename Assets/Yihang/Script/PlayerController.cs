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


    [Header("Character properties")]
    [SerializeField] public float speed = 300.0f; //Character moving
    public bool isCharacterMoving = false;

    [Header("Ammo system")]
    [SerializeField] public float ammoRange = 10f; //bullet range
    public GameObject ammo;
    [SerializeField] private int currentAmmo = 0;
    public GameObject stone;

    [Header("Scale system")]
    [SerializeField] private int changeScalePickUp = 1;
    [SerializeField] private int currentChangeScale = 0;
    [SerializeField] private float changeScaleTimer = 0f;

    [Header("Door system")]
    [SerializeField] private GameObject ButtonDoor;
        
    [Header("Other")]
    [SerializeField] private Text ammoText;
    [SerializeField] private int npcCount = 0;



    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentAmmo = 500;
        currentChangeScale = 0;
        gameManager = GameManager.instance;
        ButtonDoor = GameObject.FindGameObjectWithTag("ButtonDoor");                              
    }

    private void Update()
    {
        //to shoot stone
        //if (Input.GetButtonDown("Fire2"))
        //{
        //    Instantiate(stone, transform.position, Quaternion.identity);
        //}

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
        gameManager.getAmmo(currentAmmo);
        //int ammoStr = (int)currentBullet;
        //ammoText.text = ammoStr.ToString();

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
        }//AmmoPickUp
        else if (collision.gameObject.CompareTag("ChangeScale"))
        {
            currentChangeScale += changeScalePickUp;
            Destroy(collision.gameObject);

        }//ScaleChangePickUp
        else if (collision.gameObject.CompareTag("hpLoose") && gameManager.isProtect == false)
        {
            gameManager.hpLoose();
        }
        else if (collision.gameObject.CompareTag("HPadd"))
        {
            gameManager.hpAdd();
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.CompareTag("coin"))
        {
            gameManager.coinCollect();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("diamond"))
        {
            gameManager.diamondCollect();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Door"))
        {
            gameManager.inDoorArea = true;
        }
        else if (collision.gameObject.CompareTag("key"))
        {
            gameManager.hasKey = true;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("DoorButton"))
        {
            Destroy(ButtonDoor);
        }
        else if (collision.gameObject.CompareTag("food"))
        {
            gameManager.enegryAdd();
            Destroy(collision.gameObject);
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
            collision.gameObject.SetActive(false);
            npcCount++;

        }
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

         if (Input.GetButtonDown("Fire1")&&currentAmmo>0 &&gameManager.canShoot == true )
         {
             currentAmmo--;

            
             GameObject bulletRing = Instantiate(ammo, myRigidbody.position, Quaternion.identity);
             bulletRing.GetComponent<Rigidbody2D>().AddForce(bulletDirection * ammoRange, ForceMode2D.Impulse);
                
         }
         else if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(stone, transform.position, Quaternion.identity);
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

        if (npcCount == 3)
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
}

