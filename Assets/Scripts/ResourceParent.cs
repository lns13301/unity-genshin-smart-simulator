using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ResourceParent : MonoBehaviour
{
    public string resourceName;
    public ResourceSaveDataFile resourceSaveDataFile;
    public bool isMobile = true;

    public ResourceData resourceData;

    public int resourceTransformIndex;

    // public GameObject resourcePrefab;

    // Start is called before the first frame update
    void Start()
    {
        resourceSaveDataFile = new ResourceSaveDataFile();

        LoadResourceDataFromJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("To Json Data")]
    public void SaveResourceDataToJson()
    {
        Debug.Log("리소스 저장 성공");

        string jsonData = JsonUtility.ToJson(resourceSaveDataFile, true);
        File.WriteAllText(SaveOrLoad(isMobile, true, resourceName), AESCrypto.AESEncrypt128(jsonData));
    }

    [ContextMenu("From Json Data")]
    public void LoadResourceDataFromJson()
    {
        try
        {
            Debug.Log("리소스 정보 로드 성공");
            string jsonData = File.ReadAllText(SaveOrLoad(isMobile, false, resourceName));
            resourceSaveDataFile = JsonUtility.FromJson<ResourceSaveDataFile>(AESCrypto.AESDecrypt128(jsonData));

            for (int i = 0; i < transform.childCount; i++)
            {
                try
                {
                    ResourceSaveData resourceSaveData = resourceSaveDataFile.resourceSaveDatas[i];
                    ResourceData resource = transform.GetChild(i).GetComponent<Resource>().resourceData;

                    resource.index = i;
                    resource.isLooted = resourceSaveData.isLooted;
                    resource.expiredTime = DeserializeDateTime(resourceSaveData.expiredTime);

                    if (resource.isLooted)
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
                    }
                    else
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    }
                }
                catch (Exception)
                {
                    ResourceData resource = transform.GetChild(i).GetComponent<Resource>().resourceData;
                    resource.index = i;

                    ResourceData resourceData = transform.GetChild(i).GetComponent<Resource>().resourceData;

                    resourceSaveDataFile.resourceSaveDatas.Add(
                        new ResourceSaveData(resourceData.enName, resourceData.isLooted, SerializeDateTime(resourceData.expiredTime)));

                    if (resource.isLooted)
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
                    }
                    else
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("리소스 로드 오류");

            string jsonData = JsonUtility.ToJson(resourceSaveDataFile, true);

            for (int i = 0; i < transform.childCount; i++)
            {
                if (resourceSaveDataFile.resourceSaveDatas == null || resourceSaveDataFile.resourceSaveDatas.Count <= i)
                {
                    ResourceData resourceData = transform.GetChild(i).GetComponent<Resource>().resourceData;
                    resourceSaveDataFile.resourceSaveDatas = new List<ResourceSaveData>();

                    resourceSaveDataFile.resourceSaveDatas.Add(
                        new ResourceSaveData(resourceData.enName, resourceData.isLooted, SerializeDateTime(resourceData.expiredTime)));
                }
            }

            File.WriteAllText(SaveOrLoad(isMobile, false, resourceName), AESCrypto.AESEncrypt128(jsonData));
            LoadResourceDataFromJson();
        }
    }

    public string SaveOrLoad(bool isMobile, bool isSave, string fileName)
    {
        if (isSave)
        {
            if (isMobile)
            {
                // 모바일 저장
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 저장
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
        else
        {
            if (isMobile)
            {
                // 모바일 로드
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 로드
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
    }

    public string SerializeDateTime(DateTime dateTime)
    {
        string answer = "";

        answer += dateTime.ToString("yyyy");
        answer += "-";
        answer += dateTime.ToString("MM");
        answer += "-";
        answer += dateTime.ToString("dd");
        answer += "-";
        answer += dateTime.ToString("HH");
        answer += "-";
        answer += dateTime.ToString("mm");
        answer += "-";
        answer += dateTime.ToString("ss");

        return answer;
    }

    public DateTime DeserializeDateTime(string dateTime)
    {
        string[] time = dateTime.Split('-');

        return new DateTime(int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]), int.Parse(time[3]), int.Parse(time[4]), int.Parse(time[5]));
    }
}

[System.Serializable]
public class ResourceSaveDataFile
{
    public List<ResourceSaveData> resourceSaveDatas;
}
