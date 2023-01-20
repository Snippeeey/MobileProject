using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderController : MonoBehaviour
{
    [HideInInspector]
    public bool moved;
    public bool isGrouded;
    [HideInInspector]
    public bool alive,flip;
    public Collider2D backWheel, frontWheel;
    private bool backWheelTouch, frontWheeltouch;


    private int zminreset, zmaxreset;
    public int score,scoreCount;
    public float zmin, zmax;
    public float speed;
    public float rotationSpeed; 
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
        if(Input.GetButtonDown("Fire1"))
        {
            moved = true; 
        }

        if (Input.GetButtonUp("Fire1"))
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
           Debug.Log("dhjdb");
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
        yield return new WaitForSeconds(0.1f);
        if(backWheelTouch && frontWheeltouch)
        {
            scoreCount *= 2; 
        }
        score += scoreCount;
        scoreCount = 0;
        flip = false;

    }
    

    public void Death()
    {
        Debug.Log("mort");
        alive = true;
        myAnimator.SetBool("Death",true); 
    }
    public void EndDeath()
    {
        myAnimator.SetBool("Finito", true);
    }
    private void OnCollisionEnter2D()
    {
        isGrouded = true;
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
       
    }
    private void OnCollisionStay2D()
    {
        isGrouded = true;
    }
    private void OnCollisionExit2D()
    {
        isGrouded = false; 
        
    }

}
