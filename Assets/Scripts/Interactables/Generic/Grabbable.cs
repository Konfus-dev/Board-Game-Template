using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grabbable : MonoBehaviour
{
    #region globals
    public GrabbableData data;
    private bool isRotating = false;
    #endregion

    public abstract void OnRightClick();

    protected void Start()
    {
        data.rb = this.GetComponent<Rigidbody>();
        data.restingPos = this.transform.position;
    }

    public IEnumerator RotateToGrabOrientation(float duration)
    {
        //Make sure there is only one instance of this function running
        if (isRotating)
        {
            yield break; ///exit if this is still running
        }

        float counter = 0;

        //Get the current position of the object to be moved
        Quaternion startRot = this.transform.localRotation;

        data.rb.isKinematic = true;

        //Rotate card
        while ((this.transform.localRotation != Quaternion.Euler(data.holdingOrientation)) && (counter < duration))
        {
            counter += Time.deltaTime;
            this.transform.localRotation = Quaternion.Slerp(startRot, Quaternion.Euler(data.holdingOrientation), counter / duration);
            yield return null;
        }

        this.transform.localRotation = Quaternion.Euler(data.holdingOrientation);
        data.rb.isKinematic = false;
    }

    private void FixedUpdate()
    {
        if (data.rb.IsSleeping())
        {
            data.restingPos = this.transform.position;
        }
    }
}
