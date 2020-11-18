using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMenuTab : MonoBehaviour
{
    public static ResourceMenuTab instance;

    public GameObject[] tabButtons;

    public GameObject menuSet;
    public GameObject resourceFrame;
    public GameObject content;

    public List<GameObject> resourceParents;

    public bool isMenuTabOn;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        resourceParents = ScaleController.instance.resourceParents;

        isMenuTabOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMenuContent()
    {
        SetMenuContentDestroy();

        for (int i = 0; i < resourceParents.Count; i++)
        {
            ResourceData resourceData = MakeResourceData(resourceParents[i].GetComponent<ResourceParent>().resourceData);
            GameObject go = Instantiate(resourceFrame);
            go.GetComponent<ResourceFrame>().resourceTransformIndex = resourceParents[i].GetComponent<ResourceParent>().resourceTransformIndex;

            go.transform.SetParent(content.transform);
            go.GetComponent<ResourceFrame>().SetResourceWithBaseSetting(resourceData);
            // go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }

    private void SetMenuContentDestroy()
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
        SetMenuContentDestroy();
        SetMenuContent();
        GameManager.instance.SetResources();
    }

    public void SortResourceList(List<ResourceParent> resourceParents)
    {
        resourceParents.Sort((a, b) => b.resourceData.code.CompareTo(a.resourceData.code));
    }

    public ResourceData MakeResourceData(ResourceData rd)
    {
        return new ResourceData(rd.code, rd.position, rd.enName, rd.koName, rd.code, rd.type, rd.grade, rd.regenTime, rd.information, false, rd.expiredTime, rd.spritePath);
    }

    public void OnMenuTab()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        SetMenuContent();

        isMenuTabOn = true;
    }

    public void OffMenuTab()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        SetMenuContentDestroy();

        isMenuTabOn = false;
    }

    public void ButtonOnOff()
    {
        if (isMenuTabOn)
        {
            OffMenuTab();
        }
        else
        {
            OnMenuTab();
        }
    }
}
