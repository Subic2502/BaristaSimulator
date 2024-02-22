using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ManagmentDisplay : MonoBehaviour
{

    private GameManager gameManager;

    public Toggle hiredWaiter;

    public TextMeshProUGUI stoVrednost;

    public TextMeshProUGUI kafaStanje;
    public TextMeshProUGUI smutiStanje;
    public TextMeshProUGUI sokStanje;
    public TextMeshProUGUI cajStanje;
    public TextMeshProUGUI kroasanStanje;
    public TextMeshProUGUI sendvicStanje;

    public TextMeshProUGUI ukupnaCenaVrednost;
    public TextMeshProUGUI stoUkupnoVrednost;
    public TextMeshProUGUI kafaUkupnoVrednost;
    public TextMeshProUGUI smutiUkupnoVrednost;
    public TextMeshProUGUI cajUkupnoVrednost;
    public TextMeshProUGUI sokUkupnoVrednost;
    public TextMeshProUGUI kroasanUkupnoVrednost;
    public TextMeshProUGUI sendvicUkupnoVrednost;

    public TMP_InputField stoKolicina;
    public TMP_InputField kafaKolicina;
    public TMP_InputField smutiKolicina;
    public TMP_InputField cajKolicina;
    public TMP_InputField sokKolicina;
    public TMP_InputField kroasanKolicina;
    public TMP_InputField sendviKolicina;




    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        // Call this method whenever the panel is displayed
        UpdateBarInventoryTexts();
    }

    public void IzracunajButtonClick() // Assuming this is Button1
    {

        bool isWaiterHired = hiredWaiter.isOn;

        int.TryParse(stoKolicina.text, out int stoKolicinaInt);
        int.TryParse(kafaKolicina.text, out int kafaKolicinaInt);
        int.TryParse(smutiKolicina.text, out int smutiKolicinaInt);
        int.TryParse(cajKolicina.text, out int cajKolicinaInt);
        int.TryParse(sokKolicina.text, out int sokKolicinaInt);
        int.TryParse(kroasanKolicina.text, out int kroasanKolicinaInt);
        int.TryParse(sendviKolicina.text, out int sendviKolicinaInt);

        AssetCalculator assetCalc = new AssetCalculator();

        if (assetCalc == null)
        {
            Debug.LogError("AssetCalculator is null!");
            return;
        }
        
        List<int> sumAll = assetCalc.sumItUp(isWaiterHired,stoKolicinaInt,kafaKolicinaInt,smutiKolicinaInt,cajKolicinaInt,sokKolicinaInt,kroasanKolicinaInt,sendviKolicinaInt);

        stoUkupnoVrednost.text = sumAll[1].ToString();
        kafaUkupnoVrednost.text = sumAll[2].ToString();
        smutiUkupnoVrednost.text = sumAll[3].ToString();
        cajUkupnoVrednost.text = sumAll[4].ToString();
        sokUkupnoVrednost.text = sumAll[5].ToString();
        kroasanUkupnoVrednost.text = sumAll[6].ToString();
        sendvicUkupnoVrednost.text = sumAll[7].ToString();
        ukupnaCenaVrednost.text = sumAll[8].ToString();



    }

    public void SledeciDanButtonClick() // Assuming this is Button2
    {
        StartNewDay();
    }

    void StartNewDay()
    {
        // Stop any ongoing game-related processes (e.g., customer spawning)
        StopAllCoroutines();

        // Start the game with new settings
        gameManager.StartCoroutine(gameManager.SpawnCustomerWithDelay());
    }

    private void UpdateBarInventoryTexts()
    {
        // Call the Inventory class's getBarInventory function
        try
        {
            Inventory Inventory = FindObjectOfType<Inventory>();
            List<Asset> barInventory = Inventory.barInventory;

            Asset kafaIzListe = barInventory[0];
            kafaStanje.text = kafaIzListe.quantity.ToString();

            Asset smutiIzListe = barInventory[1];
            smutiStanje.text = smutiIzListe.quantity.ToString();

            Asset cajIzListe = barInventory[2];
            cajStanje.text = cajIzListe.quantity.ToString();

            Asset SokIzListe = barInventory[3];
            sokStanje.text = SokIzListe.quantity.ToString();

            Asset kroasanIzListe = barInventory[4];
            kroasanStanje.text = kroasanIzListe.quantity.ToString();

            Asset sendvicIzListe = barInventory[5];
            sendvicStanje.text = sendvicIzListe.quantity.ToString();
        }
        catch (Exception e)
        {
            Debug.LogError("Error in getBarInventory function: " + e.Message);
        }
    }
}
