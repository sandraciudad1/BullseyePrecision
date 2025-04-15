using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetsController : MonoBehaviour
{
    [SerializeField] GameObject target2;
    [SerializeField] GameObject target4;
    float speed = 0.7f; 

    void Update()
    {
        if (target2.activeInHierarchy)
        {
            float zPosition = Mathf.PingPong(Time.time * speed, 65f - 59f) + 59f;
            target2.transform.position = new Vector3(target2.transform.position.x, target2.transform.position.y, zPosition);
        }
        if (target4.activeInHierarchy)
        {
            float xPosition = Mathf.PingPong(Time.time * speed, 97f - 95f) + 95f;
            target4.transform.position = new Vector3(xPosition, target4.transform.position.y, target4.transform.position.z);
        } 
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
