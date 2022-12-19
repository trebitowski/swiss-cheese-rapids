using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseScript : MonoBehaviour
{
    // private float moveSpeed;
    public float riverSpeed = 2;
    public float rapidSpeed = 3;
    public float destroyZone;
    public Collider2D cheeseCollider;
    LayerMask riverMask;
    // Start is called before the first frame update
    void Start()
    {
        // moveSpeed = riverSpeed;
        riverMask = LayerMask.GetMask("River");
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
        float moveSpeed = riverSpeed;
        if (cheeseCollider.IsTouchingLayers(riverMask)) {
            moveSpeed = rapidSpeed;
        }

        transform.position = transform.position + Vector3.up * (moveSpeed + Score.speedDifficulty) * Time.deltaTime;
        if (transform.position.y < destroyZone + Camera.main.transform.position.y) {
            Destroy(gameObject);
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {

    //     if (other.gameObject.tag == "Riverbank") {
    //         moveSpeed = rapidSpeed;
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D other)
    // {

    //     if (other.gameObject.tag == "Riverbank") {
    //         moveSpeed = riverSpeed;
    //     }
    // }
}