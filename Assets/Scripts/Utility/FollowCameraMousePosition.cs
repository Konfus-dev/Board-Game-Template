using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraMousePosition : MonoBehaviour
{
    public Camera cursorCamera;
    internal Plane plane = new Plane(Vector3.up, 0);

    void Update()
    {
        Ray ray = cursorCamera.ScreenPointToRay(Input.mousePosition);
        float dist;
        if (plane.Raycast(ray, out dist))
        {
            Vector3 point = ray.GetPoint(dist);
            Vector3 targetPos = new Vector3(point.x, 0f, point.z);
            this.transform.position = targetPos;
        }
    }
}
