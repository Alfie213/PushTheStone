using System;
using UnityEngine;

public class MouseDetector : MonoBehaviour
{
    private void OnMouseEnter()
    {
        Debug.Log("mouse enter");
    }

    private void OnMouseExit()
    {
        Debug.Log("mouse exit");
    }
}
