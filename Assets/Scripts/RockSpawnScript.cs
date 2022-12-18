using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawnScript : MonoBehaviour
{
    public GameObject rock;
    public float spawnRate = 2;
    private float timer = 0;
    public float riverOffset = 7;
    // Start is called before the first frame update
    void Start()
    {
        spawnRock();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate) {
            timer += Time.deltaTime;
        } else {
            int count = Random.Range(1,5);
            for (int i = 0; i < count; i++)
            {
                spawnRock();
            }       
            timer = 0;
        }
    }

    void spawnRock() {
        float lowestPoint = transform.position.x - riverOffset;
        float highestPoint = transform.position.x + riverOffset;
        

        Instantiate(rock, new Vector3(Random.Range(lowestPoint, highestPoint), transform.position.y, transform.position.z), transform.rotation);
    }
}
