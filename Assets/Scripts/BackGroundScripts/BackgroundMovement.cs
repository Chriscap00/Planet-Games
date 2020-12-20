using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float BackgrounMovementSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x+BackgrounMovementSpeed,transform.position.y, transform.position.z);
    }
}
