using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bulletBehaviour : MonoBehaviour
{

    public float countDown = 5f;
    [SerializeField] AudioSource shootAudio;

    // Start is called before the first frame update

    void Start()
    {
        shootAudio = GetComponent<AudioSource>();

        shootAudio.Play();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject); //Modified by Liang
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
