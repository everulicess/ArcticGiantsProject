using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{ 
    
    public static Vector3 GetFirstSpawnPoint(string position)
    {
        switch (position)
        {
            case "First":
                return new Vector3(-4, 5, -6);
            case "Second":
                return  new Vector3(18,5,-26); 
            default:
                return new Vector3(-4, 5, -6);
        }
        

    }
    public static Vector3 GetSecondtSpawnPoint()
    {
        //return new Vector3(Random.Range(-3,-9), 5, Random.Range(-8, -5));
        return new Vector3(Random.Range(-3,-9), 5, Random.Range(-8, -5));

    }

    public static void SetRenderLayerInChildren(Transform transform, int layerNumber)
    {
        foreach (Transform trans in transform.GetComponentsInChildren<Transform>(true))
            trans.gameObject.layer = layerNumber;
    }
}
