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
        angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

        
        if (input == 0 && angle != 0f){
            angle += rotateBack * Time.deltaTime;
            if (Mathf.Abs(angle) < 1) {
                angle = 0;
            }
            if (angle > 0) {
                rotateBack = -returnAngleStrength;
            } else {
                rotateBack = returnAngleStrength;
            }
        }
        transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        
        float normalizedAngle = Mathf.InverseLerp(-maxAngle, maxAngle, angle);
        float horizontalSpeed = Mathf.Lerp(-moveStrength, moveStrength, normalizedAngle);

        transform.position = transform.position + Vector3.right * horizontalSpeed * Time.deltaTime;
    }
}
