using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public static string ORANGE_COLOR = "e59e00";
    public static string RED_COLOR = "d50707";
    public static string ORANGE_RED_COLOR = "ff6800";
    public static string GREEN_COLOR = "63ae2f";
    public static string BLUE_COLOR = "1a5ed9";
    public static string YELLOW_COLOR = "fbf236";
    public static string PINK_COLOR = "d95763";
    public static string SKY_BLUE_COLOR = "4e89f2";

    public ResourceData resourceData;

    public bool isLooted;
    public DateTime expiredTime;

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowLeftTime();
    }

    public void SetLootedTime()
    {
        if (isLooted)
        {
            return;
        }

        expiredTime = DateTime.Now.AddHours(resourceData.regenTime);

        isLooted = true;
        sprite.color = new Color(1, 1, 1, 0.4f);
        ParticleManager.instance.CreateEffect(transform.position, gameObject);
    }

    public void CancelLooting()
    {
        expiredTime = DateTime.Now;
        isLooted = false;
        sprite.color = new Color(1, 1, 1, 1);
    }

    public void ShowRegenTimer()
    {
        // 만료 확인
        if ((expiredTime - DateTime.Now).TotalSeconds < 0)
        {
            isLooted = false;
            sprite.color = new Color(1, 1, 1, 1);
        }
    }

    public void SetTextAll()
    {
        ResourceInformation resourceInformation = ResourceInformation.instance;
        Language language = LanguageManager.instance.language;

        if (isLooted)
        {
            resourceInformation.texts[3].text = "리젠 날짜 : "
                + GetColorText(expiredTime.ToString("MM"), BLUE_COLOR) + "월 " 
                + GetColorText(expiredTime.ToString("dd"), BLUE_COLOR) + "일 "
                + GetColorText(expiredTime.ToString("HH"), BLUE_COLOR) + "시 " 
                + GetColorText(expiredTime.ToString("mm"), BLUE_COLOR) + "분 " 
                + GetColorText(expiredTime.ToString("ss"), BLUE_COLOR) + "초";
        }
        else
        {
            resourceInformation.texts[3].text = "채집 가능한 상태입니다.";
        }

        resourceInformation.texts[0].text = "이름 : " + resourceData.koName;
        resourceInformation.texts[1].text = "분포 개수 : " + GetColorText("" + resourceData.count, PINK_COLOR);
        resourceInformation.texts[2].text = "리젠 시간 : " + GetColorText("" + resourceData.regenTime, ORANGE_COLOR) + "시간";
    }

    private void ShowLeftTime()
    {
        TimeSpan leftTime = expiredTime - DateTime.Now;
        string timeColor = ORANGE_RED_COLOR;

        if (leftTime.TotalSeconds < 21600)
        {
            timeColor = SKY_BLUE_COLOR;
        }
        else if (leftTime.TotalSeconds < 43200)
        {
            timeColor = GREEN_COLOR;
        }
        else if (leftTime.TotalSeconds < 86400)
        {
            timeColor = ORANGE_COLOR;
        }

        if (isLooted)
        {
            ResourceInformation.instance.texts[4].text = "남은 시간 : ";

            if (leftTime.Days != 0)
            {
                ResourceInformation.instance.texts[4].text += GetColorText("" + leftTime.Days, timeColor) + "일 ";
            }
            if (leftTime.Hours != 0)
            {
                ResourceInformation.instance.texts[4].text += GetColorText("" + leftTime.Hours, timeColor) + "시간 ";
            }
            if (leftTime.Minutes != 0)
            {
                ResourceInformation.instance.texts[4].text += GetColorText("" + leftTime.Minutes, timeColor) + "분 ";
            }
            if (leftTime.Seconds != 0)
            {
                ResourceInformation.instance.texts[4].text += GetColorText("" + leftTime.Seconds, timeColor) + "초";
            }
        }
        else
        {
            ResourceInformation.instance.texts[4].text = "";
        }
    }

    public string GetColorText(string text, string colorValue)
    {
        return "<color=#" + colorValue + ">" + text + "</color>";
    }

    public void OnInformationUI()
    {
        ResourceInformation.instance.OnInformation(this);
        SetTextAll();
    }
}
