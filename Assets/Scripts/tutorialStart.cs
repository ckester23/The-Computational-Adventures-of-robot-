using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialStart : MonoBehaviour
{
    public GameObject blankTV;
    public GameObject displayText;

    public Collider player;

    // Start is called before the first frame update
    void Start()
    {
        displayText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            blankTV.SetActive(false);
            displayText.SetActive(true);
        }
    }
}
