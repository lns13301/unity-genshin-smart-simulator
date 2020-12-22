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
    public Weapon weapon;

    public Item(int code, string enName, string koName, int count, ItemType type, Grade grade, string spritePath, Character character = null, Weapon weapon = null)
    {
        this.count = count;
        this.code = code;
        this.enName = enName;
        this.koName = koName;
        this.type = type;
        this.spritePath = spritePath;
        this.grade = grade;
        this.character = character;
        this.weapon = weapon;

        sprite = LoadSprite(spritePath);
    }

    [ContextMenu("From Json Data")]
    public Sprite LoadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    [ContextMenu("From Json Data")]
    public Sprite LoadSprite()
    {
        return Resources.Load<Sprite>(spritePath);
    }

    [ContextMenu("From Json Data")]
    public Animator LoadAnimator(string path)
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

    public int GetElementAscensionJewelItemCode()
    {
        switch (character.element)
        {
            case Element.PYRO:
                return 10100;
            case Element.HYDRO:
                return 10104;
            case Element.ELECTRO:
                return 10108;
            case Element.ANEMO:
                return 10112;
            case Element.CRYO:
                return 10116;
            case Element.GEO:
                return 10120;
            default:
                return 10124;
        }
    }

    public int GetElementAscensionItemCode()
    {
        switch (character.element)
        {
            case Element.PYRO:
                return 10200;
            case Element.HYDRO:
                return 10201;
            case Element.ELECTRO:
                return 10202;
            case Element.ANEMO:
                return 10203;
            case Element.CRYO:
                return 10204;
            case Element.GEO:
                return 10205;
            default:
                return -1;
        }
    }

    public int GetElementIndex()
    {
        switch (character.element)
        {
            case Element.PYRO:
                return 0;
            case Element.HYDRO:
                return 1;
            case Element.ELECTRO:
                return 2;
            case Element.ANEMO:
                return 3;
            case Element.CRYO:
                return 4;
            case Element.GEO:
                return 5;
            default:
                return -1;
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
    MATERIAL_LIYUE,
    ARTIFACT,
    TALENTMATERIAL
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
