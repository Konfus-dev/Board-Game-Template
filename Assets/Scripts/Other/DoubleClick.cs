using UnityEngine;
using System.Collections;

public class DoubleClick : MonoBehaviour
{
    #region globals
    //public stuff
    public bool doubleClick = false;
    public float doubleClickTime = 0.25f;

    //private stuff
    private float lastClickTime = -10f;
    private bool clickedOn = false;
    #endregion

    // Update is called once per frame
    private void Update()
    {
        //if click
        if (Input.GetMouseButtonDown(0) && clickedOn)
        {
            //get time between clicks
            float timeDelta = Time.time - lastClickTime;

            //if click again w/in a certain time
            if (timeDelta < doubleClickTime)
            {
                //Debug.Log("double click" + timeDelta);
                doubleClick = true;
                lastClickTime = 0;
            }
            //set last click time
            else
            {
                lastClickTime = Time.time;
                doubleClick = false;
            }
        }
    }

    private void OnMouseOver()
    {
        clickedOn = true;
    }

    private void OnMouseExit()
    {
        clickedOn = false;
    }
}