using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region globals
    public float MinFov = 15f;
    public float MaxFov = 90f;
    public float MinAngle = -80f;
    public float MaxAngle = 0.1f;
    public float SensitivityZ = 10f;
    public float SensitivityX = 2f;
    public float SensitivityY = 2f;
    public Transform Hand;
    public Transform CamParent;

    float yaw;
    float pitch;
    Camera cam;
    Manager manager;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager")
            .GetComponent<Manager>();
        cam = GetComponent<Camera>();
        pitch = CamParent.eulerAngles.y;
        yaw = CamParent.eulerAngles.x;
        Hand.eulerAngles = CamParent.eulerAngles;
    }

    // Update is called once per frame
    private void Update()
    {
        bool click = Input.GetButton("Fire1");

        Zoom();

        //Debug.Log(manager.IsMovingDie);

        if(!manager.IsGrabbing)
        {
            if(click) Rotate();
        }
    }

    #region private
    //zoom in and out w/ scrollwheel
    private void Zoom()
    {
        float fov = cam.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * SensitivityZ;
        fov = Mathf.Clamp(fov, MinFov, MaxFov);
        cam.fieldOfView = fov;
    }

    //rotate cam
    private void Rotate()
    {
        yaw += SensitivityX * Input.GetAxis("Mouse X");
        pitch -= SensitivityY * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, MinAngle, MaxAngle);
        CamParent.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        Hand.eulerAngles = CamParent.eulerAngles;
    }
}
    #endregion

