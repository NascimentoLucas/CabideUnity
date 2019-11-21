using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Network : MonoBehaviour
{
    private const char charSpliter = ';';
    public const char charValueSpliter = '=';

    [Header("Setup")]
    [SerializeField]
    private ManagerDevice managerDevice;

    [Header("UI")]
    [SerializeField]
    private Text configText;
    [SerializeField]
    private Button buttonStartListen;
    [SerializeField]
    private Button buttonStoptListen;
    [SerializeField]
    private Button buttonSetStableAxis;

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
        SetButtonStartListen(true);
    }



    public void StartListen()
    {
        if (!run)
        {
            Debug.Log("Start Listen: " + url);
            run = true;
            StartCoroutine(GetText());
            SetButtonStartListen(false);
        }
    }

    public void StopListen()
    {
        run = false;
        SetButtonStartListen(true);
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

    private void SetButtonStartListen(bool b)
    {
        buttonStartListen.interactable = b;
        buttonStoptListen.interactable = !b;
        buttonSetStableAxis.interactable = !b;
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
