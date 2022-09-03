using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Publics Var
        public CharacterController cc;
        public Transform groundCheck;
        public int keys;
        

    //Privates Var
        private float speed = 4;
        private float gravity = -19.62f;
        private float groundDistance = 0.04f;
        private bool isGround;
        private float jumpHeight = 2;
        private Vector3 velocity;
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
