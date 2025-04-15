using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windController : MonoBehaviour
{
    public Vector3 windDirection = new Vector3(1, 0, 0); 
    public float windSpeed = 5f; 

    public static windController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector3 ObtenerFuerzaViento()
    {
        return windDirection.normalized * windSpeed;
    }
}
