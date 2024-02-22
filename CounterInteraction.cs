using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterInteraction : MonoBehaviour
{
    public float waitTime = 5f;
    public string assetName = "Sok";

    private void OnMouseDown()
    {
        Debug.Log("Uslo u klik!");
        GameObject igracObject = GameObject.FindWithTag("Igrac");

        GameManager gameManager = FindObjectOfType<GameManager>();

        Vector3 currentPosition = transform.position;
        Vector3 igracPosition = igracObject.transform.position;
        if (Vector3.Distance(currentPosition, igracPosition) <= 3f)
        {
            gameManager = FindObjectOfType<GameManager>();
            Inventory inventoryRef = FindObjectOfType<Inventory>();
            List<GameObject> listOfCustomers = gameManager.listOfCustomers;
            List<Asset> inventory = inventoryRef.assets;

            for (int i = listOfCustomers.Count - 1; i >= 0; i--)
            {
                GameObject customer = listOfCustomers[i];
            
                // Get the CustomerScript component attached to the customer GameObject
                CustomerScript customerScript = customer.GetComponent<CustomerScript>();

                // Check if the customerScript is not null
                if (customerScript != null)
                {
                    // Access fields in CustomerScript
                    string order = customerScript.order;

                    // Example: Check if the customer's name is "John"
                    if (inventoryRef.HasAsset(order))
                    {
                        inventoryRef.RemoveAsset(order,1);
                        Destroy(customer);
                        listOfCustomers.Remove(customer);
                    }

                    // ... other checks or actions based on customerScript fields
                }
            }
        }
        else
        {
             // The current GameObject is outside the radius
            gameManager.setObavestenje("Nisi dovoljno blizu objekta!");
            Debug.Log("Nisi dovoljno blizu objekta!");
        }
    }
}
