using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
    }
}
