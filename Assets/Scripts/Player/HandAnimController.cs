using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimController : MonoBehaviour
{

    #region globals
    public Animator HandAnim;

    Hand hand;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //get animator
        HandAnim = GetComponent<Animator>();
        //get hand
        hand = GameObject.Find("HandParent")
            .GetComponent<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hand.PlayGrabAnim) HandAnim.Play("GrabIdle");

        else if(hand.PlayPointAnim) HandAnim.Play("PointIdle");

        else HandAnim.Play("Idle");
    }
}
