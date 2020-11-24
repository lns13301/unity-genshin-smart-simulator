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
    public string spritePathTalent = "Images/UI/Stat/";

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

        itemDataFile.itemDatas.Add(new Item(0, "Amber", "엠버", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Amber", new Character(0, Element.PYRO, Grade.UNIQUE, 1000)));
        itemDataFile.itemDatas.Add(new Item(1, "Kaeya", "케이야", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Kaeya", new Character(1, Element.CRYO, Grade.UNIQUE)));
        itemDataFile.itemDatas.Add(new Item(2, "Lisa", "리사", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Lisa", new Character(2, Element.ELECTRO, Grade.UNIQUE)));
        itemDataFile.itemDatas.Add(new Item(3, "Barbara", "바바라", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Barbara", new Character(3, Element.HYDRO, Grade.UNIQUE, 1006)));
        itemDataFile.itemDatas.Add(new Item(4, "Xiangling", "향릉", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Xiangling", new Character(4, Element.PYRO, Grade.UNIQUE)));
        itemDataFile.itemDatas.Add(new Item(5, "Razor", "레이저", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Razor", new Character(5, Element.ELECTRO, Grade.UNIQUE)));
        itemDataFile.itemDatas.Add(new Item(6, "Bennett", "베넷", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Bennett", new Character(6, Element.PYRO, Grade.UNIQUE, 1018)));
        itemDataFile.itemDatas.Add(new Item(7, "Xingqiu", "행추", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Xingqiu", new Character(7, Element.HYDRO, Grade.UNIQUE)));
        itemDataFile.itemDatas.Add(new Item(8, "Beidou", "북두", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Beidou", new Character(8, Element.ELECTRO, Grade.UNIQUE, 1012)));
        itemDataFile.itemDatas.Add(new Item(9, "Sucrose", "설탕", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Sucrose", new Character(9, Element.ANEMO, Grade.UNIQUE)));
        itemDataFile.itemDatas.Add(new Item(10, "Ningguang", "응광", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Ningguang", new Character(10, Element.GEO, Grade.UNIQUE)));
        itemDataFile.itemDatas.Add(new Item(11, "Noelle", "노엘", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Noelle", new Character(11, Element.GEO, Grade.UNIQUE)));
        itemDataFile.itemDatas.Add(new Item(12, "Fischl", "피슬", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Fischl", new Character(12, Element.ELECTRO, Grade.UNIQUE)));
        itemDataFile.itemDatas.Add(new Item(13, "Chongyun", "중운", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Chongyun", new Character(13, Element.CRYO, Grade.UNIQUE)));
        itemDataFile.itemDatas.Add(new Item(14, "Diona", "디오나", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Diona", new Character(14, Element.CRYO, Grade.UNIQUE)));
        //itemDataFile.itemDatas.Add(new Item(15, "Xinyan", "신염", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "ShinYeom", new Character(15, Element.PYRO, Grade.UNIQUE)));

        itemDataFile.itemDatas.Add(new Item(500, "Diluc", "다이루크", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Diluc", new Character(500, Element.PYRO, Grade.LEGEND)));
        itemDataFile.itemDatas.Add(new Item(501, "Mona", "모나", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Mona", new Character(501, Element.HYDRO, Grade.LEGEND)));
        itemDataFile.itemDatas.Add(new Item(502, "Keqing", "각청", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Keqing", new Character(502, Element.ELECTRO, Grade.LEGEND)));
        itemDataFile.itemDatas.Add(new Item(503, "QiQi", "치치", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "QiQi", new Character(503, Element.CRYO, Grade.LEGEND)));
        itemDataFile.itemDatas.Add(new Item(504, "Jean", "진", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Jean", new Character(504, Element.ANEMO, Grade.LEGEND)));

        itemDataFile.itemDatas.Add(new Item(700, "Venti", "벤티", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Venti", new Character(700, Element.ANEMO, Grade.LEGEND)));
        itemDataFile.itemDatas.Add(new Item(701, "Klee", "클레", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Klee", new Character(701, Element.PYRO, Grade.LEGEND)));
        itemDataFile.itemDatas.Add(new Item(702, "Tartaglia", "타르탈리아", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Tartaglia", new Character(702, Element.HYDRO, Grade.LEGEND)));
        //itemDataFile.itemDatas.Add(new Item(703, "Zhongli", "종려", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Zhongli", new Character(703, Element.GEO, Grade.LEGEND)));
        //itemDataFile.itemDatas.Add(new Item(704, "Xiao", "소", 1, ItemType.CHARACTER, Grade.LEGEND, spritePathHero + "Xiao", new Character(704, Element.ANEMO, Grade.LEGEND)));

        itemDataFile.itemDatas.Add(new Item(1000, "Cool Steel", "차가운 칼날", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Cool Steel"));
        itemDataFile.itemDatas.Add(new Item(1001, "Dark Iron Sword", "암철검", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Dark Iron Sword"));
        itemDataFile.itemDatas.Add(new Item(1002, "Fillet Blade", "흘호 생선회칼", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Fillet Blade"));
        itemDataFile.itemDatas.Add(new Item(1003, "Harbinger of Dawn", "여명신검", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Harbinger of Dawn"));
        itemDataFile.itemDatas.Add(new Item(1004, "Skyrider Sword", "비천어검", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Skyrider Sword"));
        //itemDataFile.itemDatas.Add(new Item(1005, "Traveler's Handy Sword", "여행자의 검", 1, ItemType.SWORD, Grade.EPIC, spritePathWeapon + "Traveler's Handy Sword"));

        itemDataFile.itemDatas.Add(new Item(1100, "Bloodtainted Greatsword", "드래곤 블러드 소드", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "Bloodtainted Greatsword"));
        itemDataFile.itemDatas.Add(new Item(1101, "Debate Club", "훌륭한 대화수단", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "Debate Club"));
        itemDataFile.itemDatas.Add(new Item(1102, "Ferrous Shadow", "강철의 그림자", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "Ferrous Shadow"));
        //itemDataFile.itemDatas.Add(new Item(1103, "Skyrider Greatsword", "비천대어검", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "Skyrider Greatsword"));
        itemDataFile.itemDatas.Add(new Item(1104, "White Iron Greatsword", "백철 대검", 1, ItemType.CLAYMORE, Grade.EPIC, spritePathWeapon + "White Iron Greatsword"));

        itemDataFile.itemDatas.Add(new Item(1200, "Black Tassel", "흑술창", 1, ItemType.POLEARM, Grade.EPIC, spritePathWeapon + "Black Tassel"));
        itemDataFile.itemDatas.Add(new Item(1201, "Halberd", "미늘창", 1, ItemType.POLEARM, Grade.EPIC, spritePathWeapon + "Halberd"));
        itemDataFile.itemDatas.Add(new Item(1202, "White Tassel", "백술창", 1, ItemType.POLEARM, Grade.EPIC, spritePathWeapon + "White Tassel"));

        itemDataFile.itemDatas.Add(new Item(1300, "Emerald Orb", "비취 오브", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Emerald Orb"));
        itemDataFile.itemDatas.Add(new Item(1301, "Magic Guide", "마도 서론", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Magic Guide"));
        itemDataFile.itemDatas.Add(new Item(1302, "Otherworldly Story", "이세계 여행기", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Otherworldly Story"));
        itemDataFile.itemDatas.Add(new Item(1303, "Thrilling Tales of Dragon Slayers", "드래곤 슬레이어 영웅담", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Thrilling Tales of Dragon Slayers"));
        itemDataFile.itemDatas.Add(new Item(1304, "Twin Nephrite", "1급 보옥", 1, ItemType.CATALYST, Grade.EPIC, spritePathWeapon + "Twin Nephrite"));

        itemDataFile.itemDatas.Add(new Item(1400, "Messenger", "전령", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Messenger"));
        itemDataFile.itemDatas.Add(new Item(1401, "Raven Bow", "까마귀깃 활", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Raven Bow"));
        itemDataFile.itemDatas.Add(new Item(1402, "Recurve Bow", "곡궁", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Recurve Bow"));
        itemDataFile.itemDatas.Add(new Item(1403, "Sharpshooter's Oath", "신궁의 서약", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Sharpshooter's Oath"));
        itemDataFile.itemDatas.Add(new Item(1404, "Slingshot", "탄궁", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Slingshot"));
        itemDataFile.itemDatas.Add(new Item(1405, "Messenger", "전령", 1, ItemType.BOW, Grade.EPIC, spritePathWeapon + "Messenger"));

        ///////////////////////////////////////////////////////////////////////////////////////////////

        itemDataFile.itemDatas.Add(new Item(2000, "Favonius Sword", "페보니우스 검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Favonius Sword"));
        itemDataFile.itemDatas.Add(new Item(2001, "Sacrificial Sword", "제례검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Sacrificial Sword"));
        itemDataFile.itemDatas.Add(new Item(2002, "Lion's Roar", "용의 포효", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Lion's Roar"));
        itemDataFile.itemDatas.Add(new Item(2003, "The Flute", "피리검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "The Flute"));
        //itemDataFile.itemDatas.Add(new Item(2004, "Iron Sting", "강철 벌침", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Iron Sting"));
        //itemDataFile.itemDatas.Add(new Item(2005, "Prototype Rancour", "참암 프로토타입", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Prototype Rancour"));
        //itemDataFile.itemDatas.Add(new Item(2006, "Royal Longsword", "왕실의 장검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Royal Longsword"));
        //itemDataFile.itemDatas.Add(new Item(2007, "Blackcliff Longsword", "흑암 장검", 1, ItemType.SWORD, Grade.UNIQUE, spritePathWeapon + "Blackcliff Longsword"));

        itemDataFile.itemDatas.Add(new Item(2100, "Favonius Greatsword", "페보니우스 대검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Favonius Greatsword"));
        itemDataFile.itemDatas.Add(new Item(2101, "Sacrificial Greatsword", "제례 대검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Sacrificial Greatsword"));
        itemDataFile.itemDatas.Add(new Item(2102, "Rainslasher", "빗물 베기", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Rainslasher"));
        itemDataFile.itemDatas.Add(new Item(2103, "The Bell", "시간의 검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "The Bell"));
        //itemDataFile.itemDatas.Add(new Item(2104, "Serpent Spine", "이무기 검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Serpent Spine"));
        //itemDataFile.itemDatas.Add(new Item(2105, "Whiteblind", "백영검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Whiteblind"));
        //itemDataFile.itemDatas.Add(new Item(2106, "Blackcliff Slasher", "흑암참도", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Bloodtainted Greatsword"));
        //itemDataFile.itemDatas.Add(new Item(2107, "Prototype Aminus", "고화 프로토타입", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Prototype Aminus"));
        //itemDataFile.itemDatas.Add(new Item(2108, "Royal Greatsword", "왕실의 대검", 1, ItemType.CLAYMORE, Grade.UNIQUE, spritePathWeapon + "Royal Greatsword"));

        itemDataFile.itemDatas.Add(new Item(2200, "Favonius Lance", "페보니우스 장창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Favonius Lance"));
        itemDataFile.itemDatas.Add(new Item(2201, "Dragon's Bane", "용학살창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Dragon's Bane"));
        //itemDataFile.itemDatas.Add(new Item(2202, "Deathmatch", "결투의 창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Deathmatch"));
        //itemDataFile.itemDatas.Add(new Item(2203, "Crescent Pike", "유월창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Crescent Pike"));
        //itemDataFile.itemDatas.Add(new Item(2204, "Prototype Grudge", "별의 낫 프로토타입", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Prototype Grudge"));
        //itemDataFile.itemDatas.Add(new Item(2205, "Blackcliff Pole", "흑암창", 1, ItemType.POLEARM, Grade.UNIQUE, spritePathWeapon + "Blackcliff Pole"));

        itemDataFile.itemDatas.Add(new Item(2300, "Favonius Codex", "페보니우스 비전", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Favonius Codex"));
        itemDataFile.itemDatas.Add(new Item(2301, "Sacrificial Fragments", "제례의 악장", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Sacrificial Fragments"));
        itemDataFile.itemDatas.Add(new Item(2302, "Eye of Perception", "소심", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Eye of Perception"));
        itemDataFile.itemDatas.Add(new Item(2303, "The Widsith", "음유시인의 악장", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "The Widsith"));
        //itemDataFile.itemDatas.Add(new Item(2304, "Mappa Mare", "만국 항해용해도", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Mappa Mare"));
        //itemDataFile.itemDatas.Add(new Item(2305, "Solar Pearl", "일월의 정수", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Solar Pearl"));
        //itemDataFile.itemDatas.Add(new Item(2306, "Blackcliff Amulet", "흑암 홍옥", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Blackcliff Amulet"));
        //itemDataFile.itemDatas.Add(new Item(2307, "Prototype Malice", "황금 호박 프로토타입", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Prototype Malice"));
        //itemDataFile.itemDatas.Add(new Item(2308, "Royal Grimoire", "왕실의 비전록", 1, ItemType.CATALYST, Grade.UNIQUE, spritePathWeapon + "Royal Grimoire"));

        itemDataFile.itemDatas.Add(new Item(2400, "Favonius Warbow", "페보니우스 활", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Favonius Warbow"));
        itemDataFile.itemDatas.Add(new Item(2401, "Sacrificial Bow", "제례활", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Sacrificial Bow"));
        itemDataFile.itemDatas.Add(new Item(2402, "Rust", "녹슨 활", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Rust"));
        itemDataFile.itemDatas.Add(new Item(2403, "The Stringless", "절현", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "The Stringless"));
        //itemDataFile.itemDatas.Add(new Item(2404, "The Viridescent Hunt", "청록색 활", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "The Viridescent Hunt"));
        //itemDataFile.itemDatas.Add(new Item(2405, "Compound Bow", "강철궁", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Compound Bow"));
        //itemDataFile.itemDatas.Add(new Item(2406, "Blackcliff Warbow", "흑암 배틀 보우", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Blackcliff Warbow"));
        //itemDataFile.itemDatas.Add(new Item(2407, "Prototype Crescent", "담월 프로토타입", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Prototype Crescent"));
        //itemDataFile.itemDatas.Add(new Item(2408, "Royal Bow", "왕실의 장궁", 1, ItemType.BOW, Grade.UNIQUE, spritePathWeapon + "Royal Bow"));

        ///////////////////////////////////////////////////////////////////////////////////////////////

        itemDataFile.itemDatas.Add(new Item(3000, "Aquila Favonia", "매의 검", 1, ItemType.SWORD, Grade.LEGEND, spritePathWeapon + "Aquila Favonia"));
        itemDataFile.itemDatas.Add(new Item(3001, "Skyward Blade", "천공의 검", 1, ItemType.SWORD, Grade.LEGEND, spritePathWeapon + "Skyward Blade"));

        itemDataFile.itemDatas.Add(new Item(3100, "Wolf's Gravestone", "늑대의 말로", 1, ItemType.CLAYMORE, Grade.LEGEND, spritePathWeapon + "Wolf's Gravestone"));
        itemDataFile.itemDatas.Add(new Item(3101, "Skyward Pride", "천공의 대검", 1, ItemType.CLAYMORE, Grade.LEGEND, spritePathWeapon + "Skyward Pride"));

        itemDataFile.itemDatas.Add(new Item(3200, "Primordial Jade Winged Spear", "화박연", 1, ItemType.POLEARM, Grade.LEGEND, spritePathWeapon + "Primordial Jade Winged Spear"));
        itemDataFile.itemDatas.Add(new Item(3201, "Skyward Spine", "천공의 마루", 1, ItemType.POLEARM, Grade.LEGEND, spritePathWeapon + "Skyward Spine"));

        itemDataFile.itemDatas.Add(new Item(3300, "Lost Prayer to the Sacred Winds", "사풍 원서", 1, ItemType.CATALYST, Grade.LEGEND, spritePathWeapon + "Lost Prayer to the Sacred Winds"));
        itemDataFile.itemDatas.Add(new Item(3301, "Skyward Atlas", "천공의 두루마리", 1, ItemType.CATALYST, Grade.LEGEND, spritePathWeapon + "Skyward Atlas"));

        itemDataFile.itemDatas.Add(new Item(3400, "Amos's Bow", "아모스의 활", 1, ItemType.BOW, Grade.LEGEND, spritePathWeapon + "Amos's Bow"));
        itemDataFile.itemDatas.Add(new Item(3401, "Skyward Harp", "천공의 하프", 1, ItemType.BOW, Grade.LEGEND, spritePathWeapon + "Skyward Harp"));

        /////////////////////////////////////////////////////////////////////////////////////////////

        itemDataFile.itemDatas.Add(new Item(4300, "Memory of Dust", "속세의 자물쇠", 1, ItemType.CATALYST, Grade.LEGEND, spritePathWeapon + "Memory of Dust"));

        /////////////////////////////////////////////////////////////////////////////////////////////

        itemDataFile.itemDatas.Add(new Item(10000, "Teachings of \"Freedom\"", "\"자유\"의 가르침", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "teaching_of_freedom"));
        itemDataFile.itemDatas.Add(new Item(10001, "Guide to \"Freedom\"", "\"자유\"의 인도", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "guide_to_freedom"));
        itemDataFile.itemDatas.Add(new Item(10002, "Philosophies of \"Freedom\"", "\"자유\"의 철학", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "philosophies_of_freedom"));
        itemDataFile.itemDatas.Add(new Item(10003, "Teachings of \"Resistance\"", "\"투쟁\"의 가르침", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "teaching_of_resistance"));
        itemDataFile.itemDatas.Add(new Item(10004, "Guide to \"Resistance\"", "\"투쟁\"의 인도", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "guide_to_resistance"));
        itemDataFile.itemDatas.Add(new Item(10005, "Philosophies of \"Resistance\"", "\"투쟁\"의 철학", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "philosophies_of_resistance"));
        itemDataFile.itemDatas.Add(new Item(10006, "Teachings of \"Ballad\"", "\"시문\"의 가르침", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "teaching_of_ballad"));
        itemDataFile.itemDatas.Add(new Item(10007, "Guide to \"Ballad\"", "\"시문\"의 인도", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "guide_to_ballad"));
        itemDataFile.itemDatas.Add(new Item(10008, "Philosophies of \"Ballad\"", "\"시문\"의 철학", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "philosophies_of_ballad"));
        itemDataFile.itemDatas.Add(new Item(10009, "Teachings of \"Prosperity\"", "\"번영\"의 가르침", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "teaching_of_prosperity"));
        itemDataFile.itemDatas.Add(new Item(10010, "Guide to \"Prosperity\"", "\"번영\"의 인도", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "guide_to_prosperity"));
        itemDataFile.itemDatas.Add(new Item(10011, "Philosophies of \"Prosperity\"", "\"번영\"의 철학", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "philosophies_of_prosperity"));
        itemDataFile.itemDatas.Add(new Item(10012, "Teachings of \"Diligence\"", "\"근면\"의 가르침", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "teaching_of_diligence"));
        itemDataFile.itemDatas.Add(new Item(10013, "Guide to \"Diligence\"", "\"근면\"의 인도", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "guide_to_diligence"));
        itemDataFile.itemDatas.Add(new Item(10014, "Philosophies of \"Diligence\"", "\"근면\"의 철학", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "philosophies_of_diligence"));
        itemDataFile.itemDatas.Add(new Item(10015, "Teachings of \"Gold\"", "\"황금\"의 가르침", 1, ItemType.TALENTMATERIAL, Grade.RARE, spritePathTalent + "teaching_of_gold"));
        itemDataFile.itemDatas.Add(new Item(10016, "Guide to \"Gold\"", "\"황금\"의 인도", 1, ItemType.TALENTMATERIAL, Grade.EPIC, spritePathTalent + "guide_to_gold"));
        itemDataFile.itemDatas.Add(new Item(10017, "Philosophies of \"Gold\"", "\"황금\"의 철학", 1, ItemType.TALENTMATERIAL, Grade.UNIQUE, spritePathTalent + "philosophies_of_gold"));

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

        itemDataFile.itemDatas.Add(new Item(10300, "Damaged Mask", "부서진 가면", 1, ItemType.MATERIAL, Grade.NORMAL, spritePathTalent + "damaged_mask"));
        itemDataFile.itemDatas.Add(new Item(10301, "Stained Mask", "오염된 가면", 1, ItemType.MATERIAL, Grade.RARE, spritePathTalent + "stained_mask"));
        itemDataFile.itemDatas.Add(new Item(10302, "Ominous Mask", "불길한 가면", 1, ItemType.MATERIAL, Grade.EPIC, spritePathTalent + "ominous_mask"));
        itemDataFile.itemDatas.Add(new Item(10303, "Divining Scroll", "이능 두루마리", 1, ItemType.MATERIAL, Grade.NORMAL, spritePathTalent + "divining_scroll"));
        itemDataFile.itemDatas.Add(new Item(10304, "Sealed Scroll", "봉마의 두루마리", 1, ItemType.MATERIAL, Grade.RARE, spritePathTalent + "sealed_scroll"));
        itemDataFile.itemDatas.Add(new Item(10305, "Forbidden Curse Scroll", "금주의 두루마리", 1, ItemType.MATERIAL, Grade.EPIC, spritePathTalent + "forbidden_curse_scroll"));
        itemDataFile.itemDatas.Add(new Item(10306, "Firm Arrowhead", "견고한 화살촉", 1, ItemType.MATERIAL, Grade.NORMAL, spritePathTalent + "firm_arrowhead"));
        itemDataFile.itemDatas.Add(new Item(10307, "Sharp Arrowhead", "날카로운 화살촉", 1, ItemType.MATERIAL, Grade.NORMAL, spritePathTalent + "sharp_arrowhead"));
        itemDataFile.itemDatas.Add(new Item(10308, "Weathered Arrowhead", "역전의 화살촉", 1, ItemType.MATERIAL, Grade.NORMAL, spritePathTalent + "weathered_arrowhead"));

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
