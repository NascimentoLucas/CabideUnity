using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
public class Network : MonoBehaviour
{
    private const char charSpliter = ';';
    public const char charValueSpliter = '=';

    [SerializeField]
    private string[] data;

    private bool run;


    private void Start()
    {
        run = false;
    }

    public void buttonStart()
    {
        run = true;
        StartCoroutine(GetText());
    }

    public void buttonStop()
    {
        run = false;
    }


    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://192.168.0.35:80");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            data = www.downloadHandler.text.Split(charSpliter);
            ManagerDevice.Instace.UpdateDevices(data);

            if (run)
            {
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(GetText());
            }
            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
        }
    }


    public string[] Data { get => data; set => data = value; }
}
