using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterAbilities : MonoBehaviour
{

    public int player_health = 3;
    public int energy_capsule_size = 20;
    public float invincibility_period = 1;
    public float block_cost = 1;
    public float block_hit_energy = 1;
    public float attack_cost = 1;
    public float attack_linger = 0.1f;
    [HideInInspector] public int player_currenthealth;
    [HideInInspector] public float player_energy;
    [HideInInspector] public int max_energy;
    [HideInInspector] public int overload_energy;

    public int player_tools;
    // Start is called before the first frame update

    private PlayerCharacterMove player_move;
    private BoxCollider hurt_box;

    private float time_passed;
    private float attack_time_passed;
    private bool absorb_state = false;
    private bool blocking = false;

    [HideInInspector] public bool block_enabled = false;
    [HideInInspector] public bool attack_enabled = false;

    void Start()
    {
        player_currenthealth = player_health;
        player_energy = 0;
        time_passed = 0;
        max_energy = energy_capsule_size;
        overload_energy = energy_capsule_size + (energy_capsule_size / 5);
        player_move = GetComponent<PlayerCharacterMove>();
        hurt_box = transform.GetChild(1).GetComponent<BoxCollider>();
    }

    void OnAbsorb(InputValue toggle) {
        absorb_state = toggle.isPressed;
    }

    void OnBlock(InputValue toggle) {
        blocking = toggle.isPressed;
    }

    void OnAttack() {
        DoAttack();
    }

    void DoAttack() {
        if (attack_enabled && player_energy >= attack_cost) {
              player_energy -= attack_cost;
              //Call any attack animations here.

              hurt_box.enabled = true;
              attack_time_passed = attack_linger;
        }
    }

    public void ApplyEnergy(float energy, Vector3 source_displacement)
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
            ApplyDamage(source_displacement);
        }
    }

    public void ApplyDamage(Vector3 source_displacement)
    {
        if (block_enabled && blocking && player_energy > 0 && time_passed <= 0){
            player_energy -= block_hit_energy;
            time_passed = invincibility_period;
        }
        else if (time_passed <= 0) {
            player_currenthealth -= 1;
            time_passed = invincibility_period;
            player_move.KnockBack(source_displacement);
            Debug.Log(player_currenthealth);
            if (player_currenthealth <= 0) {
                //call for player death
                Debug.Log(player_currenthealth);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Maintain some circumstance or state
        if (player_energy < 0) {player_energy = 0;}
    }

    void FixedUpdate()
    {
        //Decrement time counters
        time_passed -= Time.deltaTime;
        attack_time_passed -= Time.deltaTime;

        //Apply passive effects
        if (blocking && block_enabled) {
            player_energy -= block_cost * Time.deltaTime;
        }

        //End finished effects
        if (attack_time_passed <= 0) {
            hurt_box.enabled = false;
        }
    }
}
