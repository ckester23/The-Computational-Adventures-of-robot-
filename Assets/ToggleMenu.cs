using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{

    public GameObject overlay;

    public void ToggleOverlay(){
        overlay.SetActive(!overlay.activeSelf);
    }
}
