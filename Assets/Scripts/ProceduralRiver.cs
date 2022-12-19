using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRiver : MonoBehaviour
{
    private float perlinFreq = 0.008f; // frequency of perlin noise
    private float perlinAmp = 4;  // amplitude of perlin noise
    private int riverLength = 210;
    private float t;
    private float startTime;
    public float speed; // units per second
    private float unit_height = 10; // number of pixels in a Unity unit
    [SerializeField] GameObject water;
    public GameObject[] cheeses;
    public GameObject[] obstacles;
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
    // Start is called before the first frame update
    void Start(){
        Generation();
        obstacleTimer = obstacleRate; // spawn obstacles right away
        //startTime = Time.time;
    }

    // Update is called once per frame
    // void Update(){
    //     float offset = 15;
    //     float timeOffset;
    //     int layercount;
    //     // wait some deltaTime
    //     if (Time.time - startTime >= 1.0f/(unit_height*speed))
    //     {
    //         timeOffset = Time.time - startTime - 1.0f/(unit_height*speed);
    //         t += 1.0f;
    //         // add top layer of water
    //         Vector2 waterPos = riverFuntion(t);
    //         float edgeDist = perlinAmp*(Mathf.PerlinNoise(perlinFreq*t + offset, 5000.0f) - 0.5f)*2.0f; // dist from river center to first river edge
    //         Vector2 waterEdge = waterPos + riverFuntionPerp(edgeDist);
    //         layercount = transform.childCount;
    //         Transform lastChild = transform.GetChild(layercount-1);
    //         spawnObj(water, waterEdge.x, (int)(lastChild.position.y + 0.1f));
    //         startTime = startTime + 1.0f/(unit_height*speed);
    //     }
    // }


    // Update is called once per frame
    void Update(){
        float offset = 15;
        float camTopPos = 40.0f + Camera.main.transform.position.y;

        obstacleTimer += Time.deltaTime;
        cheeseTimer += Time.deltaTime;

        if (waterEdge.y/unit_height < camTopPos)
        {
            t += 1.0f;
            // add top layer of water
            Vector2 waterPos = riverFuntion(t);
            float edgeDist = perlinAmp*(Mathf.PerlinNoise(perlinFreq*t + offset, 0.25f*perlinFreq*t) - 0.5f)*2.0f; // dist from river center to first river edge
            waterEdge = waterPos + riverFuntionPerp(edgeDist);
            spawnRiver(water, (float)((int)(waterEdge.x*10.0f))/10.0f, (float)((int)(waterEdge.y/unit_height*10.0f))/10.0f);

                  
            if (obstacleTimer > Mathf.Lerp(obstacleRate, maxObstacleRate, Score.spawnDifficulty)) {
                 int count = Random.Range(1, Score.spawnTries + 1);
                for (int i = 0; i < count; i++)
                {
                    spawnObstacle(waterEdge.x, Random.Range(waterEdge.y/unit_height - obstacleHeightVariation, waterEdge.y/unit_height + obstacleHeightVariation));   
                } 
                obstacleTimer = Random.Range(0, 0.5f);
            }

            
            if (cheeseTimer > Mathf.Lerp(cheeseRate, maxCheeseRate, Score.spawnDifficulty)) {
                spawnCheese(waterEdge.x, waterEdge.y/unit_height);    
                cheeseTimer = Random.Range(0, cheeseTimer);
            }
        }

        

    }

    void Generation(){
        float edgeDist;
        float offset = 15;
        Vector2 waterPos;
        for (int ind = 0; ind < riverLength; ind++)
        {
            t = (float)ind;
            waterPos = riverFuntion(t);
            edgeDist = perlinAmp*(Mathf.PerlinNoise(perlinFreq*t + offset, 0.25f*perlinFreq*t) - 0.5f)*2.0f; // dist from river center to first river edge
            waterEdge = waterPos + riverFuntionPerp(edgeDist);

            // first river side
            spawnRiver(water, (float)((int)(waterEdge.x*10.0f))/10.0f, (float)((int)(waterEdge.y/unit_height*10.0f))/10.0f);
        }
    }

    // Spawn an instance of the game object
    void spawnRiver(GameObject obj, float width, float height){
        GameObject objInst = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        objInst.transform.parent = this.transform;
    }

    // Parametric function (one input, two outputs) describing river center curve
    Vector2 riverFuntion(float t){
        Vector2 pos = new Vector2();
        pos.x = 0.0f;
        pos.y = t-riverLength/2.0f;
        return pos;
    }

    // Parametric function perpendicular to the river curve
    Vector2 riverFuntionPerp(float t){
        Vector2 perp = new Vector2();
        perp.x = t;
        perp.y = 0;
        return perp;
    }

    void spawnCheese(float width, float height){
        GameObject objInst = Instantiate(cheeses[Random.Range(0,cheeses.Length)], new Vector2(Random.Range(width - spawnWidthRange, width + spawnWidthRange), height), Quaternion.identity);
        objInst.transform.parent = this.transform;
    }

    void spawnObstacle(float width, float height){
        GameObject objInst = Instantiate(obstacles[Random.Range(0,obstacles.Length)], new Vector2(Random.Range(width - spawnWidthRange, width + spawnWidthRange), height), Quaternion.identity);
        objInst.transform.parent = this.transform;
    }
}