using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasResolutionManager : MonoBehaviour
{
    public RectTransform[] canvas;

    public RectTransform[] component;
    public GameObject resultImageParent;
    public Transform resultButtonInformation;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
            canvas[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
        }

        for (int i = 0; i < component.Length; i++)
        {
            component[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, component[i].rect.width * (Screen.width / 1920));
            component[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, component[i].rect.height * (Screen.height / 1080));
        }

        for (int i = 0; i < 10; i++)
        {
            Transform now = resultImageParent.transform.GetChild(i);

            now.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal, now.GetChild(0).GetComponent<RectTransform>().rect.width * (Screen.width / 1920));
            now.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical, now.GetChild(0).GetComponent<RectTransform>().rect.height * (Screen.height / 1080));

            for (int j = 0; j < 3; j++)
            {
                now.GetChild(1).GetChild(j).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal, now.GetChild(1).GetChild(j).GetComponent<RectTransform>().rect.width * (Screen.width / 1920));
                now.GetChild(1).GetChild(j).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Vertical, now.GetChild(1).GetChild(j).GetComponent<RectTransform>().rect.height * (Screen.height / 1080));
            }

            for (int j = 0; j < 4; j++)
            {
                now.GetChild(2).GetChild(j).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal, now.GetChild(2).GetChild(j).GetComponent<RectTransform>().rect.width * (Screen.width / 1920));
                now.GetChild(2).GetChild(j).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Vertical, now.GetChild(2).GetChild(j).GetComponent<RectTransform>().rect.height * (Screen.height / 1080));
            }

            for (int j = 0; j < 5; j++)
            {
                now.GetChild(3).GetChild(j).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal, now.GetChild(3).GetChild(j).GetComponent<RectTransform>().rect.width * (Screen.width / 1920));
                now.GetChild(3).GetChild(j).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Vertical, now.GetChild(3).GetChild(j).GetComponent<RectTransform>().rect.height * (Screen.height / 1080));
            }

            for (int j = 0; j < 7; j++)
            {
                now.GetChild(4).GetChild(j).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal, now.GetChild(4).GetChild(j).GetComponent<RectTransform>().rect.width * (Screen.width / 1920));
                now.GetChild(4).GetChild(j).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Vertical, now.GetChild(4).GetChild(j).GetComponent<RectTransform>().rect.height * (Screen.height / 1080));
            }

            // information button size

            for (int j = 0; j < 10; j++)
            {
                resultButtonInformation.GetChild(j).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal, resultButtonInformation.GetChild(j).GetComponent<RectTransform>().rect.width * (Screen.width / 1920));
                resultButtonInformation.GetChild(j).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Vertical, resultButtonInformation.GetChild(j).GetComponent<RectTransform>().rect.height * (Screen.height / 1080));
            }
        }
    }
}
