using UnityEngine;
using System.Collections;

public class ManagerDevice : MonoBehaviour
{
    public static ManagerDevice Instace { get; private set; }
    

    [SerializeField]
    private Device[] devices;

    private void Awake()
    {
        if(Instace == null)
        {
            Instace = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void UpdateDevices(string[] data)
    {
        foreach (Device device in devices)
        {
            device.UpdateAxis(data);
        }
    }
}
