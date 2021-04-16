using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTiles : MonoBehaviour
{
    public Tilemap destructibleTilemap;
    public int health;
    //public float countDownTimer = 20f;
    public bool isContact = false;
    [SerializeField] AudioSource wallAudio;

    private void Start()
    {
        destructibleTilemap = GetComponent<Tilemap>();    
    }

    private void Update()
    {

        //if (isContact)
        //{
        //    countDownTimer -= Time.fixedDeltaTime;
        //}
        //else if (health <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnContact(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isContact = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isContact = true;
            

            if (Input.GetButtonDown("Fire3"))
            {
                Vector3 hitPosition = Vector3.zero;

                foreach (ContactPoint2D hit in collision.contacts)
                {
                    //Get the positions of the contact where it belongs to the correct tile
                    hitPosition.x = hit.point.x + 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y + 0.01f * hit.normal.y;
                    //Set the tile in the position to disappear
                    destructibleTilemap.SetTile(destructibleTilemap.WorldToCell(hitPosition), null);
                    wallAudio = GetComponent<AudioSource>();
                    wallAudio.Play();
                }

                isContact = false;
            }
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isContact = false;
        }
    }

}
