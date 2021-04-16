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

    //This script is created by Yihang, and later on modified by Liang
    //in some properties and methods.

    public static GameManager instance = null;
    public GameObject shopManager;

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
    

    [Header("Character properties")]
    public GameObject Player;
    private float MaxHp = 100;
    
    public bool canShoot = true;
    private int ammo;




    public float currentHP;
    public float current_hungryValue;
    public int coin = 0;
    public bool isInvincible = false;
    public bool isFull = false;

    public GameObject Canvas;






    [SerializeField] private GameObject pauseMenu;
    
    
    [SerializeField] private hp healthBar;
    
    [SerializeField] private hp dBar; //defend value bar
    
    [SerializeField] private hp hBar;
    
    [SerializeField] private Text coinText;
    [SerializeField] private GameObject endMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private Text ammoDisplay;
    [SerializeField] private AudioSource hurtSound;







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

        //coin = PlayerPrefs.GetInt("Coin", 0);
        //PlayerPrefs.DeleteKey("Coin");
    }


    public void getAmmo(int v)
    {
        
            this.ammo = v;
        
    }

    //Update is called once per frame
 
    public void coinCollect()
    {
        coin += 1;

        //PlayerPrefs.SetInt("Coin", coin);
        
    }
    public void diamondCollect()
    {
        coin += 10;

        //PlayerPrefs.SetInt("Coin", coin);
    }
    public void HPLose()
    {
        if (isInvincible)
        {
            currentHP = MaxHp;
            healthBar.getHealth(currentHP);
        }
        else
        {
            currentHP -= 5;
            healthBar.getHealth(currentHP);
            Player.GetComponent<PlayerController>().hurtEffect(0.2f);
            hurtSound.Play();
        }
    
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
        if (isFull)
        {
            current_hungryValue = maxHungryValue;
            hBar.getEnegry(current_hungryValue);
        }
        else
        {
            current_hungryValue -= Time.deltaTime / 1.2f;
            hBar.getEnegry(current_hungryValue);
        }
        
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

 
   
    public void gameOver()
    {
        if (isLoose==true/*currentHP<=0 || current_hungryValue<=0*/)
        {
            //isLoose=true;
            Destroy(Player);
            Time.timeScale = 0;
            pauseMenu.SetActive(false);
            endMenu.SetActive(true);
        }
        //else if (Canvas.GetComponent<CountdownTimer>().currentTime < 0)
        //{
        //    isLoose = true;
        //    Destroy(Player);
        //    Time.timeScale = 0;
        //    pauseMenu.SetActive(false);
        //    endMenu.SetActive(true);
        //}
        
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

    public void SetInvincible()
    {
        isInvincible = true;
    }

    public void SetFull()
    {
        isFull = true;
    }

    

    void Update()
    {
        //shopManager.GetComponent<ShopManager>().Buy();
        gameOver();
        Win();
        pauseDisplay();
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
        if(currentHP <= 0 || current_hungryValue <= 0)
        {
            isLoose = true;
        }

        hungry();
        int coinStr = (int)coin;
        coinText.text = coinStr.ToString();

        if (Player.GetComponent<PlayerController>().hasUnlimitBullet)
        {
            ammoDisplay.text = "¡Þ";
        }
        else
        {
            ammoDisplay.text = ammo.ToString();
        }




    }
}
