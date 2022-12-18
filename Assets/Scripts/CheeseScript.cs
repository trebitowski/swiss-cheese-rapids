using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseScript : MonoBehaviour
{
    public float moveSpeed;
    public float destroyZone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
    //     transform.position = transform.position + Vector3.down * moveSpeed * Time.deltaTime;

    //     if (transform.position.y < destroyZone) {
    //         Destroy(gameObject);
    //     }
    // }

        // Update is called once per frame
    void Update()
    {
        if (transform.position.y < destroyZone + Camera.main.transform.position.y) {
            Destroy(gameObject);
        }
    }
}