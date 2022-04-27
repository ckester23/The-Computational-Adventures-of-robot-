using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarManager : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hitText;
    public DataHandler datah;
    float current_energy;
    float current_health;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        current_energy = datah.player_current_energy;
        healthText.text = current_energy.ToString() + " ENERGY";

        current_health = datah.player_current_health;
        if (current_health != 1)
        {
            hitText.text = current_health.ToString() + " HITS LEFT";
        } else
        {
            hitText.text = current_health.ToString() + " HIT LEFT";
        }
    }
}
