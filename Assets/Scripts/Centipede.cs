using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Centipede : Part {

    private Vector3 gridPosition;

    public float speed = 10.0f;

    private ArrayList centipedeSegment;

    private int segmentCount = 0;
    private int segmentNumber = 0;

    private BoxCollider2D boxCollider2D;
    private float leftradius;
    private float upperradius;

    private int wave = 5;

    Mushrooms mushrooms;

    public SpriteRenderer spriteRenderer;
    private Sprite spriteSegmentBody;

    private Vector3 moveDirection = new Vector3(0.1f, 0);

    public static bool isHead = true;

    private float minX, maxX, minY, maxY;

    // Use this for initialization
    private void Awake()
    {
        mushrooms = Resources.Load<Mushrooms>("Mushroom");
        // Simulating segment ID
        segmentNumber = SegmentBody.AddSegment();

        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = Resources.Load<Sprite>("PartBody");

        GetCameraSizes();

        gridPosition = new Vector2(0, maxY - 1);
    }

    // Update is called once per frame
    void Update ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        MoveCentipede();
	}

    // Get segments collider size
    private void ColliderSize()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        Vector3 pos = transform.position;
        leftradius = pos.x - boxCollider2D.bounds.min.x;
        upperradius = pos.y - boxCollider2D.bounds.min.y;
    }

    private void MoveCentipede()
    {
        if (gridPosition.x >= maxX)
        {
            gridPosition.y -= 0.4f;
            gridPosition.x -= .2f;
            moveDirection = new Vector3(-0.1f, 0);
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }
        else if (gridPosition.x <= minX)
        {
            gridPosition.y -= 0.4f;
            gridPosition.x += .2f;
            moveDirection = new Vector3(0.1f, 0);
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }
        else if (gridPosition.y <= minY)
        {
            Destroy(gameObject);
        }
        else
        {
            gridPosition += moveDirection * speed * 0.02f;
        }

        transform.position = new Vector3(gridPosition.x, gridPosition.y);
    }

    private void GetCameraSizes()
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Damage player when it bumps into it
        Part part = collider.GetComponent<Part>();
        if (part is Wizard)
        {
            part.ReceiveDamage();
        }
        // If it bumps into mushroom - reverse direction
        if (part is Mushrooms)
        {
            moveDirection = new Vector3(moveDirection.x * -1, moveDirection.y);

            gridPosition.y -= 0.4f;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }

    }

    public override void ReceiveDamage()
    {
        try
        {
            if (SegmentBody.centipede[segmentNumber + 1] != null)
            {
                Centipede CurrentSegment = (Centipede)SegmentBody.centipede[segmentNumber + 1];
                CurrentSegment.spriteRenderer.sprite = Resources.Load<Sprite>("Centipede");
            }

        }
        catch (Exception exception)
        {
            Debug.Log(exception.ToString());
        }

        Vector3 position = transform.position;
        Mushrooms newMushrooms = Instantiate(mushrooms, position, mushrooms.transform.rotation);
        GameHandler.score += (GameHandler.wave * 2);
        GameHandler.livingSegments--;
        base.ReceiveDamage();
    }
}
