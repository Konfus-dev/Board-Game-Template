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

    public Vector3 handOffset;
    public Vector3 holdingOrientation;
    #endregion
}
