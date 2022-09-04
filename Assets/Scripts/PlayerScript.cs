using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Publics Var
        public CharacterController cc;
        public Transform groundCheck;
        public int keys;
        
        
    //Private Var
        private float groundDistance = 0.06f;
        private float jumpHeight;       
        private bool isGround;
        private float gravity = -19.20f;
        private float speed = 3;
        private bool isWalking;
        private bool IsCoroutineRunning = false;
        private bool isCheck;
        Vector3 velocity;
        private void Start() {
            FindObjectOfType<AudioManager>().Play("StaticSound");
        }
        void Update() {
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

            if (Input.anyKey)
            {
                isWalking = true;
            }
            else
                isWalking = false;
            
            
            if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                if (!isCheck ) {
                    CheckSound();
                }else {return;}
                isCheck = true;
            } 
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        }
        void CheckSound() {
            if(!IsCoroutineRunning) {
                StartCoroutine(WalkSound());
            }else{return;}
        }
        IEnumerator WalkSound() {
            IsCoroutineRunning = true;
            if (IsCoroutineRunning & isGround) {
                FindObjectOfType<AudioManager>().Play("Walk");
            }
            yield return new WaitForSeconds(.5f);
            if (!isWalking) {
                StopCoroutine(WalkSound());
                isCheck = false;
            }
            else {
                StartCoroutine(WalkSound());
            }
            IsCoroutineRunning = false;
        }
        
}
