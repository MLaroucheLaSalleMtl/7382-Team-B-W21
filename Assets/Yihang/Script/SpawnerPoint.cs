using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPoint : MonoBehaviour
{
    Rigidbody2D spawnerRigid;
    private float timeToCreate = 5f;
    private float nextTime;
    private float maxHP = 100;
    private float currentHP = 100;
    [SerializeField] private GameObject randomEnemy;
    [SerializeField] private GameObject diamond;

    // Start is called before the first frame update
    void Start()
    {
        spawnerRigid = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        nextTime = 5;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            currentHP -= 20;

        }
    }


    public void spwanerDestroyed()
    {
        if (currentHP <= 0)
        {
            GameObject _diamond = Instantiate(diamond, spawnerRigid.position, Quaternion.identity);
            Destroy(gameObject);

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (nextTime<Time.time)
        {
            
            GameObject _ranEnemy = Instantiate(randomEnemy, spawnerRigid.position, Quaternion.identity);
            nextTime = Time.time + timeToCreate;
        }
        spwanerDestroyed();
  
    }
}
