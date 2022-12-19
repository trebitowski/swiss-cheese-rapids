using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRiver : MonoBehaviour
{
    private float perlinFreq = 0.008f; // frequency of perlin noise
    private float perlinAmp = 4;  // amplitude of perlin noise
    private int riverLength = 600;
    private float t;
    private float startTime;
    public float speed; // units per second
    private int pixel_height = 3; // pixel height of water/bank layer
    private float unit_height = 10; // number of pixels in a Unity unit
    [SerializeField] GameObject water;
    [SerializeField] GameObject bank_left;
    [SerializeField] GameObject bank_right;
    public GameObject[] cheeses;
    public GameObject[] obstacles;
    public GameObject[] sieves;
    public GameObject whiteCaps;

    public float sieveChance = 0.05f;
    private int sieveCounter = 0;
    public int obstacleTries;
    public float spawnWidthRange; // the total range of the spawnable width of the river
    Vector2 waterEdge;

    public float obstacleRate;
    public float maxObstacleRate;
    private float obstacleTimer;
    public float obstacleHeightVariation;
    public float cheeseRate;
    public float maxCheeseRate;
    private float cheeseTimer;
    private float whiteCapsTimer = 0;
    public float whiteCapsRate;
    private float camSize = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        Generation();
        obstacleTimer = obstacleRate; // spawn obstacles right away
    }

    // Update is called once per frame
    void Update()
    {
        float offset = 15;
        float camPos = Camera.main.transform.position.y;
        float buffer = 40.0f;

        obstacleTimer += Time.deltaTime;
        cheeseTimer += Time.deltaTime;
        whiteCapsTimer += Time.deltaTime;

        if (waterEdge.y / unit_height < camPos + buffer)
        {
            t += (float)(pixel_height - 1);
            // add top layer of water
            Vector2 waterPos = riverFuntion(t);
            float edgeDist = perlinAmp * (Mathf.PerlinNoise(perlinFreq * t, 0.25f * perlinFreq * t) - 0.5f) * 2.0f; // dist from river center to first river edge
            float bankLeftPos = Mathf.PerlinNoise(perlinFreq * t + offset, perlinFreq * t); // dist from river center to first river edge
            float bankRightPos = Mathf.PerlinNoise(perlinFreq * t + 2 * offset, perlinFreq * t); // dist from river center to first river edge            waterEdge = waterPos + riverFuntionPerp(edgeDist);
            waterEdge = waterPos + riverFuntionPerp(edgeDist);
            spawnRiver(water, (float)((int)(waterEdge.x * 10.0f)) / 10.0f, (float)((int)(waterEdge.y / unit_height * 10.0f)) / 10.0f);
            spawnRiver(bank_left, (float)((int)(waterEdge.x * 10.0f) - Mathf.Lerp(90, 110, bankLeftPos)) / 10.0f, (float)((int)(waterEdge.y / unit_height * 10.0f)) / 10.0f);
            spawnRiver(bank_right, (float)((int)(waterEdge.x * 10.0f) + Mathf.Lerp(90, 110, bankRightPos)) / 10.0f, (float)((int)(waterEdge.y / unit_height * 10.0f)) / 10.0f);


            if (obstacleTimer > Mathf.Lerp(obstacleRate, maxObstacleRate, Score.spawnDifficulty))
            {
                if (sieveCounter <= 0 && Random.value < sieveChance)
                {
                    spawnSieve(waterEdge.x, waterEdge.y / unit_height);
                    sieveCounter = 5;
                }
                else
                {
                    sieveCounter--;
                    int count = Random.Range(1, Score.spawnTries + 1);
                    List<float> positions = new List<float>();
                    for (int i = 0; i < count; i++)
                    {
                        float newPosition = Random.Range(waterEdge.x - spawnWidthRange, waterEdge.x + spawnWidthRange);
                        bool valid = true;
                        foreach (float position in positions)
                        {
                            if (Mathf.Abs(position - newPosition) < 3)
                            {
                                valid = false;
                            }
                        }
                        if (valid == true)
                        {
                            positions.Add(newPosition);
                            spawnObstacle(newPosition, Random.Range(waterEdge.y / unit_height - obstacleHeightVariation, waterEdge.y / unit_height + obstacleHeightVariation));
                        }
                    }
                }
                obstacleTimer = Random.Range(0, 0.5f);
            }

            if (cheeseTimer > Mathf.Lerp(cheeseRate, maxCheeseRate, Score.spawnDifficulty))
            {
                spawnCheese(waterEdge.x, waterEdge.y / unit_height);
                cheeseTimer = Random.Range(0, cheeseTimer);
            }

            if (whiteCapsTimer > whiteCapsRate)
            {
                float rapidsRadius = 11.4f/2.0f;
                int numCaps = 1;
                for (int i = 0; i < numCaps; i++)
                {
                    float xPos = Random.Range(waterPos.x - rapidsRadius, waterPos.x + rapidsRadius);
                    float yPos = Random.Range(camPos - camSize, camPos + camSize);
                    spawnWhiteCaps(xPos, yPos);                    
                }
                whiteCapsTimer = Random.Range(0, whiteCapsTimer);
            }

        }
    }

    void Generation()
    {
        float edgeDist;
        float offset = 15;
        Vector2 waterPos;
        for (int ind = 0; ind < riverLength; ind += pixel_height - 1)
        {
            t = (float)ind;
            waterPos = riverFuntion(t);
            edgeDist = perlinAmp * (Mathf.PerlinNoise(perlinFreq * t, 0.25f * perlinFreq * t) - 0.5f) * 2.0f; // dist from river center to first river edge
            float bankLeftPos = Mathf.PerlinNoise(perlinFreq * t + offset, perlinFreq * t); // dist from river center to first river edge
            float bankRightPos = Mathf.PerlinNoise(perlinFreq * t + 2 * offset, perlinFreq * t); // dist from river center to first river edge
            waterEdge = waterPos + riverFuntionPerp(edgeDist);

            // first river side
            spawnRiver(water, (float)((int)(waterEdge.x * 10.0f)) / 10.0f, (float)((int)(waterEdge.y / unit_height * 10.0f)) / 10.0f);
            spawnRiver(bank_left, (float)((int)(waterEdge.x * 10.0f) - Mathf.Lerp(90, 110, bankLeftPos)) / 10.0f, (float)((int)(waterEdge.y / unit_height * 10.0f)) / 10.0f);
            spawnRiver(bank_right, (float)((int)(waterEdge.x * 10.0f) + Mathf.Lerp(90, 110, bankRightPos)) / 10.0f, (float)((int)(waterEdge.y / unit_height * 10.0f)) / 10.0f);
        }
    }

    // Spawn an instance of the game object
    void spawnRiver(GameObject obj, float width, float height)
    {
        GameObject objInst = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        objInst.transform.parent = this.transform;
    }

    // Parametric function (one input, two outputs) describing river center curve
    Vector2 riverFuntion(float t)
    {
        Vector2 pos = new Vector2();
        pos.x = 0.0f;
        pos.y = t - riverLength / 2.0f;
        return pos;
    }

    // Parametric function perpendicular to the river curve
    Vector2 riverFuntionPerp(float t)
    {
        Vector2 perp = new Vector2();
        perp.x = t;
        perp.y = 0;
        return perp;
    }

    void spawnCheese(float width, float height)
    {
        GameObject objInst = Instantiate(cheeses[Random.Range(0, cheeses.Length)], new Vector2(Random.Range(width - spawnWidthRange, width + spawnWidthRange), height), Quaternion.identity);
        objInst.transform.parent = this.transform;
    }

    void spawnWhiteCaps(float width, float height)
    {
        GameObject objInst = Instantiate(whiteCaps, new Vector2(width, height), Quaternion.identity);
        objInst.transform.parent = this.transform;
    }

    void spawnObstacle(float width, float height)
    {
        int ind = Random.Range(0, obstacles.Length);
        Quaternion rotation = Quaternion.identity;
        // check if its a log
        if (obstacles[ind].name == "Log0")
        {
            rotation = Quaternion.AngleAxis(Random.Range(-90, 90), Vector3.forward);
        }
        GameObject objInst = Instantiate(obstacles[ind], new Vector2(width, height), rotation);
        objInst.transform.parent = this.transform;
    }

    void spawnSieve(float width, float height)
    {
        int direction = Random.Range(0, 3);
        int index = direction + (Score.sieveDifficulty * 3);
        GameObject objInst = Instantiate(sieves[index], new Vector2(width, height), Quaternion.identity);
        objInst.transform.parent = this.transform;
    }
}