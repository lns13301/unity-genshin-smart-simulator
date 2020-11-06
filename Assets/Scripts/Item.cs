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

    public string[] GetCharacterNameWithColor()
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
                return null;
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
    ADVENTUREITEM
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
