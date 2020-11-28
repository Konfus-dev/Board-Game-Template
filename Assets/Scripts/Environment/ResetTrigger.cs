using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
        if (otherRB != null)
        {
            //Todo: add some smoke effect or something when its reset...
            other.transform.position = other.GetComponent<GrabbableData>().restingPos + Vector3.up;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
