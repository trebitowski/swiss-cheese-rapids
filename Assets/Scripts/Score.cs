using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI cheeseText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI distanceGameOverText;
    public TextMeshProUGUI cheeseGameOverText;
    public TextMeshProUGUI scoreGameOverText;
    public TextMeshProUGUI multiplierGameOverText;
    public GameObject scoreCamera;
    public float scorePerDistance;
    public int scorePerCheese;
    public int numberCheese;

    public float speedDifficultyStep = 50f;
    public float maxSpawnRateAtDistance = 1000f;

    public static float speedDifficulty = 0;
    public static float spawnDifficulty = 1;
    public static int spawnTries = 1;
    public static int sieveDifficulty = 0;

    public int totalScore;

    void Start() {
        speedDifficulty = 0;
        spawnDifficulty = 1;
        spawnTries = 1;
        sieveDifficulty = 0;

        multiplierGameOverText.text = scorePerCheese.ToString() + "x";
    }

    // Update is called once per frame
    void Update()
    {
        int distanceScore = (int)(scoreCamera.transform.position.y * scorePerDistance);
        // int score = (int)(scorePerCheese * numberCheese) + distanceScore;
        cheeseText.text = numberCheese.ToString();
        distanceText.text = distanceScore.ToString();

        cheeseGameOverText.text = numberCheese.ToString();
        distanceGameOverText.text = distanceScore.ToString();

        totalScore = (int)(numberCheese * scorePerCheese) + distanceScore;
        scoreGameOverText.text =  totalScore.ToString();
        speedDifficulty = ((int)(distanceScore / speedDifficultyStep)) / 2.0f;
        spawnDifficulty = Mathf.InverseLerp(0, maxSpawnRateAtDistance, distanceScore);
        if (spawnTries == 1 && distanceScore > 75) {
            spawnTries = 2;
        } else if (spawnTries == 2 && distanceScore > 150) {
            spawnTries = 3;
        }

        if (sieveDifficulty == 0 && distanceScore > 75) {
            sieveDifficulty = 1;
        } else if (sieveDifficulty == 1 && distanceScore > 150) {
            sieveDifficulty = 2;
        } else if (sieveDifficulty == 2 && distanceScore > 225) {
            sieveDifficulty = 3;
        } else if (sieveDifficulty == 3 && distanceScore > 300) {
            sieveDifficulty = 4;
        }
    }

    public void addCheese() {
        numberCheese += 1;
    }
}
