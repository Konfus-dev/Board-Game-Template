using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObj : MonoBehaviour
{
    #region globals
    public bool PickUp;
    public bool CanGrab;

    float dist = 20f;
    float minDist;
    float maxDist;
    Camera cam;
    Hand hand;
    Plane plane = new Plane(Vector3.up, 0);
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        hand = GameObject.Find("HandParent")
            .GetComponent<Hand>();
        minDist = hand.MinDist;
        maxDist = hand.MaxDist;
    }

    // Update is called once per frame
    void Update()
    {
        if (PickUp && CanGrab) Grab();
    }

    #region private
    private void Grab()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out dist))
        {
            Vector3 point = ray.GetPoint(dist);
            Vector3 targetPos = new Vector3(Mathf.Clamp(point.x, minDist, maxDist), -0.5f, Mathf.Clamp(point.z, minDist, maxDist));
            //Debug.Log(targetPos);
            transform.position = targetPos;
        }
    }
    #endregion
}
