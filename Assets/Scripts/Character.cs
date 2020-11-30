using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public int code;
    public Element element;
    public Grade grade;
    public int skillStartCode;
    public string enName;
    public string koName;
    public int phase; // 돌파
    public AscensionMaterial ascensionMaterial; // 돌파 재료
    public int level;
    public int levelLimit;
    public int exp;
    public int nextExp;
    public int friendship;

    public Attribute attribute;
    public Item equipedWeapon;
    public List<Item> artifacts;
    public Constellation constellation;
    public Talent talent;
    public Profile profile;

    public int ascensionItemCode;
    public int ascensionMobItemCode;

    public Character(int code, Element element, Grade grade, int skillStartCode = 1000, int ascensionItemCode = 10300, int ascensionMobItemCode = 10300
        , string enName = null, string koName = null, int phase = 0, AscensionMaterial ascensionMaterial = null
        , int level = 1, int levelLimit = 20, int exp = 0, int nextExp = 0, int friendship = 0
        , Attribute attribute = null, Item equipedWeapon = null, List<Item> artifacts = null
        , Constellation constellation = null, Talent talent = null, Profile profile = null)
    {
        this.code = code;
        this.element = element;
        this.grade = grade;
        this.skillStartCode = skillStartCode;
        this.ascensionItemCode = ascensionItemCode;
        this.ascensionMobItemCode = ascensionMobItemCode;
        this.enName = enName;
        this.koName = koName;
        this.phase = phase;
        this.ascensionMaterial = ascensionMaterial;
        this.level = level;
        this.levelLimit = levelLimit;
        this.exp = exp;
        this.nextExp = nextExp;
        this.friendship = friendship;

        this.attribute = attribute;
        this.equipedWeapon = equipedWeapon;
        this.artifacts = artifacts;
        this.constellation = constellation;
        this.talent = talent;
        this.profile = profile;
    }
}

[System.Serializable]
public class Constellation
{

}

[System.Serializable]
public class Talent
{

}

[System.Serializable]
public class Profile
{

}

[System.Serializable]
public class AscensionMaterial
{

}

[System.Serializable]
public enum Element
{
    PYRO = 0, // FIRE
    HYDRO = 1, // WATER
    ANEMO = 2, // WIND
    ELECTRO = 3, // LIGHTNING
    DENDRO = 4, // NATURE
    CRYO = 5, // FROST
    GEO = 6 // EARTH
}
