
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

            Score scoreObject = FindObjectOfType<Score>();
            int score = scoreObject.totalScore;
            int cheese = scoreObject.numberCheese;
            int distance = scoreObject.distanceScore;
            // Debug.Log(score);
            Leaderboard leaderBoard = FindObjectOfType<Leaderboard>();
            // Debug.Log(leaderBoard);

            //Get local highscores and update if new scores are better
            int oldScore = PlayerPrefs.GetInt("totalScore", 0);
            int oldCheese = PlayerPrefs.GetInt("cheeseScore", 0);
            int oldDistance = PlayerPrefs.GetInt("distanceScore", 0);
            if (oldScore < score) PlayerPrefs.SetInt("totalScore", score);
            if (oldCheese < cheese) PlayerPrefs.SetInt("cheeseScore", cheese);
            if (oldDistance < distance) PlayerPrefs.SetInt("distanceScore", distance);

            StartCoroutine(leaderBoard.SubmitScoreRoutine(score, cheese, distance));
            gameOverUI.SetActive(true);
        }
    }


}
