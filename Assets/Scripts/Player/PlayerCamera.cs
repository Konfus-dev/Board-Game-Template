using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    #region globals
    [HideInInspector]
    public bool isRotating = false;

    public float minFov = 15f;
    public float maxFov = 90f;
    public float minAngle = -80f;
    public float maxAngle = 0.1f;
    public float zoomSensitivity = 10f;
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;

    internal Camera cam;
    internal Player player;
    internal float yaw;
    internal float pitch;
    #endregion

    private void Start()
    {
        player = this.GetComponent<Player>();
        cam = Camera.main;

        pitch = this.transform.eulerAngles.y;
        yaw = this.transform.eulerAngles.x;
    }

    private void Update()
    {
        if (player.input.zoomInput > 0) 
        {
            Zoom();
        }

        if (player.grab.grabbedObj == null && player.input.leftInteract)
        {
            Rotate();
        }
        else isRotating = false;
    }

    private void Zoom()
    {
        
    }

    private void Rotate()
    {
        yaw += sensitivityX * Input.GetAxis("Mouse X");
        pitch -= sensitivityY * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, minAngle, maxAngle);
        this.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        isRotating = true;
    }
}

