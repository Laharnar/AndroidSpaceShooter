using System.Collections;
using UnityEngine;

/// <summary>
/// Ally or enemy bullet
/// Must be initialized manualy in instantiation if you want it destroyed.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {

    bool hostileToPlayer;

    float lifetime = 1;
    float damage = 1;

    public Vector3 localDirection = new Vector3(0, 0, 1);

	// Use this for initialization
	internal void Init (float lifetime, int forceForward, bool hostileToPlayer) {
        this.lifetime = lifetime;
        this.hostileToPlayer = hostileToPlayer;

        GetComponent<Rigidbody>().AddRelativeForce(localDirection * forceForward);

        Destroy(gameObject, lifetime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != gameObject.layer)
        {
            //don't hit you own with your bullets
            if (!hostileToPlayer && other.tag == "Player"//player-sided protection
                || hostileToPlayer && other.tag != "Player")//enemy-sided protection
            {
                return;
            }

            //if other object can be damaged, hit it, else pass through. 
            StateMachine hassm = other.GetComponent<StateMachine>();
            if (hassm)
            {
                //apply damage to the hit object
                other.GetComponent<StateMachine>().Hit(damage);

                //get rid of bullet
                float animationTime = 0;
                Destroy(gameObject, animationTime);
            }
        }
    }
}
