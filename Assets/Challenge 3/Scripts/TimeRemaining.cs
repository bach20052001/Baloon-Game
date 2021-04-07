using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeRemaining : MonoBehaviour
{
    public float timeToRemain;

    [SerializeField] private float deltaTime;

    private TextMeshProUGUI time;

    // Start is called before the first frame update
    void Start()
    {
        time = GetComponent<TextMeshProUGUI>();
        //default
        timeToRemain = 3;
        deltaTime = 1;

        StartCoroutine(Remaining());
    }

    private IEnumerator Remaining()
    {
        while (timeToRemain > 0)
        {
            time.text = timeToRemain.ToString();
            yield return new WaitForSeconds(deltaTime);
            timeToRemain--;
            if (timeToRemain == 0)
            {
                gameObject.SetActive(false);
            }
        }
        yield return null;
    }
}
