using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    private Animator animator;
    int loadToScene;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChanged;
    }

    InterstitialState state;

    private void OnInterstitialStateChanged(InterstitialState state)
    {
       this.state = state;
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.anyKeyDown)
            {
                loadToScene = 1;
                Bridge.advertisement.ShowInterstitial();
                FadeToLeve();
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                loadToScene = 2;
                StartLevel();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                loadToScene = 4;
                FadeToLeve();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                loadToScene = 0;
                FadeToLeve();
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                loadToScene = 0;
                FadeToLeve();
            }
            if (Final.IsTheEndGame())
            {
                loadToScene = 3;
                FinalLevel();
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (Input.anyKeyDown)
            {
                loadToScene = 0;
                FadeToLeve();
                Bridge.advertisement.ShowInterstitial();
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (Input.anyKeyDown)
            {
                loadToScene = 1;
                FadeToLeve();
            }
        }
    }

    public void FadeToLeve()
    {
        animator.SetTrigger("fade");
    }

    public void FinalLevel()
    {
        animator.SetTrigger("finalfade");
    }

    public void StartLevel()
    { 
        animator.SetTrigger("startfade");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(loadToScene);
    }
}
