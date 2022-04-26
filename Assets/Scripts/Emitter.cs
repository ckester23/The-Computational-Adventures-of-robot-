using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public int energy_per_sec = 0;

    public Transform player_pos;
    public PlayerCharacterAbilities player_stats;
    public EventHandler eventHandler;

    private float time_start;
    private float current_time;

    bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player_pos)
        {
            time_start = Time.deltaTime;
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
            time_start = 0;
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
                    current_time = Time.deltaTime;
                    player_stats.ApplyEnergy((current_time - time_start) * energy_per_sec);

                    time_start = current_time;
                }
            }
        }
    }
}
