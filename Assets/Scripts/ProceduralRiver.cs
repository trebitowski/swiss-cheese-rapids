using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRiver : MonoBehaviour
{
    private float perlinFreq = 0.008f; // frequency of perlin noise
    private float perlinAmp = 5;  // amplitude of perlin noise
    private int riverLength = 201;
    private float t;
    GameObject[] waterArray;

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
        float edgeDist;
        float offset = 15;
        Vector2 waterPos, waterEdge;
        for (int ind = 0; ind <= riverLength; ind++)
        {
            t = (float)ind;
            waterPos = riverFuntion(t);
            edgeDist = perlinAmp*(Mathf.PerlinNoise(perlinFreq*t + offset, 5000.0f) - 0.5f)*2.0f; // dist from river center to first river edge
            waterEdge = waterPos + riverFuntionPerp(edgeDist);

            // first river side
            spawnObj(water, waterEdge.x, waterEdge.y);
        }
    }

    // Spawn an instance of the game object
    GameObject spawnObj(GameObject obj, float width, float height){
        GameObject objInst = Instantiate(obj, new Vector2(width, 0.1f*height), Quaternion.identity);
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
