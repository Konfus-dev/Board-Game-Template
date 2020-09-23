using UnityEngine;

public class Dice : MonoBehaviour
{
    #region globals
    //public arrays
    public DiceSide[] DiceSides;
    public Dice[] Dices; //I know the plural is dice not dices but Dice is not allowed cause that be the class name XD
    //public bools
    public bool CanRoll;
    //public vectors
    public Vector3 StartPos;
    //private bools
    bool hasLanded = false;
    bool rolled = false;
    bool isMoving = false;
    bool isCurrentDie = false;

    //private rb, vectors, transforms
    Rigidbody rb;
    //private ints, floats, etc.
    int Roll;
    //float dist = 20f;
    //float minDist;
    //float maxDist;
    //private other
    //Plane plane = new Plane(Vector3.up, 0);
    PickUpObj pickUp;
    Manager manager;
    Camera cam;
    Hand hand;
    DoubleClick doubleClick;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        //find stuff
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        doubleClick = GetComponent<DoubleClick>();
        manager = GameObject.Find("Manager")
            .GetComponent<Manager>();
        hand = GameObject.Find("HandParent")
            .GetComponent<Hand>();
        pickUp = GetComponent<PickUpObj>();
        //minDist = hand.MinDist;
        //maxDist = hand.MaxDist;

        //get start position
        StartPos = transform.position;

        //rb.useGravity = false;
        //rb.isKinematic = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //play hand animations
        //hand.PlayAnims(playGrabAnim, playPointAnim);

        //move die
        //if(isMoving && isCurrentDie) MoveDie();

        //if die rolled
        if (rb.IsSleeping() && !hasLanded && rolled)
        {
            hasLanded = true;
            //rb.useGravity = false;
            rb.isKinematic = true;
        }
        //reset dice
        else if (rolled && hasLanded)
        {
            RollCheck();
            Reset();
        }
        //if dice lands funky
        else if (rb.IsSleeping() && hasLanded && Roll == 0)
        {
            RollAgain();
        }

        //playPointAnim = false;
    }

    //when you click on die
    private void OnMouseDown()
    {
        //Debug.Log("MouseDown");
        rb.isKinematic = true;
        isCurrentDie = true;
        isMoving = true;
        pickUp.PickUp = isMoving;
        pickUp.CanGrab = isCurrentDie;
        manager.IsGrabbing = isMoving;
    }

    //when you release mouse button
    private void OnMouseUp()
    {
        rb.isKinematic = false;
        isMoving = false;
        if (CanRoll && !isMoving && doubleClick.doubleClick)
        {
            foreach (Dice die in Dices)
            {
                die.RollDice();
            }

            RollDice();
        }
        isCurrentDie = false;
        manager.IsGrabbing = isMoving;
        pickUp.PickUp = isMoving;
        pickUp.CanGrab = isCurrentDie;
    }

    private void OnMouseOver()
    {
        hand.PlayPointAnim = true;
    }

    private void OnMouseExit()
    {
        hand.PlayPointAnim = false; 
    }

    #region public
    //roll dice
    public void RollDice()
    {
        if (!rolled && !hasLanded)
        {
            CanRoll = false;
            rolled = true;
            //rb.useGravity = true;
            rb.AddForce(Random.Range(10, 100), Random.Range(400, 450), Random.Range(10, 100));
            rb.AddTorque(Random.Range(100, 500), Random.Range(10, 500), Random.Range(10, 500));
        }
    }
    
    #endregion
    #region private
    //move dice
    private void MoveDie()
    {
        /*Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out dist))
        {
            Vector3 point = ray.GetPoint(dist);
            Vector3 targetPos = new Vector3(Mathf.Clamp(point.x, minDist, maxDist), -0.5f, Mathf.Clamp(point.z, minDist, maxDist));
            //Debug.Log(targetPos);
            transform.position = targetPos;
            if (!doubleClick.doubleClick) rb.isKinematic = true;
            else rb.isKinematic = false;
        }*/
    }

    //reroll dice
    private void RollAgain()
    {
        Reset();
        rolled = true;
        //rb.useGravity = true;
        rb.AddForce(Random.Range(10, 100), Random.Range(400, 450), Random.Range(10, 100));
        rb.AddTorque(Random.Range(100, 500), Random.Range(10, 500), Random.Range(10, 500));
    }

    //reset dice
    private void Reset()
    {
        rolled = false;
        hasLanded = false;
        //rb.useGravity = false;
        rb.isKinematic = false;
        //rb.position = StartPos;
    }

    //check roll value
    private void RollCheck()
    {
        Roll = 0;

        foreach(DiceSide side in DiceSides)
        {
            if(side.IsGrounded()) 
            {
                Roll = side.SideVal;

                Debug.Log(Roll + " has been rolled!");

                ReturnRoll();
            }
        }
    }

    //add roll to roll total
    private void ReturnRoll()
    {
        manager.AddToTotal(Roll);
    }
    #endregion
}
