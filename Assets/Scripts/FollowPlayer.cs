using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
    private float camera_offset;
    private float camera_z;
    void Start () {
        camera_offset = transform.position.y - player.transform.position.y;
        camera_z = transform.position.z;
    }

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(0, player.transform.position.y + camera_offset, camera_z);
    }
}