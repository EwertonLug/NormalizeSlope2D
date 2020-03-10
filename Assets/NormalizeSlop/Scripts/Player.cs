using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public NormalizeSlope normalizeSlope;
    private Rigidbody2D rigidbody2D;
  
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D    = GetComponent<Rigidbody2D>();
        normalizeSlope = GetComponent<NormalizeSlope>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
        jump();
    }
    void move(){
        float direction = Input.GetAxis("Horizontal");
        this.normalizeSlope.MoveAndNormalizeSlope(direction,speed);//Left or Right
    }
    void jump(){
        if(Input.GetKey(KeyCode.Space) && normalizeSlope.grounded){
            rigidbody2D.AddForce(Vector2.up*600);
        }
    }
}
