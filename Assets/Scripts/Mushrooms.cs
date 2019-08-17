using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushrooms : Part {

    private int lives = 3;

    Bonus AdditionalLives;

    public SpriteRenderer spriteRenderer;
    private Sprite spriteMushroom;
    
    
    // Use this for initialization
    void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = Resources.Load<Sprite>("mushroom04a");
    }
	
	// Update is called once per frame
	void Update () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Part part = collider.GetComponent<Part>();
        if (part is Wizard)
        {
            part.ReceiveDamage();
        }
        if (part is Mushrooms)
        {
            Mushrooms EnterMush = new Mushrooms();
            Vector3 pos = transform.position;
            pos.x += 0.9f;
            pos.y += 0.9f;
            transform.position = pos;
        }
    }

    public override void ReceiveDamage()
    {        
        lives--;

        if (lives <= 0)
        {
            AdditionalLives = Resources.Load<Bonus>("Bonus");

            //random bonus generator with chance
            int chance = Random.Range(0, 100);
            
            if (chance >= 85 && chance <= 94)
            {
                // Spawn life bonus
                // Implement bonus appearance with chance
                Bonus newBonus = Instantiate(AdditionalLives, transform.position, AdditionalLives.transform.rotation);
                Bonus.bonusNumber = 0;
            }
            
            GameHandler.score += (GameHandler.wave * 25);
            base.ReceiveDamage();
        }
    }
}
