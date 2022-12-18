using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftScript : MonoBehaviour
{
    public Rigidbody2D raftRigidBody;
    public float moveStrength;
    public float maxAngle = 60;
    public float angleStrength;
    public float returnAngleStrength;
    public float targetAngle = 0;
    public float riverSpeed = 8;
    float rotateBack = 0f;
    float angle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        float riverHorizontalComponent = riverSpeed * Mathf.Sin(Mathf.Deg2Rad * targetAngle);

        // transform.position = transform.position + Time.deltaTime * Vector3.right *  // original
            // (riverHorizontalComponent + horizontalSpeed);

        transform.position = transform.position + Time.deltaTime * Vector3.right * 
            (riverHorizontalComponent + horizontalSpeed) + Time.deltaTime * Vector3.up * riverSpeed;
    }
}
