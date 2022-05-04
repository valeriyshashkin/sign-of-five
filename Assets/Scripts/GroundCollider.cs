using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public PlayerMovement player;

    void OnTriggerStay(Collider collider)
    {
        player.grounded = true;
    }

    void OnTriggerExit(Collider collider)
    {
        player.grounded = false;
    }
}
