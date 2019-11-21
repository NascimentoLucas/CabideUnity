using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Accelerometer : Device
{
    [Header("UI")]
    [SerializeField]
    private Text textFeedbackStatus;
    [SerializeField]
    private Text textFeedAxis;
    [SerializeField]
    private Text textFeedStableAxis;
    [SerializeField]
    private Slider slider;

    private Vector3 stableAxis = Vector3.zero;

    [Header("Setup")]
    [SerializeField]
    private SetupObject distanceSetup;

    [Header("Info")]
    [SerializeField]
    private float distance;
    private float actualDistance;

    private int actualState;

    private bool run = false;

    private void Awake()
    {
        axisName = new string[] { "lastAcX", "lastAcY", "lastAcZ" };
        distanceSetup.updateVariable = data =>
        {
            try
            {
                distance = float.Parse(data);
            }
            catch 
            {
                distance = 1000;
            }
        };
    }

    private void Start()
    {
        textFeedStableAxis.text = "Stable Axis: " + stableAxis.ToString();
        try
        {
            distance = float.Parse(Data.GetInstance().GetDataInfo(DataKeys.keyUrl));

        }
        catch
        {
            distance = 1000;
        }

        slider.minValue = distance * 0.5f;
        slider.maxValue = distance * 2;
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
                textFeedbackStatus.text = "Stable";
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