using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulleyBehavriou : MonoBehaviour
{
    Rigidbody2D enemyBulletRigid;
    Vector2 bulletDir = new Vector2(0, -1);

    public Transform player;
    private float bulletSpeed=10f;
    // Start is called before the first frame update
    void Start()
    {
        enemyBulletRigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        bulletDir = (player.transform.position - transform.position).normalized * bulletSpeed;
        enemyBulletRigid.velocity = new Vector2(bulletDir.x, bulletDir.y);
        Destroy(this.gameObject, 2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
         
        }
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject); //Modified by Liang
        }

    }

    

    // Update is called once per frame
    void Update()
    {
    
    }
}
