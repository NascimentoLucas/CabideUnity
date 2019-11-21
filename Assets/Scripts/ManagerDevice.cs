using UnityEngine;
using System.Collections;

public class ManagerDevice : MonoBehaviour
{    
    [SerializeField]
    private Device[] devices;

    public void UpdateDevices(string[] data)
    {
        foreach (Device device in devices)
        {
            device.UpdateAxis(data);
        }
    }
}
