using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin;
    public float zoomOutMax;

    public Vector2 pivot;

    public float actionPincTtimer;

    // Update is called once per frame
    void Update()
    {
        if (ResourceMenuTab.instance.isMenuTabOn)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && actionPincTtimer <= 0)
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);

            actionPincTtimer = 0.5f;
        }
        else if (Input.GetMouseButton(0) && actionPincTtimer <= 0)
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));

        FixOverFrontier(pivot);

        if (actionPincTtimer > 0)
        {
            actionPincTtimer -= Time.deltaTime;
        }
    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

    private void FixOverFrontier(Vector2 pivot)
    {
        Vector3 cameraPos = Camera.main.transform.position;

        if (cameraPos.x > pivot.x)
        {
            Camera.main.transform.position = new Vector3(pivot.x, cameraPos.y, cameraPos.z);
        }
        if (cameraPos.x < -pivot.x)
        {
            Camera.main.transform.position = new Vector3(-pivot.x, cameraPos.y, cameraPos.z);
        }

        if (cameraPos.y > pivot.y)
        {
            Camera.main.transform.position = new Vector3(cameraPos.x, pivot.y, cameraPos.z);
        }
        if (cameraPos.y < -pivot.y)
        {
            Camera.main.transform.position = new Vector3(cameraPos.x, -pivot.y, cameraPos.z);
        }
    }
}
