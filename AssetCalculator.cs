using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssetCalculator 
{
    public bool hiredWaiter;
    public int priceOfWaiter;

    public int numberOfNewTables;
    public int priceOfNewTable=5000;
    public int sumOfPriceTable;

    public int numberOfCoffee;
    public int priceOfCoffee=30;
    public int sumOfPriceCoffee;

    public int numberOfSmoothie;
    public int priceOfSmoothie=100;
    public int sumOfPriceSmoothie;

    public int numberOfTea;
    public int priceOfTea=40;
    public int sumOfPriceTea;

    public int numberOfSoda;
    public int priceOfSoda=70;
    public int sumOfPriceSoda;

    public int numberOfKroasan;
    public int priceOfKroasan=100;
    public int sumOfPriceKroasan;

    public int numberOfSandwich;
    public int priceOfSandwich=150;
    public int sumOfPriceSandwich;

    public int sumTotal;

    public List<int> sumItUp(bool waiter,int tables,int coffee,int smoothie,int tea,int soda,int kroasan,int sandwich){

        List<int> allNumbers = new List<int>();;

        if(waiter == true){
            allNumbers.Add(priceOfWaiter);
        }else{
            allNumbers.Add(0);
        }
        sumOfPriceTable = tables * priceOfNewTable;
        allNumbers.Add(sumOfPriceTable);

        sumOfPriceCoffee = coffee * priceOfCoffee;
        allNumbers.Add(sumOfPriceCoffee);

        sumOfPriceSmoothie = smoothie * priceOfSmoothie;
        allNumbers.Add(sumOfPriceSmoothie);

        sumOfPriceTea = tea * priceOfTea;
        allNumbers.Add(sumOfPriceTea);

        sumOfPriceSoda = soda * priceOfSoda;
        allNumbers.Add(sumOfPriceSoda);

        sumOfPriceKroasan = kroasan * priceOfKroasan;
        allNumbers.Add(sumOfPriceKroasan);

        sumOfPriceSandwich = sandwich * priceOfSandwich;
        allNumbers.Add(sumOfPriceSandwich);

        sumTotal = priceOfWaiter + sumOfPriceTable + sumOfPriceCoffee + sumOfPriceSmoothie + sumOfPriceTea + sumOfPriceSoda + sumOfPriceKroasan + sumOfPriceSandwich;
        allNumbers.Add(sumTotal);

        return allNumbers;
    }
}
