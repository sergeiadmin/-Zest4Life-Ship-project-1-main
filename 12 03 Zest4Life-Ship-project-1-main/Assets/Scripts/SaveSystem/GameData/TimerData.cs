[System.Serializable]
public class TimeManagerData
{
    public int timeLeft;
    public bool takingAway;

    public TimeManagerData(TimeManager timeManager)
    {
        timeLeft = timeManager.TimeLeft;
        takingAway = timeManager.TakingAway;
    }
}