using System.Collections;
using UnityEngine;

public class Card : Grabbable
{

    #region globals
    private bool isMoving = false;
    private bool isFlipping = false;
    private bool isFlipped = false;
    #endregion

    public override void OnRightClick()
    {
        if (isFlipped)
        {
            StartCoroutine(RotateCard(Quaternion.Euler(new Vector3(-90, this.transform.rotation.eulerAngles.y, this.transform.eulerAngles.z)), .3f));
            isFlipped = false;
        }

        else
        {
            StartCoroutine(RotateCard(Quaternion.Euler(new Vector3(90, this.transform.rotation.eulerAngles.y, this.transform.eulerAngles.z)), .3f));
            isFlipped = true;
        }
    }

    public IEnumerator MoveCardTo(Vector3 toPosition, float duration)
    {
        //Make sure there is only one instance of this function running
        if (isMoving)
        {
            yield break; ///exit if this is still running
        }
        isMoving = true;

        float liftCounter = 0;
        float moveCounter = 0;

        //Get the current position
        Vector3 startPos = this.transform.position;

        //disable physics
        data.rb.isKinematic = true;

        //lift card
        while (liftCounter < .2f)
        {
            liftCounter += Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPos, startPos + Vector3.up, liftCounter / duration);
            yield return null;
        }

        //Get the current position
        startPos = this.transform.position;

        //Move card
        while (moveCounter < duration)
        {
            moveCounter += Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPos, toPosition, moveCounter / duration);
            yield return null;
        }

        //re-enable physics
        data.rb.isKinematic = false;
        isMoving = false;
    }

    public IEnumerator RotateCard(Quaternion toRotation, float duration)
    {
        //Make sure there is only one instance of this function running
        if (isFlipping)
        {
            yield break; ///exit if this is still running
        }
        isFlipping = true;

        float counter = 0;

        //Get the current position of the object to be moved
        Quaternion startRot = this.transform.rotation;

        data.rb.isKinematic = true;

        //Rotate card
        while (counter < duration)
        {
            counter += Time.deltaTime;
            this.transform.localRotation = Quaternion.Slerp(startRot, toRotation, counter / duration);
            yield return null;
        }

        this.transform.localRotation = toRotation;
        data.rb.isKinematic = false;
        isFlipping = false;
    }
}
