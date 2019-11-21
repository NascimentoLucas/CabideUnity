using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Accelerometer : Device
{
    [SerializeField]
    private Text textFeedbackStatus;
    [SerializeField]
    private Text textFeedAxis;
    [SerializeField]
    private Text textFeedStableAxis;

    private Vector3 stableAxis = Vector3.zero;

    [SerializeField]
    private float distance;
    private float actualDistance;

    private int actualState;

    private bool run = false;

    private void Awake()
    {
        axisName = new string[] { "lastAcX", "lastAcY", "lastAcZ" };
    }

    private void Start()
    {
        textFeedStableAxis.text = "Stable Axis: " + stableAxis.ToString();
    }

    private void Update()
    {
        if (run)
        {
            actualDistance = Vector3.Distance(stableAxis, axis);
            if (actualDistance > distance)
            {
                textFeedbackStatus.text = "Moved. Distance to change: " + (distance - actualDistance);
            }
            else
            {
                textFeedbackStatus.text = "Stable. Distance to change: " + (distance - actualDistance);
            }
            textFeedStableAxis.text = "Stable Axis: " + stableAxis.ToString();
        }
        textFeedAxis.text = "Actual Axis: " + axis.ToString();

    }

    public void SetStableAxis()
    {
        run = true;
        stableAxis = axis;
    }
}
