using UnityEngine;

public class CameraLayer : MonoBehaviour
{
    #region globals
    Camera cam;
    Camera camParent;
    #endregion

    void Start()
    {
        cam = this.GetComponent<Camera>();
        camParent = this.transform.parent.GetComponent<Camera>();
    }

    private void Update()
    {
        cam.fieldOfView = camParent.fieldOfView;
    }
}

