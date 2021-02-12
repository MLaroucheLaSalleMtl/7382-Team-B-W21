using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityEngine.Tutorials
{
    public class Move : MonoBehaviour
    {
        Rigidbody2D rigid;
        Animator anim;
        Vector2 dir = new Vector2(0,-1);
        Vector2 bulletDirection = new Vector2(0, -1);

        public float speed = 300.0f;
        public float jumpHeight = 8f;
        public float AmmoPower = 10f;
        public GameObject bullet;
        private int AmmoPickUp=5;
        private int changeScalePickUp = 1;
     
        [SerializeField] private int currentBullet=0;
        [SerializeField] private int changeScale = 0;
     

        // Start is called before the first frame update
        void Start() {
       
            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            currentBullet = 0;
            changeScale = 0;
            
          
           
        }
  
        public void OnMove() 
        {
            dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Debug.Log(dir);
	    }
        public void OnRotate()
        {
            bulletDirection= new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Debug.Log(dir);
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
        } 

  
        private void attack()
        {

            if (Input.GetButtonDown("Fire1") && currentBullet > 0)
            {
                currentBullet--;

                GameObject bulletRing = Instantiate(bullet, rigid.position, Quaternion.identity);
                bulletRing.GetComponent<Rigidbody2D>().AddForce(bulletDirection * AmmoPower, ForceMode2D.Impulse);


            }
        }
        private void reduceScale()
        {
            if (Input.GetKeyDown(KeyCode.J)&&changeScale>0 )
            {
                changeScale--;
                transform.localScale = new Vector2(0.5f, 0.5f);
                
            }
            else if (Input.GetKeyUp(KeyCode.J))
            {
                transform.localScale = new Vector2(1.0f, 1.0f);
            }
        }

       
        private void Movement()
        {
       
            Vector2 pos = new Vector2();
            pos += (dir.normalized*speed)*Time.fixedDeltaTime;
            rigid.velocity = pos;
            if (Input.GetButtonDown("Jump"))
            {
                rigid.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            }
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
            

        }
	    void FixedUpdate() {
         
            Animate();
            Movement();
            
        }
    }
}
