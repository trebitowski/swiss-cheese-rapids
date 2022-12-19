using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseMenuFall : MonoBehaviour
{
    public float speed;

    private RectTransform rect;

    void Start() {
        rect = gameObject.GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        rect.position = rect.position + Vector3.down * speed * Time.deltaTime;
    }
}
