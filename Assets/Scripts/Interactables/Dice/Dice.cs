using System.Collections.Generic;
using UnityEngine;

public class Dice : Grabbable
{
    #region globals
    public AudioClip rolledAudio;
    #endregion

    protected override void Start()
    {
        data.rb = GetComponent<Rigidbody>();
        base.Start();
    }

    public override void OnInteract()
    {
        if (Hand.Instance.grabbedObj != null && Hand.Instance.grabbedObj == this.gameObject)
        {
            RollDice();
        }
        base.OnInteract();
    }

    public void RollDice()
    {
        data.rb.AddForce(Random.Range(10, 100), Random.Range(0, 2), Random.Range(10, 100));
        data.rb.AddTorque(Random.Range(100, 500), Random.Range(10, 500), Random.Range(10, 500));
        grabbableAuidoEmitter.clip = rolledAudio;
        grabbableAuidoEmitter.Play();
    }
}
