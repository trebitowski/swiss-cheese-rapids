using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCapSpeed : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float scale = 0.1f;
        transform.position = transform.position + Vector3.up * scale * (RaftScript.rapidSpeed + Score.speedDifficulty) * Time.deltaTime;
    }
}
