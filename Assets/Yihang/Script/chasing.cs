using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasing : MonoBehaviour
{
    Rigidbody2D enemyRigid;
    private GameManager gameManager;

    [SerializeField] private float enemeySpeed = 1;
    [SerializeField] private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);

        }else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.isLoose == false)
        {
           
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, enemeySpeed * Time.deltaTime);
            
        }
    }
}
