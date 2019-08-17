using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour {

    // NOT USE THIS CLASSS

    public static GameAssets i;

    public void Awake()
    {
        i = this;
    }

    public Sprite wizardSprite;
    public Sprite mushroomSprite;
}
