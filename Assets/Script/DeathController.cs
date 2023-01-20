using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    private RiderController rc;
    // Start is called before the first frame update
    void Start()
    {
        rc = GetComponentInParent<RiderController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.GetComponent<GroundController>().kill == true)
       {
            rc.Death();
       }
    }
}
