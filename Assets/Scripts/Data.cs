using UnityEngine;
using System.Collections;

public class Data : MonoBehaviour
{
    private static Data Instance;

    public const string keyUrl = "keyUrlToAcessArduino";

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
    }//http://192.168.0.35:80

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
   
    public string GetDataInfo(string key)
    {
        return PlayerPrefs.GetString(key, "keyDidNotFind");
    }

    public void SetDataInfo(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
}
