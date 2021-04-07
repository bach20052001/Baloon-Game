using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveLeftX : MonoBehaviour
{
    public float speed = 30;
    private float leftBound = -10;


    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        else
        // If game is not over, move to the left
        if (GameManager.Instance.GameState == GameBaseState.PLAY )
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        // If object goes off screen that is NOT the background, destroy it
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }
}
