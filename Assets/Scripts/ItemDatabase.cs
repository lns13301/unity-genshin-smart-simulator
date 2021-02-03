using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public ItemDataFile itemDataFile;

    private string spritePathHero = "Images/Character/";
    private string spritePathWeapon = "Images/Weapon/";
    private string effectsPath = "Effects/";
    private string spritePathTalent = "Images/UI/Stat/TalentItem/";
    private string spritePathWeaponTalent = "Images/UI/Stat/WeaponItem/";

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

    public Item findItemByCode(int code)
    {
        for (int i = 0; i < itemDB.Count; i++)
        {
            if (itemDB[i].code == code)
            {
                return itemDB[i];
            }
        }
        return null;
    }

    public Item findItemByIndex(int code)
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

        itemDataFile.itemDatas.Add(new Item(0, "Amber", "엠버", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Amber", new Character(0, Element.PYRO, Grade.UNIQUE, 1000, 10408, 10310, 10000, 10702)));
        itemDataFile.itemDatas.Add(new Item(1, "Kaeya", "케이아", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Kaeya", new Character(1, Element.CRYO, Grade.UNIQUE, 1048, 10400, 10325, 10008, 10704)));
        itemDataFile.itemDatas.Add(new Item(2, "Lisa", "리사", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Lisa", new Character(2, Element.ELECTRO, Grade.UNIQUE, 1066, 10409, 10315, 10008, 10700)));
        itemDataFile.itemDatas.Add(new Item(3, "Barbara", "바바라", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Barbara", new Character(3, Element.HYDRO, Grade.UNIQUE, 1006, 10406, 10305, 10000, 10703)));
        itemDataFile.itemDatas.Add(new Item(4, "Xiangling", "향릉", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Xiangling", new Character(4, Element.PYRO, Grade.UNIQUE, 1114, 10404, 10315, 10016, 10700)));
        itemDataFile.itemDatas.Add(new Item(5, "Razor", "레이저", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Razor", new Character(5, Element.ELECTRO, Grade.UNIQUE, 1096, 10412, 10300, 10004, 10700)));
        itemDataFile.itemDatas.Add(new Item(6, "Bennett", "베넷", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Bennett", new Character(6, Element.PYRO, Grade.UNIQUE, 1018, 10411, 10325, 10004, 10701)));
        itemDataFile.itemDatas.Add(new Item(7, "Xingqiu", "행추", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Xingqiu", new Character(7, Element.HYDRO, Grade.UNIQUE, 1120, 10407, 10300, 10020, 10705)));
        itemDataFile.itemDatas.Add(new Item(8, "Beidou", "북두", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Beidou", new Character(8, Element.ELECTRO, Grade.UNIQUE, 1012, 10405, 10325, 10020, 10702)));
        itemDataFile.itemDatas.Add(new Item(9, "Sucrose", "설탕", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Sucrose", new Character(9, Element.ANEMO, Grade.UNIQUE, 1102, 10411, 10320, 10000, 10704)));
        itemDataFile.itemDatas.Add(new Item(10, "Ningguang", "응광", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Ningguang", new Character(10, Element.GEO, Grade.UNIQUE, 1078, 10403, 10330, 10012, 10704)));
        itemDataFile.itemDatas.Add(new Item(11, "Noelle", "노엘", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Noelle", new Character(11, Element.GEO, Grade.UNIQUE, 1084, 10409, 10300, 10004, 10700)));
        itemDataFile.itemDatas.Add(new Item(12, "Fischl", "피슬", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Fischl", new Character(12, Element.ELECTRO, Grade.UNIQUE, 1036, 10408, 10310, 10008, 10704)));
        itemDataFile.itemDatas.Add(new Item(13, "Chongyun", "중운", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Chongyun", new Character(13, Element.CRYO, Grade.UNIQUE, 1024, 10402, 10300, 10016, 10702)));
        itemDataFile.itemDatas.Add(new Item(14, "Diona", "디오나", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Diona", new Character(14, Element.CRYO, Grade.UNIQUE, 1132, 10400, 10310, 10000, 10707)));
        itemDataFile.itemDatas.Add(new Item(15, "Xinyan", "신염", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Xinyan", new Character(15, Element.PYRO, Grade.UNIQUE, 1138, 10410, 10325, 10020, 10708)));

        itemDataFile.itemDatas.Add(new Item(500, "Diluc", "다이루크", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Diluc", new Character(500, Element.PYRO, Grade.LEGEND, 1030, 10408, 10330, 10004, 10701)));
        itemDataFile.itemDatas.Add(new Item(501, "Mona", "모나", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Mona", new Character(501, Element.HYDRO, Grade.LEGEND, 1072, 10406, 10320, 10004, 10703)));
        itemDataFile.itemDatas.Add(new Item(502, "Keqing", "각청", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Keqing", new Character(502, Element.ELECTRO, Grade.LEGEND, 1054, 10402, 10320, 10012, 10703)));
        itemDataFile.itemDatas.Add(new Item(503, "QiQi", "치치", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "QiQi", new Character(503, Element.CRYO, Grade.LEGEND, 1090, 10410, 10305, 10012, 10705)));
        itemDataFile.itemDatas.Add(new Item(504, "Jean", "진", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Jean", new Character(504, Element.ANEMO, Grade.LEGEND, 1042, 10413, 10300, 10004, 10701)));

        itemDataFile.itemDatas.Add(new Item(700, "Venti", "벤티", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Venti", new Character(700, Element.ANEMO, Grade.LEGEND, 1108, 10401, 10315, 10008, 10705)));
        itemDataFile.itemDatas.Add(new Item(701, "Klee", "클레", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Klee", new Character(701, Element.PYRO, Grade.LEGEND, 1060, 10406, 10305, 10000, 10703)));
        itemDataFile.itemDatas.Add(new Item(702, "Tartaglia", "타르탈리아", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Tartaglia", new Character(702, Element.HYDRO, Grade.LEGEND, 1126, 10414, 10330, 10000, 10707)));
        itemDataFile.itemDatas.Add(new Item(703, "Zhongli", "종려", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Zhongli", new Character(703, Element.GEO, Grade.LEGEND, 1144, 10402, 10315, 10020, 10708)));
        itemDataFile.itemDatas.Add(new Item(704, "Albedo", "알베도", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Albedo", new Character(704, Element.GEO, Grade.LEGEND, 1150, 10401, 10305, 10008, 10708)));
        itemDataFile.itemDatas.Add(new Item(705, "Ganyu", "감우", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Ganyu", new Character(705, Element.CRYO, Grade.LEGEND, 1156, 10415, 10320, 10016, 10706)));
        itemDataFile.itemDatas.Add(new Item(706, "Xiao", "소", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Xiao", new Character(706, Element.ANEMO, Grade.LEGEND, 1162, 10415, 10310, 10012, 10706, AscensionType.JUVENILE_JADE)));

        itemDataFile.itemDatas.Add(new Item(1000, "Cool Steel", "차가운 칼날", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Cool Steel", null, new Weapon(10500, 10620, 10310)));
        itemDataFile.itemDatas.Add(new Item(1001, "Dark Iron Sword", "암철검", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Dark Iron Sword", null, new Weapon(10512, 10608, 10300)));
        itemDataFile.itemDatas.Add(new Item(1002, "Fillet Blade", "흘호 생선회칼", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Fillet Blade", null, new Weapon(10516, 10612, 10325)));
        itemDataFile.itemDatas.Add(new Item(1003, "Harbinger of Dawn", "여명신검", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Harbinger of Dawn", null, new Weapon(10504, 10600, 10315)));
        itemDataFile.itemDatas.Add(new Item(1004, "Skyrider Sword", "비천어검", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Skyrider Sword", null, new Weapon(10520, 10604, 10330)));
        //itemDataFile.itemDatas.Add(new Item(1005, "Traveler's Handy Sword", "여행자의 검", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Traveler's Handy Sword"));

        itemDataFile.itemDatas.Add(new Item(1100, "Bloodtainted Greatsword", "드래곤 블러드 소드", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "Bloodtainted Greatsword", null, new Weapon(10504, 10600, 10310)));
        itemDataFile.itemDatas.Add(new Item(1101, "Debate Club", "훌륭한 대화수단", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "Debate Club", null, new Weapon(10516, 10612, 10300)));
        itemDataFile.itemDatas.Add(new Item(1102, "Ferrous Shadow", "강철의 그림자", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "Ferrous Shadow", null, new Weapon(10500, 10620, 10320)));
        //itemDataFile.itemDatas.Add(new Item(1103, "Skyrider Greatsword", "비천대어검", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "Skyrider Greatsword"));
        itemDataFile.itemDatas.Add(new Item(1104, "White Iron Greatsword", "백철 대검", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "White Iron Greatsword", null, new Weapon(10508, 10616, 10315)));

        itemDataFile.itemDatas.Add(new Item(1200, "Black Tassel", "흑술창", 1, ItemType.POLEARM, Grade.EPIC, spritePathWeapon + "Black Tassel", null, new Weapon(10520, 10604, 10310)));
        itemDataFile.itemDatas.Add(new Item(1201, "Halberd", "미늘창", 1, ItemType.POLEARM, Grade.EPIC, spritePathWeapon + "Halberd", null, new Weapon(10516, 10612, 10320)));
        itemDataFile.itemDatas.Add(new Item(1202, "White Tassel", "백술창", 1, ItemType.POLEARM, Grade.EPIC, spritePathWeapon + "White Tassel", null, new Weapon(10512, 10608, 10330)));

        itemDataFile.itemDatas.Add(new Item(1300, "Emerald Orb", "비취 오브", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Emerald Orb", null, new Weapon(10512, 10608, 10325)));
        itemDataFile.itemDatas.Add(new Item(1301, "Magic Guide", "마도 서론", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Magic Guide", null, new Weapon(10500, 10620, 10310)));
        itemDataFile.itemDatas.Add(new Item(1302, "Otherworldly Story", "이세계 여행기", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Otherworldly Story", null, new Weapon(10500, 10620, 10315)));
        itemDataFile.itemDatas.Add(new Item(1303, "Thrilling Tales of Dragon Slayers", "드래곤 슬레이어 영웅담", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Thrilling Tales of Dragon Slayers", null, new Weapon(10504, 10600, 10305)));
        itemDataFile.itemDatas.Add(new Item(1304, "Twin Nephrite", "1급 보옥", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Twin Nephrite", null, new Weapon(10516, 10612, 10330)));

        itemDataFile.itemDatas.Add(new Item(1400, "Messenger", "전령", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Messenger", null, new Weapon(10516, 10612, 10325)));
        itemDataFile.itemDatas.Add(new Item(1401, "Raven Bow", "까마귀깃 활", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Raven Bow", null, new Weapon(10500, 10620, 10310)));
        itemDataFile.itemDatas.Add(new Item(1402, "Recurve Bow", "곡궁", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Recurve Bow", null, new Weapon(10508, 10616, 10305)));
        itemDataFile.itemDatas.Add(new Item(1403, "Sharpshooter's Oath", "신궁의 서약", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Sharpshooter's Oath", null, new Weapon(10504, 10600, 10315)));
        itemDataFile.itemDatas.Add(new Item(1404, "Slingshot", "탄궁", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Slingshot", null, new Weapon(10512, 10608, 10300)));

        ///////////////////////////////////////////////////////////////////////////////////////////////

        itemDataFile.itemDatas.Add(new Item(2000, "Favonius Sword", "페보니우스 검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Favonius Sword", null, new Weapon(10500, 10620, 10310)));
        itemDataFile.itemDatas.Add(new Item(2001, "Sacrificial Sword", "제례검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Sacrificial Sword", null, new Weapon(10508, 10616, 10305)));
        itemDataFile.itemDatas.Add(new Item(2002, "Lion's Roar", "용의 포효", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Lion's Roar", null, new Weapon(10512, 10608, 10325)));
        itemDataFile.itemDatas.Add(new Item(2003, "The Flute", "피리검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "The Flute", null, new Weapon(10504, 10600, 10315)));
        //itemDataFile.itemDatas.Add(new Item(2004, "Iron Sting", "강철 벌침", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Iron Sting", null, new Weapon(10520, 10604, 10320)));
        //itemDataFile.itemDatas.Add(new Item(2005, "Prototype Rancour", "참암 프로토타입", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Prototype Rancour"));
        //itemDataFile.itemDatas.Add(new Item(2006, "Royal Longsword", "왕실의 장검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Royal Longsword"));
        //itemDataFile.itemDatas.Add(new Item(2007, "Blackcliff Longsword", "흑암 장검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Blackcliff Longsword"));

        itemDataFile.itemDatas.Add(new Item(2100, "Favonius Greatsword", "페보니우스 대검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Favonius Greatsword", null, new Weapon(10508, 10616, 10330)));
        itemDataFile.itemDatas.Add(new Item(2101, "Sacrificial Greatsword", "제례 대검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Sacrificial Greatsword", null, new Weapon(10504, 10600, 10310)));
        itemDataFile.itemDatas.Add(new Item(2102, "Rainslasher", "빗물 베기", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Rainslasher", null, new Weapon(10516, 10612, 10306)));
        itemDataFile.itemDatas.Add(new Item(2103, "The Bell", "시간의 검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "The Bell", null, new Weapon(10500, 10620, 10320)));
        //itemDataFile.itemDatas.Add(new Item(2104, "Serpent Spine", "이무기 검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Serpent Spine"));
        //itemDataFile.itemDatas.Add(new Item(2105, "Whiteblind", "백영검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Whiteblind"));
        //itemDataFile.itemDatas.Add(new Item(2106, "Blackcliff Slasher", "흑암참도", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Bloodtainted Greatsword"));
        //itemDataFile.itemDatas.Add(new Item(2107, "Prototype Aminus", "고화 프로토타입", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Prototype Aminus"));
        //itemDataFile.itemDatas.Add(new Item(2108, "Royal Greatsword", "왕실의 대검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Royal Greatsword"));

        itemDataFile.itemDatas.Add(new Item(2200, "Favonius Lance", "페보니우스 장창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Favonius Lance", null, new Weapon(10508, 10616, 10315)));
        itemDataFile.itemDatas.Add(new Item(2201, "Dragon's Bane", "용학살창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Dragon's Bane", null, new Weapon(10516, 10612, 10305)));
        //itemDataFile.itemDatas.Add(new Item(2202, "Deathmatch", "결투의 창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Deathmatch"));
        //itemDataFile.itemDatas.Add(new Item(2203, "Crescent Pike", "유월창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Crescent Pike"));
        //itemDataFile.itemDatas.Add(new Item(2204, "Prototype Grudge", "별의 낫 프로토타입", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Prototype Grudge"));
        //itemDataFile.itemDatas.Add(new Item(2205, "Blackcliff Pole", "흑암창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Blackcliff Pole"));

        itemDataFile.itemDatas.Add(new Item(2300, "Favonius Codex", "페보니우스 비전", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Favonius Codex", null, new Weapon(10500, 10620, 10305)));
        itemDataFile.itemDatas.Add(new Item(2301, "Sacrificial Fragments", "제례의 악장", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Sacrificial Fragments", null, new Weapon(10508, 10616, 10325)));
        itemDataFile.itemDatas.Add(new Item(2302, "Eye of Perception", "소심", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Eye of Perception", null, new Weapon(10516, 10612, 10300)));
        itemDataFile.itemDatas.Add(new Item(2303, "The Widsith", "음유시인의 악장", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "The Widsith", null, new Weapon(10504, 10600, 10300)));
        //itemDataFile.itemDatas.Add(new Item(2304, "Mappa Mare", "만국 항해용해도", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Mappa Mare"));
        //itemDataFile.itemDatas.Add(new Item(2305, "Solar Pearl", "일월의 정수", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Solar Pearl"));
        //itemDataFile.itemDatas.Add(new Item(2306, "Blackcliff Amulet", "흑암 홍옥", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Blackcliff Amulet"));
        //itemDataFile.itemDatas.Add(new Item(2307, "Prototype Malice", "황금 호박 프로토타입", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Prototype Malice"));
        //itemDataFile.itemDatas.Add(new Item(2308, "Royal Grimoire", "왕실의 비전록", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Royal Grimoire"));

        itemDataFile.itemDatas.Add(new Item(2400, "Favonius Warbow", "페보니우스 활", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Favonius Warbow", null, new Weapon(10508, 10616, 10320)));
        itemDataFile.itemDatas.Add(new Item(2401, "Sacrificial Bow", "제례활", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Sacrificial Bow", null, new Weapon(10504, 10600, 10315)));
        itemDataFile.itemDatas.Add(new Item(2402, "Rust", "녹슨 활", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Rust", null, new Weapon(10512, 10608, 10300)));
        itemDataFile.itemDatas.Add(new Item(2403, "The Stringless", "절현", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "The Stringless", null, new Weapon(10500, 10620, 10310)));
        //itemDataFile.itemDatas.Add(new Item(2404, "The Viridescent Hunt", "청록색 활", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "The Viridescent Hunt"));
        //itemDataFile.itemDatas.Add(new Item(2405, "Compound Bow", "강철궁", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Compound Bow"));
        //itemDataFile.itemDatas.Add(new Item(2406, "Blackcliff Warbow", "흑암 배틀 보우", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Blackcliff Warbow"));
        //itemDataFile.itemDatas.Add(new Item(2407, "Prototype Crescent", "담월 프로토타입", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Prototype Crescent"));
        //itemDataFile.itemDatas.Add(new Item(2408, "Royal Bow", "왕실의 장궁", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Royal Bow"));

        ///////////////////////////////////////////////////////////////////////////////////////////////

        itemDataFile.itemDatas.Add(new Item(3000, "Aquila Favonia", "매의 검", 1, ItemType.SWORD, Grade.LEGEND, spritePathWeapon + "Aquila Favonia", null, new Weapon(10500, 10620, 10310)));
        itemDataFile.itemDatas.Add(new Item(3001, "Skyward Blade", "천공의 검", 1, ItemType.SWORD, Grade.LEGEND, spritePathWeapon + "Skyward Blade", null, new Weapon(10504, 10600, 10315)));

        itemDataFile.itemDatas.Add(new Item(3100, "Wolf's Gravestone", "늑대의 말로", 1, ItemType.CLAYMORE, Grade.LEGEND, spritePathWeapon + "Wolf's Gravestone", null, new Weapon(10508, 10616, 10305)));
        itemDataFile.itemDatas.Add(new Item(3101, "Skyward Pride", "천공의 긍지", 1, ItemType.CLAYMORE, Grade.LEGEND, spritePathWeapon + "Skyward Pride", null, new Weapon(10504, 10600, 10315)));

        itemDataFile.itemDatas.Add(new Item(3200, "Primordial Jade Winged Spear", "화박연", 1, ItemType.POLEARM, Grade.LEGEND, spritePathWeapon + "Primordial Jade Winged Spear", null, new Weapon(10512, 10608, 10330)));
        itemDataFile.itemDatas.Add(new Item(3201, "Skyward Spine", "천공의 마루", 1, ItemType.POLEARM, Grade.LEGEND, spritePathWeapon + "Skyward Spine", null, new Weapon(10508, 10616, 10305)));

        itemDataFile.itemDatas.Add(new Item(3300, "Lost Prayer to the Sacred Winds", "사풍 원서", 1, ItemType.CATALYST, Grade.LEGEND, spritePathWeapon + "Lost Prayer to the Sacred Winds", null, new Weapon(10508, 10616, 10315)));
        itemDataFile.itemDatas.Add(new Item(3301, "Skyward Atlas", "천공의 두루마리", 1, ItemType.CATALYST, Grade.LEGEND, spritePathWeapon + "Skyward Atlas", null, new Weapon(10504, 10600, 10310)));

        itemDataFile.itemDatas.Add(new Item(3400, "Amos's Bow", "아모스의 활", 1, ItemType.BOW, Grade.LEGEND, spritePathWeapon + "Amos's Bow", null, new Weapon(10508, 10616, 10315)));
        itemDataFile.itemDatas.Add(new Item(3401, "Skyward Harp", "천공의 날개", 1, ItemType.BOW, Grade.LEGEND, spritePathWeapon + "Skyward Harp", null, new Weapon(10504, 10600, 10310)));

        /////////////////////////////////////////////////////////////////////////////////////////////

        itemDataFile.itemDatas.Add(new Item(4300, "Memory of Dust", "속세의 자물쇠", 1, ItemType.CATALYST, Grade.LEGEND, spritePathWeapon + "Memory of Dust", null, new Weapon(10520, 10604, 10300)));
        itemDataFile.itemDatas.Add(new Item(4100, "The Unforged", "무공의 검", 1, ItemType.CLAYMORE, Grade.LEGEND, spritePathWeapon + "The Unforged", null, new Weapon(10516, 10612, 10325)));
        itemDataFile.itemDatas.Add(new Item(4200, "Vortex Vanquisher", "관홍의 창", 1, ItemType.POLEARM, Grade.LEGEND, spritePathWeapon + "Vortex Vanquisher", null, new Weapon(10520, 10604, 10330)));
        itemDataFile.itemDatas.Add(new Item(4000, "Summit Shaper", "참봉의 칼날", 1, ItemType.SWORD, Grade.LEGEND, spritePathWeapon + "Summit Shaper", null, new Weapon(10512, 10608, 10300)));
        itemDataFile.itemDatas.Add(new Item(4001, "Primordia lJade Cutter", "반암결록", 1, ItemType.SWORD, Grade.LEGEND, spritePathWeapon + "Primordia lJade Cutter", null, new Weapon(10516, 10612, 10325)));

        /////////////////////////////////////////////////////////////////////////////////////////////

        itemDataFile.itemDatas.Add(new Item(10000, "Teachings of \"Freedom\"", "\"자유\"의 가르침", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "teaching_of_freedom"));
        itemDataFile.itemDatas.Add(new Item(10001, "Guide to \"Freedom\"", "\"자유\"의 인도", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "guide_to_freedom"));
        itemDataFile.itemDatas.Add(new Item(10002, "Philosophies of \"Freedom\"", "\"자유\"의 철학", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "philosophies_of_freedom"));
        itemDataFile.itemDatas.Add(new Item(10004, "Teachings of \"Resistance\"", "\"투쟁\"의 가르침", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "teaching_of_resistance"));
        itemDataFile.itemDatas.Add(new Item(10005, "Guide to \"Resistance\"", "\"투쟁\"의 인도", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "guide_to_resistance"));
        itemDataFile.itemDatas.Add(new Item(10006, "Philosophies of \"Resistance\"", "\"투쟁\"의 철학", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "philosophies_of_resistance"));
        itemDataFile.itemDatas.Add(new Item(10008, "Teachings of \"Ballad\"", "\"시문\"의 가르침", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "teaching_of_ballad"));
        itemDataFile.itemDatas.Add(new Item(10009, "Guide to \"Ballad\"", "\"시문\"의 인도", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "guide_to_ballad"));
        itemDataFile.itemDatas.Add(new Item(10010, "Philosophies of \"Ballad\"", "\"시문\"의 철학", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "philosophies_of_ballad"));
        itemDataFile.itemDatas.Add(new Item(10012, "Teachings of \"Prosperity\"", "\"번영\"의 가르침", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "teaching_of_prosperity"));
        itemDataFile.itemDatas.Add(new Item(10013, "Guide to \"Prosperity\"", "\"번영\"의 인도", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "guide_to_prosperity"));
        itemDataFile.itemDatas.Add(new Item(10014, "Philosophies of \"Prosperity\"", "\"번영\"의 철학", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "philosophies_of_prosperity"));
        itemDataFile.itemDatas.Add(new Item(10016, "Teachings of \"Diligence\"", "\"근면\"의 가르침", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "teaching_of_diligence"));
        itemDataFile.itemDatas.Add(new Item(10017, "Guide to \"Diligence\"", "\"근면\"의 인도", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "guide_to_diligence"));
        itemDataFile.itemDatas.Add(new Item(10018, "Philosophies of \"Diligence\"", "\"근면\"의 철학", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "philosophies_of_diligence"));
        itemDataFile.itemDatas.Add(new Item(10020, "Teachings of \"Gold\"", "\"황금\"의 가르침", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "teaching_of_gold"));
        itemDataFile.itemDatas.Add(new Item(10021, "Guide to \"Gold\"", "\"황금\"의 인도", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "guide_to_gold"));
        itemDataFile.itemDatas.Add(new Item(10022, "Philosophies of \"Gold\"", "\"황금\"의 철학", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "philosophies_of_gold"));

        itemDataFile.itemDatas.Add(new Item(10100, "Agnidus Agate Sliver", "불타오르는 마노 가루", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "agnidus_agate_sliver"));
        itemDataFile.itemDatas.Add(new Item(10101, "Agnidus Agate Fragment", "불타오르는 마노 조각", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "agnidus_agate_fragment"));
        itemDataFile.itemDatas.Add(new Item(10102, "Agnidus Agate Chunk", "불타오르는 마노 덩이", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "agnidus_agate_chunk"));
        itemDataFile.itemDatas.Add(new Item(10103, "Agnidus Agate Gemstone", "불타오르는 마노", 1, ItemType.TALENTMATERIAL, Grade.LEGEND, spritePathTalent + "agnidus_agate_gemstone"));
        itemDataFile.itemDatas.Add(new Item(10104, "Varunada Lazurite Sliver", "순수한 청금석 가루", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "varunada_lazurite_sliver"));
        itemDataFile.itemDatas.Add(new Item(10105, "Varunada Lazurite Fragment", "순수한 청금석 조각", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "varunada_lazurite_fragment"));
        itemDataFile.itemDatas.Add(new Item(10106, "Varunada Lazurite Chunk", "순수한 청금석 덩이", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "varunada_lazurite_chunk"));
        itemDataFile.itemDatas.Add(new Item(10107, "Varunada Lazurite Gemstone", "순수한 청금석", 1, ItemType.TALENTMATERIAL, Grade.LEGEND, spritePathTalent + "varunada_lazurite_gemstone"));
        itemDataFile.itemDatas.Add(new Item(10108, "Vajrada Amethyst Sliver", "승리의 자수정 가루", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "vajrada_amethyst_sliver"));
        itemDataFile.itemDatas.Add(new Item(10109, "Vajrada Amethyst Fragment", "승리의 자수정 조각", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "vajrada_amethyst_fragment"));
        itemDataFile.itemDatas.Add(new Item(10110, "Vajrada Amethyst Chunk", "승리의 자수정 덩이", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "vajrada_amethyst_chunk"));
        itemDataFile.itemDatas.Add(new Item(10111, "Vajrada Amethyst Gemstone", "승리의 자수정", 1, ItemType.TALENTMATERIAL, Grade.LEGEND, spritePathTalent + "vajrada_amethyst_gemstone"));
        itemDataFile.itemDatas.Add(new Item(10112, "Vayuda Turquoise Sliver", "자유로운 터키석 가루", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "vayuda_turquoise_sliver"));
        itemDataFile.itemDatas.Add(new Item(10113, "Vayuda Turquoise Fragment", "자유로운 터키석 조각", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "vayuda_turquoise_fragment"));
        itemDataFile.itemDatas.Add(new Item(10114, "Vayuda Turquoise Chunk", "자유로운 터키석 덩이", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "vayuda_turquoise_chunk"));
        itemDataFile.itemDatas.Add(new Item(10115, "Vayuda Turquoise Gemstone", "자유로운 터키석", 1, ItemType.TALENTMATERIAL, Grade.LEGEND, spritePathTalent + "vayuda_turquoise_gemstone"));
        itemDataFile.itemDatas.Add(new Item(10116, "Shivada Jade Sliver", "서늘한 빙옥 가루", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "shivada_jade_sliver"));
        itemDataFile.itemDatas.Add(new Item(10117, "Shivada Jade Fragment", "서늘한 빙옥 조각", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "shivada_jade_fragment"));
        itemDataFile.itemDatas.Add(new Item(10118, "Shivada Jade Chunk", "서늘한 빙옥 덩이", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "shivada_jade_chunk"));
        itemDataFile.itemDatas.Add(new Item(10119, "Shivada Jade Gemstone", "서늘한 빙옥", 1, ItemType.TALENTMATERIAL, Grade.LEGEND, spritePathTalent + "shivada_jade_gemstone"));
        itemDataFile.itemDatas.Add(new Item(10120, "Prithiva Topaz Sliver", "단단한 황옥 가루", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "prithiva_topaz_sliver"));
        itemDataFile.itemDatas.Add(new Item(10121, "Prithiva Topaz Fragment", "단단한 황옥 조각", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "prithiva_topaz_fragment"));
        itemDataFile.itemDatas.Add(new Item(10122, "Prithiva Topaz Chunk", "단단한 황옥 덩이", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "prithiva_topaz_chunk"));
        itemDataFile.itemDatas.Add(new Item(10123, "Prithiva Topaz Gemstone", "단단한 황옥", 1, ItemType.TALENTMATERIAL, Grade.LEGEND, spritePathTalent + "prithiva_topaz_gemstone"));
        itemDataFile.itemDatas.Add(new Item(10124, "Brilliant Diamond Sliver", "휘황찬란한 다이아몬드 파편", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "brilliant_diamond_sliver"));
        itemDataFile.itemDatas.Add(new Item(10125, "Brilliant Diamond Fragment", "휘황찬란한 다이아몬드 단편", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "brilliant_diamond_fragment"));
        itemDataFile.itemDatas.Add(new Item(10126, "Brilliant Diamond Chunk", "휘황찬란한 다이아몬드 덩어리", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "brilliant_diamond_chunk"));
        itemDataFile.itemDatas.Add(new Item(10127, "Brilliant Diamond Gemstone", "휘황찬란한 다이아몬드", 1, ItemType.TALENTMATERIAL, Grade.LEGEND, spritePathTalent + "brilliant_diamond_gemstone"));

        itemDataFile.itemDatas.Add(new Item(10200, "Everflame Seed", "꺼지지 않는 불씨", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "everflame_seed"));
        itemDataFile.itemDatas.Add(new Item(10201, "Cleansing Heart", "물처럼 맑은 마음", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "cleansing_heart"));
        itemDataFile.itemDatas.Add(new Item(10202, "Lightning Prism", "뇌광 프리즘", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "lightning_prism"));
        itemDataFile.itemDatas.Add(new Item(10203, "Hurricane Seed", "폭풍의 씨앗", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "hurricane_seed"));
        itemDataFile.itemDatas.Add(new Item(10204, "Hoarfrost Core", "서리의 핵", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "hoarfrost_core"));
        itemDataFile.itemDatas.Add(new Item(10205, "Basalt Pillar", "현암의 탑", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "basalt_pillar"));

        itemDataFile.itemDatas.Add(new Item(10207, "Juvenile Jade", "설익은 옥", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "juvenile_jade"));


        itemDataFile.itemDatas.Add(new Item(10300, "Damaged Mask", "부서진 가면", 1, ItemType.MATERIAL, Grade.NORMAL, spritePathTalent + "damaged_mask"));
        itemDataFile.itemDatas.Add(new Item(10301, "Stained Mask", "오염된 가면", 1, ItemType.MATERIAL, Grade.RARE, spritePathTalent + "stained_mask"));
        itemDataFile.itemDatas.Add(new Item(10302, "Ominous Mask", "불길한 가면", 1, ItemType.MATERIAL, Grade.EPIC, spritePathTalent + "ominous_mask"));
        itemDataFile.itemDatas.Add(new Item(10305, "Divining Scroll", "이능 두루마리", 1, ItemType.MATERIAL, Grade.NORMAL, spritePathTalent + "divining_scroll"));
        itemDataFile.itemDatas.Add(new Item(10306, "Sealed Scroll", "봉마의 두루마리", 1, ItemType.MATERIAL, Grade.RARE, spritePathTalent + "sealed_scroll"));
        itemDataFile.itemDatas.Add(new Item(10307, "Forbidden Curse Scroll", "금주의 두루마리", 1, ItemType.MATERIAL, Grade.EPIC, spritePathTalent + "forbidden_curse_scroll"));
        itemDataFile.itemDatas.Add(new Item(10310, "Firm Arrowhead", "견고한 화살촉", 1, ItemType.MATERIAL, Grade.NORMAL, spritePathTalent + "firm_arrowhead"));
        itemDataFile.itemDatas.Add(new Item(10311, "Sharp Arrowhead", "날카로운 화살촉", 1, ItemType.MATERIAL, Grade.RARE, spritePathTalent + "sharp_arrowhead"));
        itemDataFile.itemDatas.Add(new Item(10312, "Weathered Arrowhead", "역전의 화살촉", 1, ItemType.MATERIAL, Grade.EPIC, spritePathTalent + "weathered_arrowhead"));
        itemDataFile.itemDatas.Add(new Item(10315, "Slime_condensate", "슬라임 응축액", 1, ItemType.MATERIAL, Grade.NORMAL, spritePathTalent + "slime_condensate"));
        itemDataFile.itemDatas.Add(new Item(10316, "Slime Secretions", "슬라임청", 1, ItemType.MATERIAL, Grade.RARE, spritePathTalent + "slime_secretions"));
        itemDataFile.itemDatas.Add(new Item(10317, "Slime Concentrate", "슬라임 원액", 1, ItemType.MATERIAL, Grade.EPIC, spritePathTalent + "slime_concentrate"));
        itemDataFile.itemDatas.Add(new Item(10320, "Whopperflower Nectar", "구라구라 꽃꿀", 1, ItemType.MATERIAL, Grade.NORMAL, spritePathTalent + "whopperflower_nectar"));
        itemDataFile.itemDatas.Add(new Item(10321, "Shimmering Nectar", "반짝반짝 꽃꿀", 1, ItemType.MATERIAL, Grade.RARE, spritePathTalent + "shimmering_nectar"));
        itemDataFile.itemDatas.Add(new Item(10322, "Energy Nectar", "원소 꽃꿀", 1, ItemType.MATERIAL, Grade.EPIC, spritePathTalent + "energy_nectar"));
        itemDataFile.itemDatas.Add(new Item(10325, "Treasure Hoarder Insignia", "보물찾기 까마귀 휘장", 1, ItemType.MATERIAL, Grade.NORMAL, spritePathTalent + "treasure_hoarder_insignia"));
        itemDataFile.itemDatas.Add(new Item(10326, "Silver Raven Insignia", "실버 까마귀 휘장", 1, ItemType.MATERIAL, Grade.RARE, spritePathTalent + "silver_raven_insignia"));
        itemDataFile.itemDatas.Add(new Item(10327, "Golden Raven Insignia", "골드 까마귀 휘장", 1, ItemType.MATERIAL, Grade.EPIC, spritePathTalent + "golden_raven_insignia"));
        itemDataFile.itemDatas.Add(new Item(10330, "Recruits Insignia", "신병의 휘장", 1, ItemType.MATERIAL, Grade.NORMAL, spritePathTalent + "recruits_insignia"));
        itemDataFile.itemDatas.Add(new Item(10331, "Sergeants Insignia", "사관의 휘장", 1, ItemType.MATERIAL, Grade.RARE, spritePathTalent + "sergeants_insignia"));
        itemDataFile.itemDatas.Add(new Item(10332, "Lieutenants Insignia", "위관의 휘장", 1, ItemType.MATERIAL, Grade.EPIC, spritePathTalent + "lieutenants_insignia"));

        itemDataFile.itemDatas.Add(new Item(10400, "Calla Lily", "통통 연꽃", 1, ItemType.MATERIAL_MONDSTADT, Grade.NORMAL, spritePathTalent + "calla_lily"));
        itemDataFile.itemDatas.Add(new Item(10401, "Cecilia", "세실리아꽃", 1, ItemType.MATERIAL_MONDSTADT, Grade.NORMAL, spritePathTalent + "cecilia"));
        itemDataFile.itemDatas.Add(new Item(10402, "Cor Lapis", "콜 라피스", 1, ItemType.MATERIAL_LIYUE, Grade.NORMAL, spritePathTalent + "cor_lapis"));
        itemDataFile.itemDatas.Add(new Item(10403, "Glaze Lily", "유리백합", 1, ItemType.MATERIAL_LIYUE, Grade.NORMAL, spritePathTalent + "glaze_lily"));
        itemDataFile.itemDatas.Add(new Item(10404, "Jueyun Chili", "절운고추", 1, ItemType.MATERIAL_LIYUE, Grade.NORMAL, spritePathTalent + "jueyun_chili"));
        itemDataFile.itemDatas.Add(new Item(10405, "Noctilous Jade", "야박석", 1, ItemType.MATERIAL_LIYUE, Grade.NORMAL, spritePathTalent + "noctilous_jade"));
        itemDataFile.itemDatas.Add(new Item(10406, "Philanemo Mushroom", "바람버섯", 1, ItemType.MATERIAL_MONDSTADT, Grade.NORMAL, spritePathTalent + "philanemo_mushroom"));
        itemDataFile.itemDatas.Add(new Item(10407, "Silk Flower", "예상꽃", 1, ItemType.MATERIAL_LIYUE, Grade.NORMAL, spritePathTalent + "silk_flower"));
        itemDataFile.itemDatas.Add(new Item(10408, "Small Lamp Grass", "등불꽃", 1, ItemType.MATERIAL_MONDSTADT, Grade.NORMAL, spritePathTalent + "small_lamp_grass"));
        itemDataFile.itemDatas.Add(new Item(10409, "Valberry", "낙락베리", 1, ItemType.MATERIAL_MONDSTADT, Grade.NORMAL, spritePathTalent + "valberry"));
        itemDataFile.itemDatas.Add(new Item(10410, "Violetgrass", "유리 주머니", 1, ItemType.MATERIAL_LIYUE, Grade.NORMAL, spritePathTalent + "violetgrass"));
        itemDataFile.itemDatas.Add(new Item(10411, "Windwheel Aster", "풍차 국화", 1, ItemType.MATERIAL_MONDSTADT, Grade.NORMAL, spritePathTalent + "windwheel_aster"));
        itemDataFile.itemDatas.Add(new Item(10412, "Wolfhook", "고리고리 열매", 1, ItemType.MATERIAL_MONDSTADT, Grade.NORMAL, spritePathTalent + "wolfhook"));
        itemDataFile.itemDatas.Add(new Item(10413, "Dandelion Seed", "민들레 씨앗", 1, ItemType.MATERIAL_MONDSTADT, Grade.NORMAL, spritePathTalent + "dandelion_seed"));
        itemDataFile.itemDatas.Add(new Item(10414, "Starconch", "별소라", 1, ItemType.MATERIAL_LIYUE, Grade.NORMAL, spritePathTalent + "starconch"));
        itemDataFile.itemDatas.Add(new Item(10415, "Qingxin", "청심", 1, ItemType.MATERIAL_LIYUE, Grade.NORMAL, spritePathTalent + "qingxin"));

        itemDataFile.itemDatas.Add(new Item(10500, "Tile of Decarabians Tower", "고탑 왕의 잔해", 1, ItemType.MATERIAL_MONDSTADT, Grade.RARE, spritePathWeaponTalent + "tile_of_decarabians_tower"));
        itemDataFile.itemDatas.Add(new Item(10501, "Debris of Decarabians City", "고탑 왕의 절망", 1, ItemType.MATERIAL_MONDSTADT, Grade.EPIC, spritePathWeaponTalent + "debris_of_decarabians_city"));
        itemDataFile.itemDatas.Add(new Item(10502, "Fragment of Decarabians Epic", "고탑 왕의 조각", 1, ItemType.MATERIAL_MONDSTADT, Grade.UNIQUE, spritePathWeaponTalent + "fragment_of_decarabians_epic"));
        itemDataFile.itemDatas.Add(new Item(10503, "Scattered Piece of Decarabianss Dream", "고탑 왕의 깨진 꿈", 1, ItemType.MATERIAL_MONDSTADT, Grade.LEGEND, spritePathWeaponTalent + "scattered_piece_of_decarabianss_dream"));
        itemDataFile.itemDatas.Add(new Item(10504, "Boreal Wolfs Milk Tooth", "라이언 투사의 족쇄", 1, ItemType.MATERIAL_MONDSTADT, Grade.RARE, spritePathWeaponTalent + "boreal_wolfs_milk_tooth"));
        itemDataFile.itemDatas.Add(new Item(10505, "Boreal Wolfs Cracked Tooth", "라이언 투사의 쇠사슬", 1, ItemType.MATERIAL_MONDSTADT, Grade.EPIC, spritePathWeaponTalent + "boreal_wolfs_cracked_tooth"));
        itemDataFile.itemDatas.Add(new Item(10506, "Boreal Wolfs Broken Fang", "라이언 투사의 수갑", 1, ItemType.MATERIAL_MONDSTADT, Grade.UNIQUE, spritePathWeaponTalent + "boreal_wolfs_broken_fang"));
        itemDataFile.itemDatas.Add(new Item(10507, "Boreal Wolfs Nostalgia", "라이언 투사의 이념", 1, ItemType.MATERIAL_MONDSTADT, Grade.LEGEND, spritePathWeaponTalent + "boreal_wolfs_nostalgia"));
        itemDataFile.itemDatas.Add(new Item(10508, "Fetters of The Dandelion Gladiator", "칼바람 울프의 젖니", 1, ItemType.MATERIAL_MONDSTADT, Grade.RARE, spritePathWeaponTalent + "fetters_of_the_dandelion_gladiator"));
        itemDataFile.itemDatas.Add(new Item(10509, "Chains of The Dandelion Gladiator", "칼바람 울프의 이빨", 1, ItemType.MATERIAL_MONDSTADT, Grade.EPIC, spritePathWeaponTalent + "chains_of_the_dandelion_gladiator"));
        itemDataFile.itemDatas.Add(new Item(10510, "Shackles of The Dandelion Gladiator", "칼바람 울프의 이빨", 1, ItemType.MATERIAL_MONDSTADT, Grade.UNIQUE, spritePathWeaponTalent + "shackles_of_the_dandelion_gladiator"));
        itemDataFile.itemDatas.Add(new Item(10511, "Dream of The Dandelion Gladiator", "칼바람 울프의 그리운 고향", 1, ItemType.MATERIAL_MONDSTADT, Grade.LEGEND, spritePathWeaponTalent + "dream_of_the_dandelion_gladiator"));
        itemDataFile.itemDatas.Add(new Item(10512, "Luminous Sands from Guyun", "고운한림의 매끄러운 모래", 1, ItemType.MATERIAL_LIYUE, Grade.RARE, spritePathWeaponTalent + "luminous_sands_from_guyun"));
        itemDataFile.itemDatas.Add(new Item(10513, "Lustrous Stone from Guyun", "고운한림의 휘암", 1, ItemType.MATERIAL_LIYUE, Grade.EPIC, spritePathWeaponTalent + "lustrous_stone_from_guyun"));
        itemDataFile.itemDatas.Add(new Item(10514, "Relic from Guyun", "고운한림의 해골", 1, ItemType.MATERIAL_LIYUE, Grade.UNIQUE, spritePathWeaponTalent + "relic_from_guyun"));
        itemDataFile.itemDatas.Add(new Item(10515, "Divine Body from Guyun", "고운한림의 매끄러운 신체", 1, ItemType.MATERIAL_LIYUE, Grade.LEGEND, spritePathWeaponTalent + "divine_body_from_guyun"));
        itemDataFile.itemDatas.Add(new Item(10516, "Mist Veiled Lead Elixir", "안개구름 속의 흑연단", 1, ItemType.MATERIAL_LIYUE, Grade.RARE, spritePathWeaponTalent + "mist_veiled_lead_elixir"));
        itemDataFile.itemDatas.Add(new Item(10517, "Mist Veiled Mercury Elixir", "안개 구름속의 수은단", 1, ItemType.MATERIAL_LIYUE, Grade.EPIC, spritePathWeaponTalent + "mist_veiled_mercury_elixir"));
        itemDataFile.itemDatas.Add(new Item(10518, "Mist Veiled Gold Elixir", "안개 구름속의 금단", 1, ItemType.MATERIAL_LIYUE, Grade.UNIQUE, spritePathWeaponTalent + "mist_veiled_gold_elixir"));
        itemDataFile.itemDatas.Add(new Item(10519, "Mist Veiled Primo Elixir", "안개 구름속의 전환", 1, ItemType.MATERIAL_LIYUE, Grade.LEGEND, spritePathWeaponTalent + "mist_veiled_primo_elixir"));
        itemDataFile.itemDatas.Add(new Item(10520, "Grain of Aerosiderite", "흑운철 한 알", 1, ItemType.MATERIAL_LIYUE, Grade.RARE, spritePathWeaponTalent + "grain_of_aerosiderite"));
        itemDataFile.itemDatas.Add(new Item(10521, "Piece of Aerosiderite", "흑운철 조각", 1, ItemType.MATERIAL_LIYUE, Grade.EPIC, spritePathWeaponTalent + "piece_of_aerosiderite"));
        itemDataFile.itemDatas.Add(new Item(10522, "Bit of Aerosiderite", "흑운철 일각", 1, ItemType.MATERIAL_LIYUE, Grade.UNIQUE, spritePathWeaponTalent + "bit_of_aerosiderite"));
        itemDataFile.itemDatas.Add(new Item(10523, "Chunk of Aerosiderite", "흑운철 덩이", 1, ItemType.MATERIAL_LIYUE, Grade.LEGEND, spritePathWeaponTalent + "chunk_of_aerosiderite"));

        itemDataFile.itemDatas.Add(new Item(10600, "Dead Ley Line Branch", "지맥의 낡은 가지", 1, ItemType.MATERIAL, Grade.RARE, spritePathWeaponTalent + "dead_ley_line_branch"));
        itemDataFile.itemDatas.Add(new Item(10601, "Dead Ley Line Leaves", "지맥의 마른 잎", 1, ItemType.MATERIAL, Grade.EPIC, spritePathWeaponTalent + "dead_ley_line_leaves"));
        itemDataFile.itemDatas.Add(new Item(10602, "Ley Line Sprout", "지맥의 새싹", 1, ItemType.MATERIAL, Grade.UNIQUE, spritePathWeaponTalent + "ley_line_sprout"));
        itemDataFile.itemDatas.Add(new Item(10604, "Fragile Bone Shard", "약한 뼛조각", 1, ItemType.MATERIAL, Grade.RARE, spritePathWeaponTalent + "fragile_bone_shard"));
        itemDataFile.itemDatas.Add(new Item(10605, "Sturdy Bone Shard", "단단한 뼛조각", 1, ItemType.MATERIAL, Grade.EPIC, spritePathWeaponTalent + "sturdy_bone_shard"));
        itemDataFile.itemDatas.Add(new Item(10606, "Fossilized Bone Shard", "석화한 뼛조각", 1, ItemType.MATERIAL, Grade.UNIQUE, spritePathWeaponTalent + "fossilized_bone_shard"));
        itemDataFile.itemDatas.Add(new Item(10608, "Hunters Sacrificial Knife", "사냥꾼의 제도", 1, ItemType.MATERIAL, Grade.RARE, spritePathWeaponTalent + "hunters_sacrificial_knife"));
        itemDataFile.itemDatas.Add(new Item(10609, "Agents Sacrificial Knife", "특수 요원의 제도", 1, ItemType.MATERIAL, Grade.EPIC, spritePathWeaponTalent + "agents_sacrificial_knife"));
        itemDataFile.itemDatas.Add(new Item(10610, "Inspectors Sacrificial Knife", "검사관의 제도", 1, ItemType.MATERIAL, Grade.UNIQUE, spritePathWeaponTalent + "inspectors_sacrificial_knife"));
        itemDataFile.itemDatas.Add(new Item(10612, "Mist Grass Pollen", "안개꽃 가루", 1, ItemType.MATERIAL, Grade.RARE, spritePathWeaponTalent + "mist_grass_pollen"));
        itemDataFile.itemDatas.Add(new Item(10613, "Mist Grass", "안개풀 주머니", 1, ItemType.MATERIAL, Grade.EPIC, spritePathWeaponTalent + "mist_grass"));
        itemDataFile.itemDatas.Add(new Item(10614, "Mist Grass Wick", "안개 등심", 1, ItemType.MATERIAL, Grade.UNIQUE, spritePathWeaponTalent + "mist_grass_wick"));
        itemDataFile.itemDatas.Add(new Item(10616, "Chaos Device", "혼돈의 장치", 1, ItemType.MATERIAL, Grade.RARE, spritePathWeaponTalent + "chaos_device"));
        itemDataFile.itemDatas.Add(new Item(10617, "Chaos Circuit", "혼돈의 회로", 1, ItemType.MATERIAL, Grade.EPIC, spritePathWeaponTalent + "chaos_circuit"));
        itemDataFile.itemDatas.Add(new Item(10618, "Chaos Core", "혼돈의 노심", 1, ItemType.MATERIAL, Grade.UNIQUE, spritePathWeaponTalent + "chaos_core"));
        itemDataFile.itemDatas.Add(new Item(10620, "Heavy Horn", "무거운 나팔", 1, ItemType.MATERIAL, Grade.RARE, spritePathWeaponTalent + "heavy_horn"));
        itemDataFile.itemDatas.Add(new Item(10621, "Black Bronze Horn", "흑동 나팔", 1, ItemType.MATERIAL, Grade.EPIC, spritePathWeaponTalent + "black_bronze_horn"));
        itemDataFile.itemDatas.Add(new Item(10622, "Black Crystal Horn", "흑수정 나팔", 1, ItemType.MATERIAL, Grade.UNIQUE, spritePathWeaponTalent + "black_crystal_horn"));

        itemDataFile.itemDatas.Add(new Item(10700, "Dvalins Claw", "동풍의 발톱", 1, ItemType.MATERIAL, Grade.LEGEND, spritePathTalent + "dvalins_claw"));
        itemDataFile.itemDatas.Add(new Item(10701, "Dvalins Plume", "동풍의 깃털", 1, ItemType.MATERIAL, Grade.LEGEND, spritePathTalent + "dvalins_plume"));
        itemDataFile.itemDatas.Add(new Item(10702, "Dvalins Sigh", "동풍의 숨결", 1, ItemType.MATERIAL, Grade.LEGEND, spritePathTalent + "dvalins_sigh"));
        itemDataFile.itemDatas.Add(new Item(10703, "Ring Of Boreas", "북풍의 고리", 1, ItemType.MATERIAL, Grade.LEGEND, spritePathTalent + "ring_of_boreas"));
        itemDataFile.itemDatas.Add(new Item(10704, "Spirit Locket Of Boreas", "북풍의 영혼상자", 1, ItemType.MATERIAL, Grade.LEGEND, spritePathTalent + "spirit_locket_of_boreas"));
        itemDataFile.itemDatas.Add(new Item(10705, "Tail Of Boreas", "북풍의 꼬리", 1, ItemType.MATERIAL, Grade.LEGEND, spritePathTalent + "tail_of_boreas"));
        itemDataFile.itemDatas.Add(new Item(10706, "Shadow Of The Warrior", "무예의 혼 : 고영", 1, ItemType.MATERIAL, Grade.LEGEND, spritePathTalent + "shadow_of_the_warrior"));
        itemDataFile.itemDatas.Add(new Item(10707, "Shard Of Foul Legacy", "마왕의 칼날 : 조각", 1, ItemType.MATERIAL, Grade.LEGEND, spritePathTalent + "shard_of_foul_legacy"));
        itemDataFile.itemDatas.Add(new Item(10708, "Tusk Of Monoceros Caeli", "하늘을 삼킨 고래 : 뿔", 1, ItemType.MATERIAL, Grade.LEGEND, spritePathTalent + "tusk_of_monoceros_caeli"));

        // 서늘한 빙옥, 자유로운 터키석, 승리의 자수정, 단단한 황옥, 순수한 청금석
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

    public Item makeItem(Item item, int count = 1)
    {
        return new Item(item.code, item.enName, item.koName, count, item.type, item.grade, item.spritePath, item.character);
    }
}

[System.Serializable]
public class ItemDataFile
{
    public List<Item> itemDatas;
}
