using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidSpawner : MonoBehaviour
{
    public Transform m_spawnPoint;
    public GameObject m_waterPrefab;
    public int m_count;
    public float m_delay;

    private float m_timer;
    private int m_spawned;

	// Use this for initialization
	void Start ()
    {
        m_timer = m_delay;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_timer -= Time.deltaTime;
        
        if (m_timer <= 0 && m_spawned < m_count)
        {
            m_timer = m_delay;
            m_spawned++;
            Instantiate(m_waterPrefab, m_spawnPoint.position + new Vector3(Random.value, 0, Random.value), Quaternion.Euler(0, 0, 0));
        }
	}
}
