using UnityEngine;
using UnityEngine.UI;
using System.Collections;

enum State
{
    WORKING,
    MOVED
}

public class Accelerometer : Device
{
    private const int max= 15000, min = -1500;

    [SerializeField]
    private Text text;

    private Vector3 lastAxis;

    private State state;

    private void Awake()
    {
        axisName = new string[] { "lastAcX", "lastAcY", "lastAcZ" };
    }

    private void Update()
    {
        if ((axis.x > max / 2 || axis.x < min / 2) )
        {
            state = State.WORKING;
        }
        else
        {
            state = State.MOVED;
        }
        lastAxis = axis;
        text.text = state.ToString();
    }
}
