using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWater : MonoBehaviour
{
    public FluidSpawner m_spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_spawner.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_spawner.enabled = false;
        }
    }
}
