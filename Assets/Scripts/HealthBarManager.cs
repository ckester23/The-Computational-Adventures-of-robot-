using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarManager : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI toolText;
    public DataHandler datah;

    float current_energy;
    float current_health;
    int current_tools;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        current_energy = datah.player_current_energy;
        healthText.text = current_energy.ToString("F1") + " ENERGY";

        current_tools = datah.player_current_tools;
        toolText.text = current_tools.ToString() + " TOOLS";

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
