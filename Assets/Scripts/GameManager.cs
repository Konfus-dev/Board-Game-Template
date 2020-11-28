using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region globals
    public int numberOfDays;
    public int numberOfPlayers;

    public bool inMenu = false;
    #endregion

    private void Start()
    {
        DealCards();
    }

    private void DealCards()
    {
        //deal cards
        StartCoroutine(CardDeck.Instance.DealCards());
    }

}
