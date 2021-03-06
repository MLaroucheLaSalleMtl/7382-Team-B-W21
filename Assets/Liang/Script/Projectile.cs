using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour
{
    //Liang

    //private Vector2 target;
    //public float Speed;
    //public GameObject Bullet;
    public int damage;
    public float rangeTimer = 3f;
    [SerializeField] AudioSource stoneAudio;

    // Start is called before the first frame update
    void Start()
    {
        stoneAudio = GetComponent<AudioSource>();
        stoneAudio.Play();
        //target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (rangeTimer > 0)
        {
            rangeTimer -= Time.fixedDeltaTime;
        }
        if (rangeTimer <= 0)
        {
            Destroy(gameObject);
        }

        //transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        //if (Vector2.Distance(transform.position, target) < 0.1f)
        //{
        //    //Instantiate(Bullet, transform.position, Quaternion.identity);
        //    Destroy(gameObject);
        //}
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Destructable"))
        {
            Destroy(gameObject);
            if (collision.gameObject.GetComponent<DestructibleSprite>().health >= 0)
            {
                collision.gameObject.GetComponent<DestructibleSprite>().health -= damage;
                collision.gameObject.GetComponent<DestructibleSprite>().PlaySoundEffect();
            }
            //else if (collision.gameObject.GetComponent<DestructibleTiles>().health >= 0)
            //{
            //    collision.gameObject.GetComponent<DestructibleSprite>().health -= damage;
            //}
            else
                collision.gameObject.GetComponent<DestructibleSprite>().PlaySoundEffect();
        }
        else if(collision.CompareTag("Destruct"))
        {
            Destroy(gameObject);
            if (collision.gameObject.GetComponent<DestructibleTiles>().health >= 0)
            {
                collision.gameObject.GetComponent<DestructibleTiles>().health -= damage;
                collision.gameObject.GetComponent<DestructibleSprite>().PlaySoundEffect();
            }
            else
                collision.gameObject.GetComponent<DestructibleSprite>().PlaySoundEffect();
        }


    }
}
