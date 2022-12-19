using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeoutDelete : MonoBehaviour
{
    private float initTime;
    // Start is called before the first frame update
    void Start()
    {
        initTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float delayBuffer = 1.3f; // time in seconds to wait before deleting
        if (Time.time > initTime + delayBuffer){
            Destroy(gameObject);
        }
    }
}
