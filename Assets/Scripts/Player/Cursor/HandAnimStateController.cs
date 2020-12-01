using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimStateController : MonoBehaviour
{

    #region globals
    public Animator handAnim;
    private Hand hand;
    #endregion

    void Start()
    {
        //get animator
        handAnim = GetComponent<Animator>();
        //get hand
        hand = this.transform.parent.GetComponent<Hand>();
    }

    void Update()
    {
        if(hand.playGrabAnim) handAnim.Play("GrabIdle");

        else if(hand.playPointAnim) handAnim.Play("PointIdle");

        else handAnim.Play("Idle");
    }
}
