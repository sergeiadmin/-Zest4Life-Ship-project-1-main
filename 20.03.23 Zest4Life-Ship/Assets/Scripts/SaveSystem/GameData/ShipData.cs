[System.Serializable]
public class ShipData
{
    public float[] position;
    public int waypointIndex;
    public bool singleDelivery;

    public ShipData(ShipMovement ship)
    {
        waypointIndex = ship.WaypointIndex;
        singleDelivery = ship.SingleDelivery;
        
        position = new float[3];
        position[0] = ship.transform.position.x;
        position[1] = ship.transform.position.y;
        position[2] = ship.transform.position.z;
    }
}
