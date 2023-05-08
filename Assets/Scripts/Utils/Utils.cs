using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-3,-9), 5, Random.Range(-8, -5));

    }

    public static void SetRenderLayerInChildren(Transform transform, int layerNumber)
    {
        foreach (Transform trans in transform.GetComponentsInChildren<Transform>(true))
            trans.gameObject.layer = layerNumber;
    }
}
