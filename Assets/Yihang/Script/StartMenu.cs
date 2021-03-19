using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private int index;


    private void Start()
    {
        Time.timeScale = 1;
        index = SceneManager.GetActiveScene().buildIndex + 1;
    }
    public void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void loadScene()
    {
        SceneManager.LoadScene(index);
    }
}
