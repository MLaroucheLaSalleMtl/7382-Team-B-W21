using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehaviour : MonoBehaviour
{
    public float attackCountDown = 3f;
    [SerializeField] AudioSource stoneAudio;
    // Start is called before the first frame update
    void Start()
    {
        stoneAudio = GetComponent<AudioSource>();
        stoneAudio.Play();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (attackCountDown > 0)
        {
            attackCountDown -= Time.fixedDeltaTime;
        }
        if (attackCountDown <= 0)
        {
            Destroy(gameObject);
        }
    }
}
