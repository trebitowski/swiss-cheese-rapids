using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseMenuDestroy : MonoBehaviour
{
    private float destroyZone = -900;

    private RectTransform rect;

    void Start() {
        rect = gameObject.GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (rect.position.y < destroyZone ) {
            Destroy(gameObject);
        }
    }
}
