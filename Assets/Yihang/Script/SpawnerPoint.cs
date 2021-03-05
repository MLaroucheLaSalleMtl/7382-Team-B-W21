using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPoint : MonoBehaviour
{
    Rigidbody2D spawnerRigid;
    private float timeToCreate = 5f;
    private float nextTime;
    [SerializeField] private GameObject randomEnemy;

    // Start is called before the first frame update
    void Start()
    {
        spawnerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextTime<Time.time)
        {
            
            GameObject _ranEnemy = Instantiate(randomEnemy, spawnerRigid.position, Quaternion.identity);
            nextTime = Time.time + timeToCreate;
        }
  
    }
}
