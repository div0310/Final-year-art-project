using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;
    public float health;
    bool isGuardCollidingPlayer;
    HealthBarSliderScript _healthBarSliderScript;

    public Rigidbody playerRB;
    public float walking_speed, sneakWalk_speed, oldWalking_speed, rotation_speed;
    //public float sprint_speed;
    public float _playerTurnSpeed = 180f;
    public bool walking;//if player walking true or false
    public Transform playerTrans, modelTrans;
    bool _isMaze;

    public float recoilForce = 500f;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        _healthBarSliderScript = FindObjectOfType<HealthBarSliderScript>();
        _healthBarSliderScript.SetMaxHealth(health); // assign given health in inspector to slider.
        _isMaze = SceneManager.GetActiveScene().name.StartsWith("Maze");

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Guard"))
        {
            // Calculate the direction from the player to the guard

            Vector3 directionToGuard = collision.transform.position - transform.position;
            isGuardCollidingPlayer = true;
            Invoke(nameof(ResetCollisionFlag), 0.5f);

            // Define recoil directions
            Vector3[] recoilDirections = { transform.forward, -transform.right, transform.right };

            // Loop through each recoil direction
            foreach (Vector3 recoilDirection in recoilDirections)
            {
                // Check if the guard is in the specified direction relative to the player
                if (Vector3.Dot(directionToGuard, recoilDirection) > 0)
                {
                    Debug.Log($"Guard is {(recoilDirection == transform.forward ? "in front" : recoilDirection == -transform.right ? "to the left" : "to the right")} of the player");

                    // Apply recoil force in the opposite direction if it's a maze
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
        playerRB.AddForce(recoilDirection * recoilForce);
        health -= 10; // can set a threshold instead of 1.
        _healthBarSliderScript.SetHealth(health);

        if (health < 1)
        {
            //implement restart game logic
        }

        Debug.Log(health);
    }

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

    IEnumerator RotatePlayer(Quaternion targetRotation, float speed)
    {
        float step = speed * Time.deltaTime;
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
            yield return null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //SetRotation(false);
            playerAnim.SetTrigger("Walk");
            playerAnim.ResetTrigger("Idle");
            walking = true;
            //steps1.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("Walk");
            playerAnim.SetTrigger("Idle");
            walking = false;
            //steps1.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetRotation(true);
            playerAnim.SetTrigger("Walk");
            playerAnim.ResetTrigger("Idle");
            walking = true;

            Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + 180f, 0);
            StartCoroutine(RotatePlayer(targetRotation, _playerTurnSpeed));

            //steps1.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("Walk");
            playerAnim.SetTrigger("Idle");
            walking = false;
            //steps1.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAnim.SetTrigger("Sneak Walk");
            playerAnim.ResetTrigger("Idle");
            //steps1.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            playerAnim.ResetTrigger("Sneak Walk");
            playerAnim.SetTrigger("Idle");
            //steps1.SetActive(false);
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
                //steps1.SetActive(false);
                //steps2.SetActive(true);
                walking_speed = walking_speed + rotation_speed;
                playerAnim.SetTrigger("Slow Run");
                playerAnim.ResetTrigger("Walk");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                //steps1.SetActive(true);
                //steps2.SetActive(false);
                walking_speed = oldWalking_speed;
                playerAnim.ResetTrigger("Slow Run");
                playerAnim.SetTrigger("Walk");
            }
        }
    }
}