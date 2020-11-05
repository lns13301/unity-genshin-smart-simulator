using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public ItemDataFile itemDataFile;

    public string spritePathHero = "Images/Character/";
    public string spritePathWeapon = "Images/Weapon/";
    public string effectsPath = "Effects/";

    public Dictionary<int, Item> itemDatas = new Dictionary<int, Item>();

    private void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>();

    public GameObject entityItemPrefab;
    public Vector2[] pos;

    private void Start()
    {
        itemDataFile = new ItemDataFile();
        itemDataFile.itemDatas = new List<Item>();

        //saveItemData();
        loadItemData();

        //spawnItem();

        // 딕셔너리에 아이템 정보 입력
        for (int i = 0; i < itemDB.Count; i++)
        {
            itemDatas.Add(itemDB[i].code, itemDB[i]);
        }
    }

    public int findItemDBPositionByCode(int code)
    {
        for (int i = 0; i < itemDB.Count; i++)
        {
            if (itemDB[i].code == code)
            {
                return i;
            }
        }

        return -1;
    }

    public Item findItemByName(string ko)
    {
        for (int i = 0; i < itemDB.Count; i++)
        {
            if (itemDB[i].koName == ko)
            {
                return itemDB[i];
            }
        }
        return null;
    }

    /*    public Item findItemByCode(int code)
        {
            for (int i = 0; i < itemDB.Count; i++)
            {
                if (itemDB[i].code == code)
                {
                    return itemDB[i];
                }
            }
            return null;
        }*/

    public Item findItemByCode(int code)
    {
        return itemDatas[code];
    }

    public Item pickRandomItem()
    {
        return itemDB[Random.Range(0, itemDB.Count)];
    }

    [ContextMenu("From Json Data")]
    public Sprite loadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    [ContextMenu("To Json Data")]
    public void saveItemData()
    {
        Debug.Log("저장 성공");
        itemDataFile.itemDatas = new List<Item>();

        itemDataFile.itemDatas.Add(new Item(0, "Diluc", "다이루크", 1, ItemType.HERO, Grade.LEGEND, spritePathHero + "Diluc", Element.PYRO));
        itemDataFile.itemDatas.Add(new Item(1, "Keqing", "각청", 1, ItemType.HERO, Grade.LEGEND, spritePathHero + "Keqing", Element.ELECTRO));
        itemDataFile.itemDatas.Add(new Item(2, "QiQi", "치치", 1, ItemType.HERO, Grade.LEGEND, spritePathHero + "QiQi", Element.CRYO));
        itemDataFile.itemDatas.Add(new Item(3, "Jean", "진", 1, ItemType.HERO, Grade.LEGEND, spritePathHero + "Jean", Element.ANEMO));
        itemDataFile.itemDatas.Add(new Item(4, "Mona", "모나", 1, ItemType.HERO, Grade.LEGEND, spritePathHero + "Mona", Element.HYDRO));
        itemDataFile.itemDatas.Add(new Item(50, "Venti", "벤티", 1, ItemType.HERO, Grade.LEGEND, spritePathHero + "Venti", Element.ANEMO));
        itemDataFile.itemDatas.Add(new Item(51, "Klee", "클레", 1, ItemType.HERO, Grade.LEGEND, spritePathHero + "Klee", Element.PYRO));
        itemDataFile.itemDatas.Add(new Item(52, "Xiao", "소", 1, ItemType.HERO, Grade.LEGEND, spritePathHero + "Xiao", Element.ANEMO));
/*        itemDataFile.itemDatas.Add(new Item(53, "Zhongli", "종려", 1, ItemType.HERO, Grade.LEGEND, spritePathHero + "Zhongli", Element.GEO));
        itemDataFile.itemDatas.Add(new Item(54, "Tartaglia", "타르탈리안", 1, ItemType.HERO, Grade.LEGEND, spritePathHero + "Tartaglia", Element.HYDRO));*/
        itemDataFile.itemDatas.Add(new Item(101, "Fischl", "피슬", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Fischl", Element.ELECTRO));
        itemDataFile.itemDatas.Add(new Item(102, "Razor", "레이저", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Razor", Element.ELECTRO));
        itemDataFile.itemDatas.Add(new Item(103, "Xiangling", "향릉", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Xiangling", Element.PYRO));
        itemDataFile.itemDatas.Add(new Item(104, "Barbara", "바바라", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Barbara", Element.HYDRO));
        itemDataFile.itemDatas.Add(new Item(105, "Bennett", "베넷", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Bennett", Element.PYRO));
        itemDataFile.itemDatas.Add(new Item(106, "Chongyun", "중운", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Chongyun", Element.CRYO));
        itemDataFile.itemDatas.Add(new Item(107, "Xingqiu", "행추", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Xingqiu", Element.HYDRO));
        itemDataFile.itemDatas.Add(new Item(108, "Beidou", "북두", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Beidou", Element.ELECTRO));
        itemDataFile.itemDatas.Add(new Item(109, "Kaeya", "케이야", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Kaeya", Element.CRYO));
        itemDataFile.itemDatas.Add(new Item(110, "Lisa", "리사", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Lisa", Element.ELECTRO));
        itemDataFile.itemDatas.Add(new Item(111, "Ningguang", "응광", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Ningguang", Element.GEO));
        itemDataFile.itemDatas.Add(new Item(112, "Noelle", "노엘", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Noelle", Element.GEO));
        itemDataFile.itemDatas.Add(new Item(113, "Sucrose", "설탕", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Sucrose", Element.ANEMO));
        itemDataFile.itemDatas.Add(new Item(114, "Amber", "엠버", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Amber", Element.PYRO));
        /*        itemDataFile.itemDatas.Add(new Item(115, "Diona", "디오나", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Noelle", Element.CRYO));
                itemDataFile.itemDatas.Add(new Item(116, "Xinyan", "신염", 1, ItemType.HERO, Grade.UNIQUE, spritePathHero + "Noelle", Element.PYRO));*/


        itemDataFile.itemDatas.Add(new Item(1000, "Aquila Favonia", "매의 검", 1, ItemType.SWORD, Grade.LEGEND, spritePathWeapon + "Aquila Favonia"));
        itemDataFile.itemDatas.Add(new Item(1001, "Skyward Blade", "천공의 검", 1, ItemType.SWORD, Grade.LEGEND, spritePathWeapon + "Skyward Blade"));

        itemDataFile.itemDatas.Add(new Item(1100, "Wolf's Gravestone", "늑대의 말로", 1, ItemType.CLAYMORE, Grade.LEGEND, spritePathWeapon + "Wolf's Gravestone"));
        itemDataFile.itemDatas.Add(new Item(1101, "Skyward Pride", "천공의 대검", 1, ItemType.CLAYMORE, Grade.LEGEND, spritePathWeapon + "Skyward Pride"));

        itemDataFile.itemDatas.Add(new Item(1200, "Amos's Bow", "아모스의 활", 1, ItemType.BOW, Grade.LEGEND, spritePathWeapon + "Amos's Bow"));
        itemDataFile.itemDatas.Add(new Item(1201, "Skyward Harp", "천공의 하프", 1, ItemType.BOW, Grade.LEGEND, spritePathWeapon + "Skyward Harp"));

        itemDataFile.itemDatas.Add(new Item(1300, "Lost Prayer to the Sacred Winds", "사풍 원서", 1, ItemType.CATALYST, Grade.LEGEND, spritePathWeapon + "Lost Prayer to the Sacred Winds"));
        itemDataFile.itemDatas.Add(new Item(1301, "Skyward Atlas", "천공의 두루마리", 1, ItemType.CATALYST, Grade.LEGEND, spritePathWeapon + "Skyward Atlas"));

        itemDataFile.itemDatas.Add(new Item(1400, "Primordial Jade Winged Spear", "화박연", 1, ItemType.POLEARM, Grade.LEGEND, spritePathWeapon + "Primordial Jade Winged Spear"));
        itemDataFile.itemDatas.Add(new Item(1401, "Skyward Spine", "천공의 마루", 1, ItemType.POLEARM, Grade.LEGEND, spritePathWeapon + "Skyward Spine"));        

        ///////////////////////////////////////////////////////////////////////////////////////////////

        itemDataFile.itemDatas.Add(new Item(2000, "Favonius Sword", "페보니우스 검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Favonius Sword"));
        itemDataFile.itemDatas.Add(new Item(2001, "Lion's Roar", "용의 포효", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Lion's Roar"));
        itemDataFile.itemDatas.Add(new Item(2002, "Sacrificial Sword", "제례검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Sacrificial Sword"));
        itemDataFile.itemDatas.Add(new Item(2003, "The Flute", "피리검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "The Flute"));
        //itemDataFile.itemDatas.Add(new Item(2004, "Iron Sting", "강철 벌침", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Iron Sting"));
        //itemDataFile.itemDatas.Add(new Item(2005, "Prototype Rancour", "참암 프로토타입", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Prototype Rancour"));
        //itemDataFile.itemDatas.Add(new Item(2006, "Royal Longsword", "왕실의 장검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Royal Longsword"));
        //itemDataFile.itemDatas.Add(new Item(2007, "Blackcliff Longsword", "흑암 장검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Blackcliff Longsword"));

        itemDataFile.itemDatas.Add(new Item(2100, "Favonius Greatsword", "페보니우스 대검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Favonius Greatsword"));
        itemDataFile.itemDatas.Add(new Item(2101, "Rainslasher", "빗물 베기", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Rainslasher"));
        itemDataFile.itemDatas.Add(new Item(2102, "Sacrificial Greatsword", "제례 대검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Sacrificial Greatsword"));
        itemDataFile.itemDatas.Add(new Item(2103, "The Bell", "시간의 검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "The Bell"));
        //itemDataFile.itemDatas.Add(new Item(2104, "Serpent Spine", "이무기 검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Serpent Spine"));
        //itemDataFile.itemDatas.Add(new Item(2105, "Whiteblind", "백영검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Whiteblind"));
        //itemDataFile.itemDatas.Add(new Item(2106, "Blackcliff Slasher", "흑암참도", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Bloodtainted Greatsword"));
        //itemDataFile.itemDatas.Add(new Item(2107, "Prototype Aminus", "고화 프로토타입", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Prototype Aminus"));
        //itemDataFile.itemDatas.Add(new Item(2108, "Royal Greatsword", "왕실의 대검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Royal Greatsword"));

        itemDataFile.itemDatas.Add(new Item(2200, "Favonius Warbow", "페보니우스 활", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Favonius Warbow"));
        itemDataFile.itemDatas.Add(new Item(2201, "Rust", "녹슨 활", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Rust"));
        itemDataFile.itemDatas.Add(new Item(2202, "Sacrificial Bow", "제례활", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Sacrificial Bow"));
        itemDataFile.itemDatas.Add(new Item(2203, "The Stringless", "절현", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "The Stringless"));
        //itemDataFile.itemDatas.Add(new Item(2204, "The Viridescent Hunt", "청록색 활", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "The Viridescent Hunt"));
        //itemDataFile.itemDatas.Add(new Item(2205, "Compound Bow", "강철궁", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Compound Bow"));
        //itemDataFile.itemDatas.Add(new Item(2206, "Blackcliff Warbow", "흑암 배틀 보우", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Blackcliff Warbow"));
        //itemDataFile.itemDatas.Add(new Item(2207, "Prototype Crescent", "담월 프로토타입", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Prototype Crescent"));
        //itemDataFile.itemDatas.Add(new Item(2208, "Royal Bow", "왕실의 장궁", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Royal Bow"));

        itemDataFile.itemDatas.Add(new Item(2300, "Eye of Perception", "소심", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Eye of Perception"));
        itemDataFile.itemDatas.Add(new Item(2301, "Favonius Codex", "페보니우스 비전", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Favonius Codex"));
        itemDataFile.itemDatas.Add(new Item(2302, "Sacrificial Fragments", "제례의 악장", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Sacrificial Fragments"));
        itemDataFile.itemDatas.Add(new Item(2303, "The Widsith", "음유시인의 악장", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "The Widsith"));
        //itemDataFile.itemDatas.Add(new Item(2304, "Mappa Mare", "만국 항해용해도", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Mappa Mare"));
        //itemDataFile.itemDatas.Add(new Item(2305, "Solar Pearl", "일월의 정수", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Solar Pearl"));
        //itemDataFile.itemDatas.Add(new Item(2306, "Blackcliff Amulet", "흑암 홍옥", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Blackcliff Amulet"));
        //itemDataFile.itemDatas.Add(new Item(2307, "Prototype Malice", "황금 호박 프로토타입", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Prototype Malice"));
        //itemDataFile.itemDatas.Add(new Item(2308, "Royal Grimoire", "왕실의 비전록", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Royal Grimoire"));

        itemDataFile.itemDatas.Add(new Item(2401, "Dragon's Bane", "용학살창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Dragon's Bane"));
        itemDataFile.itemDatas.Add(new Item(2402, "Favonius Lance", "페보니우스 장창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Favonius Lance"));
        //itemDataFile.itemDatas.Add(new Item(2403, "Deathmatch", "결투의 창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Deathmatch"));
        //itemDataFile.itemDatas.Add(new Item(2403, "Crescent Pike", "유월창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Crescent Pike"));
        //itemDataFile.itemDatas.Add(new Item(2404, "Prototype Grudge", "별의 낫 프로토타입", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Prototype Grudge"));
        //itemDataFile.itemDatas.Add(new Item(2405, "Blackcliff Pole", "흑암창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Blackcliff Pole"));

        ///////////////////////////////////////////////////////////////////////////////////////////////

        itemDataFile.itemDatas.Add(new Item(3000, "Cool Steel", "차가운 칼날", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Cool Steel"));
        itemDataFile.itemDatas.Add(new Item(3001, "Dark Iron Sword", "암철검", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Dark Iron Sword"));
        itemDataFile.itemDatas.Add(new Item(3002, "Fillet Blade", "흘호 생선회칼", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Fillet Blade"));
        itemDataFile.itemDatas.Add(new Item(3003, "Harbinger of Dawn", "여명신검", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Harbinger of Dawn"));
        itemDataFile.itemDatas.Add(new Item(3004, "Skyrider Sword", "비천어검", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Skyrider Sword"));
        itemDataFile.itemDatas.Add(new Item(3005, "Traveler's Handy Sword", "여행자의 검", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Traveler's Handy Sword"));

        itemDataFile.itemDatas.Add(new Item(3100, "Bloodtainted Greatsword", "드래곤 블러드 소드", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "Bloodtainted Greatsword"));
        itemDataFile.itemDatas.Add(new Item(3101, "Debate Club", "훌륭한 대화수단", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "Debate Club"));
        itemDataFile.itemDatas.Add(new Item(3102, "Ferrous Shadow", "강철의 그림자", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "Ferrous Shadow"));
        itemDataFile.itemDatas.Add(new Item(3103, "Skyrider Greatsword", "비천대어검", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "Skyrider Greatsword"));
        itemDataFile.itemDatas.Add(new Item(3104, "White Iron Greatsword", "백철 대검", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "White Iron Greatsword"));

        itemDataFile.itemDatas.Add(new Item(3200, "Messenger", "전령", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Messenger"));
        itemDataFile.itemDatas.Add(new Item(3201, "Raven Bow", "까마귀깃 활", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Raven Bow"));
        itemDataFile.itemDatas.Add(new Item(3202, "Recurve Bow", "곡궁", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Recurve Bow"));
        itemDataFile.itemDatas.Add(new Item(3203, "Sharpshooter's Oath", "신궁의 서약", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Sharpshooter's Oath"));
        itemDataFile.itemDatas.Add(new Item(3204, "Slingshot", "탄궁", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Slingshot"));
        itemDataFile.itemDatas.Add(new Item(3205, "Messenger", "전령", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Messenger"));

        itemDataFile.itemDatas.Add(new Item(3300, "Emerald Orb", "비취 오브", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Emerald Orb"));
        itemDataFile.itemDatas.Add(new Item(3301, "Magic Guide", "마도 서론", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Magic Guide"));
        itemDataFile.itemDatas.Add(new Item(3302, "Otherworldly Story", "이세계 여행기", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Otherworldly Story"));
        itemDataFile.itemDatas.Add(new Item(3303, "Thrilling Tales of Dragon Slayers", "드래곤 슬레이어 영웅담", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Thrilling Tales of Dragon Slayers"));
        itemDataFile.itemDatas.Add(new Item(3304, "Twin Nephrite", "1급 보옥", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Twin Nephrite"));

        itemDataFile.itemDatas.Add(new Item(3400, "Black Tassel", "흑술창", 1, ItemType.POLEARM, Grade.EPIC, spritePathWeapon + "Black Tassel"));
        itemDataFile.itemDatas.Add(new Item(3401, "Halberd", "미늘창", 1, ItemType.POLEARM, Grade.EPIC, spritePathWeapon + "Halberd"));
        itemDataFile.itemDatas.Add(new Item(3402, "White Tassel", "백술창", 1, ItemType.POLEARM, Grade.EPIC, spritePathWeapon + "White Tassel"));

        string jsonData = JsonUtility.ToJson(itemDataFile, true);

        File.WriteAllText(saveOrLoad(false, true, "ItemData"), jsonData);
    }

    [ContextMenu("From Json Data")]
    public void loadItemData()
    {
        try
        {
            Debug.Log("아이템 정보 로드 성공");
            itemDataFile = JsonUtility.FromJson<ItemDataFile>(Resources.Load<TextAsset>("ItemData").ToString());

            for (int i = 0; i < itemDataFile.itemDatas.Count; i++)
            {
                itemDataFile.itemDatas[i].sprite = loadSprite(itemDataFile.itemDatas[i].spritePath);
                itemDB.Add(itemDataFile.itemDatas[i]);
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("로드 오류");

            string jsonData = JsonUtility.ToJson(itemDataFile, true);

            File.WriteAllText(saveOrLoad(false, false, "ItemData"), jsonData);
            loadItemData();
        }
    }

    public string saveOrLoad(bool isMobile, bool isSave, string fileName)
    {
        if (isSave)
        {
            if (isMobile)
            {
                // 모바일 저장
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 저장
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
        else
        {
            if (isMobile)
            {
                // 모바일 로드
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 로드
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
    }

    public Item makeItem(Item item)
    {
        return new Item(item.code, item.enName, item.koName, item.count, item.type, item.grade, item.spritePath, item.element);
    }
}

[System.Serializable]
public class ItemDataFile
{
    public List<Item> itemDatas;
}
