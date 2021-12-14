using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
{
    private int currentScore = 0;

    TMP_Text scoreText;

    void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }
    public void incrementScore() {
        currentScore++;
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
