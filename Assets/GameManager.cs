using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



/// <summary>
/// This is the master script that controls most of the features in the game
/// Variables and Methods are being listed with the description of their use.
/// 
/// 
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Game Condition")]
    //Win or lose
    public bool isLoose = false;
    public bool isWin = false;

    [Header("Door system")]
    //Key door condition
    public bool inDoorArea = false;
    public bool hasKey = false;
    [SerializeField] private GameObject door;

    [Header("Protection shield system")]
    public bool isProtect = false;
    private float maxValue = 100;//max charater defend value
    [SerializeField] private float currentValue; //current character value

    [Header("Hunger system")]
    public bool saveMode = false;//hungry value is higer than 20;
    private float maxHungryValue = 100;
    [SerializeField] private float current_hungryValue;

    [Header("Character properties")]
    public GameObject Player;
    private float MaxHp = 100;
    [SerializeField] private float currentHP;
    public bool canShoot = true;
    private int ammo;













    [SerializeField] private GameObject pauseMenu;
    
    
    [SerializeField] private hp healthBar;
    
    [SerializeField] private hp dBar; //defend value bar
    
    [SerializeField] private hp hBar;
    [SerializeField] private int coin = 0;
    [SerializeField] private Text coinText;
    [SerializeField] private GameObject endMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private Text ammoDisplay;





    // Start is called before the first frame update

    private void Awake()
    {
      
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }
    }
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        pauseMenu.SetActive(false);

        door = GameObject.FindGameObjectWithTag("Door");

        currentHP = MaxHp;
        healthBar.MaxHealth(MaxHp);

        currentValue = maxValue;
        dBar.MaxDefend(maxValue);

        current_hungryValue = maxHungryValue;
        hBar.maxEnegry(maxHungryValue);

        endMenu.SetActive(false);

        winMenu.SetActive(false);


    }


    public void getAmmo(int v)
    {
        this.ammo = v;
    }

    //Update is called once per frame
 
    public void coinCollect()
    {
        coin += 1;
    }
    public void diamondCollect()
    {
        coin += 10;
    }
    public void hpLoose()
    {
        currentHP -=5;
        healthBar.getHealth(currentHP);
    }

    public void hpAdd()
    {
        if (currentHP>=95)
        {
            currentHP=MaxHp;
            healthBar.getHealth(currentHP);
        }
        else if (currentHP <95)
        {
            currentHP += 5;
            healthBar.getHealth(currentHP);
        }
    }

    public void enegryAdd()
    {
        if (current_hungryValue <= 90)
        {
            current_hungryValue += 10;
            hBar.getEnegry(current_hungryValue);
        }
        if (current_hungryValue > 90)
        {
            current_hungryValue = maxHungryValue;
            hBar.getEnegry(current_hungryValue);
        }
    }

    public void enegryChange()
    {
       
          current_hungryValue -= Time.deltaTime / 1.2f;
          hBar.getEnegry(current_hungryValue);
        
    }

    public void hungry()
    {
        if (current_hungryValue < 20)
        {
            saveMode = true;
        }else if (current_hungryValue >= 20)
        {
            saveMode = false;
        }
    }

    public void enegryback()
    {
        if (current_hungryValue < maxHungryValue)
        {
            current_hungryValue += Time.deltaTime / 6;
            hBar.getEnegry(current_hungryValue);
        }
    }
  
    public void pauseDisplay()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& currentHP >= 0 &&current_hungryValue >= 0)
        {
            canShoot = false;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

    }

 
   
    private void gameOver()
    {
        if (currentHP<=0||current_hungryValue<=0)
        {
            isLoose=true;
            Destroy(Player);
            Time.timeScale = 0;
            pauseMenu.SetActive(false);
            endMenu.SetActive(true);

         
        }
    }
    private void Win()
    {        
        if (isWin == true)
        {
            winMenu.SetActive(true);
            Time.timeScale = 0;
            canShoot = false;
        }
    }



    private void openDoor() //keydoor open
    {
        if (hasKey == true && inDoorArea == true && Input.GetKeyDown(KeyCode.G))
        {
            Destroy(door);
        }

    }
    private void protectSelf() //defent function
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentValue > 0)
        {
            isProtect = true;

        }
        else if (Input.GetKeyUp(KeyCode.Space))

        {
            isProtect = false;
        }
    }

    void Update()
    {
        gameOver();
        Win();
        pauseDisplay();
        openDoor();
        protectSelf();
        if (currentValue <= 0) { isProtect = false; }

        if (isProtect == true)
        {
            currentValue -= Time.deltaTime * 20;
            dBar.getDefend(currentValue);
        }
        else if (isProtect == false && currentValue < maxValue)
        {
            currentValue += Time.deltaTime * 20;
            dBar.getDefend(currentValue);
        }

        hungry();
        int coinStr = (int)coin;
        coinText.text = coinStr.ToString();
        ammoDisplay.text = ammo.ToString();


    }
}
