using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public SpriteRenderer mouseRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        bool flip = input < 0;

        if (input != 0) {
            mouseRenderer.flipX = flip;
        }
    }
}
