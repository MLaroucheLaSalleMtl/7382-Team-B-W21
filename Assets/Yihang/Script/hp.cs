using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp : MonoBehaviour
{

    public Slider hpSlider;
    public Slider defendSlider;
    public Slider hungrySlider;
    public GameObject pauseMenu;




    public void MaxHealth(float health)
    {
        hpSlider.maxValue = health;
        

    }


    public void getHealth(float health)
    {
        hpSlider.value = health;
    }

    public void MaxDefend(float dValue)
    {
        defendSlider.maxValue = dValue;


    }


    public void getDefend(float dValue)
    {
        defendSlider.value = dValue;
    }


    public void maxEnegry(float enegry)
    {
        hungrySlider.maxValue = enegry;


    }


    public void getEnegry(float enegry)
    {
        hungrySlider.value = enegry;
    }
    // Start is called before the first frame update

    public void pauseDisplay()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        
    }
    void Start()
    {
        pauseMenu.SetActive(false);
    }
   
    // Update is called once per frame
    void Update()
    {

        pauseDisplay();
      
    }
}
