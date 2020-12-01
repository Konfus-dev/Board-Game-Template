using System.Collections.Generic;
using UnityEngine;

public class Dice : Grabbable
{
    #region globals
    private List<DiceSide> diceSides ;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        data.rb = GetComponent<Rigidbody>();

        diceSides = new List<DiceSide>();
        foreach (Transform child in transform)
        {
            diceSides.Add(child.gameObject.GetComponent<DiceSide>());
        }
        base.Start();
    }

    //if object is grabbed and has been right clicked
    public override void OnRightClick()
    {
        if (Hand.Instance.grabbedObj != null && Hand.Instance.grabbedObj == this.gameObject)
        {
            RollDice();
        }
    }

    //roll dice
    public void RollDice()
    {
        data.rb.AddForce(Random.Range(10, 100), Random.Range(0, 2), Random.Range(10, 100));
        data.rb.AddTorque(Random.Range(100, 500), Random.Range(10, 500), Random.Range(10, 500));
        
    }
}
