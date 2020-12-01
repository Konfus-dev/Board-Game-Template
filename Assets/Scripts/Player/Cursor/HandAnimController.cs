using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimController : MonoBehaviour
{

    #region globals
    public Animator handAnim;
    private Hand hand;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //get animator
        handAnim = GetComponent<Animator>();
        //get hand
        hand = this.transform.parent.GetComponent<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hand.playGrabAnim) handAnim.Play("GrabIdle");

        else if(hand.playPointAnim) handAnim.Play("PointIdle");

        else handAnim.Play("Idle");
    }
}
