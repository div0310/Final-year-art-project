using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;
    public HealthBarSystem healthBar;
    public int maxHealth = 100;
    public int currentHealth;
    bool isGuardCollidingPlayer;

    public Rigidbody playerRB;
    public float walking_speed, sneakWalk_speed, oldWalking_speed, rotation_speed;
    public float _playerTurnSpeed = 180f;
    public bool walking;//if player walking true or false
    public Transform playerTrans, modelTrans;
    bool _isMaze;

    public float recoilForce = 500f;
    public float recoilForceMagnitude = 1.0f;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        healthBar = FindObjectOfType<HealthBarSystem>();

        // make sure the health bar system is same between scenes
        DontDestroyOnLoad(healthBar.gameObject);

        // Set initial health
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

        _isMaze = SceneManager.GetActiveScene().name.StartsWith("Maze");

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Guard"))
        {
            // calculate direction from the player to the guard

            Vector3 directionToGuard = collision.transform.position - transform.position;
            isGuardCollidingPlayer = true;
            Invoke(nameof(ResetCollisionFlag), 0.5f);

            // define recoil directions
            Vector3[] recoilDirections = { transform.forward, -transform.right, transform.right };

            // loop through each recoil direction
            foreach (Vector3 recoilDirection in recoilDirections)
            {
                // check if the guard is in the specified direction relative to the player
                if (Vector3.Dot(directionToGuard, recoilDirection) > 0)
                {
                    Debug.Log($"Guard is {(recoilDirection == transform.forward ? "in front" : recoilDirection == -transform.right ? "to the left" : "to the right")} of the player");

                    // apply recoil force in the opposite direction if it's a maze
                    Vector3 oppositeDirection = -recoilDirection;
                    Vector3 recoilForce = _isMaze ? Vector3.zero : oppositeDirection * 10;

                    TakeDamage(recoilForce);
                    break; // Exit the loop once recoil direction is found
                }
            }
        }
    }

    private void ResetCollisionFlag() => isGuardCollidingPlayer = false;


    void TakeDamage(Vector3 recoilDirection)
    {
        // calculate recoil force based on the recoilDirection and recoilForce
        Vector3 recoilForce = recoilDirection.normalized * recoilForceMagnitude;

        // apply the recoil force to the player's Rigidbody
        playerRB.AddForce(recoilForce);

        // decrease player's health
        currentHealth -= 10;

        //update Health
        healthBar.SetHealth(currentHealth);

        // check if player's health has reached zero
        if (currentHealth <= 0)
        {
            // implement game over or restart logic here
            SceneManager.LoadScene("Game Over Scene");
        }

        Debug.Log("Player took damage. Health: " + currentHealth);
    }

    //function for player rotation
    void SetRotation(bool faceCamera)
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        Quaternion rotation = faceCamera ? Quaternion.LookRotation(-cameraForward) : Quaternion.LookRotation(cameraForward);//set rotation to opposite 
        modelTrans.rotation = rotation;
    }
    void FixedUpdate()
    {
        if (!isGuardCollidingPlayer) // this flag necessary to prevent rb velocity and addforce contradiction.
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerRB.velocity = transform.forward * walking_speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerRB.velocity = -transform.forward * walking_speed * Time.deltaTime;
            }
        }
    }

    //Player movement code
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetRotation(false);
            playerAnim.SetTrigger("Walk");
            playerAnim.ResetTrigger("Idle");
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("Walk");
            playerAnim.SetTrigger("Idle");
            walking = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetRotation(true);
            playerAnim.SetTrigger("Walk");
            playerAnim.ResetTrigger("Idle");
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("Walk");
            playerAnim.SetTrigger("Idle");
            walking = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAnim.SetTrigger("Sneak Walk");
            playerAnim.ResetTrigger("Idle");
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            playerAnim.ResetTrigger("Sneak Walk");
            playerAnim.SetTrigger("Idle");
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -rotation_speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, rotation_speed * Time.deltaTime, 0);
        }
        if (walking == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                walking_speed = walking_speed + rotation_speed;
                playerAnim.SetTrigger("Slow Run");
                playerAnim.ResetTrigger("Walk");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                walking_speed = oldWalking_speed;
                playerAnim.ResetTrigger("Slow Run");
                playerAnim.SetTrigger("Walk");
            }
        }
    }
}
