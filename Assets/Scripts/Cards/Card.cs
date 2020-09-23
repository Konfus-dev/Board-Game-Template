using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    #region globals
    bool isMoving;
    bool isCurrentCard;
    Rigidbody rb;
    Hand hand;
    Manager manager;
    PickUpObj pickUp;
    DoubleClick doubleClick;
    Vector3 StartAngles; 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //get stuff
        rb = GetComponent<Rigidbody>();
        manager = GameObject.Find("Manager")
            .GetComponent<Manager>();
        hand = GameObject.Find("HandParent")
            .GetComponent<Hand>();
        pickUp = GetComponent<PickUpObj>();
        doubleClick = GetComponent<DoubleClick>();
        StartAngles = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (doubleClick.doubleClick) FlipCard();
    }

    //when you click on card
    private void OnMouseDown()
    {
        //Debug.Log("MouseDown");
        isMoving = true;
        isCurrentCard = true;
        rb.isKinematic = true;
        rb.useGravity = false;
        manager.IsGrabbing = isMoving;
        pickUp.PickUp = isMoving;
        pickUp.CanGrab = isCurrentCard;
    }

    //when you release mouse button
    private void OnMouseUp()
    {
        isMoving = false;
        isCurrentCard = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        manager.IsGrabbing = isMoving;
        pickUp.PickUp = isMoving;
        pickUp.CanGrab = isCurrentCard;
    }

    //mouse over card
    private void OnMouseOver()
    {
        hand.PlayPointAnim = true;
    }

    //mouse exit
    private void OnMouseExit()
    {
        hand.PlayPointAnim = false;
    }

    #region private
    //flip card
    private void FlipCard()
    {
        //when double click flip card 180 degrees on x axis
        transform.eulerAngles = new Vector3(90, StartAngles.y, StartAngles.z);
    }
    #endregion
}
