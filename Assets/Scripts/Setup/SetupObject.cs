using UnityEngine;
using UnityEngine.UI;

public class SetupObject : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Text configText;
    [SerializeField]
    private InputField input;
    [SerializeField]
    private Button buttonSave;

    [SerializeField]
    private DataKeys key;

    private Data.DataModification father;

    private string data;
    private string configOriginalText;

    private void Start()
    {
        data = Data.GetInstance().GetDataInfo(key);
        father(data);
        configOriginalText = configText.text;
        UpdateTextSetup();
        buttonSave.onClick.AddListener(SaveNew);
    }

    private void UpdateTextSetup()
    {
        configText.text = "Saved: \"" + data + "\"!.\n" + configOriginalText;
    }

    public void SaveNew()
    {
        data = input.text;
        input.text = "";
        Data.GetInstance().SetDataInfo(key, data);
        UpdateTextSetup();
        father(data);
    }

    public Data.DataModification updateVariable { get => father; set => father = value; }
}
