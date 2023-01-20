using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels_Controller : MonoBehaviour
{
    public bool isGrounded;
    private RiderController rd;
    public Wheels_Controller otherWC;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponentInParent<RiderController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D()
    {
        isGrounded = true;
        rd.Wheelstouchground();
    }
    private void OnCollisionStay2D()
    {
        isGrounded = true;
        rd.WheelsStayOnGround();
    }
    private void OnCollisionExit2D()
    {
        isGrounded = false;
        if (otherWC.isGrounded == false)
        {
            rd.WheelsLeaveTheGround();
        }

    }
}