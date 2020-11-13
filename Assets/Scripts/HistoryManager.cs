using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryManager : MonoBehaviour
{
    public static HistoryManager instance;
    public GameObject historySet;
    public GameObject itemFrame;
    public GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        historySet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHistory(List<Item> target)
    {
        SetHistoryDestroy();

        target.Reverse();

        for (int i = 0; i < target.Count;)
        {
            Item item = ItemDatabase.instance.findItemByName(target[i].koName);

            if (item == null)
            {
                target.RemoveAt(i);
                continue;
            }

            if (target[i] == null)
            {
                break;
            }

            GameObject go = Instantiate(itemFrame);

            go.transform.SetParent(content.transform);
            go.GetComponent<ItemFrame>().SetItemWithBaseSetting(ItemDatabase.instance.makeItem(item), ++i, 0f);
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            // CanvasResolutionManager.instance.SetResolution(go.GetComponent<RectTransform>());

            // 타이틀 제목
/*            GameObject title = go.transform.GetChild(0).gameObject;
            title.GetComponent<Text>().text = ItemDatabase.instance.questDB[GameManager.instance.playerData.startQuest[i]].questTitle;*/
        }

        target.Reverse();
    }

    private void SetHistoryDestroy()
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

    public void ButtonHistory()
    {
        GameManager.instance.OffNoticeAll();

        if (GameManager.instance.detail.activeSelf)
        {
            GameManager.instance.detail.SetActive(false);
        }

        if (InventoryManager.instance.inventorySet.activeSelf)
        {
            InventoryManager.instance.inventorySet.SetActive(false);
        }

        SoundManager.instance.PlayOneShotEffectSound(1);
        historySet.SetActive(true);

        int index = BannerManager.instance.onBannerIndex;
        PlayerData playerData = GameManager.instance.GetPlayerData();

        switch (index)
        {
            case 0:
                SetHistory(playerData.noelleHistory);
                break;
            case 1:
                SetHistory(playerData.characterHistory);
                break;
            case 2:
                SetHistory(playerData.weaponHistory);
                break;
            case 3:
                SetHistory(playerData.normalHistory);
                break;
        }
    }

    public void OffHistory()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        historySet.SetActive(false);
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
}
