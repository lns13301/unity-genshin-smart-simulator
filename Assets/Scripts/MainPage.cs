using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPage : MonoBehaviour
{
    public static MainPage instance;

    public GameObject exitNotice;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        SoundManager.instance.PlayMusic(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(int sceneIndex)
    {
        if (GameManager.instance.isValidTimeOver())
        {
            return;
        }

        SoundManager.instance.PlayOneShotEffectSound(2);
        SceneManager.LoadScene(sceneIndex);
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void ButtonExit()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);

        exitNotice.SetActive(true);
    }

    public void OffExitNotice()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        exitNotice.SetActive(false);
    }
}
