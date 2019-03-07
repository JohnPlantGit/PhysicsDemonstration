using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CustomCharacterController ragdoll = other.gameObject.GetComponent<CustomCharacterController>();
        if (ragdoll != null)
            ragdoll.RagdollOn = true;
    }
}
