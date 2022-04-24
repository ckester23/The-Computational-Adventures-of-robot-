using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyPursuit : MonoBehaviour
{
    public GameObject player;
    public float delay = 0;
    public float movement_speed = 5;
    public float pursuit_time = 1;

    private float pursuit_accumulator = 0;
    private float time_since_move = 0;
    private Vector3 facing_angle;

    void Start ()
    {
        facing_angle = new Vector3(0.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        if(time_since_move >= delay) {
            transform.position += transform.forward * Time.deltaTime * movement_speed;
            pursuit_accumulator += Time.deltaTime;

            if(pursuit_accumulator >= pursuit_time) {
                time_since_move = 0;
                pursuit_accumulator = 0;
            }
        }
        else {
            time_since_move += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        //Increment rotation towards destination
        transform.Rotate(facing_angle * 50.0f * Time.deltaTime);

        Vector3 d = player.transform.position - transform.position;

        //Normalize vector
        d.Normalize();

        //Find new angle from d vector
        float angle = Vector3.Angle(Vector3.forward, d);

        //Invert direction if negative angle
        Vector3 cross = Vector3.Cross(Vector3.forward, d);
        if (cross.y < 0.0f) {
            angle = -angle;
        }

        facing_angle.y = angle;
        transform.eulerAngles = facing_angle;
    }
}
