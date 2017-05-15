using UnityEngine;

class LayerMasking
{
    public static void DoesLayerExist(string layer)
    {
        if (LayerMask.GetMask(layer) < -5)
        {
            Debug.Log("Layer mask "+layer + "doesn't exist.");
        }
    }
}
