using UnityEngine;
using System.Collections;
using System;

public class Spaceship : MonoBehaviour {

    public float hp = 10;

    public float speed = 10;
    public float weaponCooldown = 0.5f;

    public Transform[] guns;
    public Transform boundsLU, boundsRD;

    public ShootingMachine gunnery;
    public SpriteRenderer sprite;


    Vector3 movement;

    /// <summary>
    /// can't lose hp, for things like invincibility
    /// </summary>
    internal bool lockHp = false;

    public bool Dead { get { return hp < 1; } }

    internal void Move(Vector3 input)
    {
        Up(input.z);
        Right(input.x);
    }

    void Start()
    {
        gunnery.Init(weaponCooldown, guns);
    }

    internal void Explode()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        float animationTime = 0.1f;
        Destroy(gameObject, animationTime);

        ScoreWorth sw =  GetComponent<ScoreWorth>();
        if (sw)
        {
            Game.IncreaseScore(sw);
        }
    }

    void Update()
    {
        UpdateMovement();
    }

    internal void Up(float force)
    {
        movement.z = FormulateMovement(force, movement.y);
    }

    internal IEnumerator Invincibility(float length)
    {
        if (!Dead)
        {
            //make object blink while invincible
            lockHp = true;
            length += Time.time;
            while (Time.time < length)
            {
                sprite.enabled = !sprite.enabled;
                yield return 0;
            }
            sprite.enabled = true;
            lockHp = false;
        }
    }

    internal void Down(float force)
    {
        movement.z = FormulateMovement(-force, movement.y);
    }

    internal void Left(float force)
    {
        movement.x = FormulateMovement(-force, movement.x);
    }

    internal void Right(float force)
    {
        movement.x = FormulateMovement(force, movement.x);
    }

    /// <summary>
    /// How input's force gets applied.
    /// it increses by speed when there is some input
    /// </summary>
    /// <param name="force"></param>
    /// <param name="currentValue"></param>
    /// <returns></returns>
    private float FormulateMovement(float force, float currentValue)
    {
        return speed * force;
    }

    private void UpdateMovement()
    {
        Vector3 step = transform.position+ movement * Time.deltaTime;
        Vector3 move = movement * Time.deltaTime;

        if (boundsLU != null && boundsRD != null)
        {
            if (!(step.x < boundsRD.position.x
                && step.x > boundsLU.position.x)
                )
            {
                move.x = 0;
            }

            if (!(step.z < boundsLU.position.z
                && step.z > boundsRD.position.z))
            {
                move.z = 0;
            }
        }

        transform.Translate(move);
    }

    [System.Obsolete("DON'T USE CONSTANT MOTION ON PLAYER")]
    void ConstantMotion(Vector3 direction) { }
    
}
