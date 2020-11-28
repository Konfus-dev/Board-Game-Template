
using UnityEngine;

public class OutOfBoundsReset : MonoBehaviour
{

    public Vector3 restingPos;
    private Rigidbody rb;

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
