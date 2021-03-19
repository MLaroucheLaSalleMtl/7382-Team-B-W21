using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bulletBehaviour : MonoBehaviour
{

    float countDown = 5f;

    // Start is called before the first frame update

    void Start()
    {
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    //Update is called once per frame
    void Update()
    {
        
        if (countDown > 0)
        {
            countDown -= Time.fixedDeltaTime;
        }
        if (countDown <= 0)
        {
            Destroy(gameObject);
        }

        
    }
}
