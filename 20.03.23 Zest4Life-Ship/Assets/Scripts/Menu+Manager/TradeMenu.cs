using UnityEngine;
using TMPro;

public class TradeMenu : MonoBehaviour
{
    // Allows buying/selling oils when possible, commands ship launch
    
    [SerializeField] private GameObject oilDisplay;
    [SerializeField] private GameObject coinDisplay;
    [SerializeField] private GameObject notEnoughMessage;
    [SerializeField] private GameObject cargoReady;
    
    [SerializeField] private GameObject buyOilButton;
    [SerializeField] private GameObject sellOilButton;
    [SerializeField] private GameObject sendShipButton;
    
    [SerializeField] private Storage storage;

    [SerializeField] private int oilAmount;
    [SerializeField] private int coinAmount;

    private bool _shipAway;
    private bool _cargoWaiting;

    public int ProfitCoins
    {
        get => coinAmount;
        set => coinAmount = value;
    }

    public int OilAmount
    {
        get => oilAmount;
        set => oilAmount = value;
    }

    public bool ShipAway
    {
        get => _shipAway;
        set => _shipAway = value;
    }

    public bool CargoWaiting
    {
        get => _cargoWaiting;
        set => _cargoWaiting = value;
    }

    private void Update()
    {
        if (oilAmount == 0) {sendShipButton.SetActive(false);}
        if (_shipAway)
        {
            sendShipButton.SetActive(false);
            buyOilButton.SetActive(false);
            sellOilButton.SetActive(false);
        }
        
        if(_cargoWaiting){cargoReady.SetActive(true);} else {cargoReady.SetActive(false);}

        oilDisplay.GetComponent<TextMeshProUGUI>().text = oilAmount switch
        {
            > 0 => "+" + oilAmount,
            < 0 => oilAmount.ToString(),
            _ => "0"
        };

        coinDisplay.GetComponent<TextMeshProUGUI>().text = coinAmount switch
        {
            > 0 => "Ожидаемый заработок: +" + coinAmount,
            < 0 => "Ожидаемый заработок: " + coinAmount,
            _ => "Ожидаемый заработок: 0"
        };
    }

    public void BuyOil ()
    {
        if (storage.Coins + coinAmount >= 100)
        {
            oilAmount += 1;
            coinAmount -= 100;
            sendShipButton.SetActive(true);
        }
        else
        {
            notEnoughMessage.GetComponent<TextMeshProUGUI>().text = "Недостаточно монет!";
            notEnoughMessage.GetComponent<Animation>().Play("NotEnoughTradeAnim");
        }
    }
    
    public void SellOil ()
    {
        if (storage.Oil > -oilAmount)
        {
            oilAmount -= 1;
            coinAmount += 100;
            sendShipButton.SetActive(true);
        }
        else
        {
            notEnoughMessage.GetComponent<TextMeshProUGUI>().text = "Недостаточно нефти!";
            notEnoughMessage.GetComponent<Animation>().Play("NotEnoughTradeAnim");
        }
    }

    public void CallSendShip()
    {
        FindObjectOfType<ShipVillager>().CarryCargo();
        _cargoWaiting = true;
        buyOilButton.SetActive(false);
        sellOilButton.SetActive(false);
        sendShipButton.SetActive(false);
        if (oilAmount < 0)
        {
            storage.Oil -= -oilAmount;
        }
        if (oilAmount > 0)
        {
            storage.Coins -= -coinAmount;
        }
    }

    public void ShipLeaving()
    {
        FindObjectOfType<ShipMovement>().FollowPath();
    }

    public void ResetTrading()
    {
        oilAmount = 0;
        coinAmount = 0;
        buyOilButton.SetActive(true);
        sellOilButton.SetActive(true);
        sendShipButton.SetActive(true);
    }
}