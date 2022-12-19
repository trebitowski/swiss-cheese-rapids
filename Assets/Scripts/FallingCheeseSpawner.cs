using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FallingCheeseSpawner : MonoBehaviour
{

    public Transform parent;
    public GameObject cheese;
    public Vector2 scale;
    public Vector2 speed;

    public Vector2 transparency;

    public Vector2 positon;
    public Vector2 angle;

    public float y;

    public float spawnChance;
    // Update is called once per frame
    void Update()
    {
        if (Random.value < spawnChance) {
            float posX = Random.Range(positon.x, positon.y);
            float angleValue = Mathf.Lerp(angle.x, angle.y, Random.value);
            GameObject objInst = Instantiate(cheese, Vector2.one, Quaternion.identity, parent);

            RectTransform rect = objInst.GetComponent<RectTransform>();
            rect.position = new Vector3(posX, y, 1);
            rect.localRotation = Quaternion.Euler(0f, 0f, angleValue);

            float flip = Random.value >= 0.5f ? -1f : 1f;
            float random = Random.value;
            float scaleValue = Mathf.Lerp(scale.x, scale.y, random);
            rect.localScale = new Vector3(flip * scaleValue, scaleValue, 1);

            Color color = Color.white;
            color.a = Mathf.Lerp(transparency.x, transparency.y, random);
            
            Image img = objInst.GetComponent<Image>();
            img.color = color;

            CheeseMenuFall script = objInst.GetComponent<CheeseMenuFall>();
            script.speed = Mathf.Lerp(speed.y, speed.x, random);
        }
    }
}
