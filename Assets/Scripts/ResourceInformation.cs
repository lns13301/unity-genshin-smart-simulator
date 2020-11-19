using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInformation : MonoBehaviour
{
    public static ResourceInformation instance;

    public Text[] texts;

    public Resource resource;
    public GameObject ButtonLoot;
    public GameObject ButtonCancelLoot;

    public GameObject notice;
    public Text noticeText;

    public int stardustCount;
    public int starlightCount;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for (int i = 0; i < 6; i++)
        {
            texts[i] = transform.GetChild(i).GetComponent<Text>();
            texts[i].supportRichText = true;
        }

        gameObject.SetActive(false);

        noticeText = notice.transform.GetChild(5).GetComponent<Text>();
        noticeText.supportRichText = true;
        notice.SetActive(false);

        texts[6].transform.parent.gameObject.SetActive(false);
        texts[7].transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        resource.ShowLeftTime();
    }

    public void OnInformation(Resource resource)
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        gameObject.SetActive(true);

        this.resource = resource;

        ButtonLoot.SetActive(!resource.resourceData.isLooted);
        ButtonCancelLoot.SetActive(resource.resourceData.isLooted);
    }

    public void OffInformation()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        gameObject.SetActive(false);
    }

    public void DoLooting()
    {
        if (GameManager.instance.GetPlayerData().starDustCount < resource.resourceData.count * (resource.resourceData.regenTime / 43200))
        {
            OnLackOfResource();

            return;
        }

        SoundManager.instance.PlayOneShotEffectSound(5);
        resource.SetLootedTime();

        if (texts[6].transform.parent.gameObject.activeSelf)
        {
            GameManager.instance.GetPlayerData().starDustCount -= stardustCount;
        }
        else
        {
            GameManager.instance.GetPlayerData().starLightCount -= starlightCount;
        }

        GameManager.instance.SavePlayerDataToJson();

        gameObject.SetActive(false);
    }

    public void OnLackOfResource()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            noticeText.text = "보유한 스타더스트의 수량이 부족합니다.\n\n기원 뽑기를 통해 스타더스트를 획득하러 가시겠습니까?";
        }
        else
        {
            noticeText.text = "The number of stardust you have is not enough.\n\nDo you want to play a gacha and go get some stardust?";
        }

        notice.SetActive(true);
    }

    public void OffNotice()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);

        notice.SetActive(false);
    }

    public void YesNotice()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);

        notice.SetActive(false);

        ResourceMenuTab.instance.ChangeScene(1);
    }

    public void CancelLooting()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        resource.CancelLooting();

        gameObject.SetActive(false);
    }
}
