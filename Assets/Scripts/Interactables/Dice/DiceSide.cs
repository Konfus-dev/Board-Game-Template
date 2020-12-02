//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    public int SideVal;

    private void OnTriggerEnter(Collider hit)
    {
        this.transform.parent.GetComponent<AudioSource>()
            .PlayOneShot(this.transform.parent.GetComponent<Grabbable>().data.impactSound);
    }
}
