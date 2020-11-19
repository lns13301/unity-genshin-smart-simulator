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
    public SpriteRenderer spriteRenderer;

    public Language language;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        language = LanguageManager.instance.language;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLootedTime()
    {
        if (resourceData.isLooted)
        {
            return;
        }

        resourceData.expiredTime = DateTime.Now.AddSeconds(resourceData.regenTime);

        resourceData.isLooted = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        ParticleManager.instance.CreateEffect(transform.position, gameObject);
        spriteRenderer.sortingOrder = -1;

        SaveThisData();
    }

    public void CancelLooting()
    {
        resourceData.expiredTime = DateTime.Now;
        resourceData.isLooted = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        spriteRenderer.sortingOrder = 0;

        SaveThisData();
    }

    public void ShowRegenTimer()
    {
        // 만료 확인
        if ((resourceData.expiredTime - DateTime.Now).TotalSeconds < 0)
        {
            resourceData.isLooted = false;
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }

    public string GetMonth(string month)
    {
        switch (month)
        {
            case "1":
                return "JAN";
            case "2":
                return "FEB";
            case "3":
                return "MAR";
            case "4":
                return "APR";
            case "5":
                return "MAY";
            case "6":
                return "JUN";
            case "7":
                return "JUL";
            case "8":
                return "AUG";
            case "9":
                return "SEP";
            case "10":
                return "OCT";
            case "11":
                return "NOV";
            case "12":
                return "DEC";
        }

        return null;
    }

    public void SetTextAll()
    {
        ResourceInformation resourceInformation = ResourceInformation.instance;
        Language language = LanguageManager.instance.language;

        if (resourceData.isLooted && resourceData.regenTime < 0)
        {
            resourceInformation.texts[3].text = "";
        }
        else if (resourceData.isLooted)
        {
            if (language == Language.KOREAN)
            {
                resourceInformation.texts[3].text = "리젠 날짜 : "
                    + GetColorText(resourceData.expiredTime.ToString("MM"), BLUE_COLOR) + "월 "
                    + GetColorText(resourceData.expiredTime.ToString("dd"), BLUE_COLOR) + "일 "
                    + GetColorText(resourceData.expiredTime.ToString("HH"), BLUE_COLOR) + "시 "
                    + GetColorText(resourceData.expiredTime.ToString("mm"), BLUE_COLOR) + "분 "
                    + GetColorText(resourceData.expiredTime.ToString("ss"), BLUE_COLOR) + "초";
            }
            else
            {
                resourceInformation.texts[3].text = "Respawn Date : "
                    + GetColorText(GetMonth(resourceData.expiredTime.ToString("MM")), BLUE_COLOR) + ", "
                    + GetColorText(resourceData.expiredTime.ToString("dd"), BLUE_COLOR) + "  "
                    + GetColorText(resourceData.expiredTime.ToString("HH"), BLUE_COLOR) + ":"
                    + GetColorText(resourceData.expiredTime.ToString("mm"), BLUE_COLOR) + ":"
                    + GetColorText(resourceData.expiredTime.ToString("ss"), BLUE_COLOR);
            }
        }
        else
        {
            if (language == Language.KOREAN)
            {
                resourceInformation.texts[3].text = "채집 가능한 상태입니다.";
            }
            else
            {
                resourceInformation.texts[3].text = "You can collect it.";
            }
        }

        if (language == Language.KOREAN)
        {
            resourceInformation.texts[0].text = "이름 : " + resourceData.koName;
            resourceInformation.texts[1].text = "분포 개수 : " + GetColorText("" + resourceData.count, PINK_COLOR);

            if (resourceData.regenTime > 0)
            {
                resourceInformation.texts[2].text = "리젠 시간 : " + GetColorText("" + resourceData.regenTime / 3600, ORANGE_COLOR) + " 시간";
            }
            else
            {
                resourceInformation.texts[2].text = "1회성 채집물입니다.";
            }

            resourceInformation.texts[5].text = "정보 : " + resourceData.information;
        }
        else
        {
            resourceInformation.texts[0].text = "Name : " + resourceData.enName;
            resourceInformation.texts[1].text = "Count : " + GetColorText("" + resourceData.count, PINK_COLOR);

            if (resourceData.regenTime > 0)
            {
                resourceInformation.texts[2].text = "Respawn Time : " + GetColorText("" + resourceData.regenTime / 3600, ORANGE_COLOR) + " Hours";
            }
            else
            {
                resourceInformation.texts[2].text = "It's a one-time collection.";
            }

            resourceInformation.texts[5].text = "Information : " + resourceData.information;
        }

        if (resourceData.information == "")
        {
            resourceInformation.texts[5].text = "";
        }

        if (resourceData.regenTime < 0)
        {
            ResourceInformation.instance.starlightCount = 1;
            resourceInformation.texts[7].text = " -" + ResourceInformation.instance.starlightCount;
            resourceInformation.texts[6].transform.parent.gameObject.SetActive(false);
            resourceInformation.texts[7].transform.parent.gameObject.SetActive(true);
        }
        else
        {
            ResourceInformation.instance.stardustCount = resourceData.count * (resourceData.regenTime / 21600);
            resourceInformation.texts[6].text = " -" + ResourceInformation.instance.stardustCount;
            resourceInformation.texts[6].transform.parent.gameObject.SetActive(true);
            resourceInformation.texts[7].transform.parent.gameObject.SetActive(false);
        }

        SaveThisData();
    }

    public void ShowLeftTime()
    {
        TimeSpan leftTime = resourceData.expiredTime - DateTime.Now;
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

        if (resourceData.isLooted && leftTime.TotalSeconds < 0 && resourceData.regenTime > 0)
        {
            CancelLooting(); // 나중에 수확기능으로 변경하던지..
        }

        if (resourceData.isLooted && resourceData.regenTime > 0)
        {
            if (language == Language.KOREAN)
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
                ResourceInformation.instance.texts[4].text = "Remaining Time : ";

                if (leftTime.Days != 0)
                {
                    ResourceInformation.instance.texts[4].text += GetColorText("" + leftTime.Days, timeColor) + "Day(s) ";
                }
                if (leftTime.Hours != 0)
                {
                    ResourceInformation.instance.texts[4].text += GetColorText("" + leftTime.Hours, timeColor) + "Hour(s) ";
                }
                if (leftTime.Minutes != 0)
                {
                    ResourceInformation.instance.texts[4].text += GetColorText("" + leftTime.Minutes, timeColor) + "Minute(s) ";
                }
                if (leftTime.Seconds != 0)
                {
                    ResourceInformation.instance.texts[4].text += GetColorText("" + leftTime.Seconds, timeColor) + "Sec(s)";
                }
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

    public void SaveThisData()
    {
        transform.parent.GetComponent<ResourceParent>().resourceSaveDataFile.resourceSaveDatas[resourceData.index] = 
            new ResourceSaveData(
                resourceData.enName, resourceData.isLooted, transform.parent.GetComponent<ResourceParent>().SerializeDateTime(resourceData.expiredTime));

        transform.parent.GetComponent<ResourceParent>().SaveResourceDataToJson();
    }
}
