using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is the script that controls the button Start game, as it uses an AsyncOeration to pre
/// load the secenes. I have used the method that requires a buildIndex for the button to call
/// and load the next scene.
/// 
/// /// </summary>
/// 
public class LoadScene : MonoBehaviour
{
    private AsyncOperation async;

    public void BtnLoadScene() //No parameter == next scene
    {
        if (async != null) return; //If there is alrady something in progress, dont do the following code

        Scene currentScene = SceneManager.GetActiveScene(); //Return the active scene
        async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1); //Load the next scene

    }

    public void BtnLoadScene(int i) //i == scene number
    {
        if (async != null) return; //If there is alrady something in progress, dont do the following code

        async = SceneManager.LoadSceneAsync(i); //Load the scene at position (i)

    }

    public void BtnLoadScene(string s) //s == name of the scene
    {
        if (async != null) return; //If there is alrady something in progress, dont do the following code


        async = SceneManager.LoadSceneAsync(s); //Load the scene named (s)

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
