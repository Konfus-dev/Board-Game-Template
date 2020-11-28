using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region globals
    public float minFov = 15f;
    public float maxFov = 90f;
    public float minAngle = -80f;
    public float maxAngle = 0.1f;
    public float zoomSensitivity = 10f;
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;
    public Transform camParent;

    float yaw;
    float pitch;
    Camera cam;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        pitch = camParent.eulerAngles.y;
        yaw = camParent.eulerAngles.x;
        Hand.Instance.transform.eulerAngles = camParent.eulerAngles;
    }

    // Update is called once per frame
    private void Update()
    {
        bool click = Input.GetButton("Fire1");

        Zoom();

        //Debug.Log(manager.IsMovingDie);

        if(Hand.Instance.grabbedObj == null && camParent != null)
        {
            if(click) Rotate();
        }
    }

    #region private
    //zoom in and out w/ scrollwheel
    private void Zoom()
    {
        float fov = cam.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        cam.fieldOfView = fov;
    }

    //rotate cam
    private void Rotate()
    {
        yaw += sensitivityX * Input.GetAxis("Mouse X");
        pitch -= sensitivityY * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, minAngle, maxAngle);
        camParent.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        Hand.Instance.transform.eulerAngles = camParent.eulerAngles;
    }
}
    #endregion

