using UnityEngine;
using System.Collections;

public enum DataKeys
{
    keyUrl,
    keyDistance,
}

public class Data : MonoBehaviour
{
    public delegate void DataModification(string data);

    private static Data Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;


        }
        else
        {
            Destroy(this);
        }
    }

    public static Data GetInstance()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<Data>();
            if (Instance == null)
            {
                GameObject g = new GameObject();
                g.AddComponent<Data>();
            }
        }

        return Instance;
    }
   
    public string GetDataInfo(DataKeys key)
    {
        return PlayerPrefs.GetString(key.ToString(), "keyDidNotFind");
    }

    public void SetDataInfo(DataKeys key, string value)
    {
        PlayerPrefs.SetString(key.ToString(), value);
    }
}
