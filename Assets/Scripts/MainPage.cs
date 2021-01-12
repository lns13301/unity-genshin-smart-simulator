using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPage : MonoBehaviour
{
    public static MainPage instance;

    public GameObject exitNotice;
    public GameObject creditButton;
    public GameObject information;

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

    public void SetLanguage()
    {
        if (LanguageManager.instance.language == Language.KOREAN)
        {
            creditButton.SetActive(false);
            creditButton.transform.GetChild(0).GetComponent<Text>().text = "크레딧";
            //information.transform.GetChild(3).GetComponent<Text>().text = "커뮤니티 정보입니다."
        }
        else
        {
            creditButton.SetActive(false);
            creditButton.transform.GetChild(0).GetComponent<Text>().text = "Credit";
        }
    }

    public void OnCredit()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        information.SetActive(true);
    }
    
    public void OffCredit()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        information.SetActive(false);
    }

    public void OpenLink()
    {
        Application.OpenURL("https://taimienphi.vn");
    }
}
