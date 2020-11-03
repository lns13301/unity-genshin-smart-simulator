using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    // Ad
    public GameObject popUp;
    public Text popUpText;

    // Start is called before the first frame update
    void Start()
    {
        popUpText = popUp.transform.GetChild(1).GetComponent<Text>();
        popUp.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void giveReward()
    {
        popUp.gameObject.SetActive(true);

        if (GameManager.instance.AddWishes(500, 500))
        {
            GameManager.instance.SavePlayerDataToJson();
        }
        else
        {

        }
    }

    public void popUpUiOnOff()
    {
        if (popUp.gameObject.activeSelf)
        {
            popUp.gameObject.SetActive(false);
        }
    }
}
