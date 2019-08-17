using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Part {

    private float speed = 50.0f;

    private SpriteRenderer spriteRender;
    private Vector3 direction;
    private Vector2 screenPosition;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = Resources.Load<Sprite>("Bull");
    }

    public Vector3 Direction
    {
        set
        {
            direction = value;
        }
    }

    void Update()
    {
        spriteRender = GetComponent<SpriteRenderer>();

        // Move towards the bullet after it creates
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        // Destroy bullet if it goes out of the screen
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Damage unit which it enters but not if it is player
        Part part = collider.GetComponent<Part>();
        if (part && !(part is Wizard) && !(part is Bonus))
        {
            part.ReceiveDamage();
            Destroy(this.gameObject);
        }
    }
}
