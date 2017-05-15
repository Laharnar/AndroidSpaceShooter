using UnityEngine;
using System.Collections;

public class AIPilot : MonoBehaviour {


    public Spaceship sship;

    Vector3 input;//ai always moves down, for now

    // Use this for initialization
    void Start () {
        if (sship == null)
        {
            sship = GetComponent<Spaceship>();
            if (sship == null)
            {
                //Debug.Log("Ship not assigned.", this);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        AiBehaviour();

        sship.Move(input);
    }

    void AiBehaviour()
    {
        input = new Vector3(0, 0, -1);
    }
}
