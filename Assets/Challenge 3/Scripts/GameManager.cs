using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private PlayerControllerX player;
    public GameBaseState GameState;

    [SerializeField] private TimeRemaining timeRemaining;

    public static GameManager Instance
    {
        get
        {
            //if (instance == null || SceneManager.GetActiveScene().buildIndex != 0)
            //{
            //    GameObject gameManager = new GameObject("Game Manager");
            //    Instantiate(gameManager);
            //    GameManager _instance = gameManager.AddComponent<GameManager>();
            //    instance = _instance;
            //}

            return instance;
        }
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        if (this != instance)
        {
            Destroy(gameObject);
        }

        TransitionGameState(GameBaseState.PREPLAY);
    }

    public void TransitionGameState(GameBaseState newGameState)
    {
        switch (newGameState)
        {
            case GameBaseState.PREPLAY:
                Preplay();
                break;
            case GameBaseState.PLAY:
                PlayGame();
                break;
            case GameBaseState.PAUSE:
                PauseGame();
                break;
            case GameBaseState.GAMEOVER:
                break;
        }
        GameState = newGameState;
    }

    public void Preplay()
    {
        StartCoroutine(RemainingTime());
    }

    IEnumerator RemainingTime()
    {
        timeRemaining.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeRemaining.timeToRemain);
        player.gameObject.SetActive(true);
        TransitionGameState(GameBaseState.PLAY);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        this.PostEvent(EventID.OnGamePause);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        this.PostEvent(EventID.OnGamePlay);
    }
}
