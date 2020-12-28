using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private PlayerData playerData;

    public GameObject inventorySet;
    public GameObject itemFrame;
    public GameObject content;

    public List<List<Item>> itemLists;

    public List<Item> items;
    public List<Item> characters;
    public List<Item> weapons;
    public List<Item> artifacts;
    public List<Item> foods;
    public List<Item> materials;
    public List<Item> questItems;
    public List<Item> adventureItems;

    public int nowTabIndex;

    public GameObject notice;
    public Text noticeText;
    public GameObject information;
    public Text informationText;
    public int nowInformationSlotIndex;

    public string ARE_YOU_SURE;
    public string ARE_YOU_SURE_EN;

    public int noticeState = 0;

    public GameObject stardustPage;
    public int starlightResult;
    public int stardustResult;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    void Start()
    {
        instance = this;
        playerData = GameManager.instance.GetPlayerData();
        items = playerData.items;
        characters = playerData.characters;
        weapons = playerData.weapons;
        artifacts = playerData.artifacts;
        foods = playerData.foods;
        materials = playerData.materials;
        questItems = playerData.questItems;
        adventureItems = playerData.adventureItems;

        itemLists = new List<List<Item>>();
        itemLists.Add(items);
        itemLists.Add(characters);
        itemLists.Add(weapons);
        itemLists.Add(artifacts);
        itemLists.Add(foods);
        itemLists.Add(materials);
        itemLists.Add(questItems);
        itemLists.Add(adventureItems);

        onChangeItem += GameManager.instance.SaveAndLoad;
        onChangeItem += Refresh;

        noticeText = notice.transform.GetChild(5).GetComponent<Text>();
        noticeText.supportRichText = true;
        notice.SetActive(false);

        informationText = information.transform.GetChild(3).GetComponent<Text>();
        informationText.supportRichText = true;
        information.SetActive(false);

        ARE_YOU_SURE = "정말로 하시겠습니까? \n\n 복구가 " + GetColorText("불가능", "d91d2f") + " 합니다.";
        ARE_YOU_SURE_EN = "Are you sure you want to proceed?\n\n Recovery is " + GetColorText("not possible", "d91d2f") + ".";

        information.transform.GetChild(4).gameObject.SetActive(false);
        inventorySet.SetActive(false);

        starlightResult = 0;
        stardustResult = 0;
    }

    public bool AddItem(Item item, int count = 1)
    {
        Item instanceItem = item;
        instanceItem.count = count;
        int quantity = count;

        int itemIndex = GetItemIndexByItem(instanceItem);

        // 아이템이 존재하고 남은 공간에 아이템을 넣을 수 있는지
        items[itemIndex].count += quantity;
        quantity = 0;

        if (quantity == 0)
        {
            onChangeItem.Invoke();

            return true;
        }

        // 기존에 아이템이 없고 아이템을 추가할 슬롯이 있는지
        if (onChangeItem != null)
        {
            items.Add(instanceItem);
            onChangeItem.Invoke();

            return true;
        }

        return false;
    }

    public bool RemoveItem(int slotNumber, int quantity = 1)
    {
        Item instanceItem = items[slotNumber];

        if (quantity == instanceItem.count)
        {
            items.RemoveAt(slotNumber);
            onChangeItem.Invoke();

            return true;
        }

        if (instanceItem.count > quantity)
        {
            instanceItem.count -= quantity;
            onChangeItem.Invoke();

            return true;
        }

        return false;
    }

    public bool RemoveItem(Item item, int quantity = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (quantity > item.count)
            {
                return false;
            }

            if (items[i].code == item.code)
            {
                if (items[i].count >= quantity)
                {
                    if (items[i].count == quantity)
                    {
                        items.RemoveAt(i);
                        onChangeItem.Invoke();

                        return true;
                    }

                    items[i].count -= quantity;
                    onChangeItem.Invoke();

                    return true;
                }
                else
                {
                    quantity -= items[i].count;
                    items.RemoveAt(i);
                }

                if (quantity <= 0)
                {
                    onChangeItem.Invoke();

                    return true;
                }
            }
        }

        onChangeItem.Invoke();

        return true;
    }

    public List<Item> FindItemByTypeAll(List<Item> items, ItemType type)
    {
        List<Item> results = new List<Item>();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].type == type)
            {
                results.Add(items[i]);
            }
        }
        return results.Count > 0 ? items : null;
    }

    public Item FindItemByCode(int itemCode)
    {
        Item item = null;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].code == itemCode)
            {
                item = items[i];

                return item;
            }
        }

        return item;
    }

    public List<int> GetItemIndexByTypeAll(List<Item> item, ItemType type)
    {
        List<int> itemIndexes = new List<int>();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].type == type)
            {
                itemIndexes.Add(i);
            }
        }

        return itemIndexes;
    }

    public int GetItemIndexByItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].code == item.code)
            {
                return i;
            }
        }

        return -1;
    }

    public void ShowInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log("아이템 슬롯 : " + i + "  아이템 이름 : " + items[i].koName + "  아이템 개수 : " + items[i].count);
        }
    }

    public void SetInventory()
    {
        SetInventoryDestroy();

        SortItemList(itemLists[nowTabIndex]);

        for (int i = 0; i < itemLists[nowTabIndex].Count;)
        {
            Item item = ItemDatabase.instance.findItemByCode(itemLists[nowTabIndex][i].code);
            item.count = itemLists[nowTabIndex][i].count;

            if (item == null)
            {
                itemLists[nowTabIndex].RemoveAt(i);
                continue;
            }

            if (itemLists[nowTabIndex][i] == null)
            {
                break;
            }

            GameObject go = Instantiate(itemFrame);

            go.transform.SetParent(content.transform);
            go.GetComponent<ItemFrame>().SetItemWithBaseSetting(ItemDatabase.instance.makeItem(item, item.count), ++i, 0);
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            // CanvasResolutionManager.instance.SetResolution(go.GetComponent<RectTransform>());

            if (item.type == ItemType.CHARACTER)
            {
                go.transform.GetChild(4).GetChild((int)item.character.element).gameObject.SetActive(true); // 속성 켜기
            }
        }
    }

    private void SetInventoryDestroy()
    {
        if (content.transform.childCount == 0)
        {
            return;
        }

        for (int i = content.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(content.transform.GetChild(i).gameObject);
        }
    }

    private void Refresh()
    {
        SetInventoryDestroy();
        SetInventory();
        GameManager.instance.SetResources();
    }

    public void ButtonInventoryTab(int index)
    {
        nowTabIndex = index;
        SoundManager.instance.PlayOneShotEffectSound(1);

        SetInventory(); // 탭 바꾸는걸로는 로딩안하도록 변경하려면 사용
    }
    public void ButtonInventory()
    {
        GameManager.instance.OffNoticeAll();

        SoundManager.instance.PlayOneShotEffectSound(1);
        inventorySet.SetActive(true);

        if (HistoryManager.instance.historySet.activeSelf)
        {
            HistoryManager.instance.historySet.SetActive(false);
        }

        if (GameManager.instance.detail.activeSelf)
        {
            GameManager.instance.detail.SetActive(false);
        }

        SetInventory();
    }

    public void OffInventory()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        inventorySet.SetActive(false);
    }

    public void SortItemList(List<Item> items)
    {
        items.Sort((a, b) => b.code.CompareTo(a.code));
    }

    public void ButtonSetUIFirstPosition()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        content.GetComponent<Transform>().position = new Vector2(1500, content.GetComponent<Transform>().position.y);
    }

    public void ButtonSetUILastPosition()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        content.GetComponent<Transform>().position = new Vector2(-30000, content.GetComponent<Transform>().position.y);
    }

    public void OnNotice(int textCase = 0)
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        notice.SetActive(true);

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            noticeText.text = ARE_YOU_SURE;
        }
        else
        {
            noticeText.text = ARE_YOU_SURE_EN;
        }
    }
    public void OffNotice()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        notice.SetActive(false);
    }

    public void DoRemove()
    {
        GameManager.instance.SaveAndLoad();
    }

    private string GetColorText(string text, string colorValue)
    {
        return "<color=#" + colorValue + ">" + text + "</color>";
    }

    public void ButtonDoBreak(int itemIndex)
    {
        if (SkillManager.instance.item.type == ItemType.CHARACTER)
        {
            return;
        }

        OffInformation();
        // SoundManager.instance.PlayOneShotEffectSound(1);
        notice.SetActive(true);

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            noticeText.text = "선택된 아이템을 " + GetColorText("파괴", GameManager.instance.RED_COLOR) + "하시겠습니까?";
        }
        else
        {
            noticeText.text = "Do you want to " + GetColorText("destroy", GameManager.instance.RED_COLOR) + " selected item.";
        }

        noticeState = 0; // 선택된 아이템 파괴
    }

    public void ButtonDoBreakAll(int grade)
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        notice.SetActive(true);

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            noticeText.text = "정말로 해당등급의 장비아이템을 " + GetColorText("모두 파괴", GameManager.instance.RED_COLOR) + "하시겠습니까?";
        }
        else
        {
            noticeText.text = "Do you want to " + GetColorText("destroy all", GameManager.instance.RED_COLOR) + " selected items.";
        }

        noticeState = grade; // notice state 로 제어
    }


    public void ButtonYes()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        notice.SetActive(false);
        starlightResult = 0;
        stardustResult = 0;

        bool isBreakable = false;

        if (noticeState == 0)
        {
            isBreakable = DoBreakWeapon(nowInformationSlotIndex);
        }
        else if (noticeState == 3)
        {
            isBreakable = DoBreakWeaponAll(Grade.EPIC);
        }
        else if (noticeState == 4)
        {
            isBreakable = DoBreakWeaponAll(Grade.UNIQUE);
        }

        if (!isBreakable)
        {
            return;
        }

        GameManager.instance.SavePlayerDataToJson();
        OnStardustPage();
        SkillManager.instance.OffStatUI();
        Refresh();
    }

    public bool DoBreakWeaponAll(Grade grade)
    {
        bool isBreakable = false;

        for (int i = 0; i < weapons.Count;)
        {
            if (weapons[i].grade == grade)
            {
                SetBreskResultStarDustOrStarLight(weapons[i]);
                weapons.RemoveAt(i);
                isBreakable = true;

                continue;
            }

            // Destroy(content.transform.GetChild(i).gameObject);
            i++;
        }

        return isBreakable;
    }

    public bool DoBreakWeapon(int slotNumber)
    {
        SetBreskResultStarDustOrStarLight(weapons[slotNumber]);
        weapons.RemoveAt(slotNumber);

        return true;
        // Destroy(content.transform.GetChild(slotNumber).gameObject); content 안에 index를 어차피 재정의 해줘야함
    }

    public void OffInformation()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        information.transform.GetChild(4).gameObject.SetActive(false);
        information.SetActive(false);
    }

    public void OnStardustPage()
    {
        SoundManager.instance.PlayOneShotEffectSound(5);

        stardustPage.SetActive(true);
        stardustPage.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "" + starlightResult;
        stardustPage.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "" + stardustResult;
    }

    public void OffStardustPage()
    {
        SoundManager.instance.PlayOneShotEffectSound(2);
        stardustPage.SetActive(false);
    }

    public void SetBreskResultStarDustOrStarLight(Item item)
    {
        if (item.grade == Grade.EPIC)
        {
            stardustResult += 5;
            playerData.starDustCount += 5;
        }
        else if (item.grade == Grade.UNIQUE)
        {
            stardustResult += 30;
            playerData.starDustCount += 30;
        }
        else if (item.grade == Grade.LEGEND)
        {
            starlightResult += 5;
            playerData.starLightCount += 5;
        }
    }
}
