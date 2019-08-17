using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Part {

    private Vector3 gridPosition;

    float maxSpeed = 3.5f;

    public static int lives = 10;

    private void Awake()
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        gridPosition = new Vector2(0, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance)).y + 0.25f);
    }

    private void Update()
    {
        wizardMove();
    }

    private void wizardMove()
    {
        var dist = (transform.position - Camera.main.transform.position).z;

        // BORDERS CAMERA

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        //

        //Move Wizard on the place
        gridPosition.x += Input.GetAxis("Horizontal") * maxSpeed * Time.deltaTime;
        gridPosition.y += Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime;

        transform.position = new Vector3(
          Mathf.Clamp(gridPosition.x, leftBorder, rightBorder),
          Mathf.Clamp(gridPosition.y, topBorder, bottomBorder)
        );
    }
}