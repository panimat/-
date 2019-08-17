using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour {

    // Base class for player, bonuses, centipede and mushrooms
    public virtual void ReceiveDamage()
    { 
        Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {

    }
}
