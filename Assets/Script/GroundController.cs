using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public bool kill,animated;
    public float timeToStart, rotationTime, rotationValue ;
    private float newRotZ;
    
    // Start is called before the first frame update
    void Awake()
    {
        if(animated)
        {
            newRotZ = transform.rotation.eulerAngles.z;
        }
        
       
    }

    // Update is called once per frame
    void Update()
    {
      transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, newRotZ);
    }
    IEnumerator Rotate()
    {
       
       
        animated = false; 
        yield return new WaitForSeconds(timeToStart);
        float timeElapsed = 0;
        while (timeElapsed < rotationTime)
        {
            newRotZ = Mathf.Lerp(transform.rotation.z, transform.rotation.z + rotationValue, timeElapsed / rotationTime);
            timeElapsed += Time.fixedDeltaTime;
            yield return null;
        }
            
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<RiderController>() == true && animated)
        {
            StartCoroutine(Rotate());
        }
    }
}
