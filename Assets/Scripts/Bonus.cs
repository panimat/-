﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : Part {


    //Bonus Script for mushrooms


    private float minX, maxX, minY, maxY;
    public static int bonusNumber = 0;

    private void Start()
    {
        GetCameraSizes();
    }

    private void Update()
    {
        Vector3 position = transform.position;
        if (position.y <= minY)
        {
            Destroy(gameObject);
        }
    }

    // If it reaches player - gives bonus
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Part part = collider.GetComponent<Part>();
        if (part is Wizard)
        {
            if (bonusNumber == 0)
            {
                Wizard.lives += 5;
                base.ReceiveDamage();
            }
            else if (bonusNumber == 1)
            {
                if (GameHandler.shotCooldown >= 0.1f) GameHandler.shotCooldown -= 0.01f;
                base.ReceiveDamage();
            }
        }
        else if (part)
        {

        }
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
}
