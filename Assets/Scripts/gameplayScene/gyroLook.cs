using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls camera rotation in mobile devices
public class gyroLook : MonoBehaviour
{
    [SerializeField] Transform playerBody; 
    bool gyroEnabled;    
    Gyroscope gyro;      
    Quaternion rotFix;   

    float xRotation = 0f;

    void Start()
    {
        gyroEnabled = EnableGyro();

        if (gyroEnabled)
        {
            rotFix = new Quaternion(0, 0, 1, 0); 
        }
    }

    void Update()
    {
        if (gyroEnabled)
        {
            // get gyro rotation
            Quaternion gyroRotation = gyro.attitude * rotFix;
            transform.localRotation = Quaternion.Euler(-gyroRotation.eulerAngles.x, gyroRotation.eulerAngles.y, 0);
            playerBody.localRotation = Quaternion.Euler(0, gyroRotation.eulerAngles.y, 0);
        }
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        return false;
    }
}
