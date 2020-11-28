using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{

    #region globals
    private bool isMoving;
    private bool isFlipping;
    private bool isFlipped;
    #endregion

    private void OnMouseDown()
    {
        if (isFlipped) return;

        this.tag = "Untagged";

        Vector3 cardRestPos = this.transform.position;

        StartCoroutine(MoveCardTo(cardRestPos + Vector3.up, .2f));
        StartCoroutine(RotateCard(Quaternion.Euler(new Vector3(90, this.transform.rotation.eulerAngles.y, this.transform.eulerAngles.z)), .3f));

        isFlipped = true;
    }

    public IEnumerator MoveCardTo(Vector3 toPosition, float duration)
    {
        //Make sure there is only one instance of this function running
        if (isMoving)
        {
            yield break; ///exit if this is still running
        }
        isMoving = true;

        float counter = 0;

        //Get the current position and rotation as well as target pos and rot
        Vector3 startPos = this.transform.position;

        this.GetComponent<Rigidbody>().isKinematic = true;

        //Move card
        while (counter < duration)
        {
            counter += Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPos, toPosition, counter / duration);
            yield return null;
        }

        this.GetComponent<Rigidbody>().isKinematic = false;
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

        this.GetComponent<Rigidbody>().isKinematic = true;

        //Rotate card
        while (counter < duration)
        {
            counter += Time.deltaTime;
            this.transform.localRotation = Quaternion.Slerp(startRot, toRotation, counter / duration);
            yield return null;
        }

        this.transform.localRotation = toRotation;
        this.GetComponent<Rigidbody>().isKinematic = false;
        isFlipping = false;
    }
}
