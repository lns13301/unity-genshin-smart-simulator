using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public int skillCode;

    public string name_ko;
    public string name_en;

    public string spritePath;

    public Sprite sprite;

    public List<SkillInformation> informations;
    public List<SkillAbility> abilities;

    public Skill(int skillCode, string name_ko, string name_en, string spritePath, List<SkillInformation> informations, List<SkillAbility> abilities)
    {
        this.skillCode = skillCode;
        this.name_ko = name_ko;
        this.name_en = name_en;

        this.spritePath = spritePath;

        this.informations = informations;
        this.abilities = abilities;
    }

    [ContextMenu("From Json Data")]
    public Sprite LoadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}

[System.Serializable]
public class SkillInformation
{
    public string title_ko;
    public string title_en;

    public string content_ko;
    public string content_en;

    public SkillInformation(string title_ko, string title_en, string content_ko, string content_en)
    {
        this.title_ko = title_ko;
        this.title_en = title_en;
        this.content_ko = content_ko;
        this.content_en = content_en;
    }
}

[System.Serializable]
public class SkillAbility
{
    public string name_ko;
    public string name_en;
    public string unit;        // 단위 %, 원 뭐 이런거
    public List<float> values;
    public int floatCount;

    public SkillAbility(string name_ko, string name_en, string unit, List<float> values, int floatCount =  2)
    {
        this.name_ko = name_ko;
        this.name_en = name_en;
        this.unit = unit;
        this.values = values;
        this.floatCount = floatCount;

        // LoadLevels();
    }

    private void LoadLevels()
    {
        //레벨 로딩
    }
}
