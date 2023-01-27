using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RiderController : MonoBehaviour
{
    [HideInInspector]
    public bool moved;
    public bool isGrouded;
    [HideInInspector]
    public bool alive,flip;
    public Wheels_Controller backWheel, frontWheel;
    private bool backWheelTouch, frontWheeltouch;


    private int zminreset, zmaxreset;
    public int score,scoreCount;
    public float zmin, zmax;
    public float speed;
    public float rotationSpeed;
    public TextMeshProUGUI uiScore; 
    private Rigidbody2D rg2d;
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        zminreset = 3;
        zmaxreset = 5;
        myAnimator = GetComponent<Animator>();
        rg2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDetector();
    }
    private void FixedUpdate()
    {
        MoveRider();
        CountFlips();
    }










    private void movementDetector()             // input a changer pour le mobile
    {
        if(Input.touches[0].phase == TouchPhase.Began )
        {
            moved = true; 
        }

        if (Input.touches[0].phase == TouchPhase.Ended )
        {
            moved = false;
        }
    }
    private void MoveRider()
    {
        if (moved)
        {
            if(isGrouded)
            {
                rg2d.AddForce(transform.right * speed * Time.fixedDeltaTime * 100f, ForceMode2D.Force);
            }
            else 
            {
                rg2d.AddTorque(rotationSpeed * rotationSpeed * Time.fixedDeltaTime * 100f, ForceMode2D.Force);
            }
        }
        

    }
    private void CountFlips()
    {
        if (!isGrouded &&  !flip && (transform.rotation.eulerAngles.z > zmin) && (transform.rotation.eulerAngles.z < zmax)  )
        {
          
            scoreCount++;
            flip = true; 
            
        }
        if ( (transform.rotation.eulerAngles.z > zminreset) && (transform.rotation.eulerAngles.z < zmaxreset))
        {
            flip = false; 
        }

    }
    IEnumerator PointFlipCalculator()
    {
        yield return new WaitForSeconds(0.05f);
        if(backWheel.isGrounded && frontWheel.isGrounded )
        {
            scoreCount *= 2; 
        }
        new WaitForSeconds(0.8f);
        score += scoreCount;
        uiScore.text = ""  + score.ToString();
        scoreCount = 0;
        flip = false;

    }
    

    public void Death()
    {
        
        alive = true;
        myAnimator.SetBool("Death",true); 
    }
    public void EndDeath()
    {
        myAnimator.SetBool("Finito", true);
    }
    public void Wheelstouchground()
    {


        isGrouded = true;
        if(backWheel.isGrounded && frontWheel.isGrounded == false || backWheel.isGrounded == false && frontWheel.isGrounded )
        {
            StartCoroutine(PointFlipCalculator());
        }
       
    }

    private void OnAnimatorIK(int layerIndex)
    {
       
    }
    public void WheelsStayOnGround()
    {
        isGrouded = true;
    }
    public void WheelsLeaveTheGround()
    {
        isGrouded = false; 
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

}
