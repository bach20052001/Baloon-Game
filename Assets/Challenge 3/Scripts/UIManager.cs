using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject scorePanels;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject resumePanel;
    [SerializeField] private GameObject timeRemaining;




    // Start is called before the first frame update
    void Start()
    {
        scorePanels.SetActive(true);
        gameOverPanel.SetActive(false);
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        pause.SetActive(true);
#endif
        resumePanel.SetActive(false);
        this.RegisterListener(EventID.OnHitBomb, (param) => OnHitBombHandler());
        this.RegisterListener(EventID.OnGamePause, (param) => OnGamePauseHandler());
        this.RegisterListener(EventID.OnGamePlay, (param) => OnGamePlayHandler());
        this.RegisterListener(EventID.OnPreGame, (param) => OnPreGameHandler());


    }

    private void OnPreGameHandler()
    {
        timeRemaining.SetActive(true);
    }

    private void OnGamePlayHandler()
    {
        resumePanel.SetActive(false);
    }

    private void OnGamePauseHandler()
    {
        resumePanel.SetActive(true);
    }

    private void OnHitBombHandler()
    {
        scorePanels.SetActive(false);
        gameOverPanel.SetActive(true);
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        pause.SetActive(false);
#endif
    }
}
