using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region globals
    public bool rightInteract = false;
    public bool leftInteract = false;
    public bool magnify = false;
    public float zoomInput = 0.0f;
    #endregion

    private void Update()
    {
        if (Input.GetButton("Right Interact")) rightInteract = true;
        else rightInteract = false;

        if (Input.GetButton("Left Interact")) leftInteract = true;
        else leftInteract = false;

        if (Input.GetButtonDown("Magnify")) magnify = true;
        else magnify = false;
    }
}
