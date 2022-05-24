using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerCharacterMove : MonoBehaviour
{
    // Publicly set variables that affect player movement
    public float speed = 1;
    public float stopping_speed = 1;

    public float jump_power = 1;
    public int jump_cost = 5;

    public int grab_cost = 2;

    public float descent_speed = 1;
    public bool linear_decent = false;

    public float knockback_x_bias = 1;
    public float knockback_y_bias = 1;

    /* Todo: implement an abstract object that handles all the cost info for
        each ability and uses that. Could also hold the enumerated table for
        use with ability aliasing.*/

    //public TextMeshProUGUI countText;
    //public GameObject winTextObject;

    // p_ indicates it is something that will be directly applied to the player

    // Internal variables used for movement calculations.
    private Rigidbody p_Rigid;
    private CapsuleCollider p_Colider;
    private BoxCollider p_slipColider;
    private PlayerCharacterAbilities player_stats;
    private GameObject currentWall;
    private float p_groundDisplacement;
    private float p_wallDisplacement;
    private float movementX;
    private float movementY;
    private Vector3 direction_vector = new Vector3(0,0,0);
    //private int directions = 0;

    private bool in_air;
    public bool doublejump_enabled = false;
    public bool wallgrab_enabled = false;
    private bool jumps_used = false;
    private bool on_ground = true;
    private bool grabbing = false;

    //Direction Constants
    //int DOWN = 0;
    //int UP = 1;
    int RIGHT = 2;
    int LEFT = 3;

    int current_direction = 2;
    private int old_direction = 2;

    bool fix_state = false;
    bool isTouchObject = false;

    public Material fixedGreen;

    Renderer brokenThing;

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
        p_Colider = GetComponent<CapsuleCollider>();
        p_slipColider = GetComponent<BoxCollider>();
        player_stats = GetComponent<PlayerCharacterAbilities>();
        p_groundDisplacement = p_Colider.bounds.extents.y;
        p_wallDisplacement = p_slipColider.bounds.extents.x;

        in_air = false;

        //SetCountText();
        //winTextObject.SetActive(false);
    }

    //Case handling movement for player object
    void OnMove(InputValue movementValue)
    {
        old_direction = current_direction;
        //Determine direction of player movement
        retrieved_vector = movementValue.Get<Vector2>();
        movementX = retrieved_vector.x;

        if (movementX > 0) {
            current_direction = RIGHT;
            if (old_direction != current_direction){DoFlip();}
        }
        else if (movementX < 0) {
            current_direction = LEFT;
            if (old_direction != current_direction){DoFlip();}
        }
        /*
        if (IsBlocked(directions, p_wallDisplacement)) {
            movementX = 0;
        }
        */
        //movementY = retrieved_vector.y;
    }

    void DoFlip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
    }

    void OnWallGrab(InputValue toggle)
    {
        //Determine direction of player movement
        grabbing = toggle.isPressed;
        if (player_stats.player_energy <= 0) {grabbing = false;}
    }

    void GrabState(bool state)
    {
        if (state && player_stats.player_energy > 0 && wallgrab_enabled) {
          p_slipColider.enabled = false;
          player_stats.player_energy -= grab_cost * Time.deltaTime;
        }
        else {
          p_slipColider.enabled = true;
        }

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
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, p_groundDisplacement + 0.1f);
    }

    bool TouchingWall()
    {
        return Physics.Raycast(transform.position, Vector3.right, p_wallDisplacement + 0.1f) ||
               Physics.Raycast(transform.position, Vector3.left, p_wallDisplacement + 0.1f);
    }

    public void KnockBack(Vector3 source_displacement)
    {
        source_displacement.x *= knockback_x_bias;
        source_displacement.y *= knockback_y_bias;
        p_Rigid.AddForce(source_displacement, ForceMode.Impulse);
    }

    /*
    bool IsBlocked(int direction, float displacement)
    {
        switch(direction)
        {
            case 3:
                direction_vector = Vector3.left;
                break;
            case 2:
                direction_vector = Vector3.right;
                break;
            case 1:
                direction_vector = Vector3.up;
                break;
            case 0:
                direction_vector = Vector3.down;
                break;
        }

        return Physics.Raycast(transform.position, direction_vector, displacement + 0.1f);
    }
    */

    void OnCollisionEnter()
    {
        //Once they touch a surface, set not in air and reset jump tracking
        if (IsGrounded() || (TouchingWall() && grabbing)) {
            on_ground = true;
            in_air = false;
            jumps_used = false;
        }
    }

    void OnCollisionExit()
    {
        //Does nothing for now
        if (!IsGrounded()) {
            on_ground = false;
        }
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

        //Sets velocity for movement instantly, avoiding acceleration
        if (movementX != 0) {
            p_Velocity.Set(movementX > 0 ? speed : -speed, temp_Velocity.y, 0.0f);
            p_Rigid.AddForce(p_Velocity - temp_Velocity, ForceMode.VelocityChange);
        }
        //WIP replaces gravity
        if (linear_decent && !on_ground) {
            p_Velocity.Set(temp_Velocity.x, -descent_speed, 0.0f);
            p_Rigid.AddForce(p_Velocity - temp_Velocity, ForceMode.VelocityChange);
        }
        //Applys movement slowdown after stopping input
        else if (temp_Velocity.x != 0){
            temp_Velocity.x = -temp_Velocity.x * stopping_speed;
            temp_Velocity.y = 0.0f;
            temp_Velocity.z = 0.0f;
            p_Rigid.AddForce(temp_Velocity, ForceMode.VelocityChange);
        }

        p_Rigid.AddForce(Vector3.down * (descent_speed/10f), ForceMode.VelocityChange);

        doFix();
        GrabState(grabbing);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Double_Jump"))
        {
            other.gameObject.SetActive(false);
            doublejump_enabled = true;
        }
        else if (other.gameObject.CompareTag("Wall_Grab")) {
            other.gameObject.SetActive(false);
            wallgrab_enabled = true;
        }
        else if (other.gameObject.CompareTag("Block")) {
            other.gameObject.SetActive(false);
            player_stats.block_enabled = true;
        }
        else if (other.gameObject.CompareTag("Attack_Power")) {
            other.gameObject.SetActive(false);
            player_stats.attack_enabled = true;
        }
        else if (other.gameObject.CompareTag("tool"))
        {
            other.gameObject.SetActive(false);
            player_stats.player_tools += 1;
            // do more stuff
        }
        else if (other.gameObject.CompareTag("objective"))
        {
            brokenThing = other.GetComponent<Renderer>();
            isTouchObject = true;
            doFix();
        }
    }

    void OnFix(InputValue toggle)
    {
        fix_state = toggle.isPressed;
        doFix();
    }

    void doFix()
    {
        if (fix_state & isTouchObject)
        {
            if (player_stats.player_tools >= 3)
            {
                Debug.Log("We did it!");
                player_stats.player_tools -= 3;
                // do stuff with renderer
                brokenThing.material = fixedGreen;
                fix_state = false;
                currentWall = brokenThing.transform.GetChild(0).gameObject;

                if (currentWall != null)
                {
                    currentWall.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchObject = false;
        }
    }

}
