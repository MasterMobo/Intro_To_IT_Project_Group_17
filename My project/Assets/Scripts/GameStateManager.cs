using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameOverText gameOverText;
    public void GameOverScreen()
    {
        gameOverText.gameObject.SetActive(true);
    }
}
