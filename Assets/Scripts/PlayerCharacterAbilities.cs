using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterAbilities : MonoBehaviour
{

    public int player_health = 3;
    public int energy_capsule_size = 20;
    public float invincibility_period = 1;
    [HideInInspector] public int player_currenthealth;
    [HideInInspector] public float player_energy;
    [HideInInspector] public int max_energy;
    [HideInInspector] public int overload_energy;
    // Start is called before the first frame update

    private float time_passed;
    private bool absorb_state = false;

    void Start()
    {
        player_currenthealth = player_health;
        player_energy = 0;
        time_passed = invincibility_period;
        max_energy = energy_capsule_size;
        overload_energy = energy_capsule_size + (energy_capsule_size / 5);
    }

    void OnAbsorb(InputValue toggle) {
        absorb_state = toggle.isPressed;
    }

    public void ApplyEnergy(float energy)
    {
        if (absorb_state) {
            player_energy += energy;
            Debug.Log(player_energy);
            if (player_energy > overload_energy) {
                player_currenthealth = 0;
                //call for player death
                Debug.Log(player_currenthealth);
            }
            else if (player_energy > max_energy) {
                //call for some warning effect
            }
        }
        else {
            ApplyDamage();
        }
    }

    public void ApplyDamage()
    {
        if (time_passed <= 0) {
            player_currenthealth -= 1;
            time_passed = invincibility_period;
            Debug.Log(player_currenthealth);
            if (player_currenthealth <= 0) {
                //call for player death
                Debug.Log(player_currenthealth);
            }
        }
        else {
            time_passed -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
