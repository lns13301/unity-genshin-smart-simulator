using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasResolutionManager : MonoBehaviour
{
    public static CanvasResolutionManager instance;
    public Rect pivot;
    public RectTransform[] canvas;

    public RectTransform[] component;
    public GameObject resultImageParent;
    public Transform resultButtonInformation;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        Debug.Log(Screen.width + ", " + Screen.height);
        pivot = GameObject.Find("Canvas").GetComponent<RectTransform>().rect;

        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pivot.width);
            canvas[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pivot.height);
        }
    }

    public void SetResolution(RectTransform target)
    {
        target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, target.rect.width * (Screen.width / 1920));
        target.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, target.rect.height * (Screen.height / 1080));
    }
}
