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
    public GameObject fxBonus;
    public Wheels_Controller backWheel, frontWheel;
    private bool backWheelTouch, frontWheeltouch;

    public Camera cam;
    private int zminreset, zmaxreset;
    public int score,scoreCount;
    public float zmin, zmax, zoompower;
    public float speed;
    public float rotationSpeed;
    public TextMeshProUGUI uiScore; 
    private Rigidbody2D rg2d;
    private Animator myAnimator;
    private float cambasefov;
    private void Awake()
    {
        cam = Camera.main;
        cambasefov = cam.orthographicSize;
    }
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
        Camzoom();
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
        else if ( (transform.rotation.eulerAngles.z > zminreset) && (transform.rotation.eulerAngles.z < zmaxreset)  )
        {
            flip = false; 
        }

    }
    IEnumerator PointFlipCalculator()
    {
        yield return new WaitForSeconds(0.05f);
        if(backWheel.isGrounded && frontWheel.isGrounded &&  scoreCount>0 )
        {
            scoreCount *= 2;
            GameObject Fx = Instantiate(fxBonus, frontWheel.transform.position, frontWheel.transform.rotation);
            Fx.transform.SetParent(frontWheel.transform);
            GameObject fx2 = Instantiate(fxBonus, backWheel.transform.position, backWheel.transform.rotation);
            fx2.transform.SetParent(backWheel.transform);
        }
        new WaitForSeconds(0.8f);
        score += scoreCount;
        uiScore.text = ""  + score.ToString();
        scoreCount = 0;
        flip = false;

    }
    void Camzoom()
    {
       
        RaycastHit2D myray = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity,3);
        Debug.Log(myray.point.y + 0.5 <= transform.position.y);

        //Debug.Log(myray.point.y);
        if (myray.point.y+0.5 <= transform.position.y )
        {
            
            float diff = transform.position.y - myray.point.y;
            Debug.Log("prout"+ cam.orthographicSize);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cambasefov + diff, Time.fixedDeltaTime / zoompower);
        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cambasefov, Time.fixedDeltaTime / zoompower);
        }
    }
    

    public void Death()
    {
        
        alive = true;
        myAnimator.SetBool("Death",true);
        StartCoroutine(WaitDeath());
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
        if (collision.gameObject.GetComponent<killZone>())
        {
            Death();
        }
    }
    IEnumerator WaitDeath()
    {
        yield return new WaitForSeconds(1f);
        
        GameManager.Instance.RestartAfterDeath();
    }

}
