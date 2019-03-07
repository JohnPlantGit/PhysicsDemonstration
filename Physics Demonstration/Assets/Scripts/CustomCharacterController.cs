using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
    public float m_acceleration;
    public float m_crouchedAcceleration;
    public float m_friction;
    public float m_jumpSpeed;
    public List<Rigidbody> m_ragdollRBs = new List<Rigidbody>();
    public List<Collider> m_ragdollColliders = new List<Collider>();
    public CameraController m_cameraController = null;
    public GameObject m_hips = null;

    private Animator m_animator = null;
    private CapsuleCollider m_collider = null;
    private Vector3 m_velocity;
    private float m_yaw = 0.0f;
    private float m_pitch = 0.0f;
    private bool m_grounded = false;
    private bool m_crouching = false;
    private float m_colliderHeight;
    private float m_colliderCentre;
	// Use this for initialization
	void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<CapsuleCollider>();

        m_colliderHeight = m_collider.height;
        m_colliderCentre = m_collider.center.y;

        foreach (Rigidbody rb in m_ragdollRBs)
        {
            rb.isKinematic = true;
        }
        foreach (Collider c in m_ragdollColliders)
        {
            c.enabled = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_yaw += Input.GetAxis("Mouse X") * 2;
        m_pitch -= Input.GetAxis("Mouse Y") * 2;

        if (!RagdollOn)
        {
            if (m_grounded && Input.GetKeyDown(KeyCode.Space))
            {
                m_velocity += new Vector3(0, m_jumpSpeed, 0);
                m_animator.SetTrigger("Jump");

                RagdollOn = true;

                m_velocity = Vector3.zero;
            }

            if (!m_grounded)
            {
                m_velocity += Physics.gravity * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (m_crouching)
                {
                    RaycastHit[] raycast = Physics.RaycastAll(transform.position, Vector3.up, 2.0f);
                    if ((raycast.Length > 0 && raycast[0].distance >= 2) || raycast.Length == 0)
                    {
                        m_animator.SetLayerWeight(0, 1);
                        m_animator.SetLayerWeight(1, 0);

                        m_collider.height = m_colliderHeight;
                        m_collider.center = new Vector3(0, m_colliderCentre, 0);

                        m_crouching = false;
                    }
                }
                else
                {
                    m_animator.SetLayerWeight(0, 0);
                    m_animator.SetLayerWeight(1, 1);

                    m_collider.height = m_colliderHeight / 2.0f;
                    m_collider.center = new Vector3(0, m_colliderCentre / 2.0f, 0);

                    m_crouching = true;
                }    
            }

            Vector3 movementVector = Quaternion.Euler(0, m_yaw, 0) * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            m_velocity += movementVector * (m_crouching ? m_crouchedAcceleration : m_acceleration) * Time.deltaTime;
            transform.position += m_velocity * Time.deltaTime;
            m_velocity -= m_velocity * m_friction * Time.deltaTime;

            Vector3 lookVector = new Vector3(m_velocity.x, 0, m_velocity.z);
            if (lookVector.sqrMagnitude != 0) // update to only use velocity x / z
            {
                transform.rotation = Quaternion.LookRotation(lookVector.normalized);
            }
            m_animator.SetFloat("Speed", m_velocity.magnitude);

            if (movementVector.magnitude == 0 && m_velocity.magnitude <= 0.1)
            {
                m_velocity = Vector3.zero;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RagdollOn = false;
            }
        }
	}

    private void LateUpdate()
    {
        if (!RagdollOn)
        {
            bool colliderBelow = false;

            Collider[] collisions = Physics.OverlapCapsule(m_collider.center + new Vector3(0, m_collider.height / 2, 0) + transform.position, m_collider.center - new Vector3(0, m_collider.height / 2, 0) + transform.position, m_collider.radius, -1, QueryTriggerInteraction.Ignore);

            foreach (Collider c in collisions)
            {
                if (c == m_collider)
                    continue;

                Vector3 direction;
                float distance;
                if (Physics.ComputePenetration(m_collider, transform.position, transform.rotation, c, c.transform.position, c.transform.rotation, out direction, out distance))
                {
                    Vector3 penetration = direction * distance;
                    Vector3 velocityProjected = Vector3.Project(m_velocity, -direction);

                    transform.position += penetration;
                    m_velocity -= velocityProjected;
                }
                if (Vector3.Dot(direction, Vector3.up) > 0)
                {
                    colliderBelow = true;
                }
            }

            m_grounded = colliderBelow;
        }
    }

    public bool RagdollOn
    {
        get { return !m_animator.enabled; }
        set
        {
            if (value == true)
            {
                m_cameraController.m_parent = m_hips;
            }
            else
            {
                m_cameraController.m_parent = gameObject;
            }
            m_animator.enabled = !value;

            foreach (Rigidbody rb in m_ragdollRBs)
            {
                rb.isKinematic = !value;
                if (value == true)
                {
                    rb.velocity = m_velocity;
                }
            }
            foreach (Collider c in m_ragdollColliders)
            {
                c.enabled = value;
            }
            m_collider.enabled = !value;
        }
    }
}
