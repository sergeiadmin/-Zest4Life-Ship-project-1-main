[System.Serializable]
public partial class ShipVillagerData
{
    public bool singleDelivery;


    public ShipVillagerData(ShipVillager shipVillager)
    {
        singleDelivery = shipVillager.SingleDelivery;
    }
}
