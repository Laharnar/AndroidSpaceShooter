using UnityEngine;
using System.Collections;
using System;

public class PlayerPilot : MonoBehaviour {

    public static PlayerPilot player;

    public Spaceship sship;

    Vector3 input;

    // Use this for initialization
    void Start () {
        LayerMasking.DoesLayerExist("Spaceships");

        player = this;
        if (sship == null)
        {
            Debug.Log("No spaceship found.", this);
        }
	}
	
	// Update is called once per frame
	void Update () {

        // Recieve movement input
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");


        // move the ship based on input
        sship.Up(input.y);
        sship.Right(input.x);

        // Recieve shooting input
        if (Input.GetKey(KeyCode.Space))
        {
            sship.gunnery.TryShoot();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Spaceships") && !sship.lockHp)
        {
            other.GetComponent<StateMachine>().Hit(1);
            if (GetComponent<StateMachine>().Hit(1))
            {
                Game.m.state = GameState.Over;
            }

            //make object go on-off
            StartCoroutine(sship.Invincibility(1));
        }
    }
}
