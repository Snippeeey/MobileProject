using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 offset; 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = targetTransform.position + offset;
        newPos.z = transform.position.z;
        transform.position = newPos; 
    }
}
