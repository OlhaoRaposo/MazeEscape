using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using UnityEngine.SceneManagement;

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
        private float speed = 2;
        private bool isWalking;
        private bool IsCoroutineRunning = false;
        private bool isCheck;
        private bool isRuning = false;
        private bool stamina = true;
        private bool canRun = true;
        Vector3 velocity;
        private int rnd;
        private void Start() {
            FindObjectOfType<AudioManager>().Play("StaticSound");
            StartCoroutine("PlayAudio");
        }
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

            if (Input.GetKeyDown(KeyCode.LeftShift) && stamina == true)
            {
                if(!isRuning) 
                    speed += 4;
                    canRun = false;
                    stamina = false;
                    Invoke("StaminaMarker", 6f);
                isRuning = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if(isRuning)
                    speed = 2;
                isRuning = false;
                
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

         void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Next"))
            {
                NextPhase(col.gameObject.name.ToString());
            }
        }
        void NextPhase(string a)
        {
            SceneManager.LoadScene(a);
        }
        void StaminaMarker()
        {   
            speed = 2;
            if (stamina == false && canRun == false)
            {
                canRun = true;
                FindObjectOfType<AudioManager>().Play("Breathe");
                Invoke("StaminaMarker", 5f);
            }
            if (canRun == true)
            {
                stamina = true;
            }
        }

        IEnumerator PlayAudio()
        {
            rnd = new Random().Next(20, 48);
            int choice;
            
            choice = new Random().Next(0, 8);
            switch (choice)
            {
                case 1 :
                    FindObjectOfType<AudioManager>().Play("R1");
                    break;
                case 2 :
                    FindObjectOfType<AudioManager>().Play("R2");
                    break;
                case 3 :
                    FindObjectOfType<AudioManager>().Play("R3");
                    break;
                case 4 :
                    FindObjectOfType<AudioManager>().Play("R4");
                    break;
                case 5 :
                    FindObjectOfType<AudioManager>().Play("R5");
                    break;
                case 6 :
                    FindObjectOfType<AudioManager>().Play("R6");
                    break;
                case 7 :
                    FindObjectOfType<AudioManager>().Play("R7");
                    break;
            }
            Debug.Log("ASIMDA");
            yield return new WaitForSeconds(rnd);
            StartCoroutine("PlayAudio");
        }
}
