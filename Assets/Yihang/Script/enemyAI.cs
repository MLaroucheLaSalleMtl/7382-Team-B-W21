using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    Rigidbody2D enemyRigid;
    private float shootRate=1f;
    private float nextShootTime;
    private float maxHP=100;
    private Vector2 moveDirection;
    private float changeDir = 10f;
    private GameManager gameManager;








    [SerializeField]private Transform player;
    [SerializeField]private float enemeySpeed=5;
    [SerializeField] private float shootRange;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField]private float currentHP;
    [SerializeField] private bool isVertical;
    [SerializeField] private GameObject coin;




    
    // Start is called before the first frame update
    void Start()
    {
        enemyRigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
       
        currentHP = maxHP;
        if (isVertical)
        {
           moveDirection= Vector2.up; 
        }
        else if (!isVertical)
        {
            moveDirection=Vector2.right;
        }
        changeDir=10f;
        gameManager = GameManager.instance;

        


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            currentHP -=50;

        }
    }

    public void enemyDeath()
    {
        if (currentHP <= 0)
        {
            GameObject _coin = Instantiate(coin, enemyRigid.position, Quaternion.identity);
            Destroy(gameObject);
        
        }
      
    }
 
    private void enemyAttack()
    {

        if (gameManager.isLoose==false) {
            float distanceFromPlayer =Vector2.Distance(player.position,transform.position);
            if (distanceFromPlayer < 10 && distanceFromPlayer > shootRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, enemeySpeed * Time.deltaTime);
            }
            else if (distanceFromPlayer <= shootRange && nextShootTime < Time.time)
            {

                GameObject _enemyBullet = Instantiate(enemyBullet, enemyRigid.position, Quaternion.identity);
                nextShootTime = Time.time + shootRate;

            }
            else if (distanceFromPlayer >= 10)
            {
                changeDir -= Time.deltaTime;
                if (changeDir < 0)
                {
                    moveDirection *= -1;
                    changeDir = 10f;
                }
                Vector2 position = enemyRigid.position;
                position.x += moveDirection.x * enemeySpeed * Time.deltaTime;
                position.y += moveDirection.y * enemeySpeed * Time.deltaTime;
                enemyRigid.MovePosition(position);
            }
        }  


       
    }



    // Update is called once per frame
    void Update()
    {
        enemyAttack();
        enemyDeath();
        
      
    }
}
