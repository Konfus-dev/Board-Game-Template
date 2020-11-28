using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    #region globals
    public bool canRoll = true;

    private int roll = 0;

    private bool hasLanded = false;
    private bool rolled = false;

    private Rigidbody rb;
    private List<DiceSide> diceSides ;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        diceSides = new List<DiceSide>();
        foreach (Transform child in transform)
        {
            diceSides.Add(child.gameObject.GetComponent<DiceSide>());
        }
    }

    // Update is called once per frame
    private void Update()
    {
        TryRoll();

        //if die rolled
        if (rb.IsSleeping() && !hasLanded && rolled)
        {
            hasLanded = true;
        }
        //reset dice after roll
        else if (rolled && hasLanded)
        {
            RollCheck();
            Reset();
        }

    }

    //when you release mouse button
    private void TryRoll()
    {
        if (canRoll && Hand.Instance.grabbedObj != null && Hand.Instance.grabbedObj == this.gameObject && Input.GetMouseButtonUp(0))
        {
            Debug.Log("Rolling die");
            RollDice();
        }
    }

    //roll dice
    public void RollDice()
    {
        this.gameObject.tag = "Untagged";
        if (!rolled && !hasLanded)
        {
            canRoll = false;
            rolled = true;
            rb.AddForce(Random.Range(10, 100), Random.Range(0, 2), Random.Range(10, 100));
            rb.AddTorque(Random.Range(100, 500), Random.Range(10, 500), Random.Range(10, 500));
        }
    }

    //reset dice
    private void Reset()
    {
        canRoll = true;
        rolled = false;
        hasLanded = false;
    }

    //check roll value
    private void RollCheck()
    {
        roll = 0;

        foreach(DiceSide side in diceSides)
        {
            if(side.IsGrounded()) 
            {
                roll = side.SideVal;

                Debug.Log(roll + " has been rolled!");
            }
        }

        this.gameObject.tag = "Grabbable";
    }
}
