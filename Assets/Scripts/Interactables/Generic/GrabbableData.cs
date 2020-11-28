
using UnityEngine;

public class GrabbableData : MonoBehaviour
{
    #region globals
    public Vector3 grabRotatation;
    public Vector3 restingPos;
    private Rigidbody rb;
    #endregion

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.IsSleeping())
        {
            restingPos = this.transform.position;
        }
    }
}
