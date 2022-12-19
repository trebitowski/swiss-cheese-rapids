using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverObjectDelete : MonoBehaviour
{
    private float destroyZone = -25;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < destroyZone + Camera.main.transform.position.y) {
            Destroy(gameObject);
        }
    }
}
