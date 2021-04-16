using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    //Liang was going to modify the PlayerController script,
    //to make it properly arranged, but we did not choose to
    //continue with this, but to keep the original script, and 
    //using this layout.


    private GameManager gameManager;
    Rigidbody2D myRigidbody;
    Animator anim;
    Vector2 characterDirection = new Vector2(0, -1); //Character direction 
    Vector2 bulletDirection = new Vector2(0, -1); //Bullet will go where your are facing at

    [Header("Character properties")]
    [SerializeField] private float speed = 300.0f; //Character moving
    [SerializeField] private int maxHp = 100; //Character's maximum HP
    public bool isCharacterMoving = false;
    [SerializeField] private float currentHp;
    [SerializeField] private int coin = 0;


    [Header("Protection shield system")]
    public bool isProtect = false;
    [SerializeField] private float maxProtectValue = 100;
    [SerializeField] private float currentProtectValue;


    [Header("Ammo system")]
    [SerializeField] private int currentBullet = 0;
    [SerializeField] private float ammoRange = 500f; //bullet range
    public GameObject bullet;
    [SerializeField] private int bulletAmount = 5; //Amount given when bullet power up is picked up

    [Header("Scale system")]
    [SerializeField] private int currentChangeScale = 0;
    [SerializeField] private int changeScalePickUp = 1;
    private float changScaleDuration = 0f; // countDown

    [Header("Door system")]
    public bool inDoorArea = false;
    public bool hasKey = false;
    [SerializeField] private GameObject keyDoor;
    [SerializeField] private GameObject buttonDoor;

    [Header("Hunger system")]
    [SerializeField] private float maxHungerValue = 100f;
    [SerializeField] private float currentHungerValue;

    [Header("Other")]
    private int npcCount = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentBullet = 0;
        currentChangeScale = 0;
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
