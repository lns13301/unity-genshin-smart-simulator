using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int acquantFateCount;
    public int intertwinedFateCount;
    public int starLightCount;
    public int starDustCount;
    public int primogemCount;
    public int genesisCrystalCount;

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
    public List<Item> limitedHistory;

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

    public List<int> usedCoupon;

    public Language language;

    public bool AddCharacter(Item item)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].code == item.code)
            {
                characters[i].SetCount(characters[i].count + 1);
                return true;
            }
        }
        characters.Add(item);

        return true;
    }

    // 인벤토리 무기 최대 보유 수량 조정
    public bool AddWeapon(Item item)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].code == item.code)
            {
                weapons[i].SetCount(weapons[i].count + 1);
                return true;
            }
        }
        weapons.Add(item);

        return true;

/*        if (weapons.Count > 200)
        {
            return false;
        }

        weapons.Add(item);

        return true;*/
    }

    public void RemoveWeapon(int index)
    {
        weapons.RemoveAt(index);
    }

    public void AddItemAndHistory(Item item, int bannerIndex)
    {
        bool isPutNewItem = false;

        if (item.type == ItemType.CHARACTER)
        {
            isPutNewItem = AddCharacter(item);
        }
        else
        {
            isPutNewItem = AddWeapon(item);
        }

        if (!isPutNewItem)
        {
            return;
        }

        switch (bannerIndex)
        {
            case 0:
                noelleHistory.Add(item);
                goto default;
            case 1:
                characterHistory.Add(item);
                goto default;
            case 2:
                weaponHistory.Add(item);
                goto default;
            case 3:
                normalHistory.Add(item);
                goto default;
            case 4:
                limitedHistory.Add(item);
                goto default;
            default:
                SetHistoryRecent();
                break;
        }
    }

    private void SetHistoryRecent()
    {
        while (normalHistory.Count > 100)
        {
            normalHistory.RemoveAt(0);
        }

        while (characterHistory.Count > 100)
        {
            characterHistory.RemoveAt(0);
        }

        while (weaponHistory.Count > 100)
        {
            weaponHistory.RemoveAt(0);
        }

        while (noelleHistory.Count > 100)
        {
            noelleHistory.RemoveAt(0);
        }

        while (limitedHistory.Count > 100)
        {
            limitedHistory.RemoveAt(0);
        }
    }
}
