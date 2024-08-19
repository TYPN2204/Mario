using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColliderOff : MonoBehaviour
{
    public void OffCollider()
    {
        Debug.Log("OffCollider called");
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
}
