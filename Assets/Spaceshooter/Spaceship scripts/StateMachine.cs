using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Game state machine for a spaceships.
/// Handles damage
/// </summary>
public class StateMachine : MonoBehaviour {

    public Spaceship sship;

    public HeartManager hearts;

    // Use this for initialization
    void Start () {
        if (sship == null)
        {
            Debug.Log("Spaceship not assigned.", this);
        }

        if (hearts == null && tag == "Player")
        {
            Debug.Log("Hearts group is not assigned.");
        }
    }

    /// <summary>
    /// handles spaceship's recieveing of damage
    /// </summary>
    /// <param name="damage"></param>
    internal bool Hit(float damage)
    {
        if (sship.lockHp)
            return false;

        sship.hp -= damage;
        if(hearts)
            hearts.RemoveHeart();

        //game over
        if (sship.Dead)
        {
            sship.hp = 0;
            sship.Explode();
        }
        return sship.Dead;
    }
}
