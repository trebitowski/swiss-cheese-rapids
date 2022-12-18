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
    // Update is called once per frame
    void Update()
    {
        scoreText.text = ((int)(scorePerCheese * numberCheese) + (int)(scoreCamera.transform.position.y * scorePerDistance)).ToString();
    }

    public void addCheese() {
        numberCheese += 1;
    }
}
