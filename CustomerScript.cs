using UnityEngine;
using System.Collections;
using TMPro;

public class CustomerScript : MonoBehaviour
{
    public string order;
    Vector3 tableOcupied; 

    public TextMeshProUGUI pazarText;
    public TextMeshProUGUI obavestenjeText;

    private void OnMouseDown()
    {
        Debug.Log("Kliknuo na musteriju!Narudzbina je: "+order);
        // Handle the click on the customer
        CheckOrderAndServe(order);
        
    }
    private void OnMouseOver(){
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Desni klik");

            GameObject customer = gameObject;
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.setPopupText(customer,order);
        }


    }

    public void setTable(Vector3 tableCoord){
        tableOcupied = tableCoord;
    }

    public Vector3 getTable(){
        return tableOcupied;
    }
    

    public void SetOrder(string newOrder)
    {
        order = newOrder;
    }

    public string GetOrder()
    {
        return order;
    }
    private void CheckOrderAndServe(string order)
    {
        // Access the player's Inventory script
        Inventory playerInventory = FindObjectOfType<Inventory>();

        // Check if the player has the required asset in their inventory
        if (playerInventory != null && playerInventory.HasAsset(order))
        {
            // Serve the customer - remove the customer object and deduct the asset from the player's inventory
            ServeCustomer(order);
        }
        else
        {
            GameManager gamemanager = FindObjectOfType<GameManager>();
            gamemanager.setObavestenje("Nemas to sto musterija trazi");
        }
    }

    private void ServeCustomer(string order)
    {
        // Handle customer service logic here

        // Remove the customer object
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.freeTable(tableOcupied);
        gameManager.freeOrder(order);

        Inventory inventory = FindObjectOfType<Inventory>();
        if(string.Equals(order,"Kafa")){
            inventory.money += 150;
            inventory.RemoveAsset(order,1);
            inventory.addCompletedOrder(order,1);
            inventory.increaseReputation();
            inventory.RemoveBarAsset(order,1);
            gameManager.removeRevealedOrder(order);
        }else if(string.Equals(order,"Smuti")){
            inventory.money += 250;
            inventory.RemoveAsset(order,1);
            inventory.addCompletedOrder(order,1);
            inventory.increaseReputation();
            inventory.RemoveBarAsset(order,1);
            gameManager.removeRevealedOrder(order);
        }else if(string.Equals(order,"Čaj")){
            inventory.money += 130;
            inventory.RemoveAsset(order,1);
            inventory.addCompletedOrder(order,1);
            inventory.increaseReputation();
            inventory.RemoveBarAsset(order,1);
            gameManager.removeRevealedOrder(order);
        }else if(string.Equals(order,"Sok")){
            inventory.money += 200;
            inventory.RemoveAsset(order,1);
            inventory.addCompletedOrder(order,1);
            inventory.increaseReputation();
            inventory.RemoveBarAsset(order,1);
            gameManager.removeRevealedOrder(order);
        }
        else if(string.Equals(order,"Kroasan")){
            inventory.money += 230;
            inventory.RemoveAsset(order,1);
            inventory.addCompletedOrder(order,1);
            inventory.increaseReputation();
            inventory.RemoveBarAsset(order,1);
            gameManager.removeRevealedOrder(order);
        }
        else if(string.Equals(order,"Sendvič")){
            inventory.money += 300;
            inventory.RemoveAsset(order,1);
            inventory.addCompletedOrder(order,1);
            inventory.increaseReputation();
            inventory.RemoveBarAsset(order,1);
            gameManager.removeRevealedOrder(order);
        }
        Debug.Log("Novi pazar:"+inventory.money+" | Reputacija:"+inventory.reputation);
        Destroy(gameObject);
    }
    
}
