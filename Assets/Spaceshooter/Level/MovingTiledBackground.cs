using UnityEngine;
using System.Collections;

public class MovingTiledBackground : MonoBehaviour {

    [SerializeField]
    MeshRenderer background;

    [SerializeField]
    float speed = 1;

    Vector3 direction = new Vector3(0, 0, -1);

    [SerializeField]
    Transform starting;

    [SerializeField]
    float restart = -10;


    // Update is called once per frame
    void Update () {
        MoveTiled();
	}

    void MoveTiled()
    {
        if (transform.position.z <= restart)
        {
            transform.Translate(0, 0, starting.position.z-transform.position.z);
        }


        transform.Translate((direction*Time.deltaTime *speed));
    }
}
