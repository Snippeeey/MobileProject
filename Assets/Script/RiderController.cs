using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderController : MonoBehaviour
{
    [HideInInspector]
    public bool moved;
    public bool isGrouded;
    [HideInInspector]
    public bool alive; 





    public float speed;
    public float rotationSpeed; 
    private Rigidbody2D rg2d;
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
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
    private void OnCollisionStay2D()
    {
        isGrouded = true;
    }
    private void OnCollisionExit2D()
    {
        isGrouded = false; 
        
    }
}
