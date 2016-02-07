using UnityEngine;
using System.Collections;

public class NeclearTest : MonoBehaviour 
{

    void Start()
    {
        Rigidbody rigit = GetComponent<Rigidbody>();
        rigit.centerOfMass = new Vector3(1.08f, 1.08f, 0);
    }
}
