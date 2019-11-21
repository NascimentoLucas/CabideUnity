using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCanvas : MonoBehaviour
{ 
    [SerializeField]
    private Canvas menu;
    private Canvas actualCanvas;

    void Start()
    {
        Canvas[] c = FindObjectsOfType<Canvas>();

        foreach (Canvas canvas in c)
        {
            canvas.enabled = false;
        }

        actualCanvas = menu;
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

    public void GoToMenu()
    {
        ChangeCanvas(menu);
    }
}
