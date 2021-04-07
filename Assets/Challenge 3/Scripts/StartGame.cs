using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { PlayGame(); });
    }
    private void PlayGame()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
