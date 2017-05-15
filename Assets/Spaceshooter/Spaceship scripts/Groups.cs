using System.Collections;
using UnityEngine;

[System.Serializable]
public class Group
{
    public float cd;
    public Transform groupPref;

    internal float priority = 0;
    internal bool ready = true;

    internal IEnumerator OnCooldown()
    {
        if (!ready)
        {
            yield return null; // already on cooldown don't call it twice
        }

        ready = false;
        float next = Time.time + cd;
        while (Time.time < next)
            yield return 0;
        ready = true;
    }

    internal void IncreasePriority()
    {
        priority = priority + 1+groupPref.childCount;
    }
}