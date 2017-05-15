using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Handles firing and reloading.
/// 
/// Needs manual initialization by another script.
/// </summary>
public class ShootingMachine : MonoBehaviour {

    public Transform bullet;
    public float bulletLife = 1;
    public int bulletSpeed = 1000;

    bool ready = true;
    float cd = 0.5f;
    
    Transform[] guns;

    internal void Init(float cd, params Transform[] guns)
    {
        this.guns = guns;
        this.cd = cd;
    }

    internal void TryShoot()
    {
        if (guns == null)
        {
            Debug.Log("No guns.");
        }
        if (bullet == null)
        {
            Debug.Log("No bullet.");
        }

        if (ready)
        {
            Fire(0);
            ready = false;
            StartCoroutine(Reload(cd));
        }
    }

    private IEnumerator Reload(float cd)
    {
        cd += Time.time;
        while (Time.time < cd)
        {
            yield return 0;
        }
        ready = true;
    }


    private void Fire(int gun)
    {
        Transform bulletInstance = Instantiate(bullet, guns[gun].position, guns[gun].rotation) as Transform;
        Bullet[] bss = bulletInstance.GetComponentsInChildren<Bullet>();

        if(bss != null)
        foreach (var b in bss)
        {
            b.Init(bulletLife, bulletSpeed, false);
        }
    }
}
