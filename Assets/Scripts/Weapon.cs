using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public int ascensionItemCode;
    public int ascensionEliteMobItemCode;
    public int ascensionMobItemCode;

    public Weapon(int ascensionItemCode, int ascensionEliteMobItemCode, int ascensionMobItemCode)
    {
        this.ascensionItemCode = ascensionItemCode;
        this.ascensionEliteMobItemCode = ascensionEliteMobItemCode;
        this.ascensionMobItemCode = ascensionMobItemCode;
    }
}
