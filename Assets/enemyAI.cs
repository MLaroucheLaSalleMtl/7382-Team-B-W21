using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    Rigidbody2D enemyRigid;
    private float shootRate=1f;
    private float nextShootTime;

   

    [SerializeField]private Transform player;
    [SerializeField]private float enemeySpeed=5;
    [SerializeField] private float shootRange;
    [SerializeField] private GameObject enemyBullet;




    
    // Start is called before the first frame update
    void Start()
    {
       
        enemyRigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

      


    }

    private void enemyAttack()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < 10 && distanceFromPlayer > shootRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, enemeySpeed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootRange && nextShootTime < Time.time)
        {
            GameObject _enemyBullet = Instantiate(enemyBullet, enemyRigid.position, Quaternion.identity);
            nextShootTime = Time.time + shootRate;



        }
    }

    // Update is called once per frame
    void Update()
    {

        enemyAttack();
    }
}
