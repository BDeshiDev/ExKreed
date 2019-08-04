using System;
using System.Collections;
using System.Collections.Generic;
using bdeshi.utility;
using UnityEngine;
using  UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSecneWhenEnabled : MonoBehaviour
{
    public string sceneName;
    public Image fadeImage;
    public FiniteTimer fadeTimer =  new FiniteTimer(0,1.5f);
    public bool shouldFadeAudio = false;
    public Color endColor;

    private void OnEnable()
    {
        StartCoroutine(startTransition());
    }

    public IEnumerator startTransition()
    {
        Color startColor = fadeImage.color;
        fadeImage.gameObject.SetActive(true);
        while (!fadeTimer.isComplete)
        {
            fadeTimer.updateTimer(Time.deltaTime);
            fadeImage.color = Color.Lerp(startColor, Color.black, fadeTimer.Ratio);
            yield return null;
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
