using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCube : MonoBehaviour
{
    public Rigidbody m_cube;
    public Transform m_resetPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_cube.isKinematic = true;
            m_cube.position = m_resetPosition.position;
        }
    }
}
