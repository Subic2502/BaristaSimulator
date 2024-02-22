using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    public GameObject infoPanel;  
    public TextMeshProUGUI  list1Text;        
    public TextMeshProUGUI  list2Text;        
    public TextMeshProUGUI  pazarVrednost;       

    /*private void Update()
    {
        // Check if the player releases the 'I' key
        if (Input.GetKeyUp(KeyCode.K))
        {
             Debug.Log("Startovao Update Infoa");
            // Toggle the visibility of the info panel
            infoPanel.SetActive(!infoPanel.activeSelf);
        }
        // Check if the player holds the 'I' key
        if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("I key held down");
        }
    }*/

    private void UpdateInfo()
    {
        // TODO: Update list1Text and list2Text with your dynamic data
        // For example, if you have two lists:
        // list1Text.text = string.Join("\n", yourList1);
        // list2Text.text = string.Join("\n", yourList2);
    }
}
