using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody player;

    // Movement + Cooldown
    [Header("Movement Var")]
    private Vector3 targetDirection;
    private float changeDirectionCooldown;

    // Speed
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;

    // Collect components
    private Rigidbody enemyRb;
    private EnemyAwareness enemyAwareness;

    // Keep the car on the ground (thanks Finn)
    private float yHeight = 0f;

    [Header("Raycast")]
    private LayerMask obstacleLayerMask;
    [Min(1)] private float hitRange = 4;
    private RaycastHit hit;
    public Transform thisEnemyPos;

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyAwareness = GetComponent<EnemyAwareness>();
        targetDirection = transform.forward;
        yHeight = transform.position.y;
    }

    // Call these functions
    void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateToTarget();
        SetVelocity();
        KeepCarOnGroundHeight();
        ObstacleCheck();
    }

    void KeepCarOnGroundHeight()
    {
        // for car to the starting height (ground)
        transform.position = new Vector3(transform.position.x, yHeight, transform.position.z);
    }

    void UpdateTargetDirection()
    {
        //Start with possible direction change, unless awareness area entered - then target player
        PlayerTargeting();
        RandomDirectionChange();
    }

    // Make sure cooldown is aligned with frame rate time (in case of skipping), and if cooldown runs out, change direction
    // Random angle to pick, then rotate around angle axis - for target direction movement rotate towards it
    // Wait before starting change again
    void RandomDirectionChange()
    {
        changeDirectionCooldown -= Time.fixedDeltaTime;

        if (changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-45f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            targetDirection = rotation * targetDirection;

            changeDirectionCooldown = Random.Range(1, 5);
            //Debug.Log("Picked new direction at random: " + angleChange);
        }
    }

    void PlayerTargeting()
    {
        if (enemyAwareness.AwareofPlayer)
        {
            targetDirection = enemyAwareness.DesiredDir;
        }
    }

    void RotateToTarget()
    {
        // Otherwise, use Quaternion to change movement (including rotation - from current position "forward" to the target direction Vector2 ref'd above)
        // Rotation = (current rotation, to target, at the defined speed * frame rate) - then set to the rigidbody below.

        // Quaternion targetRotation = Quaternion.LookRotation(targetDirection, new Vector3(0, 0, 1)) ;
        // Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        float desiredAngle = Mathf.Atan2(targetDirection.x, targetDirection.z);
        transform.localEulerAngles = new Vector3(0, desiredAngle, 0) * rotationSpeed;

        Debug.Log("Using angle: " + desiredAngle);
    }

    // Again if 0, ignore (don't move), otherwise move on Y axis at x speed
    void SetVelocity()
    {
        enemyRb.velocity = transform.forward * speed;
    }

    void ObstacleCheck()
    {
        // The raycast hits based on player's position and within range, only check interactionLayerMask - out 'saves' the hit to check
        if (Physics.Raycast(thisEnemyPos.position, thisEnemyPos.forward, out hit, hitRange, obstacleLayerMask))
        {
            // Avoid obstacles
            RandomDirectionChange();
            Debug.Log("Colliding with " + gameObject.name);
        }

        /*if (hit.collider != null)
        {
            
        }*/

        else return;
    }
}


//This is a demonstration of counters, nothing to do with the adjoining code - Finn
//float timeOfLastJump;
//void Jump(){
//  if (Time.time - timeOfLastJump > jumpCooldown)
//     timeOfLastJump = Time.time;
//}

/*
  * Ignore the enemy rotation - treat that as an emergent graphical feature.
  * Constantly move the enemy forward.
  * Add its target velocity to its current velocity, then renormalise.
  * 
  * 
  * 
  */