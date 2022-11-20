using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Info")]
    [SerializeField] private Text _scoreText;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _liveImage;
    [SerializeField] private GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: "+ 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int _playerScore)
    {
        _scoreText.text = "Score: " + _playerScore.ToString();
    }

    public void LivesImage(int currentLives)
    {
        _liveImage.sprite = _livesSprites[currentLives];
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
