
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject scoreUI;
    public GameObject gameOverUI;
    
    bool gameHasEnded = false;
    public void EndGame() {
        if (gameHasEnded == false) {
            gameHasEnded = true;
            // Debug.Log("Game Over!");
            //scoreUI.SetActive(false);
            int score = FindObjectOfType<Score>().totalScore;
            Debug.Log(score);
            Leaderboard leaderBoard = FindObjectOfType<Leaderboard>();
            Debug.Log(leaderBoard);
            StartCoroutine(leaderBoard.SubmitScoreRoutine(score));
            gameOverUI.SetActive(true);
        }
    }


}
