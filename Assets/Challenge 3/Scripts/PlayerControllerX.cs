﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;

    public float floatForce = 250;
    private float gravityModifier = 1.5f;
    private float topBound;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;


    // Start is called before the first frame update
    void Start()
    {
        topBound = ScreenBound.Instance.GetHeight();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
#if UNITY_WEBGL || UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            if (transform.position.y < topBound)
            {
                playerRb.AddForce(Vector3.up * floatForce);
            }
        }
#endif

#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        if (Input.touchCount == 1 && !gameOver)
        {
            if (Input.GetTouch(0).tapCount > 0)
            {
                if (transform.position.y < topBound)
                {
                    playerRb.AddForce(Vector3.up * floatForce / 12);
                }
            }
        }
#endif
        playerRb.gameObject.transform.position = new Vector3(playerRb.gameObject.transform.position.x
                                                            , Mathf.Clamp(playerRb.gameObject.transform.position.y, 1, topBound - 1)
                                                            , playerRb.gameObject.transform.position.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }

    }

}
