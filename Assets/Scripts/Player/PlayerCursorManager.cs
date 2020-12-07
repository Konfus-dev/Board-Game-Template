using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursorManager : MonoBehaviour
{

    #region globals
    internal Player player;
    internal PlayerGrab playerGrab;

    internal GameObject handCursor;
    internal GameObject magnifyingCursor;
    internal Camera camera;
    internal Animator animator;

    internal bool magnifyToggle;
    #endregion

    private void Start()
    {
        Cursor.visible = false;

        player = this.transform.parent.GetComponent<Player>();
        playerGrab = this.transform.GetComponent<PlayerGrab>();
        camera = Camera.main;
        animator = this.GetComponentInChildren<Animator>();

        Debug.Log(animator);

        handCursor = this.transform.Find("Hand").gameObject;
        magnifyingCursor = this.transform.Find("Magnifying Glass").gameObject;

        magnifyingCursor.SetActive(false);
    }

    private void Update()
    {

        RaycastHit hit = new RaycastHit();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (player.input.leftInteract || player.input.rightInteract)
        {
            //grab cursor
            animator.Play("Grab");
        }
        else if (Physics.Raycast(ray, out hit) && 
            (hit.transform.CompareTag("Grabbable") || hit.transform.CompareTag("Clickable")))
        {
            //point cursor
            animator.Play("Point");
        }
        else
        {
            //reg cursor
            animator.Play("Idle");
        }

        if (player.input.magnify)
        {
            ToggleMagnify();
        }

    }

    private void ToggleMagnify()
    {
        magnifyToggle = !magnifyToggle;
        if (magnifyToggle)
        {
            handCursor.SetActive(false);
            magnifyingCursor.SetActive(true);
        }
        else
        {
            handCursor.SetActive(true);
            magnifyingCursor.SetActive(false);
        }
    }
}
