using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
     public CharacterController cc;
        public float speed;
        public float gravity = -9.81f;
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public bool isGround;
        public float jumpHeight;
        Vector3 velocity;
        void Update()
        {
            isGround = Physics.CheckSphere(groundCheck.position,groundDistance);
            if (isGround && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
    
            Vector3 move = transform.right * x + transform.forward * z;
            cc.Move(move * speed * Time.deltaTime);
    
           
            velocity.y += gravity * Time.deltaTime;
            cc.Move(velocity * Time.deltaTime);
    
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        }
}
