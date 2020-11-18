using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int code;
    public string enName;
    public string koName;
    public int count;
    public ItemType type;
    public Grade grade;
    public string spritePath;

    public Sprite sprite;

    public Character character;

    public Item(int code, string enName, string koName, int count, ItemType type, Grade grade, string spritePath, Character character = null)
    {
        this.count = count;
        this.code = code;
        this.enName = enName;
        this.koName = koName;
        this.type = type;
        this.spritePath = spritePath;
        this.grade = grade;
        this.character = character;

        sprite = loadSprite(spritePath);
    }

    [ContextMenu("From Json Data")]
    public Sprite loadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    [ContextMenu("From Json Data")]
    public Animator loadAnimator(string path)
    {
        return Resources.Load<Animator>(path);
    }

    public Item MakeItem(Item item)
    {
        return new Item(item.code, item.enName, item.koName, item.count, item.type, item.grade, item.spritePath, item.character);
    }

    public string GetItemTypeToKorean()
    {
        switch (type)
        {
            case ItemType.CHARACTER:
                return "캐릭터";
            case ItemType.SWORD:
                return "한손검";
            case ItemType.CLAYMORE:
                return "양손검";
            case ItemType.BOW:
                return "활";
            case ItemType.CATALYST:
                return "법구";
            case ItemType.POLEARM:
                return "창";
            default:
                return "아이템";
        }
    }

    public string GetItemTypeToEnglish()
    {
        switch (type)
        {
            case ItemType.CHARACTER:
                return "Character";
            case ItemType.SWORD:
                return "Sword";
            case ItemType.CLAYMORE:
                return "Claymore";
            case ItemType.BOW:
                return "Bow";
            case ItemType.CATALYST:
                return "Catalyst";
            case ItemType.POLEARM:
                return "Polearm";
            default:
                return "item";
        }
    }

    public string[] GetCharacterNameWithColorKorean()
    {
        string[] answer = new string[2];

        switch (character.element)
        {
            case Element.PYRO:
                answer[0] = "e5660b";
                answer[1] = "불";
                return answer;
            case Element.HYDRO:
                answer[0] = "0e8ab3";
                answer[1] = "물";
                return answer;
            case Element.ANEMO:
                answer[0] = "548a89";
                answer[1] = "바람";
                return answer;
            case Element.ELECTRO:
                answer[0] = "9d6ece";
                answer[1] = "전기";
                return answer;
            case Element.CRYO:
                answer[0] = "6d94b4";
                answer[1] = "얼음";
                return answer;
            case Element.GEO:
                answer[0] = "f5ab23";
                answer[1] = "바위";
                return answer;
            default:
                answer[0] = "8ab958";
                answer[1] = "풀";
                return answer;
        }
    }

    public string[] GetCharacterNameWithColorEnglish()
    {
        string[] answer = new string[2];

        switch (character.element)
        {
            case Element.PYRO:
                answer[0] = "e5660b";
                answer[1] = "Pyro";
                return answer;
            case Element.HYDRO:
                answer[0] = "0e8ab3";
                answer[1] = "Hydro";
                return answer;
            case Element.ANEMO:
                answer[0] = "548a89";
                answer[1] = "Anemo";
                return answer;
            case Element.ELECTRO:
                answer[0] = "9d6ece";
                answer[1] = "Electro";
                return answer;
            case Element.CRYO:
                answer[0] = "6d94b4";
                answer[1] = "Cryo";
                return answer;
            case Element.GEO:
                answer[0] = "f5ab23";
                answer[1] = "Geo";
                return answer;
            default:
                answer[0] = "8ab958";
                answer[1] = "Dendro";
                return answer;
        }
    }

    public string GetItemGradeToKorean()
    {
        switch (grade)
        {
            case Grade.LEGEND:
                return "5성";
            case Grade.UNIQUE:
                return "4성";
            case Grade.EPIC:
                return "3성";
            case Grade.RARE:
                return "2성";
            default:
                return "1성";
        }
    }

    public string GetItemGradeToEnglish()
    {
        switch (grade)
        {
            case Grade.LEGEND:
                return "5-star";
            case Grade.UNIQUE:
                return "4-star";
            case Grade.EPIC:
                return "3-star";
            case Grade.RARE:
                return "2-star";
            default:
                return "1-star";
        }
    }

    public void SetCount(int count)
    {
        this.count = count;
    }
}

[System.Serializable]
public enum ItemType
{
    ETC,
    CHARACTER,
    SWORD,
    CLAYMORE,
    BOW,
    CATALYST,
    POLEARM,
    FLOWEROFLIFE,
    PLUMEOFDEATH,
    SANDSOFEON,
    GOBLETOFEONOTHEM,
    CIRCLETOFLOGOS,
    FOOD,
    MATERIAL,
    QUESTITEM,
    ADVENTUREITEM,
    COMMONCHEST,
    EXQUISITECHEST,
    PRECIOUSCHEST,
    LUXURIOUSCHEST,
    TIMELIMITEDCHALLENGE,
    MYSTERYOUSSEELIE,
    SHRINE,
    TELEPORTER,
    STATUEOFSEVEN,
    WORLDQUEST,
    ANEMOCULUS,
    GEOCULUS,
    MONSTER,
    CULUS,
    TREASURE,
    EVENT,
    MATERIAL_MINERAL,
    MATERIAL_FOOD,
    MATERIAL_MONDSTADT,
    METERIAL_LIYUE,
    ARTIFACT
}

[System.Serializable]
public enum Grade
{
    NORMAL,
    RARE,
    EPIC,
    UNIQUE,
    LEGEND
}
