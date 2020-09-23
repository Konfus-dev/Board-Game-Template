using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    #region globals
    //public stuff
    public float MoveSpeed = 5f;
    public Camera Cam;
    public float MaxDist = -25;
    public float MinDist = 25;
    public bool PlayGrabAnim;
    public bool PlayPointAnim;

    //private stuff
    Plane plane = new Plane(Vector3.up, 0);
    float dist = 20;
    Manager manager;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        //get manager
        manager = GameObject.Find("Manager")
            .GetComponent<Manager>();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveHand();

        if(Input.GetMouseButtonDown(0)) PlayGrabAnim = true;
        if(Input.GetMouseButtonUp(0)) PlayGrabAnim = false;
    }

    #region private
    //move hand to mouse
    private void MoveHand()
    {
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out dist))
        {
            Vector3 point = ray.GetPoint(dist);
            Vector3 targetPos = new Vector3(Mathf.Clamp(point.x, MinDist, MaxDist), 0f, Mathf.Clamp(point.z, MinDist, MaxDist));
            //Debug.Log(targetPos);
            transform.position = targetPos;
        }
    }

    #endregion
    #region public

    /*public void PlayAnims(bool PlayGrab, bool PlayPoint)
    {
        if(PlayGrab)
        {
            animation.Play("GrabAnim");
        }
        if(PlayPoint)
        {
            animation.Play("PointAnim");
        }
        else
        {
            animation.Stop("GrabAnim");
            animation.Stop("PointAnim");
        }
    }*/
    #endregion
}
