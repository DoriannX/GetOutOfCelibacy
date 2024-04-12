using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //Score
    public int Score;

    //Instance
    public static ScoreManager Instance;

    //Components
    [SerializeField] TextMeshProUGUI _scoreText;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        _scoreText.text = Score.ToString();
    }

    public void ResetScore()
    {
        Score = 0;
        _scoreText.text = Score.ToString();
    }
}
