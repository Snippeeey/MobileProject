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
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        rd.Wheelstouchground();
        if(collision.gameObject.GetComponent<killZone>())
        {
            rd.Death();
        }
        
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FinishLine>())
        {
            
            GameManager.Instance.temporaryScores = rd.score;
            GameManager.Instance.Finish();
            Debug.Log("leScoremarche");
        }
        if (collision.gameObject.GetComponent<killZone>())
        {
            rd.Death();
        }
    }
}
