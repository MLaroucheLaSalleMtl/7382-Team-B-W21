using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



    public class PlayerController : MonoBehaviour
    {
        Rigidbody2D rigid;
        Animator anim;
        Vector2 dir = new Vector2(0,-1); //Character direction
        Vector2 bulletDirection = new Vector2(0, -1); //shoot where your aim at (facing)

        public float speed = 300.0f; //Character moving
        public float AmmoPower = 10f; //bullet range
        public GameObject bullet;
        public bool isProtect = false;
      

        private int AmmoPickUp=5;
        private int changeScalePickUp = 1;
        private Transform EnemyBullet;
        private float MaxHp = 100;
        private GameManager gameManager;
        private float countDown = 0f;
        private float maxValue = 100;
        public bool inDoorArea=false;
        public bool hasKey = false;
        public bool characterMoving = false;
        public float maxHungryValue = 100;
        
        
     
      
    
 
     
        [SerializeField] private int currentBullet=0;
        [SerializeField] private int changeScale = 0;
        [SerializeField] private float currentHP;
        [SerializeField] private float currentValue;
        [SerializeField] private Text staText;
        [SerializeField] private int coin=0;
        [SerializeField]private GameObject door;
        [SerializeField] private GameObject ButtonDoor;
        [SerializeField] private float current_hungryValue;


        [SerializeField] private GameObject winMessage;
        [SerializeField] private int npcCount = 0;
        [SerializeField] private Text endGameMessage;



   
        // Start is called before the first frame update
        void Start() {
       
            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
      
        
            currentBullet = 0;
            changeScale = 0;
            currentHP = MaxHp;
            gameManager = GameManager.instance;
            currentValue = maxValue;
            door = GameObject.FindGameObjectWithTag("Door");
            ButtonDoor = GameObject.FindGameObjectWithTag("ButtonDoor");
            current_hungryValue = maxHungryValue;
            winMessage.SetActive(false);
    
                              
        }

         private void openDoor()
        {
        if (hasKey == true && inDoorArea == true && Input.GetKeyDown(KeyCode.G))
        {
            Destroy(door);
        }
       
        }
  
        //character movement
        public void OnMove() 
        {
            dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
	    } //bullet move dir


    private void isMoving()
    {

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            characterMoving = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0)
        {
            characterMoving = false;
        }

    }
    private void saveMode()
    {
        if (current_hungryValue <= 20)
        {
            speed = 100;
        }
        else if (current_hungryValue > 20)
        {

            speed = 300;
        }
    }


    //bullet move dir
    public void OnRotate()
        {
            bulletDirection= new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        }



        private void Animate()
        {
            anim.SetFloat("Hor", dir.x);
            anim.SetFloat("Ver", dir.y);
            anim.SetFloat("Magnitude", rigid.velocity.magnitude);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {

        if(collision.gameObject.CompareTag("NPC"))
        {
            npcCount += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("AmmoPickUp"))
        {
            currentBullet += AmmoPickUp;
            Destroy(collision.gameObject);
        }//AmmoPickUp
        else if (collision.gameObject.CompareTag("ChangeScale"))
        {
            changeScale += changeScalePickUp;
            Destroy(collision.gameObject);

        }//ScaleChangePickUp
        else if (collision.gameObject.CompareTag("EnemyBullet") && isProtect == false)
        {
            currentHP -= 5;
        }
        else if (collision.gameObject.CompareTag("trap") && isProtect == false)
        {
            currentHP -= 5;
        }
        //else if (collision.gameObject.CompareTag("EnemyMelee") && isProtect == false)
        //{
        //    currentHP -= 5;
        //}
        else if (collision.gameObject.CompareTag("HPadd"))
        {
            if (currentHP == 100)
            {
                currentHP += 0;
            }
            else if (currentHP < 100)
            {
                currentHP += 5;
            }
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.CompareTag("coin"))
        {
            coin += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Door"))
        {
            inDoorArea = true;
        }
        else if (collision.gameObject.CompareTag("key"))
        {
            hasKey = true;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("DoorButton"))
        {
            Destroy(ButtonDoor);
        }
        else if (collision.gameObject.CompareTag("food"))
        {
            if (current_hungryValue <= 90)
            {
                current_hungryValue += 10;
            }
            if (current_hungryValue > 90)
            {
                current_hungryValue = maxHungryValue;
            }
            Destroy(collision.gameObject);
        }
     
        
            
            
        }


    public void IsWin(int npcCount)
    {

        endGameMessage.text = "You Win!!";
        if(npcCount == 3)
        {
            winMessage.SetActive(true);
            Time.timeScale = 0;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            inDoorArea = false;
        }
    }




    private void attack()
        {

            if (Input.GetButtonDown("Fire1") && currentBullet > 0 )
            {
                currentBullet--;

                GameObject bulletRing = Instantiate(bullet, rigid.position, Quaternion.identity);
                bulletRing.GetComponent<Rigidbody2D>().AddForce(bulletDirection * AmmoPower, ForceMode2D.Impulse);
                


            }
        }

  
        private void reduceScale()
        {
            if (Input.GetKeyDown(KeyCode.J) && changeScale > 0)
            {
                changeScale--;

                transform.localScale = new Vector2(0.5f, 0.5f);
                countDown = 10f;
             

            }
            
        }

        private void protectSelf()
        {
            if (Input.GetKeyDown(KeyCode.Space) && currentValue > 0)
            {
                isProtect = true;
            
            }
            else if(Input.GetKeyUp(KeyCode.Space))
                
            {
                isProtect = false;
            }
        }

       public void Death()
       {
            if (currentHP <= 0||current_hungryValue<=0)
            {

            gameManager.isLoose = true;
        
                     
            }
       }
       private void Movement()
       {
       
            Vector2 pos = new Vector2();
            pos += (dir.normalized*speed)*Time.fixedDeltaTime;
            rigid.velocity = pos;
            
	   }

	    private void Update()
        {
            attack();
            IsWin(npcCount);
            reduceScale();
            if (Input.GetAxisRaw("Horizontal")!=0||Input.GetAxisRaw("Vertical")!=0)
            {
                OnRotate();
            }
            OnMove();
            Death();

            

        if (countDown > 0)
        {
            countDown -= Time.fixedDeltaTime;
        }
        if (countDown <= 0)
        {
          
            transform.localScale = new Vector2(1.0f, 1.0f);
            
        }
        protectSelf();
        if (currentValue <= 0) { isProtect = false; }
        
        if (isProtect == true)
        {
            currentValue -= Time.deltaTime*20;
        }else if (isProtect == false&&currentValue<maxValue)
        {
            currentValue += Time.deltaTime*20;
        }
        int staStr =(int)currentValue;
        staText.text = staStr.ToString();
        openDoor();
        isMoving();
        if (characterMoving == true)
        {
            current_hungryValue -= Time.deltaTime/2;
        } else if (characterMoving == false&&current_hungryValue<maxHungryValue)
        {
            current_hungryValue += Time.deltaTime / 6; 
        }

        saveMode();

        



    }
	    void FixedUpdate() {
         
            Animate();
            Movement();
            
        }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            npcCount++;
           
        }
    }
}

