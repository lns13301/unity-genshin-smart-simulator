using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrame : MonoBehaviour
{
    private string NEW_LINE = "\n";
    public Item item;
    public int slotIndex;
    public Text indexText;
    public Image itemImage;

    public GameObject[] transforms; // star count, symbol, particle

    public bool isItemFrame;

    public GameObject information;
    public Text informationText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    // 기존 아이템 정보 보는 탭
    public void OnItemInformation()
    {
        if (item.type == ItemType.CHARACTER)
        {
            SkillManager.instance.OnStatUI(item);

            return;
        }

        GameManager.instance.OffNoticeAll();
        SoundManager.instance.PlayOneShotEffectSound(1);
        Language language = LanguageManager.instance.language;

        if (isItemFrame)
        {
            information = GameManager.instance.information;
            informationText = GameManager.instance.informationText;
        }
        else
        {
            information = InventoryManager.instance.information;
            informationText = InventoryManager.instance.informationText;
            InventoryManager.instance.nowInformationSlotIndex = slotIndex; // 나중에 탭단위로하려면 구분해줘야함!
        }

        information.SetActive(true);

        if (!isItemFrame && item.type != ItemType.CHARACTER)
        {
            information.transform.GetChild(4).gameObject.SetActive(true);
        }

        if (language == Language.KOREAN)
        {
            informationText.text = item.koName;
        }
        else
        {
            informationText.text = item.enName;
        }

        string[] character = new string[2];

        if (item.type == ItemType.CHARACTER)
        {
            if (language == Language.KOREAN)
            {
                character = item.GetCharacterNameWithColorKorean();
            }
            else
            {
                character = item.GetCharacterNameWithColorEnglish();
            }

            informationText.text += NEW_LINE + NEW_LINE + GetColorText(character[1], character[0]);
        }
        else
        {
            if (language == Language.KOREAN)
            {
                informationText.text += NEW_LINE + NEW_LINE + GetColorText(item.GetItemTypeToKorean(), "37946e");
            }
            else
            {
                informationText.text += NEW_LINE + NEW_LINE + GetColorText(item.GetItemTypeToEnglish(), "37946e");
            }
        }

        if (language == Language.KOREAN)
        {
            informationText.text += NEW_LINE + NEW_LINE + GetColorText(item.GetItemGradeToKorean(), "d95763");
        }
        else
        {
            informationText.text += NEW_LINE + NEW_LINE + GetColorText(item.GetItemGradeToEnglish(), "d95763");
        }

        if (!isItemFrame)
        {
            if (language == Language.KOREAN)
            {
                informationText.text += NEW_LINE + NEW_LINE + GetColorText("" + item.count, "e85f37") + " 개";
            }
            else
            {
                informationText.text += NEW_LINE + NEW_LINE + GetColorText("" + item.count, "e85f37") + " ea";
            }
        }
    }

    public string GetColorText(string text, string colorValue)
    {
        return "<color=#" + colorValue + ">" + text + "</color>";
    }

    public void SetParticles(float particleDelay)
    {
        transforms[4].gameObject.SetActive(false);
        transforms[5].gameObject.SetActive(false);
        transforms[6].gameObject.SetActive(false);

        Invoke("OnParticle", particleDelay);
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

        if (item.type == ItemType.CHARACTER)
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
        slotIndex = index - 1;

        if (isItemFrame) 
        {
            // indexText.text = "최근 " + GetColorText("" + index, "e59e00") + "번째";
            indexText.text = GetColorText("" + index, "e59e00");
        }
        else
        {
            indexText.text = GetColorText("" + index, "e59e00");
        }
    }

    public void SetItemWithBaseSetting(Item item, int index, float particleDelay)
    {
        SetItem(item);
        SetIndex(index);
        SetParticles(particleDelay);
        OffDetails();
        OnDetails();
        Invoke("SetImage", 0.1f);
    }

    public void OnDetails()
    {
        GameManager.instance.OffNoticeAll();

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

        if (item.type == ItemType.CHARACTER)
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

        if (item.type == ItemType.CHARACTER)
        {
            transforms[3].gameObject.SetActive(false);
        }
    }
}
