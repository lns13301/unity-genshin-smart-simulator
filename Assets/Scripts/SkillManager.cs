using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public GameObject statSet;
    public Animator animator;
    public Image illust;
    public Text nameText;

    public GameObject skillFrame;
    public GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        animator = statSet.GetComponent<Animator>();

        statSet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStatUI(Item item)
    {
        SetSkillDestroy();

        Language language = LanguageManager.instance.language;
        SoundManager.instance.PlayOneShotEffectSound(1);
        statSet.SetActive(true);

        animator.SetBool("isUIOn", true);

        illust.sprite = item.sprite;
        
        if (language == Language.KOREAN)
        {
            nameText.text = item.koName;
        }
        else
        {
            nameText.text = item.enName;
        }

        if (item.type == ItemType.CHARACTER)
        {
            for (int i = item.character.skillStartCode; i < item.character.skillStartCode + 3; i++)
            {
                GameObject go = Instantiate(skillFrame);

                go.transform.SetParent(content.transform);
                go.GetComponent<SkillFrame>().SetSkillWithBaseSetting(SkillDatabase.instance.findSkillByCode(i));
                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void SetSkillDestroy()
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

    public void OffStatUI()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);

        animator.SetBool("isUIOn", false);

        statSet.SetActive(false);
    }

}
