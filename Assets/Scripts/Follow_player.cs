using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_player : MonoBehaviour {

    public Transform player;

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(0, player.transform.position.y + 7, -10);
    }
}