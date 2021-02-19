using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



    public class PlayerController : MonoBehaviour
    {
        Rigidbody2D rigid;
        Animator anim;
        Vector2 dir = new Vector2(0,-1);
        Vector2 bulletDirection = new Vector2(0, -1);

        public float speed = 300.0f;
        public float AmmoPower = 10f;
        public GameObject bullet;
        public bool isProtect = false;

        private int AmmoPickUp=5;
        private int changeScalePickUp = 1;
        private Transform EnemyBullet;
        private float MaxHp = 100;
        private GameManager gameManager;
        private float countDown = 0f;
        private float maxValue = 100;
      
    
 
     
        [SerializeField] private int currentBullet=0;
        [SerializeField] private int changeScale = 0;
        [SerializeField] private float currentHP;
        [SerializeField] private float currentValue;
        [SerializeField] private Text staText;
        [SerializeField] private int coin=0;




   
        // Start is called before the first frame update
        void Start() {
       
            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            currentBullet = 0;
            changeScale = 0;
            currentHP = MaxHp;
            gameManager = GameManager.instance;
            currentValue = maxValue;
           
    
                              
        }
  
        //character movement
        public void OnMove() 
        {
            dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
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
            if (collision.gameObject.CompareTag("AmmoPickUp"))
            {
                currentBullet+= AmmoPickUp;
                Destroy(collision.gameObject);
            }//AmmoPickUp
            else if (collision.gameObject.CompareTag("ChangeScale"))
            {
                changeScale += changeScalePickUp;
                Destroy(collision.gameObject);
                    
            }//ScaleChangePickUp
            else if (collision.gameObject.CompareTag("EnemyBullet")&&isProtect==false)
            {
                currentHP -= 5;
            }
            else if (collision.gameObject.CompareTag("trap")&&isProtect==false)
            {
                currentHP -= 5;
            }
            else if (collision.gameObject.CompareTag("HPadd"))
            {
                if (currentHP == 100)
                {
                    currentHP += 0;
                }else if (currentHP < 100)
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
                countDown = 5f;
             

            }
            
        }

        private void projectSelf()
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
            if (currentHP <= 0)
            {

            gameManager.isLoose=true;
        
                     
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
        projectSelf();
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

    }
	    void FixedUpdate() {
         
            Animate();
            Movement();
            
        }
    }

