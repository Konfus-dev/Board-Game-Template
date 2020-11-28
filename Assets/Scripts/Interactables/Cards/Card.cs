using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{

    #region globals
    private Vector3 startAngles;
    private bool isMoving;
    private bool isFlipping;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        startAngles = transform.localEulerAngles;
    }

    private void OnMouseDown()
    {
        Vector3 cardRestPos = this.transform.position;
        //StartCoroutine(MoveCardTo(transform))
    }

    public IEnumerator MoveCardTo(Vector3 toPosition, Quaternion toRotataion, float duration)
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
        Quaternion startRot = this.transform.rotation;

        this.GetComponent<Rigidbody>().isKinematic = true;

        //Move card
        while (counter < duration)
        {
            counter += Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPos, toPosition, counter / duration);
            this.transform.rotation = Quaternion.Lerp(startRot, toRotataion, counter / duration);
            yield return null;
        }

        this.GetComponent<Rigidbody>().isKinematic = false;
        isMoving = false;
    }

    public IEnumerator FlipCard(float duration)
    {
        //Make sure there is only one instance of this function running
        if (isFlipping)
        {
            yield break; ///exit if this is still running
        }
        isFlipping = true;

        float counter = 0;

        //Get the current position of the object to be moved
        Vector3 startRot = transform.eulerAngles;

        //Move card
        while (counter < duration)
        {
            //when double click flip card 180 degrees on x axis
            transform.eulerAngles = Vector3.Lerp(startRot, new Vector3(90, startAngles.y, startAngles.z), counter / duration);
        }
    }
}
