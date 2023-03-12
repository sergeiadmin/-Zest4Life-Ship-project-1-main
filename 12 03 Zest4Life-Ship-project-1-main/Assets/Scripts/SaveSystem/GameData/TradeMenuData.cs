[System.Serializable]
public class TradeMenuData
{
    public int oil;
    public int coins;
    public bool shipAway;

    public TradeMenuData(TradeMenu tradeMenu)
    {
        oil = tradeMenu.OilAmount;
        coins = tradeMenu.ProfitCoins;
        shipAway = tradeMenu.ShipAway;
    }
}
