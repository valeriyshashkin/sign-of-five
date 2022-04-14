using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    Vector2 direction = Vector2.right;
    SpriteRenderer sprite;
    public float timeToMove;
    float remainingTimeForMove;
    public GameObject menu;
    bool isGameOver = true;
    List<GameObject> hiddenGameObjects = new List<GameObject>();
    Renderer thisRenderer;
    Vector2 startPosition;
    public float timeToFinish;
    float remainingTimeToFinish;
    Menu menuScript;
    List<GameObject> parts = new List<GameObject>();
    public GameObject part;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        remainingTimeForMove = timeToMove;
        remainingTimeToFinish = timeToFinish;
        thisRenderer = GetComponent<Renderer>();
        menuScript = menu.GetComponent<Menu>();
        parts.Add(gameObject);
        startPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "File":
                hiddenGameObjects.Add(collider.gameObject);
                collider.gameObject.SetActive(false);
                var newPartPosition = new Vector3(
                    parts[parts.Count - 1].transform.position.x + -direction.x * sprite.bounds.size.x,
                    parts[parts.Count - 1].transform.position.y + -direction.y * sprite.bounds.size.y,
                    0
                );
                var newPart = Instantiate(part, newPartPosition, Quaternion.identity);
                parts.Add(newPart);
                break;
            case "Wall":
                thisRenderer.enabled = false;
                menu.SetActive(true);
                isGameOver = true;
                break;
        }
    }

    void Update()
    {
        if (menuScript.isReturnPressed && menuScript.GetSelectedItem() == "Continue")
        {
            isGameOver = false;
            menuScript.isReturnPressed = false;
            menu.SetActive(false);
            hiddenGameObjects.ForEach(i => i.SetActive(true));
            transform.position = startPosition;
            direction = Vector2.right;
            thisRenderer.enabled = true;
            remainingTimeToFinish = timeToFinish;
            for (int i = 1; i < parts.Count; i++)
            {
                Destroy(parts[i]);
            }
            parts.Clear();
            parts.Add(gameObject);
        }

        if (isGameOver)
        {
            return;
        }

        if (remainingTimeToFinish < 0)
        {
            isGameOver = true;
            menu.SetActive(true);
        }

        remainingTimeToFinish -= Time.deltaTime;

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
            for (int i = parts.Count - 1; i > 0; i--)
            {
                parts[i].transform.position = parts[i - 1].transform.position;
            }
            transform.Translate(direction * sprite.bounds.size.x);
            remainingTimeForMove = timeToMove;
        }
    }
}
