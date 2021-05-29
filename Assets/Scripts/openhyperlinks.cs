using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class openhyperlinks : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(gameObject.GetComponent<TextMeshProUGUI>(), Input.mousePosition,GameObject.FindObjectOfType<Camera>());
        if( linkIndex != -1 ) { // was a link clicked?
            TMP_LinkInfo linkInfo = gameObject.GetComponent<TextMeshProUGUI>().textInfo.linkInfo[linkIndex];

            // open the link id as a url, which is the metadata we added in the text field
            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}