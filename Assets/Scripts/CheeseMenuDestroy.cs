using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseMenuDestroy : MonoBehaviour
{
    public float destroyZone = -750;

    private RectTransform rect;

    void Start() {
        rect = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (rect.position.y < destroyZone ) {
            Destroy(gameObject);
        }
    }
}
