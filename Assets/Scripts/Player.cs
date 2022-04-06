using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 direction = Vector2.right;
    SpriteRenderer sprite;
    public float timeStepForMove = 0.5f;
    float remainingTimeForMove;
    public GameObject gameOverText;
    bool isGameOver = true;
    List<Renderer> hiddenGameObjects = new List<Renderer>();
    Renderer thisRenderer;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        remainingTimeForMove = timeStepForMove;
        thisRenderer = GetComponent<Renderer>();
        thisRenderer.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Coin":
                var coinRenderer = collider.gameObject.GetComponent<Renderer>();
                hiddenGameObjects.Add(coinRenderer);
                coinRenderer.enabled = false;
                break;
            case "Poison":
                var poisonRenderer = collider.gameObject.GetComponent<Renderer>();
                hiddenGameObjects.Add(poisonRenderer);
                poisonRenderer.enabled = false;
                break;
            case "Wall":
                thisRenderer.enabled = false;
                gameOverText.SetActive(true);
                isGameOver = true;
                break;
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) && isGameOver)
        {
            isGameOver = false;
            gameOverText.SetActive(false);
            hiddenGameObjects.ForEach(i => i.enabled = true);
            transform.position = new Vector3(-2.5f, 2.5f, 0f);
            direction = Vector2.right;
            thisRenderer.enabled = true;
        }

        if (isGameOver)
        {
            return;
        }

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
