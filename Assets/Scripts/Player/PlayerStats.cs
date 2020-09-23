using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]

public class PlayerStats : ScriptableObject
{
    public string PlayerName;
    public float CashMoney;
    public int Rank;
    public int Credits;
    public int Counters;
}
