using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject scoreCamera;
    public float scorePerDistance;
    public int scorePerCheese;
    public int numberCheese;

    public float speedDifficultyStep = 50f;
    public float maxSpawnRateAtDistance = 1000f;

    public static float speedDifficulty = 0;
    public static float spawnDifficulty = 1;
    public static int spawnTries = 1;
    // Update is called once per frame
    void Update()
    {
        int distanceScore = (int)(scoreCamera.transform.position.y * scorePerDistance);
        int score = (int)(scorePerCheese * numberCheese) + distanceScore;
        scoreText.text = score.ToString();

        speedDifficulty = ((int)(distanceScore / speedDifficultyStep)) / 2.0f;
        spawnDifficulty = Mathf.InverseLerp(0, maxSpawnRateAtDistance, distanceScore);
        if (spawnTries == 1 && distanceScore > 100) {
            spawnTries = 2;
        } else if (spawnTries == 2 && distanceScore > 250) {
            spawnTries = 3;
        }
    }

    public void addCheese() {
        numberCheese += 1;
    }
}
