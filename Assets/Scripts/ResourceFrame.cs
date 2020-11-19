using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceFrame : MonoBehaviour
{
    private string NEW_LINE = "\n";
    public ResourceData resourceData;

    public Image resourceImage;
    public Text resourceName;

    public int resourceTransformIndex;

    // Start is called before the first frame update
    void Start()
    {
        SetButtonColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetResourceWithBaseSetting(ResourceData resourceData)
    {
        SetResourceData(resourceData);

        if (LanguageManager.instance.language == Language.KOREAN)
        {
            resourceName.text = resourceData.koName;
        }
        else
        {
            resourceName.text = resourceData.enName;
        }

        Invoke("SetImage", 0.1f);
    }

    public void SetImage()
    {
        resourceImage.sprite = resourceData.sprite;
    }

    public void SetResourceData(ResourceData resourceData)
    {
        this.resourceData = resourceData;
    }

    public void ButtonOnOff()
    {
        if (ScaleController.instance.resourceParents[resourceTransformIndex].activeSelf)
        {
            SoundManager.instance.PlayOneShotEffectSound(3);
            ScaleController.instance.resourceParents[resourceTransformIndex].SetActive(false);
            ScaleController.instance.resourceParents[resourceTransformIndex].GetComponent<ResourceParent>().isGameObjectOn = false;
        }
        else
        {
            SoundManager.instance.PlayOneShotEffectSound(1);
            ScaleController.instance.resourceParents[resourceTransformIndex].SetActive(true);
            ScaleController.instance.resourceParents[resourceTransformIndex].GetComponent<ResourceParent>().isGameObjectOn = true;
        }

        SetButtonColor();
        ResourceMenuTab.instance.ResetItemCount();
    }

    private void SetButtonColor()
    {
        if (ScaleController.instance.resourceParents[resourceTransformIndex].activeSelf)
        {
            GetComponent<Image>().color = new Color(1, 1, 1);
        }
        else
        {
            GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
        }
    }
}
