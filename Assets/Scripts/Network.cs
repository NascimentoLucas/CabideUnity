using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
public class Main : MonoBehaviour
{
    private const char charSpliter = ';';

    [SerializeField]
    private Text text;
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
            text.text += www.downloadHandler.text + "\n";
            data = www.downloadHandler.text.Split(charSpliter);


            if (run)
            {
                yield return new WaitForSeconds(1);
                StartCoroutine(GetText());
            }
            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
        }
    }
}
