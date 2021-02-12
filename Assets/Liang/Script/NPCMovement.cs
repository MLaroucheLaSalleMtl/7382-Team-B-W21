using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{

    //NPC moving speed, walk and wait duration
    [SerializeField] private float moveSpeed; 
    [SerializeField] private float walkTime;
    [SerializeField] private float waitTime;

    //Counter and bool to control the transition between states 
    private float walkCounter;
    private float waitCounter;
    private bool isWalking;

    private Rigidbody2D myRigidbody;

    //Controlling walking direction
    private int walkDirection;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //Checking walking state
        if(isWalking)
        {
            //Counting down the walking time
            walkCounter -= Time.deltaTime; 

            //Controlling the NPC when walking towards different directions
            switch(walkDirection)
            {
                //going up
                case 0:
                    myRigidbody.velocity = new Vector2(0, moveSpeed);
                    break;
                //going right
                case 1:
                    myRigidbody.velocity = new Vector2(moveSpeed, 0);
                    break;
                //going down
                case 2:
                    myRigidbody.velocity = new Vector2(0, -moveSpeed);
                    break;
                //going left
                case 3:
                    myRigidbody.velocity = new Vector2(-moveSpeed, 0);
                    break;
            }

            //if walk counter goes to 0, wait counter will be activated
            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }

        }
        else
        {
            //if wait counter goes to 0, NPC will choose another direction to walk towards
            waitCounter -= Time.deltaTime;
            //making sure not to move during wait time.
            myRigidbody.velocity = Vector2.zero;
            if(waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}
