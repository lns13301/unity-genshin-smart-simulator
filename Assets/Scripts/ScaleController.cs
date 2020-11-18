using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{
    public static ScaleController instance;

    public Camera camera;
    public List<GameObject> resourceParents;
    public List<Transform> resources;

    // Start is called before the first frame update
    void Start()
    {
        int totalIndex = 0;

        instance = this;

        for (int h = 1; h < transform.childCount; h++)
        {
            for (int i = 0; i < transform.GetChild(h).childCount; i++)
            {
                try
                {
                    transform.GetChild(h).GetChild(i).GetComponent<ResourceParent>().resourceData =
                        transform.GetChild(h).GetChild(i).GetChild(0).GetComponent<Resource>().resourceData;

                    transform.GetChild(h).GetChild(i).GetComponent<ResourceParent>().resourceTransformIndex = totalIndex++;
                    transform.GetChild(h).GetChild(i).gameObject.SetActive(false);
                }
                catch
                {

                }

                resourceParents.Add(transform.GetChild(h).GetChild(i).gameObject);

                for (int j = 0; j < transform.GetChild(h).GetChild(i).childCount; j++)
                {
                    resources.Add(transform.GetChild(h).GetChild(i).GetChild(j).transform);

                    if (resources[i].GetComponent<Resource>().resourceData.isLooted)
                    {
                        resources[i].GetComponent<SpriteRenderer>().sortingOrder = -1;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < resources.Count; i++)
        {
            resources[i].localScale = new Vector3(camera.orthographicSize / 6, camera.orthographicSize / 6);
        }
    }
}
