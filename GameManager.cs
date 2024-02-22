using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    
    public bool hiredWaiter = true;
    public int dayNumber = 1;


    public GameObject customerPrefab;  // Reference to your customer prefab
    public GameObject orderTextPrefab;  // Reference to a prefab for the floating text
    public List<GameObject> listOfCustomers;  

    private List<string> possibleOrders = new List<string> { "Kafa","Smuti","Čaj","Sok","Sendvič","Kroasan"};
    private List<string> activeOrders = new List<string>();
    private List<string> revealedOrders = new List<string>();

    private List<Vector3> availableTables = new List<Vector3>();      // List to store coordinates of available tables
    private List<Vector3> unavailableTables = new List<Vector3>();    // List to store coordinates of unavailable tables

    Vector3 entranceVector = new Vector3(-8f, 0f, 6f);

    public GameObject gamePlayPanel; 
    public TextMeshProUGUI porudzbineText;
    public TextMeshProUGUI pazarText;
    public TextMeshProUGUI obavestenjeText; 

    public int maxCustomers;
    public int numberOfSpawned;

    public void freeTable(Vector3 tableToFree){
        unavailableTables.Remove(tableToFree);
        availableTables.Add(tableToFree);
    }
    public void freeOrder(string orderToFree){
        activeOrders.Remove(orderToFree);
    }
    public int GetMaxCustomers(){
        Inventory inventory = FindObjectOfType<Inventory>();
        if(inventory.reputation<10){
            return 10;
        }else if(inventory.reputation > 300){
            return 30;
        }else{
            return inventory.reputation/10+10;
        }
        
    }
    public void setStartingBarInventory(){
        Inventory inventory =FindObjectOfType<Inventory>();
        inventory.setStartingBarInventory();
    }
    public void readSettings(string path){
        string filePath = "Assets/settings.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // Iterate through each line
            foreach (string line in lines)
            {
                // Split the line into variable and value
                string[] parts = line.Split('=');

                // Check if the line contains "hiredWaiter"
                if (parts[0].Trim() == "hiredWaiter")
                {
                    // Try to parse the value, and handle parsing errors
                    if (bool.TryParse(parts[1].Trim(), out bool parsedBool))
                    {
                        hiredWaiter = parsedBool;
                    }
                    else
                    {
                        Debug.LogError("Error parsing hiredWaiter value: " + parts[1].Trim());
                    }
                }
                // Check if the line contains "dayNumber"
                else if (parts[0].Trim() == "dayNumber")
                {
                    // Try to parse the value, and handle parsing errors
                    if (int.TryParse(parts[1].Trim(), out int parsedInt))
                    {
                        dayNumber = parsedInt;
                    }
                    else
                    {
                        Debug.LogError("Error parsing dayNumber value: " + parts[1].Trim());
                    }
                }
            }

            // Print the values (optional)
            Debug.Log("hiredWaiter: " + hiredWaiter);
            Debug.Log("dayNumber: " + dayNumber);
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }
    }

    void Start()
    {
        string path = "Assets/settings.txt";
        readSettings(path);

        if(dayNumber == 1){
            setStartingBarInventory();
        }
        numberOfSpawned = 0;
        GetAvailableTablesCoordinates();  // Get the coordinates of available tables
        maxCustomers = GetMaxCustomers();
        StartCoroutine(SpawnCustomerWithDelay());
    }
    public void setPopupText(GameObject customer,string order)
    {
        //Create a floating text and attach it to the customer
        GameObject orderText = Instantiate(orderTextPrefab, Vector3.zero, Quaternion.identity);
        orderText.GetComponent<OrderScript>().SetOrder(order);  // Check if newOrder is correct

        //Make the orderText a child of the customer
        orderText.transform.SetParent(customer.transform);

        // Set the local position of the orderText (relative to the customer)
        orderText.transform.localPosition = new Vector3(0f, 2f, 0f);

        revealedOrders.Add(order);
        porudzbineText.text = "";
        porudzbineText.text = string.Join(", ", revealedOrders.ToArray());
    }
    public void removeRevealedOrder(string order){
        revealedOrders.Remove(order);
        porudzbineText.text = "";
        porudzbineText.text = string.Join(", ", revealedOrders.ToArray());
    }
    public void setObavestenje(string obavestenjeString){
        obavestenjeText.text = obavestenjeString;
        StartCoroutine(DelayedTextClear(4f));
    }
    private IEnumerator DelayedTextClear(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Clear the text after the specified delay
        obavestenjeText.text = "";
    }

    void GetAvailableTablesCoordinates()
    {
        GameObject[] tables = GameObject.FindGameObjectsWithTag("Table");  // Assuming tables have a "Table" tag
        foreach (GameObject table in tables)
        {
            availableTables.Add(table.transform.position);
        }
    }

    public IEnumerator SpawnCustomerWithDelay()
    {
        while (numberOfSpawned < maxCustomers)
        {
            while (availableTables.Count == 0)
            {
                Debug.Log("Imas musterija koje cekaju");

                yield return new WaitForSeconds(10f); // Cekaj 10 second
            }
            SpawnCustomer();
            yield return new WaitForSeconds(10f);  // Wait for 10 seconds before spawning the next customer
        }
    }

    void SpawnCustomer()
    {
        string newOrder = possibleOrders[Random.Range(0, possibleOrders.Count)];
        activeOrders.Add(newOrder);

        Debug.Log("New order: " + newOrder);
        bool hiredWaiterSecondCheck = hiredWaiter;
        Debug.Log("Uslo u spawnovanje,sad je hiredWaiterSecondCheck: " + hiredWaiterSecondCheck);
        if(hiredWaiterSecondCheck)
        {
            Debug.Log("Uslo u If konobarica");
            porudzbineText.text ="";
            porudzbineText.text = string.Join(", ", activeOrders.ToArray());
        }
        DisplayActiveOrders();

        GameObject customer = Instantiate(customerPrefab, entranceVector, Quaternion.identity);
        //Debug.Log("Vector:" + entranceVector);

        customer.transform.position = new Vector3(entranceVector.x, 0f, entranceVector.z);

        Vector3 destinationTable = availableTables[Random.Range(0, availableTables.Count)];
        StartCoroutine(MoveCustomerToTable(customer.transform, destinationTable));

        availableTables.Remove(destinationTable);
        unavailableTables.Add(destinationTable);

        // Attach CustomerScript to the customer object
        CustomerScript customerScript = customer.AddComponent<CustomerScript>();
        customerScript.SetOrder(newOrder);
        customerScript.setTable(destinationTable);

        listOfCustomers.Add(customer);

        // Add a BoxCollider to make the customer clickable
        BoxCollider collider = customer.AddComponent<BoxCollider>();
        collider.size = new Vector3(2f, 4f, 2f); // Adjust the size as needed


        if(hiredWaiterSecondCheck){
            Debug.Log("Uslo u If konobarica ya popup");
            //Create a floating text and attach it to the customer
            GameObject orderText = Instantiate(orderTextPrefab, Vector3.zero, Quaternion.identity);
            orderText.GetComponent<OrderScript>().SetOrder(newOrder);  // Check if newOrder is correct

            //Make the orderText a child of the customer
            orderText.transform.SetParent(customer.transform);

            // Set the local position of the orderText (relative to the customer)
            orderText.transform.localPosition = new Vector3(0f, 2f, 0f);
        }
        

        numberOfSpawned++;

        StartCoroutine(CustomerTimer(customer,newOrder,destinationTable)); // Start the timer coroutine

    }

    IEnumerator MoveCustomerToTable(Transform customerTransform, Vector3 destination)
    {
        float duration = 5f;  // Adjust the duration of the movement
        float elapsedTime = 0f;

        Vector3 startingPos = customerTransform.position;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Vector3 newPosition = Vector3.Lerp(startingPos, destination, t);

            // Explicitly set the Y-coordinate to prevent any unexpected vertical movement
            newPosition.y = 0f;

            customerTransform.position = newPosition;
            elapsedTime += Time.deltaTime;

            // Update the orderText position to follow the customer
        if (customerTransform.childCount > 0)
        {
            Transform orderTextTransform = customerTransform.GetChild(0);
            orderTextTransform.position = customerTransform.position + new Vector3(0f, 2f, 0f);
        }

        yield return null;
    }

    // Ensure the customer reaches the destination exactly
    customerTransform.position = destination;
    }

    IEnumerator CustomerTimer(GameObject customer,string newOrder,Vector3 destinationTable)
    {
        yield return new WaitForSeconds(35f); // Wait for 35 seconds

        // Check if the customer still exists (it might have been destroyed by other means)
        if (customer != null)
        {
            // Free up the table and remove the order
            freeTable(destinationTable);
            freeOrder(newOrder);

            Inventory inventory = FindObjectOfType<Inventory>();
            inventory.decreaseReputation();
            // Destroy the customer GameObject
            Destroy(customer);
            listOfCustomers.Remove(customer);
        }
    }

    void DisplayActiveOrders()
    {
        Debug.Log("Active Orders: " + string.Join(", ", activeOrders.ToArray()));
        // Update your UI or game display with the active orders
    }
}
