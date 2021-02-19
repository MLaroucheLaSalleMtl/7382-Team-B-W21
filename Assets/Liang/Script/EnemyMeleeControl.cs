using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeControl : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveTime;
    [SerializeField] private float waitTime;

    private float moveCounter;
    private float waitCounter;
    private bool isMoving;

    private Rigidbody2D myRigidbody;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        moveCounter = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {

        }
        else
        {
            waitCounter -= Time.deltaTime;
        }
            
    }
}
