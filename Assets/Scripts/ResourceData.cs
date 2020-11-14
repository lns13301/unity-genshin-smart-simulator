using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ResourceData
{
    public int code;
    public Vector2 position;
    public string enName;
    public string koName;
    public int count;
    public ItemType type;
    public Grade grade;
    public int regenTime;
    public string information;
    public bool isLooted;
    public DateTime expiredTime;
    public int index;

    public string spritePath;

    public Sprite sprite;

    public ResourceData(int code, Vector2 position, string enName, string koName, int count, ItemType type, Grade grade
        , int regenTime,string information, bool isLooted, DateTime expriedTime, string spritePath)
    {
        this.count = count;
        this.code = code;
        this.enName = enName;
        this.koName = koName;
        this.type = type;
        this.spritePath = spritePath;
        this.grade = grade;
        this.regenTime = regenTime;
        this.information = information;
        this.isLooted = isLooted;
        this.expiredTime = expriedTime;

        sprite = loadSprite(spritePath);
    }

    [ContextMenu("From Json Data")]
    public Sprite loadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}
