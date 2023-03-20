using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private ShipMovement shipMovement;
    
    [SerializeField] private GameObject timeLeftDisplay;
    [SerializeField] private int timeLeft;
    [SerializeField] private bool takingAway;

    public int TimeLeft
    {
        get => timeLeft;
        set => timeLeft = value;
    }

    public bool TakingAway
    {
        get => takingAway;
        set => takingAway = value;
    }

    private void Start()
    {
        timeLeftDisplay.GetComponent<TextMeshProUGUI>().text = "Ship returns in 00:00";
    }

    private void Update()
    {
        if (takingAway == false && timeLeft > 0)
        {
            StartCoroutine(ShipTimer());
        }
    }

    public void StartShipTimer()
    {
        timeLeft += 90;
        StartCoroutine(ShipTimer());
    }

    public void EndShipTimer()
    {
        timeLeft = 0;
    }

    public void StartTimerAfterLoad()
    {
        StartCoroutine(ShipTimer());
    }

    IEnumerator ShipTimer()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        timeLeft -= 1;
        if (timeLeft < 0)
        {
            timeLeft = 0;
        }

        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);

        timeLeftDisplay.GetComponent<TextMeshProUGUI>().text = string.Format("Ship returns in {0:00}:{1:00}", minutes, seconds);

        if (timeLeft == 0)
        {
            shipMovement.ReturnToDocks();
        }
        takingAway = false;
    }
}
