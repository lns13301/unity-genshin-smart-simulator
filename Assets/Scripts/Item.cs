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

    public Element element;

    public Item(int code, string enName, string koName, int count, ItemType type, Grade grade, string spritePath, Element element = Element.NONE)
    {
        this.count = count;
        this.code = code;
        this.enName = enName;
        this.koName = koName;
        this.type = type;
        this.spritePath = spritePath;
        this.grade = grade;
        this.element = element;

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

    public Item makeItem(Item item)
    {
        return new Item(item.code, item.enName, item.koName, item.count, item.type, item.grade, item.spritePath, item.element);
    }
}

public enum ItemType
{
    HERO,
    SWORD,
    CLAYMORE,
    BOW,
    CATALYST,
    POLEARM
}

public enum Grade
{
    NORMAL,
    RARE,
    EPIC,
    UNIQUE,
    LEGEND
}

public enum Element
{
    NONE,
    PYRO, // FIRE
    HYDRO, // WATER
    ANEMO, // WIND
    ELECTRO, // LIGHTNING
    DENDRO, // NATURE
    CRYO, // FROST
    GEO // EARTH
}
