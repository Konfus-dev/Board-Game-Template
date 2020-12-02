using System;
using UnityEngine;

[Serializable]
public class GrabbableData
{
    #region globals
    [HideInInspector]
    public Vector3 restingPos;
    [HideInInspector]
    public Rigidbody rb;

    public AudioClip pickupSound;
    public AudioClip interactSound;
    public AudioClip impactSound;
    public Vector3 handOffset;
    public Vector3 holdingOrientation;
    #endregion
}
