using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterAbilities : MonoBehaviour
{

    bool absorb_state = false;
    public int player_health = 3;
    public int energy_capsule_size = 20;
    [HideInInspector] public int player_currenthealth;
    [HideInInspector] public float player_energy;
    [HideInInspector] public int max_energy;
    [HideInInspector] public int overload_energy;
    // Start is called before the first frame update

    void Start()
    {
        player_currenthealth = player_health;
        player_energy = 0;
        max_energy = energy_capsule_size;
        overload_energy = energy_capsule_size + (energy_capsule_size / 5);
    }

    public void ApplyEnergy(float energy)
    {
        if (absorb_state) {
            player_energy += energy;
            if (player_energy > overload_energy) {
                player_currenthealth = 0;
                //call for player death
            }
            else if (player_energy > max_energy) {
                //call for some warning effect
            }
        }
        else {
            player_health -= 1;
            if (player_health <= 0) {
                //call for player death
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
