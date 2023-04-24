using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playCon : MonoBehaviour
{
    [SerializeField] private int speed = 10;

    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var a = Input.GetAxis("Horizontal");
        var b = Input.GetAxis("Vertical");
        /* if (Input.GetKey("space"))
         {
             transform.Translate(new Vector3(a, 3, 0)*speed*Time.deltaTime); 
 
         }*/
        //rigid.AddForce(new Vector3(a, b, 0)*speed*Time.deltaTime); 
        rigid.velocity = new Vector3(a, b, 0) * speed * Time.deltaTime;
        //transform.Translate(new Vector3(a, b, 0)*speed*Time.deltaTime); 
    }
}