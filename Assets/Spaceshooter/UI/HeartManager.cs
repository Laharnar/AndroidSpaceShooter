using UnityEngine;
using System.Collections;

public class HeartManager : MonoBehaviour {

    public System.Collections.Generic.List<Transform> queuedHearts;

    public void RemoveHeart()
    {
        if (queuedHearts.Count > 0)
        {
            Destroy(queuedHearts[queuedHearts.Count - 1].gameObject);
            queuedHearts.RemoveAt(queuedHearts.Count - 1);
        }
    }
}
