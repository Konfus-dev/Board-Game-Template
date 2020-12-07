using System.Collections;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    #region globals
    public GameObject grabbedObj = null;

    internal Player player;
    internal Camera playerCamera;
    internal Vector3 mouseWorldPos;
    #endregion

    private void Awake()
    {
        mouseWorldPos = Vector3.zero;
        player = this.GetComponent<Player>();
        playerCamera = Camera.main;
    }

    private void Update()
    {
        Plane plane = new Plane(Vector3.up, 0);

        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            mouseWorldPos = ray.GetPoint(distance);
        }
    }

    public bool TryGrab()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Grabbable"))
            {
                grabbedObj = hit.transform.gameObject;
                return true;
            }
        }
        return false;
    }

    public IEnumerator GrabObj()
    {
        if (grabbedObj == null)
            yield break;

        grabbedObj.GetComponent<Collider>().enabled = false;
        grabbedObj.tag = "Untagged";

        Vector3 velocity = Vector3.zero;
        Grabbable grabbable = grabbedObj.GetComponent<Grabbable>();

        StartCoroutine(grabbable.RotateToGrabOrientation(.25f));
        grabbable.OnPickUp();

        while (player.input.leftInteract)
        {
            if (Input.GetMouseButtonDown(1) && grabbable != null)
                grabbable.OnInteract();

            grabbedObj.GetComponent<Rigidbody>().velocity =
                    Vector3.SmoothDamp(grabbedObj.GetComponent<Rigidbody>().velocity, 
                    (mouseWorldPos - grabbedObj.transform.position + Vector3.up/3) * 15, ref velocity, 0.05f);
            yield return null;
        }

        grabbedObj.GetComponent<Collider>().enabled = true;
        grabbedObj.tag = "Grabbable";
        grabbedObj = null;
    }
}
