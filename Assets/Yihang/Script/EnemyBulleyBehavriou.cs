using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulleyBehavriou : MonoBehaviour
{
    Rigidbody2D enemyBulletRigid;

    public Transform player;
    private float bulletSpeed=10f;
    // Start is called before the first frame update
    void Start()
    {
        enemyBulletRigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(this.gameObject, 2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
         
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, bulletSpeed* Time.deltaTime);
    }
}
