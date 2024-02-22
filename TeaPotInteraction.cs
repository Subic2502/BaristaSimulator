using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPotInteraction : MonoBehaviour
{
    public GameObject popupPrefab;
    public float waitTime = 5f;
    public string assetName = "Čaj";

    private void OnMouseDown()
    {
        GameObject igracObject = GameObject.FindWithTag("Igrac");

        Vector3 currentPosition = transform.position;
        Vector3 igracPosition = igracObject.transform.position;
        if (Vector3.Distance(currentPosition, igracPosition) <= 3f)
        {
                ShowPopup();
        }
        else
        {
                // The current GameObject is outside the radius
                GameManager gamemanager = FindObjectOfType<GameManager>();
                gamemanager.setObavestenje("Nisi dovoljno blizu objekta!");
                Debug.Log("Nisi dovoljno blizu objekta!");
        }
    }

    private void ShowPopup()
    {
        GameObject popup = Instantiate(popupPrefab, transform.position+ new Vector3(0f, 1.5f, 0f), Quaternion.identity);
        Destroy(popup, waitTime);

        // Attach a script to the popup GameObject that handles the click event
        popup.AddComponent<PopupClickHandler>().assetName = assetName;
    }
}
