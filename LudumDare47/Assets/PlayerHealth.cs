using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public float maxHealth;
    [SerializeField]
    float health;

    public Sprite[] treasureStates;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        spriteRenderer.sprite = UpdatedSprite();

        if (health <= 0)
        {
            //end game
        }
    }

    Sprite UpdatedSprite()
    {
        int spriteValue = (int)Mathf.Round(maxHealth / health);

        if (spriteValue >= treasureStates.Length) spriteValue = 1;

        return treasureStates[spriteValue - 1];
    }
}
