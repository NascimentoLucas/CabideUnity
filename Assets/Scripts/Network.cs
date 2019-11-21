using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Network : MonoBehaviour
{
    private const char charSpliter = ';';
    public const char charValueSpliter = '=';

    [SerializeField]
    private ManagerDevice managerDevice;

    [SerializeField]
    private Text configText;

    [SerializeField]
    private string[] dataReceived;

    private string configOriginalText;
        
    private string url;

    private bool run;


    private void Start()
    {
        run = false;
        url = Data.GetInstance().GetDataInfo(Data.keyUrl);
        configOriginalText = configText.text;
        UpdateTextSetup();
    }

    public void StartListen()
    {
        if (!run)
        {
            Debug.Log("Start Listen: " + url);
            run = true;
            StartCoroutine(GetText()); 
        }
    }

    public void StopListen()
    {
        run = false;
    }


    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            dataReceived = www.downloadHandler.text.Split(charSpliter);
            managerDevice.UpdateDevices(dataReceived);

            if (run)
            {
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(GetText());
            }
            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
        }
    }

    private void UpdateTextSetup()
    {
        configText.text = "Saved: \"" + url + "\"!.\n" + configOriginalText;
    }

    public void SaveNewUrl(InputField input)
    {
        url = input.text;
        input.text = "";
        Data.GetInstance().SetDataInfo(Data.keyUrl, url);
        UpdateTextSetup();
    }

    public string[] DataReceived { get => dataReceived; set => dataReceived = value; }
}
