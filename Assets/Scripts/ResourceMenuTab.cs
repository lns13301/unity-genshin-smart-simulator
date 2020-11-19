using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResourceMenuTab : MonoBehaviour
{
    public static ResourceMenuTab instance;

    public GameObject[] tabButtons;

    public GameObject menuSet;
    public GameObject resourceFrame;
    public GameObject content;

    public List<GameObject> resourceParents;

    public bool isMenuTabOn;

    public Text[] tabTexts;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Canvas").gameObject.SetActive(true);

        instance = this;

        resourceParents = ScaleController.instance.resourceParents;

        isMenuTabOn = false;

        SetTextLanguage();
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
        GetComponent<Animator>().SetBool("isMenuOn", true);

        isMenuTabOn = true;
    }

    public void OffMenuTab()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        SetMenuContentDestroy();
        GetComponent<Animator>().SetBool("isMenuOn", false);

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

    public void ButtonSetMenuContent(int slotIndex)
    {
        SetMenuContentDestroy();
        SoundManager.instance.PlayOneShotEffectSound(2);

        for (int i = 0; i < resourceParents.Count; i++)
        {
            ResourceData resourceData = MakeResourceData(resourceParents[i].GetComponent<ResourceParent>().resourceData);

            if (slotIndex == 0 && resourceData.type != ItemType.MATERIAL_MINERAL)
            {
                continue;
            }
            else if (slotIndex == 1 && (resourceData.type != ItemType.MATERIAL_MONDSTADT && resourceData.type != ItemType.MATERIAL_LIYUE))
            {
                continue;
            }
            else if (slotIndex == 2 && resourceData.type != ItemType.MATERIAL_FOOD)
            {
                continue;
            }
            else if (slotIndex == 3 && resourceData.type != ItemType.MATERIAL)
            {
                continue;
            }
            else if (slotIndex == 4 && resourceData.type != ItemType.MONSTER)
            {
                continue;
            }
            else  if (slotIndex == 5 && (resourceData.type == ItemType.MATERIAL_MINERAL || resourceData.type == ItemType.MATERIAL_MONDSTADT || resourceData.type == ItemType.MATERIAL_LIYUE
                 || resourceData.type == ItemType.MATERIAL_FOOD || resourceData.type == ItemType.MATERIAL || resourceData.type == ItemType.MONSTER))
            {
                continue;
            }

            GameObject go = Instantiate(resourceFrame);
            go.GetComponent<ResourceFrame>().resourceTransformIndex = resourceParents[i].GetComponent<ResourceParent>().resourceTransformIndex;

            go.transform.SetParent(content.transform);
            go.GetComponent<ResourceFrame>().SetResourceWithBaseSetting(resourceData);
            // go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }

    public void OnItemCount(int value)
    {
        SoundManager.instance.PlayOneShotEffectSound(2);

        for (int i = 0; i < resourceParents.Count; i++)
        {
            if (resourceParents[i].GetComponent<ResourceParent>().isGameObjectOn)
            {
                for (int j = 0; j < resourceParents[i].transform.childCount; j++)
                {
                    if (resourceParents[i].transform.GetChild(j).GetComponent<Resource>().resourceData.count < value)
                    {
                        resourceParents[i].transform.GetChild(j).gameObject.SetActive(false);
                    }
                    else
                    {
                        resourceParents[i].transform.GetChild(j).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void ResetItemCount()
    {
        for (int i = 0; i < resourceParents.Count; i++)
        {
            if (resourceParents[i].GetComponent<ResourceParent>().isGameObjectOn)
            {
                for (int j = 0; j < resourceParents[i].transform.childCount; j++)
                {
                    resourceParents[i].transform.GetChild(j).gameObject.SetActive(true);
                }
            }
        }
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void SetTextLanguage()
    {
        if (LanguageManager.instance.language == Language.KOREAN)
        {
            tabTexts[0].text = "광물";
            tabTexts[1].text = "특산물";
            tabTexts[2].text = "요리 재료";
            tabTexts[3].text = "연금술 재료";
            tabTexts[4].text = "몬스터";
            tabTexts[5].text = "그 외";
            tabTexts[6].text = " 채 집";
            tabTexts[7].text = " 취 소";
            tabTexts[8].text = " 취 소";
            tabTexts[9].text = " 확 인";
        }
        else
        {
            tabTexts[0].text = "Mineral";
            tabTexts[1].text = "Local Specialty";
            tabTexts[2].text = "Food Material";
            tabTexts[3].text = "Alchemy Material";
            tabTexts[4].text = "Monster";
            tabTexts[5].text = "ETC";
            tabTexts[6].text = " Looting";
            tabTexts[7].text = " Cancel";
            tabTexts[8].text = " Cancel";
            tabTexts[9].text = " Confirm";
        }
    }
}
