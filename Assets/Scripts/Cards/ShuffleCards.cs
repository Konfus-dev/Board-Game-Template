using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShuffleCards : MonoBehaviour
{
    #region globals
    public Texture[] CardTextures;
    public Transform[] CardsPlaces;
    //public Texture[] CardsOne; //cards with one player place
    //public Texture[] CardsTwo; //cards with two player places
    //public Texture[] CardsThree; //cards with three player places

    //hashmap with key val pair to which card does what... ex: card with 2 places for player instead of 1 or 3
    //Dictionary<string, int> cards = new Dictionary<string, int>();
    Transform[] cards = new Transform[10];
    int[] chosen = new int[40];
    int i = 0;
    int z = 0;
    new Renderer renderer;
    bool assignedTex = false;
    bool placeCards = true;
    bool startToPlaceCards = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //populate chosen and add stuff to hashmap
        for (int j = 0; j < 40; j++)
        {
            chosen[j] = 60;
        }
        //add appropriate stuff to hashmap
        //Dictionary();
        //shuffle cards
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {

        //place cards on board
        if(placeCards) DealCards();
    }

    #region public
    //after shuffle places cards
    public void DealCards()
    {
        //put top ten cards of deck into array
        if(startToPlaceCards)
        {
            //add everything to array
            Transform card;
            for (int x = 0; x < 10; x++)
            {
                card = transform.GetChild(transform.childCount - 1);
                card.eulerAngles = new Vector3(-90, 0, 0);
                card.parent = null;
                cards[x] = card;
            }
            startToPlaceCards = false;
        }

        //placed 10 cards on board
        if (cards[9].position == CardsPlaces[9].position)
        {
            placeCards = false;
            startToPlaceCards = false;
            z = 0;
        }

        //lerp cards to pos
        if(placeCards) cards[z].position = Vector3.Lerp(cards[z].position, CardsPlaces[z].position, Time.deltaTime * 10f);

        //if at position move to next card
        if(placeCards)
        {
            if (cards[z].position == CardsPlaces[z].position)
            {
                //turn kinematic off and gravity on
                //cards[z].GetComponent<Rigidbody>().isKinematic = false;
                //cards[z].GetComponent<Rigidbody>().useGravity = true;
                //move to next
                z++;
            }
        }
    }
    #endregion
    #region private
    private void Shuffle()
    {
        foreach (Transform card in transform)
        {
            //get renderer of cars
            renderer = card.GetComponent<Renderer>();

            //int rand = Random.Range(0, 40);
            //set texture
            //renderer.material.SetTexture("_MainTex", cardTextures[rand]);
            //add already chosen textures to chosen array to keep track
            //chosen[i] = rand;
            //i++;

            //while unique card tex has not been assigned
            while (!assignedTex)
            {
                //chooose randomly between 0 and 39
                int rand = Random.Range(0, 40);
                //if chosen doesnt contain randomly chosen number
                if (!chosen.Contains(rand))
                {
                    //add player slots
                    //AddPlayerSlot(cardTextures[rand].name);
                    //set texture
                    assignedTex = true;
                    renderer.material.SetTexture("_MainTex", CardTextures[rand]);
                    //add already chosen textures to chosen array to keep track
                    chosen[i] = rand;
                    i++;
                } 
            }
            assignedTex = false;
        }
    }

    //if one place add one slot if 2 add 2 slots...
    private void AddPlayerSlot(string key)
    {
        //look up key in dictionary
        int val = 0;
        //apply appropriate transform
        if (val == 1) ;
        else if (val == 2) ;
        else if (val == 3) ;

    }

    //populates dictionary with appropriate values
    /*private void Dictionary()
    {
        foreach (Texture card1 in CardsOne) cards.Add(card1.name, 1);
        foreach (Texture card2 in CardsTwo) cards.Add(card2.name, 2);
        foreach (Texture card3 in CardsThree) cards.Add(card3.name, 3);
    }*/
    #endregion
}
