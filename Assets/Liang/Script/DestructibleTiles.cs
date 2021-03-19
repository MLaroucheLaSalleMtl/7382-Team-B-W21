using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTiles : MonoBehaviour
{
    public Tilemap DestructibleTilemap;
    public int health;

    private void Start()
    {
        DestructibleTilemap = GetComponent<Tilemap>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 hitPosition = Vector3.zero;

            foreach (ContactPoint2D hit in collision.contacts)
            {
                //Get the positions of the contact where it belongs to the correct tile
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                //Set the tile in the position to disappear
                DestructibleTilemap.SetTile(DestructibleTilemap.WorldToCell(hitPosition), null);
            }
        }
    }
}
