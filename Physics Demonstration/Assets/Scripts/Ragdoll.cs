using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ragdoll : MonoBehaviour
{
    private Animator m_animator = null;
    public List<Rigidbody> m_rigidbodies = new List<Rigidbody>();
    public List<Collider> m_colliders = new List<Collider>();
    public Collider m_mainCollider;
    public CustomCharacterController m_ccc;

	// Use this for initialization
	void Start ()
    {
        m_animator = GetComponent<Animator>();

        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        for (int i = 1; i < colliders.Length; i++)
        {
            m_colliders.Add(colliders[i]);
        }

        foreach(Rigidbody rb in m_rigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach (Collider c in m_colliders)
        {
            c.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!RagdollOn)
        {
            //transform.position += new Vector3(0, 0, 0.01f);
        }
    }

    public bool RagdollOn
    {
        get { return !m_animator.enabled; }
        set
        {
            m_animator.enabled = !value;

            foreach(Rigidbody rb in m_rigidbodies)
            {
                rb.isKinematic = !value;
            }
            foreach (Collider c in m_colliders)
            {
                c.enabled = value;
            }
            m_mainCollider.enabled = !value;
            m_ccc.enabled = !value;
        }
    }

}
