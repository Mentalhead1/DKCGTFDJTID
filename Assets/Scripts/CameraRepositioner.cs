using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRepositioner : MonoBehaviour
{
    public Camera thisCamera;

    public void Reposition(GameObject CardsHolder)
    {
        thisCamera.orthographicSize = 0;
        GetBound(CardsHolder);
        Fit(CardsHolder);
    }

    private Bounds GetBound(GameObject CardsHolder)
    {
        Bounds bound = new Bounds(CardsHolder.transform.position, Vector3.zero);
        var renderersList = CardsHolder.GetComponentsInChildren<Renderer>();
        foreach (Renderer R in renderersList)
        {
            bound.Encapsulate(R.bounds);
        }
        return bound;
    }

    private void Fit(GameObject CardsHolder) 
    {
        Bounds bound = GetBound(CardsHolder);
        Vector3 boundSize = bound.size;
        float Diag = Mathf.Sqrt( Mathf.Pow(boundSize.x,2) + Mathf.Pow(boundSize.y, 2) + Mathf.Pow(boundSize.z, 2));
        thisCamera.orthographicSize = Diag / 2f;
    }
}
