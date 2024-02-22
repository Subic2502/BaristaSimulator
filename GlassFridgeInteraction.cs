using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassFridgeInteraction : MonoBehaviour
{
    public GameObject popupPrefabKroasan;
    public GameObject popupPrefabSendvic;
    public float waitTime = 5f;
    public string assetNameKroasan = "Kroasan";
    public string assetNameSendvic = "Sendvič";

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
        GameObject popupKroasan = Instantiate(popupPrefabKroasan, transform.position+ new Vector3(-0.4f, 3f, 0f), Quaternion.identity);
        GameObject popupSendvic = Instantiate(popupPrefabSendvic, transform.position+ new Vector3(0.4f, 3f, 0f), Quaternion.identity);
        Destroy(popupKroasan, waitTime);
        Destroy(popupSendvic, waitTime);

        // Attach a script to the popup GameObject that handles the click event
        popupKroasan.AddComponent<PopupClickHandler>().assetName = assetNameKroasan;
        popupSendvic.AddComponent<PopupClickHandler>().assetName = assetNameSendvic;
    }
}
