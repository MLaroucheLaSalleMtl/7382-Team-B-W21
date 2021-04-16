using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleSprite : MonoBehaviour
{
    //Liang
    public int health;
    [SerializeField] AudioSource stoneAudio;
    [SerializeField] GameObject brickReminder; //modified by Yihang

    // Start is called before the first frame update
    void Start()
    {
        //stoneAudio = GetComponent<AudioSource>();
        brickReminder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            
            Destroy(gameObject);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            brickReminder.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            brickReminder.SetActive(false);
        }
    }

    public void PlaySoundEffect()
    {
        stoneAudio.Play();
    }
}
