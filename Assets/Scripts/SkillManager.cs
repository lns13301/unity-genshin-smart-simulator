using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public GameObject statSet;
    public Animator animator;
    public Image illust;
    public Text nameText;
    public Text countText;

    public GameObject skillFrame;
    public GameObject content;
    public GameObject skillPanel;
    public GameObject ascensionPanel;

    public GameObject informationSet;
    public GameObject informationPanel;
    public Image informationSkillImage;
    public Text informationSkillTitle;
    public int skillLevel;
    public GameObject informationContent;
    public Text informationContentText;

    public Skill skill;

    public string skillText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        animator = statSet.GetComponent<Animator>();

        statSet.SetActive(false);
        informationSet.SetActive(false);
        informationPanel = informationSet.transform.GetChild(6).gameObject;
        informationSkillImage = informationPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        informationSkillTitle = informationPanel.transform.GetChild(0).GetChild(1).GetComponent<Text>();
        skillLevel = 0;

        OffPanelAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStatUI(Item item)
    {
        SetSkillDestroy();
        OffPanelAll();

        Language language = LanguageManager.instance.language;
        SoundManager.instance.PlayOneShotEffectSound(1);
        statSet.SetActive(true);

        animator.SetBool("isUIOn", true);

        illust.sprite = item.sprite;
        
        if (language == Language.KOREAN)
        {
            nameText.text = item.koName;
            countText.text = item.count + "개";
            // countText.text = GetColorText("" + item.count, "e59e00") + "개";
        }
        else
        {
            nameText.text = item.enName;
            countText.text = item.count + "EA";
            // countText.text = GetColorText("" + item.count, "e59e00") + "EA";
        }

        if (item.type == ItemType.CHARACTER)
        {
            for (int i = item.character.skillStartCode; i < item.character.skillStartCode + 3; i++)
            {
                GameObject go = Instantiate(skillFrame);

                go.transform.SetParent(content.transform);
                go.GetComponent<SkillFrame>().SetSkillWithBaseSetting(SkillDatabase.instance.findSkillByCode(i));
                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void SetSkillDestroy()
    {
        if (content.transform.childCount == 0)
        {
            return;
        }

        for (int i = content.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(content.transform.GetChild(i).gameObject);
        }
    }

    public void OffStatUI()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);

        animator.SetBool("isUIOn", false);

        statSet.SetActive(false);
    }

    public void OnInformation(Skill skill)
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        informationSet.SetActive(true);
        ResetContentPosition();

        this.skill = skill;
        skillText = "";

        informationSkillImage.sprite = skill.sprite;
        skillLevel = 0;

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            for (int i = 0; i < skill.informations.Count; i++)
            {
                skillText += GetColorText(skill.informations[i].title_ko, "B34AE3") + "\n\n" + skill.informations[i].content_ko + "\n\n\n";
            }

            SetSkillAbilityText();
        }
        else
        {
            for (int i = 0; i < skill.informations.Count; i++)
            {
                skillText += GetColorText(skill.informations[i].title_en, "B34AE3") + "\n\n" + skill.informations[i].content_en + "\n\n\n";
            }

            SetSkillAbilityText();
        }
    }

    public void SetSkillAbilityText()
    {
        informationContentText.text = skillText;

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            informationSkillTitle.text = skill.name_ko + "\nLv " + (skillLevel + 1);

            for (int i = 0; i < skill.abilities.Count; i++)
            {
                /*                informationContentText.text += skill.abilities[i].name_ko + "  " + MakeSpace(i, false)
                                    + GetColorText(skill.abilities[i].values[skillLevel] + skill.abilities[i].unit + "\n", "e59e00");*/
                informationContentText.text += skill.abilities[i].name_ko + "\n"
                + GetColorText(skill.abilities[i].values[skillLevel] + skill.abilities[i].unit + "\n", "e59e00") + "\n";
            }
        }
        else
        {
            informationSkillTitle.text = skill.name_en + "\nLv " + (skillLevel + 1);

            for (int i = 0; i < skill.abilities.Count; i++)
            {
                /*                informationContentText.text += skill.abilities[i].name_en + "  " + MakeSpace(i, false)
                                    + GetColorText(skill.abilities[i].values[skillLevel] + skill.abilities[i].unit + "\n", "e59e00");*/
                informationContentText.text += skill.abilities[i].name_en + "\n"
                + GetColorText(skill.abilities[i].values[skillLevel] + skill.abilities[i].unit + "\n", "e59e00") + "\n";
            }
        }
    }

    public string MakeSpace(int index, bool isEnglish = true)
    {
        string space = "";

        if (!isEnglish)
        {
            for (int i = 0; i < 50 - (skill.abilities[index].name_ko.Length * 3); i++)
            {
                space += " ";
            }
        }
        else
        {
            for (int i = 0; i < 25 - (skill.abilities[index].name_en.Length * 1.5); i++)
            {
                space += " ";
            }
        }

        return space;
    }

    public void OffInformation()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        informationSet.SetActive(false);
    }

    public string GetColorText(string text, string colorValue)
    {
        return "<color=#" + colorValue + ">" + text + "</color>";
    }

    public void SetSkillLevelUP()
    {
        SoundManager.instance.PlayOneShotEffectSound(2);

        if (skillLevel == 14)
        {
            return;
        }

        skillLevel++;
        SetSkillAbilityText();
    }

    public void SetSkillLevelDown()
    {
        SoundManager.instance.PlayOneShotEffectSound(2);

        if (skillLevel == 0)
        {
            return;
        }

        skillLevel--;
        SetSkillAbilityText();
    }

    public void ResetContentPosition()
    {
        informationContent.GetComponent<RectTransform>().position = new Vector2(informationContent.GetComponent<RectTransform>().position.x, 0);
    }
    
    public void OnSkillPanel()
    {
        SoundManager.instance.PlayOneShotEffectSound(2);

        skillPanel.SetActive(true);
        ascensionPanel.SetActive(false);
    }

    public void OnAscensionPanel()
    {
        SoundManager.instance.PlayOneShotEffectSound(2);

        ascensionPanel.SetActive(true);
        skillPanel.SetActive(false);
    }

    public void OffPanelAll()
    {
        skillPanel.SetActive(false);
        ascensionPanel.SetActive(false);
        informationSet.SetActive(false);
    }
}
