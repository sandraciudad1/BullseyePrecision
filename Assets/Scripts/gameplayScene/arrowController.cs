using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Rigidbody arrowRigidbody = GetComponent<Rigidbody>();
            if (arrowRigidbody != null)
            {
                arrowRigidbody.isKinematic = true;
                arrowRigidbody.useGravity = false;
            }

            // set the arrow on target
            transform.SetParent(collision.transform);
            sublevelsManager sublevels = GameObject.Find("sublevelsManager").GetComponent<sublevelsManager>();
            if (sublevels != null)
            {
                sublevels.OnArrowHit();
            }
        }
    }
}
