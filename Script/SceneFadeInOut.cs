﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFadeInOut : MonoBehaviour {


    public float fadeSpeed = 1.5f;
    public bool sceneStarting = true;
    private RawImage rawImage;
    void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (sceneStarting)
            StartScene();
    }

    private void FadeToClear()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    private void FadeToBlack()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();
        if (rawImage.color.a <=0.0f)
        {
            rawImage.color = Color.clear;
            rawImage.enabled = false;
            sceneStarting = false;
        }
    }

    //void EndScene()
    //{
    //    rawImage.enabled = true;
    //    FadeToBlack();
    //    if (rawImage.color.a > 0.95f)
    //    {
    //        SceneManager.LoadScene(0);
    //    }
    //}

    


}
