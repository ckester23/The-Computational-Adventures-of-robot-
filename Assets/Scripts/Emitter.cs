using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public int energy_per_sec = 0;
    public bool is_damage_only = false;
    public bool is_instant_kill = false;

    public Transform player_pos;
    public PlayerCharacterAbilities player_stats;
    //public EventHandler eventHandler;

    private float current_time;

    bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player_pos)
        {
            //Might need logic here for strictly contact entities
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform == player_pos)
        {
            m_IsPlayerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.transform == player_pos)
        {
            m_IsPlayerInRange = false;

        }
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player_pos.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player_pos)
                {
                    if (is_instant_kill) {
                        player_stats.ApplyDeath();
                    }
                    else if (is_damage_only) {
                        player_stats.ApplyDamage(player_pos.position - transform.position);
                    }
                    else {
                        current_time = Time.deltaTime;
                        player_stats.ApplyEnergy(current_time * energy_per_sec, player_pos.position - transform.position);
                    }
                }
            }
        }
    }
}
