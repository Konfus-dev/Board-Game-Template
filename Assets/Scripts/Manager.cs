using UnityEngine;

public class Manager : MonoBehaviour
{
    #region globals
    //public arrays
    public PlayerStats[] PlayersStats;
    public Dice[] Dices;
    //public numbers
    public int NumberOfDays;
    public int NumberOfPlayers;
    //public bools
    public bool InMenu = false;
    public bool IsGrabbing = false;
    //private numbers
    int currentDay = 1;
    int totalRoll = 0;
    int numCardsOnBoard = 10;
    //private bools
    bool turnOver = false;
    bool newDay = true;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        //Set Cursor to not be visible
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        Menu(Input.GetKeyDown(KeyCode.Escape));

        //end turn
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player turn ended...");

            EndTurn();
            Reset();
        }

        if (newDay) DealCards();
        
    }

    #region public
    public void AddToTotal(int Roll)
    {
        totalRoll += Roll;
        Debug.Log("TOTAL ROLL: " + totalRoll);
    }
    #endregion
    #region private
    //manage days
    private void DayManager()
    {
        if (numCardsOnBoard == 1) newDay = true;
    }

    //deal cards
    private void DealCards()
    {
        //deal cards

        //new day = false again
        newDay = false;

    }

    //reset everything
    private void Reset()
    {
        totalRoll = 0;
        //PositionInAr = 0;

        foreach (Dice die in Dices)
        {
            die.CanRoll = true;
        }
    }

    //open menu
    private void Menu(bool esc)
    {
        if (InMenu && esc)
        {
            InMenu = false;
            Debug.Log("Exit menu");
            //Set Cursor to be visible
            Cursor.visible = false;
        }
        else if(!InMenu && esc)
        {
            InMenu = true;
            Debug.Log("Open menu");
            //Set Cursor to not be visible
            Cursor.visible = true;
        }
    }

    //player end turn
    private void EndTurn()
    {
        turnOver = true;
    }

    //upgrade rank
    private void UpgradeRank(int player)
    {
        PlayersStats[player].Rank++;
    }
    //add cash to player
    private void AddCash(int player, float cash)
    {
        PlayersStats[player].CashMoney += cash;
    }
    //add credits to player
    private void AddCredits(int player, int credits)
    {
        PlayersStats[player].Credits += credits;
    }
    //add counters to player
    private void AddCounters(int player, int counters)
    {
        PlayersStats[player].Counters += counters;
    }
    #endregion
}
