using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    private static int score;

    public static int ScoreInstance
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = score.ToString();
        this.RegisterListener(EventID.OnHitMoney, (param) => OnHitMoneyHandler());

    }

    private void OnHitMoneyHandler()
    {
        score += 10;
        text.text = score.ToString();
    }
}
