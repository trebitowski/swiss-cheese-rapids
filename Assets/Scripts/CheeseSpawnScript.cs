using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseSpawnScript : MonoBehaviour
{
    public GameObject cheese;
    public float spawnRate = 5;
    private float timer = 0;
    public float riverOffset = 7;
    // Start is called before the first frame update
    void Start()
    {
        spawnCheese();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate) {
            timer += Time.deltaTime;
        } else {
            spawnCheese();      
            timer = Random.Range(0, 2);
        }
    }

    void spawnCheese() {
        float lowestPoint = transform.position.x - riverOffset;
        float highestPoint = transform.position.x + riverOffset;
        

        Instantiate(cheese, new Vector3(Random.Range(lowestPoint, highestPoint), transform.position.y, transform.position.z), transform.rotation);
    }
}
