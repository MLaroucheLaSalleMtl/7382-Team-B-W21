using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



/// <summary>
/// This is the master script that controls most of the features in the game
/// Variables and Methods are being listed wit hthe description of their use.
/// 
/// 
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

 

    








    public bool isLoose = false;
    public bool isWin = false;
    public GameObject Player;

    [SerializeField] private GameObject winMessage;
    [SerializeField] private Text endGameMessage;
    [SerializeField] private GameObject pauseMenu;






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
        winMessage.SetActive(false);
        pauseMenu.SetActive(false);

    

    }

 
    //Update is called once per frame

    private void gameOver()
    {
        if (isLoose == true)
        {
            Destroy(Player);
            Time.timeScale = 0;
        }
    }
    private void Win()
    {

        endGameMessage.text = "You Win!!";
        if (isWin == true)
        {
            winMessage.SetActive(true);
            Time.timeScale = 0;
        }
    }

   


  
    void Update()
    {
        gameOver();
        Win();
   
      
        
    }
}
