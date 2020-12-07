using UnityEngine;

public class Player : MonoBehaviour
{
    #region global
    [HideInInspector]
    public PlayerInput input;
    [HideInInspector]
    public PlayerGrab grab;
    [HideInInspector]
    public PlayerCamera camera;

    internal PlayerData data;
    internal bool toggleMagnify = false;
    #endregion

    private void Start()
    {
        camera = this.GetComponent<PlayerCamera>();
        input = this.GetComponent<PlayerInput>();
        grab = this.GetComponent<PlayerGrab>();
    }

    private void Update()
    {
        if (!camera.isRotating && input.leftInteract 
            && grab.grabbedObj == null && grab.TryGrab())
        {
            StartCoroutine(grab.GrabObj());
        }
    }
}
