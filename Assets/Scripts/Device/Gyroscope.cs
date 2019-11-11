using UnityEngine;
using System.Collections;

public class Gyroscope : Device
{

    private void Awake()
    {
        axisName = new string[] { "lastGyX", "lastGyY", "lastGyZ" };
    }
}
