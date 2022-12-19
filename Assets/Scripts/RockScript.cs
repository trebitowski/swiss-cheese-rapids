using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float destroyZone = -35;
    // Start is called before the first frame update
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
