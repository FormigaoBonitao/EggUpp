using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public class FBanalytic : MonoBehaviour
{
    public static FBanalytic Instanse;


    // Awake function from Unity's MonoBehavior
    void Awake()
    {
        Instanse = this;
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    public void LevelWin(int lvl)
    {
        var tutParams = new Dictionary<string, object>();
        tutParams["Level Number"] = lvl.ToString();

        FB.LogAppEvent("Level Win", parameters: tutParams);
        Debug.Log(lvl);
    }

    public void LevelLose(int lvl)
    {
        var tutParams = new Dictionary<string, object>();
        tutParams["Level Number"] = lvl.ToString();

        FB.LogAppEvent("Level Lose", parameters: tutParams);
        Debug.Log(lvl); 
    }


}
