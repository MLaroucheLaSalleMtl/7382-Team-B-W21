using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EMeleeMovement : MonoBehaviour
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
    private Vector3 walkDirection;

    //Give the NPC a constrained area to walk around
    public Collider2D walkArea;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;
    private bool hasWalkArea;

    //Kill Player
    public float reloadTime;
    private bool reloading;
    [SerializeField] private GameObject thePlayer;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        //waitCounter = waitTime;
        //walkCounter = walkTime;
        //Randomize the melee enemy movement pattern
        walkCounter = Random.Range(walkTime * 0.75f, walkTime * 1.25f);
        waitCounter = Random.Range(waitTime * 0.75f, waitTime * 1.75f);

        ChooseDirection();

        //Activate walking area for the NPC
        if (walkArea != null)
        {
            minWalkPoint = walkArea.bounds.min;
            maxWalkPoint = walkArea.bounds.max;
            hasWalkArea = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Checking walking state
        if(isWalking)
        {
            //Counting down the walking time
            walkCounter -= Time.deltaTime;

            if(hasWalkArea && transform.position.y > maxWalkPoint.y)
            {
                //isWalking = false;
                //waitCounter = Random.Range(waitTime * 0.75f, waitTime * 1.75f);
                walkDirection = -walkDirection;
                ChooseDirection();
            }
            else if(hasWalkArea && transform.position.x > maxWalkPoint.x)
            {
                //isWalking = false;
                //waitCounter = Random.Range(waitTime * 0.75f, waitTime * 1.75f);
                walkDirection = -walkDirection;
                ChooseDirection();
            }
            else if(hasWalkArea && transform.position.y < minWalkPoint.y)
            {
                //isWalking = false;
                //waitCounter = Random.Range(waitTime * 0.75f, waitTime * 1.75f);
                walkDirection = -walkDirection;
                ChooseDirection();
            }
            else if(hasWalkArea && transform.position.x < minWalkPoint.x)
            {
                //isWalking = false;
                //waitCounter = Random.Range(waitTime * 0.75f, waitTime * 1.75f);
                walkDirection = -walkDirection;
                ChooseDirection();
            }

            myRigidbody.velocity = walkDirection;

            if(walkCounter < 0)
            {
                isWalking = false;
                walkCounter = Random.Range(walkTime * 0.75f, walkTime * 1.25f);
                //walkCounter = walkTime;
            }
            
            //if walk counter goes to 0, wait counter will be activated
            //if (walkCounter < 0)
            //{
            //    isWalking = false;
            //    //waitCounter = waitTime;
            //    waitCounter = Random.Range(waitTime * 0.75f, waitTime * 1.75f);
            //}

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

        if(reloading)
        {
            reloadTime -= Time.deltaTime;
            if(reloadTime <0)
            {
                SceneManager.LoadScene("level1");
                thePlayer.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "--Main Character--")
        {
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            reloading = true;
            thePlayer = collision.gameObject;
        }
    }

    public void ChooseDirection()
    {
        walkDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
        isWalking = true;
        walkCounter = Random.Range(walkTime * 0.75f, walkTime * 1.25f);
    }
}
