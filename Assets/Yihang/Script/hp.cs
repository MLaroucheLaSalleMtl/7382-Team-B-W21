using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class hp : MonoBehaviour
{

    public Slider hpSlider;
    public Slider defendSlider;
    public Slider hungrySlider;
    public GameObject pauseMenu;
    //public GameObject winMenu;
    private GameManager gm;
  
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
    //Start is called before the first frame update

  

    public void back()
    {

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gm.canShoot = true;
    }

    public void restart()
    {
        SceneManager.LoadScene("level1");
        Time.timeScale = 1;
    }

    public void backMainMenu()
    {
        SceneManager.LoadScene("Start menu");
    }

    public void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
#endif
     
    }
    void Start()
    {
        pauseMenu.SetActive(false);
        gm = GameManager.instance;
        Time.timeScale = 1;
    }
   
    // Update is called once per frame
    void Update()
    {

  
       
      
    }
}
