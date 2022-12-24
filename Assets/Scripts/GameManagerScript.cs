
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
            int cheese = FindObjectOfType<Score>().numberCheese;
            int distance = FindObjectOfType<Score>().distanceScore;
            Debug.Log(score);
            Leaderboard leaderBoard = FindObjectOfType<Leaderboard>();
            Debug.Log(leaderBoard);
            StartCoroutine(leaderBoard.SubmitScoreRoutine(score, cheese, distance));
            gameOverUI.SetActive(true);
        }
    }


}
