using System.Collections;
using UnityEngine;

public class Hand : MonoBehaviour
{
    #region globals
    public static Hand Instance = null;

    public Camera cam;
    public float moveSpeed = 5f;
    public float maxDist = 25;
    public float minDist = -25;
    public bool playGrabAnim = false;
    public bool playPointAnim = false;
    public GameObject grabbedObj = null;

    private Plane plane = new Plane(Vector3.up, 0);
    private float dist = 20;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        MoveHand();
        PlayPointAnim();
        PlayGrabAnim();

        if (Input.GetMouseButtonDown(0) && grabbedObj == null && TryGrab())
        {
            StartCoroutine(GrabObj());
        }
        else if (Input.GetMouseButtonUp(0) && grabbedObj != null)
        {
            DropObj();
        }
    }

    private void MoveHand()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out dist))
        {
            Vector3 point = ray.GetPoint(dist);
            Vector3 targetPos = new Vector3(Mathf.Clamp(point.x, minDist, maxDist), 0f, Mathf.Clamp(point.z, minDist, maxDist));
            transform.position = targetPos;
        }
    }

    private void PlayGrabAnim()
    {
        if (Input.GetMouseButtonDown(0)) playGrabAnim = true;
        if (Input.GetMouseButtonUp(0)) playGrabAnim = false;
    }

    private void PlayPointAnim()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Grabbable") || hit.transform.CompareTag("Clickable"))
            {
                playPointAnim = true;
            }
            else playPointAnim = false;
        }
    }

    private bool TryGrab()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
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

    private IEnumerator GrabObj()
    {
        if (grabbedObj == null)
            yield break;

        Vector3 velocity = Vector3.zero;

        Placeable placeable = grabbedObj.GetComponent<Placeable>();
        if (placeable != null)
        {
            StartCoroutine(placeable.OnGrab());
        }

        while (grabbedObj != null)
        {
            grabbedObj.GetComponent<Rigidbody>().velocity =
                    Vector3.SmoothDamp(grabbedObj.GetComponent<Rigidbody>().velocity, 
                    (this.transform.position - grabbedObj.transform.position) * 15, ref velocity, 0.05f);
            yield return null;
        }
    }

    private void DropObj()
    {
        Placeable placeable = grabbedObj.GetComponent<Placeable>();
        if (placeable != null)
            placeable.OnDrop();
        grabbedObj = null;
    }
}
