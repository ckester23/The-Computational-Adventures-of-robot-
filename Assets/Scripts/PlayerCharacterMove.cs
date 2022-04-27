using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerCharacterMove : MonoBehaviour
{
    // Publicly set variables that affect player movement
    public float speed = 1;
    public float jump_power = 1;
    public float stopping_speed = 1;
    public float descent_speed = 1;

    public int jump_cost = 5;
    /* Todo: implement an abstract object that handles all the cost info for
        each ability and uses that. Could also hold the enumerated table for
        use with ability aliasing.*/

    //public TextMeshProUGUI countText;
    //public GameObject winTextObject;

    // p_ indicates it is something that will be directly applied to the player

    // Internal variables used for movement calculations.
    private Rigidbody p_Rigid;
    private PlayerCharacterAbilities player_stats;
    private float movementX;
    private float movementY;
    private int count;
    private int health;
    private bool in_air;
    private bool doublejump_enabled = false;
    private bool jumps_used = false;

    //Stuff for vector calculations
    Vector3 z_coord;
    float velocity_x;
    Vector3 temp_Velocity;
    Vector3 p_Movement;
    Vector3 p_Velocity;
    Vector2 retrieved_vector;

    // Start is called before the first frame update
    void Start()
    {
        p_Rigid = GetComponent<Rigidbody>();
        player_stats = GetComponent<PlayerCharacterAbilities>();
        count = 0;
        health = 3;
        in_air = false;

        //SetCountText();
        //winTextObject.SetActive(false);
    }

    //Case handling movement for player object
    void OnMove(InputValue movementValue)
    {
        //Determine direction of player movement
        retrieved_vector = movementValue.Get<Vector2>();
        movementX = retrieved_vector.x;
        //movementY = retrieved_vector.y;
    }

    /*
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 8) {
            winTextObject.SetActive(true);
        }
    }
    */
    void OnCollisionEnter()
    {
        //Once they touch a serface, set not in air and reset jump tracking
        in_air = false;
        jumps_used = false;
    }

    void OnCollisionExit()
    {
        //Does nothing for now
        //in_air = true;
    }

    void OnJump()
    {
      //If the player is allowed to jump, let them jump
      if (!jumps_used) {
          Vector3 jump = new Vector3(0.0f, 2 * jump_power, 0.0f);
          p_Rigid.AddForce(jump - p_Rigid.velocity, ForceMode.VelocityChange);

          //If the player is in the air(meaning double jump has been used)
          //apply energy cost and disable additonal jumping
          if (in_air && player_stats.player_energy >= jump_cost && doublejump_enabled){
              player_stats.player_energy -= jump_cost;
              jumps_used = true;
          }
          //If the jump passes and double is allowed, set in_air so next jump
          //will be last
          else if (!in_air && doublejump_enabled) {
              in_air = true;
              if (player_stats.player_energy < jump_cost) {jumps_used = true;}
          }
          //If no double_jump, stop jumping after one jump
          else {
              jumps_used = true;
          }
      }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called at end of frame
    void FixedUpdate()
    {
        temp_Velocity = p_Rigid.velocity;
        //z_coord = PlayerCharacter.tranform.position;

        //if (z_coord.z > 0) {
          //transform.Translate(z_coord.x, z_coord.y, 0);
        //}

        //Sets velocity for movement instantly, avoiding acceleration
        if (movementX > 0) {
          p_Velocity.Set(speed, temp_Velocity.y, 0.0f);
          p_Rigid.AddForce(p_Velocity - temp_Velocity, ForceMode.VelocityChange);
        }
        else if (movementX < 0) {
          p_Velocity.Set(-speed, temp_Velocity.y, 0.0f);
          p_Rigid.AddForce(p_Velocity - temp_Velocity, ForceMode.VelocityChange);
        }
        //Applys movement slowdown after stopping input
        else if (temp_Velocity.x != 0){
          temp_Velocity.x = -temp_Velocity.x * stopping_speed;
          temp_Velocity.y = 0.0f;
          temp_Velocity.z = 0.0f;
          p_Rigid.AddForce(temp_Velocity, ForceMode.VelocityChange);
        }
        /*
        if (temp_Velocity.y < 0){
          temp_Velocity.x = 0.0f;
          temp_Velocity.y = -descent_speed;
          temp_Velocity.z = 0.0f;
          p_Rigid.AddForce(temp_Velocity, ForceMode.VelocityChange);
        }
        */

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("powerUp"))
        {
            other.gameObject.SetActive(false);
            // Add powerUp flag processing here!!!!!
            doublejump_enabled = true;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup")) {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
    }
    */
}
