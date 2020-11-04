using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrame : MonoBehaviour
{
    private string NEW_LINE = "\n";
    public Item item;
    public Text indexText;
    public Image itemImage;

    public GameObject[] transforms; // star count, symbol, particle

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public void OnItemInformation()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);

        GameObject information = GameManager.instance.information;
        Text informationText = GameManager.instance.informationText;

        information.SetActive(true);

        informationText.text = item.koName;

        string[] character = new string[2];

        if (item.type == ItemType.HERO)
        {
            character = item.GetCharacterNameWithColor();
            informationText.text += NEW_LINE + NEW_LINE + GetColorText(character[1], character[0]);
        }
        else
        {
            informationText.text += NEW_LINE + NEW_LINE + GetColorText(item.GetItemTypeToKorean(), "37946e");
        }

        informationText.text += NEW_LINE + NEW_LINE + GetColorText(item.GetItemGradeToKorean(), "d95763");
    }

    public string GetColorText(string text, string colorValue)
    {
        return "<color=#" + colorValue + ">" + text + "</color>";
    }

    public void SetParticles()
    {
        transforms[4].gameObject.SetActive(false);
        transforms[5].gameObject.SetActive(false);
        transforms[6].gameObject.SetActive(false);

        Invoke("OnParticle", 1f);
    }

    public void OnParticle()
    {
        if (item.grade == Grade.LEGEND)
        {
            transforms[6].gameObject.SetActive(true);
        }
        else if (item.grade == Grade.UNIQUE)
        {
            transforms[5].gameObject.SetActive(true);
        }
        else
        {
            transforms[4].gameObject.SetActive(true);
        }
    }

    public void SetImage()
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = item.sprite;

        if (item.type == ItemType.HERO)
        {
            itemImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
            itemImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 229.8f);
        }
        else
        {
            itemImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 97.5f);
            itemImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 97.5f);
        }
    }

    public void SetIndex(int index)
    {
        indexText.text = "최근 " + GetColorText(""+ index, "e59e00") + "번째";
    }

    public void SetItemWithBaseSetting(Item item, int index)
    {
        SetItem(item);
        SetIndex(index);
        SetParticles();
        OffDetails();
        OnDetails();
        Invoke("SetImage", 0.1f);
    }

    public void OnDetails()
    {
        if (item.grade == Grade.EPIC)
        {
            transforms[0].gameObject.SetActive(true);
        }
        if (item.grade == Grade.UNIQUE)
        {
            transforms[1].gameObject.SetActive(true);
        }
        if (item.grade == Grade.LEGEND)
        {
            transforms[2].gameObject.SetActive(true);
        }

        if (item.type == ItemType.HERO)
        {
            transforms[3].gameObject.SetActive(true); // 속성 켜기
        }
    }

    public void OffDetails()
    {
        if (item.grade == Grade.EPIC)
        {
            transforms[0].gameObject.SetActive(false);
        }
        if (item.grade == Grade.UNIQUE)
        {
            transforms[1].gameObject.SetActive(false);
        }
        if (item.grade == Grade.LEGEND)
        {
            transforms[2].gameObject.SetActive(false);
        }

        if (item.type == ItemType.HERO)
        {
            transforms[3].gameObject.SetActive(false);
        }
    }
}
