[System.Serializable]
public class OilTowerData
{
    public float oil;
    public float capacity;
    public int level;
    
    public OilTowerData (OilTower oilTower)
    {
        oil = oilTower.Oil;
        capacity = oilTower.Capacity;
        level = oilTower.Level;
    }
}
