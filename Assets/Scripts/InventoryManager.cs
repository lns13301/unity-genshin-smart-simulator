using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        inventorySet.SetActive(false);
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

        for (int i = 0; i < itemLists[nowTabIndex].Count;)
        {
            Item item = ItemDatabase.instance.findItemByName(itemLists[nowTabIndex][i].koName);

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
            go.GetComponent<ItemFrame>().SetItemWithBaseSetting(ItemDatabase.instance.makeItem(item), ++i);
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            CanvasResolutionManager.instance.SetResolution(go.GetComponent<RectTransform>());
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
    }

    public void ButtonInventoryTab(int index)
    {
        nowTabIndex = index;
        PlayerData playerData = GameManager.instance.GetPlayerData();

        SetInventory();
    }
    public void ButtonInventory()
    {
        GameManager.instance.OffNoticeAll();

        SoundManager.instance.PlayOneShotEffectSound(1);
        inventorySet.SetActive(true);
    }

    public void OffInventory()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        inventorySet.SetActive(false);
    }
}
