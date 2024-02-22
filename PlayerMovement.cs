
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 360f; // Adjust the speed at which the player rotates

    public GameObject infoPanel;
    public GameObject newDayPanel;
    public GameObject gamePlayPanel;

    public TextMeshProUGUI  list1Text;
    public TextMeshProUGUI  list2Text;
    public TextMeshProUGUI  pazarVrednost;  

    List<Asset> assets;
    List<Asset> completedOrders;

    Inventory inventory;

    private Rigidbody rb;
    private Animator animator;
    //private bool isColliding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

             
        
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

        if (movement != Vector3.zero)
        {
             Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * rotateSpeed);
        }
        else
        {
             // Set the IsWalking parameter to false when not moving
            //animator.SetBool("IsWalking", false);
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            // Ukljuci info panel
            infoPanel.SetActive(!infoPanel.activeSelf);
            UpdateInfo();
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            newDayPanel.SetActive(!newDayPanel.activeSelf);
            gamePlayPanel.SetActive(!gamePlayPanel.activeSelf);
        }
        
    }
    private void UpdateInfo()
    {
        //uPDATE informacija u info panelu
        list1Text.text = "";
        list2Text.text = "";
        pazarVrednost.text = "";

        foreach (Asset asset in inventory.assets)
        {
                list1Text.text += asset.name + ": " + asset.quantity + "\n";
        }
        foreach (Asset comOrder in inventory.completedOrders)
        {
                list2Text.text += comOrder.name + ": " + comOrder.quantity + "\n";
        }
        pazarVrednost.text = inventory.money.ToString();
    }
}

