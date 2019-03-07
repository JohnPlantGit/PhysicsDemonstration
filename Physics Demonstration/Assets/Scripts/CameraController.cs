using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject m_parent;

    private float m_yaw = 0.0f;
    private float m_pitch = 0.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = m_parent.transform.position;

        m_yaw += Input.GetAxis("Mouse X") * 2;
        m_pitch -= Input.GetAxis("Mouse Y") * 2;

        transform.eulerAngles = new Vector3(m_pitch, m_yaw, 0);
    }
}
