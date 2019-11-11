using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device : MonoBehaviour
{
    private const float adjust = 1500;

    [SerializeField]
    protected Vector3 axis = Vector3.zero;

    protected string[] axisName;

    [SerializeField]
    protected float distanceMin;

    public virtual void UpdateAxis(string[] data)
    {
        for (int i = 0; i < axisName.Length; i++)
        {
            foreach (string d in data)
            {
                if (d.ToLower().Contains(axisName[i].ToLower()))
                {
                    string[] split = d.Split(Network.charValueSpliter);
                    string value = "0";
                    if (split.Length > 0)
                    {
                        value = split[split.Length - 1];
                    }

                    float finalValue;
                    try
                    {
                        finalValue = float.Parse(value);
                    }
                    catch
                    {
                        finalValue = 0;
                        Debug.LogError("Value could not parse to float. Value =>" + value);
                    }
                    finalValue += adjust;
                    if (i == 0)
                    {
                        if (Mathf.Abs(axis.x - finalValue) > distanceMin)
                        {
                            axis.x = finalValue;
                        }
                    }
                    else if (i == 1)
                    {
                        if (Mathf.Abs(axis.y - finalValue) > distanceMin)
                        {
                            axis.y = finalValue;
                        }
                    }
                    else if (i == 2)
                    {
                        if (Mathf.Abs(axis.z - finalValue) > distanceMin)
                        {
                            axis.z = finalValue;
                        }
                    }
                }
            }
        }
    }
}
