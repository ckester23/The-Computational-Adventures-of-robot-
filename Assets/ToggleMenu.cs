using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{

    public GameObject overlay;

    void OnToggleMenu(){
        overlay.SetActive(!overlay.activeSelf);
    }
}
