//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    bool isGrounded;
    public int SideVal;

    private void OnTriggerStay(Collider hit)
    {
        if(hit.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if(hit.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
