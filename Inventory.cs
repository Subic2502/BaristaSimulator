// Inventory.cs
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Asset> assets = new List<Asset>();
    public List<Asset> barInventory = new List<Asset>();
    public int money;

    public List<Asset> completedOrders = new List<Asset>();

    public int reputation;

    public void AddAsset(string assetName, int quantity)
    {
        Asset existingAsset = assets.Find(a => a.name == assetName);

        if (existingAsset != null)
        {
            existingAsset.quantity += quantity;
        }
        else
        {
            assets.Add(new Asset(assetName, quantity));
        }
    }
    public void RemoveAsset(string assetName, int quantity)
    {
        Asset existingAsset = assets.Find(a => a.name == assetName);

        if (existingAsset != null)
        {
             // Subtract the quantity, and remove the asset if the quantity becomes zero or negative
            existingAsset.quantity -= quantity;

            if (existingAsset.quantity <= 0)
            {
                 assets.Remove(existingAsset);
            }
        }

    }
    public void AddBarAsset(string assetName, int quantity)
    {
        Asset existingAsset = barInventory.Find(a => a.name == assetName);

        if (existingAsset != null)
        {
            existingAsset.quantity += quantity;
        }
        else
        {
            barInventory.Add(new Asset(assetName, quantity));
        }
    }
    public void RemoveBarAsset(string assetName, int quantity)
    {
        Asset existingAsset = barInventory.Find(a => a.name == assetName);

        if (existingAsset != null)
        {
             // Subtract the quantity, and remove the asset if the quantity becomes zero or negative
            existingAsset.quantity -= quantity;

            if (existingAsset.quantity <= 0)
            {
                 barInventory.Remove(existingAsset);
            }
        }

    }

    public bool HasAsset(string assetName)
    {
        // Check if the player has the specified asset in their inventory
        foreach (Asset asset in assets)
        {
            if (asset.name == assetName && asset.quantity > 0)
            {
                return true;
            }
        }

        return false;
    }

    public void addCompletedOrder(string assetName, int quantity)
    {
        Asset existingAsset = completedOrders.Find(a => a.name == assetName);

        if (existingAsset != null)
        {
            existingAsset.quantity += quantity;
        }
        else
        {
            completedOrders.Add(new Asset(assetName, quantity));
        }
    }
    public void increaseReputation(){
        reputation += 1;
    }
    public void decreaseReputation(){
        reputation -= 1;
    }
    public void setStartingBarInventory(){
        AddBarAsset("Kafa",100);
        AddBarAsset("Smuti",50);
        AddBarAsset("Čaj",200);
        AddBarAsset("Sok",62);
        AddBarAsset("Kroasan",20);
        AddBarAsset("Sendvič",20);
    }
    public List<Asset> getBarInventory(){
        return barInventory;
    }
}
