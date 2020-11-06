using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int acquantFateCount;
    public int intertwinedFateCount;

    public int acquantFateTotalTryCount;
    public int acquantFateFourStarCount;
    public int acquantFateFiveStarCount;

    public int weaponTotalTryCount;
    public int weaponFourStarCount;
    public int weaponFiveStarCount;

    public int characterTotalTryCount;
    public int characterFourStarCount;
    public int characterFiveStarCount;

    public bool isPickUpWeaponAlways;
    public bool isPickUpCharacterAlways;
    public bool isPickUpNormalAlways;

    public bool isPickUpWeapon4Always;
    public bool isPickUpCharacter4Always;

    public bool hasFirstTimeNoelle;

    public List<Item> items;

    public List<Item> normalHistory;
    public List<Item> weaponHistory;
    public List<Item> characterHistory;
    public List<Item> noelleHistory;

    public int adLastTime;
    public int adLastDate;
    public int adCount;

    public List<Item> characters;
    public List<Item> weapons;
    public List<Item> artifacts;
    public List<Item> foods;
    public List<Item> materials;
    public List<Item> questItems;
    public List<Item> adventureItems;
}
