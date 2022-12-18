using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRiver : MonoBehaviour
{
    private float perlinFreq = 0.065f; // frequency of perlin noise
    private float perlinAmp = 15;  // amplitude of perlin noise
    private float riverWid = 20;  // approximate rive width
    private float riverLength = 35;
    [SerializeField] GameObject water;
    // Start is called before the first frame update
    void Start(){
        Generation();
    }

    // Update is called once per frame
    void Update(){
        //Generation();
    }

    void Generation(){
        float edgeDist1, edgeDist2;
        float t = 0;
        float stepSz = 0.5f;
        float offset = 15;
        Vector2 waterPos, waterEdge1, waterEdge2;
        for (int ind = 0; ind <= (int)(riverLength/stepSz); ind++)
        {
            waterPos = riverFuntion(t);
            edgeDist1 = -riverWid/2.0f + perlinAmp*(Mathf.PerlinNoise(perlinFreq*t + offset, 5000.0f) - 0.5f)*2.0f; // dist from river center to first river edge
            edgeDist2 = riverWid/2.0f + perlinAmp*(Mathf.PerlinNoise(perlinFreq*t + offset, 5000.0f) - 0.5f)*2.0f; // dist from river center to second river edge
            waterEdge1 = waterPos + riverFuntionPerp(edgeDist1);
            waterEdge2 = waterPos + riverFuntionPerp(edgeDist2);

            // first river side
            spawnObj(water, (int)waterEdge1.x, (int)waterEdge1.y);
            // second river side
            spawnObj(water, (int)waterEdge2.x, (int)waterEdge2.y);

            t = t + stepSz;
        }
    }

    // Spawn an instance of the game object
    GameObject spawnObj(GameObject obj, int width, int height){
        GameObject objInst = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        objInst.transform.parent = this.transform;
        return objInst;
    }

    // Parametric function (one input, two outputs) describing river center curve
    Vector2 riverFuntion(float t){
        Vector2 pos = new Vector2();
        // pos.x = 25f*(t-0.5f);
        // pos.y = 25f*4.0f*(t-0.5f);
        pos.x = 0.0f;
        pos.y = t-riverLength/2.0f;
        return pos;
    }

    // Parametric function perpendicular to the river curve
    Vector2 riverFuntionPerp(float t){
        Vector2 perp = new Vector2();
        // perp.x = -4.0f*t;
        // perp.y = t;
        perp.x = t;
        perp.y = 0;
        return perp;
    }
}
