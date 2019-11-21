using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Accelerometer : Device
{
    [SerializeField]
    private Text text;

    private Vector3 stableAxis;

    [SerializeField]
    private float distance;
    private float actualDistance;

    private int actualState;

    private bool run = false;

    private void Awake()
    {
        axisName = new string[] { "lastAcX", "lastAcY", "lastAcZ" };
    }

    private void Update()
    {
        if (run)
        {
            actualDistance = Vector3.Distance(stableAxis, axis);
            if (actualDistance > distance)
            {
                text.text = "Mexeu: " + (distance - actualDistance);
            }
            else
            {
                text.text = "Parado: " + actualDistance;
            }
        }
    }

    public void SetStableAxis()
    {
        run = true;
        stableAxis = axis;
    }
}
