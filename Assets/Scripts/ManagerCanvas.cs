using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCanvas : MonoBehaviour
{
    [SerializeField]
    private Canvas actualCanvas;

    void Start()
    {
        actualCanvas.enabled = true;
    }

    
    public void ChangeCanvas(Canvas newCanvas)
    {
        if(actualCanvas != null)
        {
            actualCanvas.enabled = false;
        }

        actualCanvas = newCanvas;

        if (actualCanvas != null)
        {
            actualCanvas.enabled = true;
        }
    }
}
