using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGrid {

    /// <summary>
    /// / NOT USE
    /// </summary>


    private Vector2 mushroomGridPosition;

    private int height;
    private int width;

    public LevelGrid(int _height, int _width)
    {
        this.height = _height;
        this.width = _width;

        SpawnMushrooms();

    }

    private void SpawnMushrooms()
    {
        mushroomGridPosition = new Vector2(Random.Range(0, width), Random.Range(0, height));

        var mushroomGameObject = new GameObject("Mushroom", typeof(SpriteRenderer));
        mushroomGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.mushroomSprite;
        mushroomGameObject.transform.position = new Vector3(mushroomGridPosition.x, mushroomGridPosition.y);
    }
}
