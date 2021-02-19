using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool isLoose = false;
    public GameObject Player;


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

    void Update()
    {
        gameOver();
    }
}
