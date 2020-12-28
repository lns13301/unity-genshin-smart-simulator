using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AscensionFrame : MonoBehaviour
{
    public string[][] ascensionMaterialCount;

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

        if (item.type != ItemType.CHARACTER)
        {
            return;
        }

        switch (frameIndex)
        {
            case 0:
                transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                    ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode()).LoadSprite();
                transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite =
                    ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode).LoadSprite();

                if (LanguageManager.instance.language == Language.KOREAN)
                {
                    transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode()).koName + " x " + ascensionMaterialCount[0][frameIndex];
                    transform.GetChild(4).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode).koName + " x " + ascensionMaterialCount[3][frameIndex];
                }
                else
                {
                    transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode()).enName + " x " + ascensionMaterialCount[0][frameIndex];
                    transform.GetChild(4).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode).enName + " x " + ascensionMaterialCount[3][frameIndex];
                }

                break;
            case 1:
                transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                    ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 1).LoadSprite();
                transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite =
                    ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode).LoadSprite();

                if (LanguageManager.instance.language == Language.KOREAN)
                {
                    transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 1).koName + " x " + ascensionMaterialCount[0][frameIndex];
                    transform.GetChild(4).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode).koName + " x " + ascensionMaterialCount[3][frameIndex];
                }
                else
                {
                    transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 1).enName + " x " + ascensionMaterialCount[0][frameIndex];
                    transform.GetChild(4).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode).enName + " x " + ascensionMaterialCount[3][frameIndex];
                }
                break;
            case 2:
                transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                    ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 1).LoadSprite();
                transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite =
                    ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode + 1).LoadSprite();

                if (LanguageManager.instance.language == Language.KOREAN)
                {
                    transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 1).koName + " x " + ascensionMaterialCount[0][frameIndex];
                    transform.GetChild(4).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode + 1).koName + " x " + ascensionMaterialCount[3][frameIndex];
                }
                else
                {
                    transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 1).enName + " x " + ascensionMaterialCount[0][frameIndex];
                    transform.GetChild(4).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode + 1).enName + " x " + ascensionMaterialCount[3][frameIndex];
                }
                break;
            case 3:
                transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                    ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 2).LoadSprite();
                transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite =
                    ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode + 1).LoadSprite();

                if (LanguageManager.instance.language == Language.KOREAN)
                {
                    transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 2).koName + " x " + ascensionMaterialCount[0][frameIndex];
                    transform.GetChild(4).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode + 1).koName + " x " + ascensionMaterialCount[3][frameIndex];
                }
                else
                {
                    transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 2).enName + " x " + ascensionMaterialCount[0][frameIndex];
                    transform.GetChild(4).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode + 1).enName + " x " + ascensionMaterialCount[3][frameIndex];
                }
                break;
            case 4:
                transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                    ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 2).LoadSprite();
                transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite =
                    ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode + 2).LoadSprite();

                if (LanguageManager.instance.language == Language.KOREAN)
                {
                    transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 2).koName + " x " + ascensionMaterialCount[0][frameIndex];
                    transform.GetChild(4).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode + 2).koName + " x " + ascensionMaterialCount[3][frameIndex];
                }
                else
                {
                    transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 2).enName + " x " + ascensionMaterialCount[0][frameIndex];
                    transform.GetChild(4).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode + 2).enName + " x " + ascensionMaterialCount[3][frameIndex];
                }
                break;
            case 5:
                transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                    ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 3).LoadSprite();
                transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite =
                    ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode + 2).LoadSprite();

                if (LanguageManager.instance.language == Language.KOREAN)
                {
                    transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 3).koName + " x " + ascensionMaterialCount[0][frameIndex];
                    transform.GetChild(4).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode + 2).koName + " x " + ascensionMaterialCount[3][frameIndex];
                }
                else
                {
                    transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.GetElementAscensionJewelItemCode() + 3).enName + " x " + ascensionMaterialCount[0][frameIndex];
                    transform.GetChild(4).GetChild(1).GetComponent<Text>().text =
                        ItemDatabase.instance.findItemByIndex(item.character.ascensionMobItemCode + 2).enName + " x " + ascensionMaterialCount[3][frameIndex];
                }
                break;
        }

        transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite =
            ItemDatabase.instance.findItemByIndex(item.GetElementAscensionItemCode()).LoadSprite();
        transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite =
            ItemDatabase.instance.findItemByIndex(item.character.ascensionItemCode).LoadSprite();

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            transform.GetChild(2).GetChild(1).GetComponent<Text>().text =
                ItemDatabase.instance.findItemByIndex(item.GetElementAscensionItemCode()).koName + " x " + ascensionMaterialCount[1][frameIndex];
            transform.GetChild(3).GetChild(1).GetComponent<Text>().text =
                ItemDatabase.instance.findItemByIndex(item.character.ascensionItemCode).koName + " x " + ascensionMaterialCount[2][frameIndex];
        }
        else
        {
            transform.GetChild(2).GetChild(1).GetComponent<Text>().text =
                ItemDatabase.instance.findItemByIndex(item.GetElementAscensionItemCode()).enName + " x " + ascensionMaterialCount[1][frameIndex];
            transform.GetChild(3).GetChild(1).GetComponent<Text>().text =
                ItemDatabase.instance.findItemByIndex(item.character.ascensionItemCode).enName + " x " + ascensionMaterialCount[2][frameIndex];
        }
    }

    private void SetAscensionCount()
    {
        ascensionMaterialCount = new string[5][];

        for (int i = 0; i < ascensionMaterialCount.Length; i++)
        {
            ascensionMaterialCount[i] = new string[6];
        }

        // 속성 보석 개수
        ascensionMaterialCount[0][0] = "1";
        ascensionMaterialCount[0][1] = "3";
        ascensionMaterialCount[0][2] = "6";
        ascensionMaterialCount[0][3] = "3";
        ascensionMaterialCount[0][4] = "6";
        ascensionMaterialCount[0][5] = "6";

        // 속성 상징 개수
        ascensionMaterialCount[1][0] = "0";
        ascensionMaterialCount[1][1] = "2";
        ascensionMaterialCount[1][2] = "4";
        ascensionMaterialCount[1][3] = "8";
        ascensionMaterialCount[1][4] = "12";
        ascensionMaterialCount[1][5] = "20";

        // 특산물
        ascensionMaterialCount[2][0] = "3";
        ascensionMaterialCount[2][1] = "10";
        ascensionMaterialCount[2][2] = "20";
        ascensionMaterialCount[2][3] = "30";
        ascensionMaterialCount[2][4] = "45";
        ascensionMaterialCount[2][5] = "60";

        // 전리품
        ascensionMaterialCount[3][0] = "3";
        ascensionMaterialCount[3][1] = "15";
        ascensionMaterialCount[3][2] = "12";
        ascensionMaterialCount[3][3] = "18";
        ascensionMaterialCount[3][4] = "12";
        ascensionMaterialCount[3][5] = "24";

        // 모라
        ascensionMaterialCount[4][0] = "20,000";
        ascensionMaterialCount[4][1] = "40,000";
        ascensionMaterialCount[4][2] = "60,000";
        ascensionMaterialCount[4][3] = "80,000";
        ascensionMaterialCount[4][4] = "100,000";
        ascensionMaterialCount[4][5] = "120,000";
    }
}
