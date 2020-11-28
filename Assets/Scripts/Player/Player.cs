using UnityEngine;

public class Player : MonoBehaviour
{
    //public arrays
    public PlayerStats PlayersStats;

    private void Start()
    {
        //Set Cursor to not be visible
        Cursor.visible = false;
    }

    void Update()
    {
        
    }

    private void UpgradeRank(int player)
    {
        PlayersStats.Rank++;
    }
    //add cash to player
    private void AddCash(int player, float cash)
    {
        PlayersStats.CashMoney += cash;
    }
    //add credits to player
    private void AddCredits(int player, int credits)
    {
        PlayersStats.Credits += credits;
    }
    //add counters to player
    private void AddCounters(int player, int counters)
    {
        PlayersStats.Counters += counters;
    }
}
