using System.Collections;
using UnityEngine;

public abstract class Grabbable : MonoBehaviour
{
    #region globals
    public GrabbableData data;
    protected AudioSource grabbableAuidoEmitter;
    private bool isRotating = false;
    #endregion

    protected virtual void Start()
    {
        data.rb = this.GetComponent<Rigidbody>();
        grabbableAuidoEmitter = this.GetComponent<AudioSource>();

        data.restingPos = this.transform.position;
    }

    public virtual void OnPickUp()
    {
        PlaySound(data.pickupSound);
    }

    public virtual void OnInteract()
    {
        PlaySound(data.interactSound);
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
        while ((this.transform.rotation != Quaternion.Euler(data.holdingOrientation)) && (counter < duration))
        {
            counter += Time.deltaTime;
            this.transform.rotation = Quaternion.Slerp(startRot, Quaternion.Euler(data.holdingOrientation), counter / duration);
            yield return null;
        }

        this.transform.rotation = Quaternion.Euler(data.holdingOrientation);
        data.rb.isKinematic = false;
    }

    private void FixedUpdate()
    {
        if (data.rb.IsSleeping())
        {
            data.restingPos = this.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlaySound(data.impactSound);
    }

    private void OnCollisionExit(Collision collision)
    {
        PlaySound(data.impactSound);
    }

    private void PlaySound(AudioClip clip)
    {
        grabbableAuidoEmitter.Stop();
        grabbableAuidoEmitter.pitch = Random.Range(1f, 1.5f);
        grabbableAuidoEmitter.volume = Random.Range(.3f, .6f);
        grabbableAuidoEmitter.PlayOneShot(clip);
    }
}
