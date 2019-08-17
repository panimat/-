using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : Part {

    public static int wave = 3;
    private float cooldown = 0;
    private float invulnerable = 0;

    public static float shotCooldown = 0.25f;

    public bool isAlive = true;
    public static int score = 0;


    private BoxCollider2D boxCollider2D;
    new private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRender;
    private Vector2 screenPosition;

    private float timer;
    private float timerMax;
    private int segments = 0;
    public static int livingSegments = 0;

    private float leftradius;
    private float upperradius;

    private float minX, maxX, minY, maxY;

    // Variables for loading prefabs
    private Centipede centipede;
    private Mushrooms mushrooms;
    private Bullet bullet;
    private Wizard wizard;
    private Wizard newWizard;


    Camera cam;
    float cameraHeigth;
    float cameraWidth;

    private LevelGrid levelGrid;


	// Use this for initialization
	private void Awake ()
    {
        SegmentBody segInfo = new SegmentBody();

        // Timer for generating segments
        timerMax = 0.8f;
        timer = timerMax;
        spriteRender = GetComponent<SpriteRenderer>();

        // Find length from center of player's ship to border
        boxCollider2D = GetComponent<BoxCollider2D>();
        Vector3 pos = transform.position;
        leftradius = pos.x - boxCollider2D.bounds.min.x;
        upperradius = pos.y - boxCollider2D.bounds.min.y;

        spriteRender = GetComponent<SpriteRenderer>();

        centipede = Resources.Load<Centipede>("Centipede");
        mushrooms = Resources.Load<Mushrooms>("Mushroom");
        bullet = Resources.Load<Bullet>("Bullet");
        wizard = Resources.Load<Wizard>("Wizard");
        Debug.Log("Centipede_Panimatstarted");

        //levelGrid = new LevelGrid(10, 10);

        GetCameraSizes();

        newWizard = Instantiate(wizard);
    }

    private void Start()
    {
        // Preparing game rules
        score = 0;
        Wizard.lives = 10;
        segments = wave * 2 + 2;
        wave = 0;
        livingSegments = 0;

        isAlive = true;

        GetCameraSizes();
        
        GenerateMushrooms();
    }

    // Update is called once per frame
    void Update () {

        // Update cooldowns
        if (timer != 0) timer -= Time.deltaTime; // 0.018f;
        cooldown -= Time.deltaTime;

        // Check if centipede is alive. If alive and has no segments - create again but with more segments
        if (isAlive && livingSegments <= 0)
        {
            isAlive = false;
            segments = wave * 2 + 2;
            livingSegments = segments;
            centipede.speed += 1.0f;
            wave++;
        }
        else if (timer <= 0 && segments > 0)
        {
            CreateCentipede();
        }

        if (Input.GetButton("Fire1") && cooldown <= 0)
            Shoot();

        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
	}

    // Creates segment of centipede
    private void CreateCentipede()
    {
        Vector3 position = transform.position;
        position.x = 0;
        position.y = maxY - 0.2f;
        Centipede newCentipede = Instantiate(centipede, position, centipede.transform.rotation);
        SegmentBody.centipede.Add(newCentipede);
        segments--;
        timer = 0.2f;
        if (segments == 0)
            isAlive = true;
    }

    // Get Maximum and minimum points of screen
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

    private void GenerateMushrooms()
    {
        for (int i = (int)minX + 2; i < (int)maxX - 2; i++)        
            for (int j = -3; j < 3; j++)
            {
                int chance = Random.Range(0, 100);
                Debug.Log(chance);
                if (chance >= 90)
                {
                    Mushrooms newMushrooms = Instantiate(mushrooms, new Vector3(i, j), mushrooms.transform.rotation);
                }
            }        
    }

    // Perfoms shooting
    private void Shoot()
    {
        Vector3 position = newWizard.transform.position;
        position.y += 0.2f;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation);

        newBullet.Direction = newBullet.transform.up;

        // Set cooldown
        cooldown = shotCooldown;
    }

    public override void ReceiveDamage()
    {
        Wizard.lives--;
    }
}