using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int health = 1;
    public bool knockback_enabled = false;

    public float knockback_x_bias = 1;
    public float knockback_y_bias = 1;

    private Rigidbody self_rigid;

    // Start is called before the first frame update
    void Start()
    {
        self_rigid = GetComponent<Rigidbody>();
    }

    public void KnockBack(Vector3 subject_location)
    {
        Vector3 source_displacement = subject_location - transform.position;
        source_displacement.x *= knockback_x_bias;
        source_displacement.y *= knockback_y_bias;
        self_rigid.AddForce(source_displacement, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.CompareTag("Hurt-Box"))
      {
          health -= 1;
          if (knockback_enabled) {KnockBack(other.transform.position);}
          if (health <= 0) {gameObject.SetActive(false);}
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
