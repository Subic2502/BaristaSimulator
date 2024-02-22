using UnityEngine;

public class OrderScript : MonoBehaviour
{
    private TextMesh orderTextMesh;

    void Start()
    {
        // Get the TextMesh component when the script starts
        orderTextMesh = GetComponent<TextMesh>();
        if (orderTextMesh == null)
        {
            Debug.LogError("TextMesh component not found on the GameObject.");
            return;
        }
        
        
    }

    public void SetOrder(string order)
    {

        orderTextMesh = GetComponent<TextMesh>();

        // Set the text of the TextMesh component
        if (orderTextMesh != null)
        {
            orderTextMesh.text = order;
        }
        else
        {
            Debug.LogError("TextMesh component is null. Make sure it is assigned in the Inspector.");
        }
    }
}

