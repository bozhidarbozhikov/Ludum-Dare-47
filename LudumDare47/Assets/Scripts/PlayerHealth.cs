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
        float spriteValue = maxHealth / health;

        if (spriteValue <= 1.33f) return treasureStates[0];
        else if (spriteValue <= 1.66f) return treasureStates[1];
        else if (spriteValue <= 2) return treasureStates[2];
        else if (spriteValue <= 2.5f) return treasureStates[3];
        else return treasureStates[4];
    }
}
