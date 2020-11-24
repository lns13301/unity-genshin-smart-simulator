using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillFrame : MonoBehaviour
{
    public Skill skill;

    public Image image;
    public Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSkillWithBaseSetting(Skill skill)
    {
        this.skill = skill;

        image.sprite = skill.sprite;

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            nameText.text = skill.name_ko;
        }
        else
        {
            nameText.text = skill.name_en;
        }
    }
}
