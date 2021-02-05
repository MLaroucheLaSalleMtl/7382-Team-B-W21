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
        public GameObject bullet;
        public float strenth = 10f;





        // Start is called before the first frame update
        void Start() {
       
            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
           
           
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
      
        private void attack()
        {
          
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject bulletRing = Instantiate(bullet, rigid.position,Quaternion.identity);
                bulletRing.GetComponent<Rigidbody2D>().AddForce(bulletDirection*strenth, ForceMode2D.Impulse);
               
               
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
