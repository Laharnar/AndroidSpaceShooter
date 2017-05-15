using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// spawns groups of enemies, each with their own calculated colldown.
/// </summary>
public class Spawning : MonoBehaviour {

    internal static Spawning m;//this can be modified to allow multiple spawners.

    // where and how fast can ships be spawned (square)
    public Transform rangeL;
    public Transform rangeR;

    [SerializeField]
    float cd = 2;

    public Group[] groups;//groups that can be spawned

    bool ready = true;

    float debugtimesinceSpawn = 0;

    // Use this for initialization
    void Start () {
        
        if (m && m!= this)
        {
            Destroy(m.gameObject);
        }
        m = this;

        if (rangeL == null ||rangeR == null)
        {
            Debug.Log("Range is not defined.", this);
        }
        if (groups == null)
        {
            Debug.Log("No groups", this);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (ready) {
            //find group that is ready, spawn it, and increase spawn priority for all other groups.
            int groupindex = IndexOfMaxPriority(groups);
            if (groupindex > -1)
            {
                //take vector between 2 points, cut it in 10 pieces and take few of those pieces.
                Vector3 poz = rangeL.position + ((rangeR.position - rangeL.position) * 0.1f * (float)UnityEngine.Random.Range(0, 10));
                poz.y = 0;

                Group group = groups[groupindex];

                Debug.Log(Time.time- debugtimesinceSpawn + " "+cd + " "+ group.cd);
                Spawn(poz, groups[groupindex]);

                //put group and spawner on cooldown
                StartCoroutine(Cooldown(cd));
                StartCoroutine(group.OnCooldown());

                debugtimesinceSpawn = Time.time;
                //increse priority on all groups, accept spawned, to make sure they all come on their turn.
                foreach (var g in groups)
                {
                    g.IncreasePriority();
                }
                groups[groupindex].priority = 0;
            }
        }
	}

    int IndexOfMaxPriority(Group[] groups)
    {
        float max = -1;
        int maxi = -1;

        for (int i = 0; i < groups.Length; i++)
        {
            if (groups[i].ready && groups[i].priority > max)
            {
                max = groups[i].priority;
                maxi = i;
            }
        }
        return maxi;
    }

    void Spawn(Vector3 spawnPosition, Group group)
    {
        Debug.Log("spawning");
        spawnPosition.y = 0;
        Instantiate(group.groupPref, spawnPosition, transform.rotation);

    }

    internal IEnumerator Cooldown(float cd)
    {
        if (!ready)
        {
            yield return null;//already on cooldown, don't call it twice
        }

        Debug.Log("waiting");
        cd += Time.time;
        while (Time.time < cd)
        {
            ready = false;
            yield return 0;
        }
        ready = true;
        Debug.Log("ready");
    }

    public static void Disable(float time)
    {
        m.StartCoroutine(m.Cooldown(time));
    }
}
