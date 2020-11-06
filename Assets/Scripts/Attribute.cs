using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute
{
    // Base Stats
    public float hp;
    public float maxHp;
    public float atk;
    public float def;
    public float elementalMastery;
    public float maxStamina;

    // Advanced Stats
    public float critRate;
    public float critDmg;
    public float healingBonus;
    public float incomingHealingBonus;
    public float energyRecharge;
    public float reduceCd;
    public float powerfulShield;

    // Elemental Type
    public float pyroDmgBonus;
    public float pyroRes;
    public float hydroDmgBonus;
    public float hydroRes;
    public float dendroDmgBonus;
    public float dendroRes;
    public float electroDmgBonus;
    public float electroRes;
    public float anemoDmgBonus;
    public float anemoRes;
    public float cryoDmgBonus;
    public float cryoRes;
    public float geoDmgBonus;
    public float geoRes;
    public float physicalDmgBonus;
    public float physicalRes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
