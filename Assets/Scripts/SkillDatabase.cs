using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SkillDatabase : MonoBehaviour
{
    public static SkillDatabase instance;

    public SkillDataFile skillDataFile;
    public List<Skill> skillDB;
    public string spritePath = "Images/UI/Stat/";

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        //SaveSkillData();
        LoadSkillData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("To Json Data")]
    public void SaveSkillData()
    {
        Debug.Log("저장 성공");
        skillDataFile.skillDatas = new List<Skill>();

        List<SkillInformation> skillInformations = new List<SkillInformation>();
        skillInformations.Add(new SkillInformation("일반공격", "Normal Attack", "활로 최대 5번 공격한다.", "Perform up to 5 consecutive shots with a bow."));
        skillInformations.Add(new SkillInformation("강공격", "Charged Attack", "피해가 더크고 정확한 조준사격을 한다. 조준시 뜨거운 화염이 화살 촉에 계속해서 모인다.", "Perform a more precise Aimed Shot with increased DMG. While aiming, flames will accumulate on the arrowhead. A fully charged flaming arrow will deal Pyro DMG."));
        skillInformations.Add(new SkillInformation("낙하공격", "Plunging Attack", "공중에서 화살비를 쏜 후 빠른 속도로 땅에 착지한다. 땅에 닿으면 범위 피해를 준다.", "Fires off a shower of arrows in mid-air before falling and striking the ground, dealing AoE DMG upon impact."));

        List<float> value1 = new List<float>();
        value1.Add(36.12f);
        value1.Add(39.06f);
        value1.Add(42f);
        value1.Add(46.2f);
        value1.Add(49.14f);
        value1.Add(52.5f);
        value1.Add(57.12f);
        value1.Add(61.74f);
        value1.Add(66.36f);
        value1.Add(71.4f);
        value1.Add(76.44f);
        value1.Add(81.48f);
        value1.Add(86.52f);
        value1.Add(91.56f);
        value1.Add(96.6f);

        List<SkillAbility> abilities = new List<SkillAbility>();
        abilities.Add(new SkillAbility("1단 공격 피해", "1-Hit DMG", "%", value1));
        abilities.Add(new SkillAbility("2단 공격 피해", "2-Hit DMG", "%", value1));
        abilities.Add(new SkillAbility("3단 공격 피해", "3-Hit DMG", "%", value1));
        abilities.Add(new SkillAbility("4단 공격 피해", "4-Hit DMG", "%", value1));
        abilities.Add(new SkillAbility("5단 공격 피해", "5-Hit DMG", "%", value1));
        abilities.Add(new SkillAbility("조준 사격", "Aimed Shot", "%", value1));
        abilities.Add(new SkillAbility("풀 차지 조준 사격", "Fully-Charged Aimed Shot", "%", value1));
        abilities.Add(new SkillAbility("낙하 기간 피해", "Plunge DMG", "%", value1));
        abilities.Add(new SkillAbility("저공 추락 충격 피해", "Low Plunge DMG", "%", value1));
        abilities.Add(new SkillAbility("고공 추락 충격 피해", "High Plunge DMG", "%", value1));

        skillDataFile.skillDatas.Add(new Skill(1000, spritePath + "Amber/normal_atack_sharpshooter", skillInformations, abilities));
        skillDataFile.skillDatas.Add(new Skill(1001, spritePath + "Amber/normal_atack_sharpshooter", skillInformations, abilities));

        string jsonData = JsonUtility.ToJson(skillDataFile, true);

        File.WriteAllText(SaveOrLoad(false, true, "SkillData"), jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadSkillData()
    {
        try
        {
            Debug.Log("스킬 정보 로드 성공");
            skillDataFile = JsonUtility.FromJson<SkillDataFile>(Resources.Load<TextAsset>("SkillData").ToString());

            for (int i = 0; i < skillDataFile.skillDatas.Count; i++)
            {
                skillDataFile.skillDatas[i].sprite = LoadSprite(skillDataFile.skillDatas[i].spritePath);
                skillDB.Add(skillDataFile.skillDatas[i]);
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("로드 오류");

            string jsonData = JsonUtility.ToJson(skillDataFile, true);

            File.WriteAllText(SaveOrLoad(false, false, "SkillData"), jsonData);
            LoadSkillData();
        }
    }

    public string SaveOrLoad(bool isMobile, bool isSave, string fileName)
    {
        if (isSave)
        {
            if (isMobile)
            {
                // 모바일 저장
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 저장
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
        else
        {
            if (isMobile)
            {
                // 모바일 로드
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 로드
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
    }

    [ContextMenu("From Json Data")]
    public Sprite LoadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}

[System.Serializable]
public class SkillDataFile
{
    public List<Skill> skillDatas;
}
