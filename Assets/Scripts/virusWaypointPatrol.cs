
using UnityEngine;

public class virusWaypointPatrol : MonoBehaviour
{

    public Transform[] waypoints;

    private Rigidbody virus_rb;

    private int _currentWaypointIndex = 0;
    public float _speed;

    bool isWalking = false;

    Animator m_Animator;
   

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool("isWalking", isWalking);
    }

    private void FixedUpdate()
    {
        Transform wp = waypoints[_currentWaypointIndex];
        virus_rb = GetComponent<Rigidbody>();


        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {
            transform.Rotate(0f, 180f, 0f);

            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            
        }

        else
        {
            isWalking = true;
            virus_rb.MovePosition(Vector3.MoveTowards(
                transform.position,
                wp.position,
                _speed * Time.deltaTime));

        }

        m_Animator.SetBool("isWalking", isWalking);
    }

}

