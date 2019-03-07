using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StartingVelocity : MonoBehaviour
{
    public Vector3 m_velocity;
	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody>().velocity = m_velocity;
	}
}
