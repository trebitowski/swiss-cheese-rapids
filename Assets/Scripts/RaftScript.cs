using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftScript : MonoBehaviour
{
    public Rigidbody2D raftRigidBody;
    public GameObject scoreManager;
    public GameObject gameManager;
    public Collider2D raftCollider;
    public Sprite normal;
    public Sprite left;
    public Sprite right;
    public Sprite crash;
    public SpriteRenderer spriteRenderer;
    bool movementEnabled = true;
    public float moveStrength;
    public float maxAngle = 60;
    public float angleStrength;
    public float returnAngleStrength;
    public float targetAngle = 0;
    public float riverSpeed = 8f;
    public static float rapidSpeed = 12.0f;
    // public float currentSpeed;
    float rotateBack = 0f;
    LayerMask riverMask;
    float angle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        // currentSpeed = rapidSpeed;
        riverMask = LayerMask.GetMask("River");
    }

    // Update is called once per frame
    void Update()
    {   

        float currentSpeed = riverSpeed;
        if (raftCollider.IsTouchingLayers(riverMask)) {
            currentSpeed = rapidSpeed;
        }
        if (movementEnabled == true) {
        bool occupied = false;
        GameObject player;
         foreach (Transform child in transform)
         {
             if (child.tag == "Player")
             {
                 player = child.gameObject;
                 occupied = true;
             }
         }

        float input = occupied == true ? Input.GetAxis("Horizontal") : 0;
        if (input == 0) {
            spriteRenderer.sprite = normal;
        } else if (input > 0) {
            spriteRenderer.sprite = right;
        } else {
            spriteRenderer.sprite = left;
        }
        angle += input * Time.deltaTime * angleStrength;
        angle = Mathf.Clamp(angle, -maxAngle + targetAngle, maxAngle + targetAngle);

        
        if (input == 0 && angle != targetAngle){
            angle += rotateBack * Time.deltaTime;
            if (Mathf.Abs(targetAngle - angle) < 1) {
                angle = targetAngle;
            }
            if (angle > targetAngle) {
                rotateBack = -returnAngleStrength;
            } else {
                rotateBack = returnAngleStrength;
            }
        }
        transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        
        float normalizedAngle = Mathf.InverseLerp(-maxAngle + targetAngle, maxAngle + targetAngle, angle);
        float horizontalSpeed = Mathf.Lerp(-moveStrength, moveStrength, normalizedAngle);

        float riverHorizontalComponent = (currentSpeed + Score.speedDifficulty) * Mathf.Sin(Mathf.Deg2Rad * targetAngle) ;

        // transform.position = transform.position + Time.deltaTime * Vector3.right *  // original
            // (riverHorizontalComponent + horizontalSpeed);

        transform.position = transform.position + Time.deltaTime * Vector3.right * 
            (riverHorizontalComponent + horizontalSpeed) + Time.deltaTime * Vector3.up * (currentSpeed + Score.speedDifficulty);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Cheese") {
            scoreManager.GetComponent<Score>().addCheese();
            AudioManager.Play("Cheese");
            Destroy(other.gameObject);
        } else if (other.gameObject.tag == "Obstacle") {
            movementEnabled = false;
            spriteRenderer.sprite = crash;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            AudioManager.Play("Splash");
            // AudioManager.Play("Death", 1f);
            gameManager.GetComponent<GameManagerScript>().EndGame();
        } else if (other.gameObject.tag == "Riverbank") {
            //Debug.Log("Riverbank");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Log0(Clone)") {
            AudioManager.Play("Log bump");
        }
    }
}
