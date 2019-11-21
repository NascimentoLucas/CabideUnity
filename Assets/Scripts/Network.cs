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
    private Button buttonStartListen;
    [SerializeField]
    private Button buttonStoptListen;
    [SerializeField]
    private Button buttonSetStableAxis;

    [Header("Setup")]
    [SerializeField]
    private SetupObject urlSetup;

    [Header("Info")]
    [SerializeField]
    private string[] dataReceived;

    [SerializeField]
    private string url;

    private bool run;

    private void Awake()
    {
        urlSetup.updateVariable = data => {
            url = data.ToString();
        };
    }

    private void Start()
    {
        run = false;
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

    public string[] DataReceived { get => dataReceived; set => dataReceived = value; }
}
