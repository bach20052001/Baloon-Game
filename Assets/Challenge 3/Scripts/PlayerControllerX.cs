using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{

    public float floatForce = 250;
    private float gravityModifier = 1.5f;
    private float topBound;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    private GameManager gameManager;

  
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
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
        if (transform.position.y <= 1)
        {
            this.PostEvent(EventID.OnHitBomb);
            GameManager.Instance.TransitionGameState(GameBaseState.GAMEOVER);
        }

        // While space is pressed and player is low enough, float up
#if UNITY_WEBGL || UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.GameState == GameBaseState.PLAY)
        {
            if (transform.position.y < topBound)
            {
                playerRb.AddForce(Vector3.up * floatForce);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameManager.GameState == GameBaseState.PLAY)
            {
                gameManager.TransitionGameState(GameBaseState.PAUSE);

            }
            else if (gameManager.GameState == GameBaseState.PAUSE)
            {
                gameManager.TransitionGameState(GameBaseState.PLAY);
            }
        }
        
#endif

#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        if (Input.touchCount == 1 && gameManager.GameState == GameBaseState.PLAY)
        {
            if (Input.GetTouch(0).phase > 0)
            {
                if (transform.position.y < topBound)
                {
                    playerRb.AddForce(Vector3.up * floatForce / 12);
                }
            }
        }
#endif
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Screen"))
        {
            this.PostEvent(EventID.OnHitBomb);
            GameManager.Instance.TransitionGameState(GameBaseState.GAMEOVER);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            this.PostEvent(EventID.OnHitBomb);
            GameManager.Instance.TransitionGameState(GameBaseState.GAMEOVER);

            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            
            Destroy(other.gameObject);
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            this.PostEvent(EventID.OnHitMoney);
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
