using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceData
{
    public int code;
    public string enName;
    public string koName;
    public int count;
    public ItemType type;
    public Grade grade;
    public int regenTime;
    public string information;
    public string spritePath;

    public Sprite sprite;

    public ResourceData(int code, string enName, string koName, int count, ItemType type, Grade grade, string spritePath)
    {
        this.count = count;
        this.code = code;
        this.enName = enName;
        this.koName = koName;
        this.type = type;
        this.spritePath = spritePath;
        this.grade = grade;

        sprite = loadSprite(spritePath);
    }

    [ContextMenu("From Json Data")]
    public Sprite loadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}
