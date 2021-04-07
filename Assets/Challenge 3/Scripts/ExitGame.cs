using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { Exit(); });
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
