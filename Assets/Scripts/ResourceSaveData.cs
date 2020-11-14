using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceSaveData
{
    public string enName;
    public bool isLooted;
    public string expiredTime;

    public ResourceSaveData(string enName, bool isLooted, string expiredTime = "")
    {
        this.enName = enName;
        this.isLooted = isLooted;
        this.expiredTime = expiredTime;
    }
}
