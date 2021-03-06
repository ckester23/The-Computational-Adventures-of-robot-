using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public float player_max_health;
    public float player_current_health = 1;
    public float player_max_energy;
    public float player_overload_energy;
    public float player_current_energy;

    public int player_coins;

    public int player_current_tools;

    public Vector3 respawnPoint;

    public PlayerCharacterAbilities player_stats;

    // Start is called before the first frame update
    void Start()
    {
        player_max_health = player_stats.player_health;
        player_current_health = player_stats.player_currenthealth;
        player_max_energy = player_stats.max_energy;
        player_overload_energy = player_stats.overload_energy;
        player_current_energy = player_stats.player_energy;

        player_current_tools = player_stats.player_tools;

        player_coins = player_stats.player_coins;

        respawnPoint = player_stats.respawnPoint;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player_max_health = player_stats.player_health;
        player_current_health = player_stats.player_currenthealth;
        player_max_energy = player_stats.max_energy;
        player_overload_energy = player_stats.overload_energy;
        player_current_energy = player_stats.player_energy;

        player_current_tools = player_stats.player_tools;

        player_coins = player_stats.player_coins;

        respawnPoint = player_stats.respawnPoint;
    }
}
