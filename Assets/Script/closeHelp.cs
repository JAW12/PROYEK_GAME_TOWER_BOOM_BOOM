using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeHelp : MonoBehaviour
{
    private void Update() {
        HideIfClickedOutside(gameObject);
    }
    private void HideIfClickedOutside(GameObject panel) {
        if (Input.GetMouseButton(0) && panel.activeSelf && 
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(), 
                Input.mousePosition, 
                Camera.main)) {
            panel.SetActive(false);
        }
    }
}
