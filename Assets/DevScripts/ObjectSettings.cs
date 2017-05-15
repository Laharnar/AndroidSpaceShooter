using UnityEngine;
using System.Collections;


/// <summary>
/// settings like layer, etc, that has to be always correct.
/// used to prevent data loss between scenes
/// </summary>
public class ObjectSettings : MonoBehaviour {

    // layering
    public bool layerWorking = false;
    public string layer = "";
    
    // Use this for initialization
    void Start () {

        if (layer == "" || layerWorking == false)
        {
            Debug.Log("Layer settings not applied", this);
        }
        try
        {
            gameObject.layer = LayerMask.NameToLayer(layer);
        }
        catch (System.Exception)
        {
            Debug.Log("Layer "+layer+" does not exist.");
        }
        
	}
}
