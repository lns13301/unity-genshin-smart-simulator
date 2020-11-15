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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for (int i = 0; i < 5; i++)
        {
            texts[i] = transform.GetChild(i).GetComponent<Text>();
            texts[i].supportRichText = true;
        }

        gameObject.SetActive(false);
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
        SoundManager.instance.PlayOneShotEffectSound(5);
        resource.SetLootedTime();

        gameObject.SetActive(false);
    }

    public void CancelLooting()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        resource.CancelLooting();

        gameObject.SetActive(false);
    }
}
