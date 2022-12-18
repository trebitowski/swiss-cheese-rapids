
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject scoreUI;
    public GameObject gameOverUI;
    bool gameHasEnded = false;
    public void EndGame() {
        if (gameHasEnded == false) {
            gameHasEnded = true;
            // Debug.Log("Game Over!");
            scoreUI.SetActive(false);
            gameOverUI.SetActive(true);
        }
    }
}
