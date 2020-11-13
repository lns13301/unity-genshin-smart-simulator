using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{
    public Camera camera;
    public Transform item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        item.localScale = new Vector3(camera.orthographicSize / 8, camera.orthographicSize / 8);
    }
}
