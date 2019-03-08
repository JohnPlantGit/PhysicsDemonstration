using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCube : MonoBehaviour
{
    public Rigidbody m_cube;
    public Transform m_dropPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_cube.isKinematic = false;
            m_cube.position = m_dropPosition.position;
        }
    }
}
