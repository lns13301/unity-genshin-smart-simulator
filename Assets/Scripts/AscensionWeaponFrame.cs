using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AscensionWeaponFrame : MonoBehaviour
{
    public string[][] ascensionMaterialCountEpic;
    public string[][] ascensionMaterialCountUnique;
    public string[][] ascensionMaterialCountLegend;

    public Item item;

    public List<Image> itemImages;
    public List<Text> itemInformations;

    public int frameIndex;

    // Start is called before the first frame update
    void Start()
    {
        SetAscensionCount();

        // Lv Text 제외하고 넣기
        for (int i = 1; i < transform.childCount; i++)
        {
            itemImages.Add(transform.GetChild(i).GetChild(0).GetComponent<Image>());
            itemInformations.Add(transform.GetChild(i).GetChild(1).GetComponent<Text>());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetAscensionData(Item item)
    {
        this.item = item;

        if (item.type == ItemType.CHARACTER)
        {
            Debug.Log("타입이 캐릭터임");
            return;
        }

        switch (frameIndex)
        {
            case 0:
                SetImage();
                break;
            case 1:
                SetImage(1);
                break;
            case 2:
                SetImage(1, 1, 1);
                break;
            case 3:
                SetImage(2, 1, 1);
                break;
            case 4:
                SetImage(2, 2, 2);
                break;
            case 5:
                SetImage(3, 2, 2);
                break;
        }

        if (item.grade == Grade.EPIC)
        {
            transform.GetChild(4).GetChild(1).GetComponent<Text>().text = " x " + ascensionMaterialCountEpic[3][frameIndex];
        }
        else if (item.grade == Grade.UNIQUE)
        {
            transform.GetChild(4).GetChild(1).GetComponent<Text>().text = " x " + ascensionMaterialCountUnique[3][frameIndex];
        }
        else
        {
            transform.GetChild(4).GetChild(1).GetComponent<Text>().text = " x " + ascensionMaterialCountLegend[3][frameIndex];
        }
    }

    public void SetImage(int itemIndex = 0, int eliteItemIndex = 0, int mobItemIndex = 0)
    {
        Debug.Log(item.weapon.ascensionItemCode);

        transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
            ItemDatabase.instance.findItemByIndex(item.weapon.ascensionItemCode + itemIndex).LoadSprite();
        transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite =
            ItemDatabase.instance.findItemByIndex(item.weapon.ascensionEliteMobItemCode + eliteItemIndex).LoadSprite();
        transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite =
            ItemDatabase.instance.findItemByIndex(item.weapon.ascensionMobItemCode + mobItemIndex).LoadSprite();

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            if (item.grade == Grade.EPIC)
            {
                transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionItemCode + itemIndex).koName + " x " + ascensionMaterialCountEpic[0][frameIndex];
                transform.GetChild(2).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionEliteMobItemCode + eliteItemIndex).koName + " x " + ascensionMaterialCountEpic[1][frameIndex];
                transform.GetChild(3).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionMobItemCode + mobItemIndex).koName + " x " + ascensionMaterialCountEpic[2][frameIndex];
            }
            else if (item.grade == Grade.UNIQUE)
            {
                transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionItemCode + itemIndex).koName + " x " + ascensionMaterialCountUnique[0][frameIndex];
                transform.GetChild(2).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionEliteMobItemCode + eliteItemIndex).koName + " x " + ascensionMaterialCountUnique[1][frameIndex];
                transform.GetChild(3).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionMobItemCode + mobItemIndex).koName + " x " + ascensionMaterialCountUnique[2][frameIndex];
            }
            else
            {
                transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionItemCode + itemIndex).koName + " x " + ascensionMaterialCountLegend[0][frameIndex];
                transform.GetChild(2).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionEliteMobItemCode + eliteItemIndex).koName + " x " + ascensionMaterialCountLegend[1][frameIndex];
                transform.GetChild(3).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionMobItemCode + mobItemIndex).koName + " x " + ascensionMaterialCountLegend[2][frameIndex];
            }
        }
        else
        {
            if (item.grade == Grade.EPIC)
            {
                transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionItemCode + itemIndex).enName + " x " + ascensionMaterialCountEpic[0][frameIndex];
                transform.GetChild(2).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionEliteMobItemCode + eliteItemIndex).enName + " x " + ascensionMaterialCountEpic[1][frameIndex];
                transform.GetChild(3).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionMobItemCode + mobItemIndex).enName + " x " + ascensionMaterialCountEpic[2][frameIndex];
            }
            else if (item.grade == Grade.UNIQUE)
            {
                transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionItemCode + itemIndex).enName + " x " + ascensionMaterialCountUnique[0][frameIndex];
                transform.GetChild(2).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionEliteMobItemCode + eliteItemIndex).enName + " x " + ascensionMaterialCountUnique[1][frameIndex];
                transform.GetChild(3).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionMobItemCode + mobItemIndex).enName + " x " + ascensionMaterialCountUnique[2][frameIndex];
            }
            else
            {
                transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionItemCode + itemIndex).enName + " x " + ascensionMaterialCountLegend[0][frameIndex];
                transform.GetChild(2).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionEliteMobItemCode + eliteItemIndex).enName + " x " + ascensionMaterialCountLegend[1][frameIndex];
                transform.GetChild(3).GetChild(1).GetComponent<Text>().text =
                    ItemDatabase.instance.findItemByIndex(item.weapon.ascensionMobItemCode + mobItemIndex).enName + " x " + ascensionMaterialCountLegend[2][frameIndex];
            }
        }
    }

    private void SetAscensionCount()
    {
        ascensionMaterialCountEpic = new string[4][];
        ascensionMaterialCountUnique = new string[4][];
        ascensionMaterialCountLegend = new string[4][];

        for (int i = 0; i < ascensionMaterialCountLegend.Length; i++)
        {
            ascensionMaterialCountEpic[i] = new string[6];
            ascensionMaterialCountUnique[i] = new string[6];
            ascensionMaterialCountLegend[i] = new string[6];
        }

        // 무기 돌파 재료
        ascensionMaterialCountEpic[0][0] = "2";
        ascensionMaterialCountEpic[0][1] = "2";
        ascensionMaterialCountEpic[0][2] = "4";
        ascensionMaterialCountEpic[0][3] = "2";
        ascensionMaterialCountEpic[0][4] = "4";
        ascensionMaterialCountEpic[0][5] = "3";

        // 엘리트 몹 재료
        ascensionMaterialCountEpic[1][0] = "2";
        ascensionMaterialCountEpic[1][1] = "8";
        ascensionMaterialCountEpic[1][2] = "4";
        ascensionMaterialCountEpic[1][3] = "8";
        ascensionMaterialCountEpic[1][4] = "6";
        ascensionMaterialCountEpic[1][5] = "12";

        // 일반 몹 재료
        ascensionMaterialCountEpic[2][0] = "1";
        ascensionMaterialCountEpic[2][1] = "5";
        ascensionMaterialCountEpic[2][2] = "4";
        ascensionMaterialCountEpic[2][3] = "6";
        ascensionMaterialCountEpic[2][4] = "4";
        ascensionMaterialCountEpic[2][5] = "8";

        // 모라
        ascensionMaterialCountEpic[3][0] = "5,000";
        ascensionMaterialCountEpic[3][1] = "10,000";
        ascensionMaterialCountEpic[3][2] = "15,000";
        ascensionMaterialCountEpic[3][3] = "20,000";
        ascensionMaterialCountEpic[3][4] = "25,000";
        ascensionMaterialCountEpic[3][5] = "30,000";


        // 무기 돌파 재료
        ascensionMaterialCountUnique[0][0] = "3";
        ascensionMaterialCountUnique[0][1] = "3";
        ascensionMaterialCountUnique[0][2] = "6";
        ascensionMaterialCountUnique[0][3] = "3";
        ascensionMaterialCountUnique[0][4] = "6";
        ascensionMaterialCountUnique[0][5] = "4";

        // 엘리트 몹 재료
        ascensionMaterialCountUnique[1][0] = "3";
        ascensionMaterialCountUnique[1][1] = "12";
        ascensionMaterialCountUnique[1][2] = "6";
        ascensionMaterialCountUnique[1][3] = "12";
        ascensionMaterialCountUnique[1][4] = "9";
        ascensionMaterialCountUnique[1][5] = "18";

        // 일반 몹 재료
        ascensionMaterialCountUnique[2][0] = "2";
        ascensionMaterialCountUnique[2][1] = "8";
        ascensionMaterialCountUnique[2][2] = "6";
        ascensionMaterialCountUnique[2][3] = "9";
        ascensionMaterialCountUnique[2][4] = "6";
        ascensionMaterialCountUnique[2][5] = "12";

        // 모라
        ascensionMaterialCountUnique[3][0] = "5,000";
        ascensionMaterialCountUnique[3][1] = "15,000";
        ascensionMaterialCountUnique[3][2] = "20,000";
        ascensionMaterialCountUnique[3][3] = "30,000";
        ascensionMaterialCountUnique[3][4] = "35,000";
        ascensionMaterialCountUnique[3][5] = "45,000";



        // 무기 돌파 재료
        ascensionMaterialCountLegend[0][0] = "5";
        ascensionMaterialCountLegend[0][1] = "5";
        ascensionMaterialCountLegend[0][2] = "9";
        ascensionMaterialCountLegend[0][3] = "5";
        ascensionMaterialCountLegend[0][4] = "9";
        ascensionMaterialCountLegend[0][5] = "6";

        // 엘리트 몹 재료
        ascensionMaterialCountLegend[1][0] = "5";
        ascensionMaterialCountLegend[1][1] = "18";
        ascensionMaterialCountLegend[1][2] = "9";
        ascensionMaterialCountLegend[1][3] = "18";
        ascensionMaterialCountLegend[1][4] = "14";
        ascensionMaterialCountLegend[1][5] = "27";

        // 일반 몹 재료
        ascensionMaterialCountLegend[2][0] = "3";
        ascensionMaterialCountLegend[2][1] = "12";
        ascensionMaterialCountLegend[2][2] = "9";
        ascensionMaterialCountLegend[2][3] = "14";
        ascensionMaterialCountLegend[2][4] = "9";
        ascensionMaterialCountLegend[2][5] = "18";

        // 모라
        ascensionMaterialCountLegend[3][0] = "10,000";
        ascensionMaterialCountLegend[3][1] = "20,000";
        ascensionMaterialCountLegend[3][2] = "30,000";
        ascensionMaterialCountLegend[3][3] = "45,000";
        ascensionMaterialCountLegend[3][4] = "55,000";
        ascensionMaterialCountLegend[3][5] = "65,000";
    }
}
