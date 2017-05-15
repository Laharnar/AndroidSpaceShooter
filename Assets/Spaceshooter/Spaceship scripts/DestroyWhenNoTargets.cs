using UnityEngine;
using System.Collections;

public class DestroyWhenNoTargets : MonoBehaviour {

    public Transform[] targets;

	// Use this for initialization
	void Start () {
        if (targets == null || targets.Length == 0)
        {
            Destroy(gameObject, 5);
        }
	}
}
