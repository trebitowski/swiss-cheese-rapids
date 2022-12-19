using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatScript : MonoBehaviour
{
    // private float moveSpeed;
    public float riverSpeed = 2;
    public float rapidSpeed = 3;
    public Collider2D floatCollider;
    LayerMask riverMask;
    // Start is called before the first frame update
    void Start()
    {
        // moveSpeed = riverSpeed;
        riverMask = LayerMask.GetMask("River");
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = riverSpeed;
        if (floatCollider.IsTouchingLayers(riverMask)) {
            moveSpeed = rapidSpeed;
        }

        transform.position = transform.position + Vector3.up * (moveSpeed + Score.speedDifficulty) * Time.deltaTime;
    }
}