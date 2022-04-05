using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 direction = Vector2.right;
    SpriteRenderer sprite;
    public float timeStepForMove = 0.5f;
    float remainingTimeForMove;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        remainingTimeForMove = timeStepForMove;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Coin":
                print("Add coin");
                break;
            case "Poison":
                print("Restart level");
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }

        if (remainingTimeForMove > 0)
        {
            remainingTimeForMove -= Time.deltaTime;
        }
        else
        {
            transform.Translate(direction * sprite.bounds.size.x);
            remainingTimeForMove = timeStepForMove;
        }
    }
}
