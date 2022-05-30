using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class endHealthManager : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI toolText;
    public TextMeshProUGUI coinText;
    public DataHandler datah;

    float current_energy;
    float current_health;
    int current_tools;
    int current_coins;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        current_energy = datah.player_current_energy;
        healthText.text = current_energy.ToString("F1") + " ENERGY";
        if (current_energy >= datah.player_max_energy)
        {
            healthText.text = current_energy.ToString("F1") + " ENERGY(OverLoading!)";
            healthText.color = Color.red;
        } else
        {
            healthText.text = current_energy.ToString("F1") + " ENERGY";
            healthText.color = Color.white;
        }

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

        current_coins = datah.player_coins;
        coinText.text = current_coins.ToString() + " COINS";
    }
}
