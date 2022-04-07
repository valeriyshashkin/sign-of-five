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
    public Vector2 startPosition;
    public TextMeshProUGUI coinCount;
    public TextMeshProUGUI timeCount;
    public float timeToFinish;
    float remainingTimeToFinish;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        remainingTimeForMove = timeToMove;
        remainingTimeToFinish = timeToFinish;
        thisRenderer = GetComponent<Renderer>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Coin":
                hiddenGameObjects.Add(collider.gameObject);
                collider.gameObject.SetActive(false);
                coinCount.text = (int.Parse(coinCount.text) + 1).ToString();
                break;
            case "Poison":
                hiddenGameObjects.Add(collider.gameObject);
                collider.gameObject.SetActive(false);
                break;
            case "Health":
                hiddenGameObjects.Add(collider.gameObject);
                collider.gameObject.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Return) && isGameOver)
        {
            isGameOver = false;
            menu.SetActive(false);
            hiddenGameObjects.ForEach(i => i.SetActive(true));
            transform.position = startPosition;
            direction = Vector2.right;
            thisRenderer.enabled = true;
            remainingTimeToFinish = timeToFinish;
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
        timeCount.text = Mathf.Ceil(remainingTimeToFinish).ToString();

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
            remainingTimeForMove = timeToMove;
        }
    }
}
