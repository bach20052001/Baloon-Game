using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Replay : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { ReplayGame(); });
    }

    public void ReplayGame()
    {   
        Score.ScoreInstance = 0;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
