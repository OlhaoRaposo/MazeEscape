using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public bool isDoor;
        public bool doorIsLocked;
        public bool isDorLever;
        
    public bool isLever;
        public GameObject doorWithLever;
        
    public bool isKey;
    public HingeJoint Hjoint;
    
    GameObject player;
    private void Start()
    {
        //SetVars
        player = GameObject.Find("Player");
    }

    public void InteractDoor() {
        //Caso seja uma porta
        if (isDoor){
            if (isLever) {
                return;
            }
            else {
                if (doorIsLocked) {
                    if (player.GetComponent<PlayerScript>().keys > 0) {
                        player.GetComponent<PlayerScript>().keys -= 1;
                        if (!Hjoint.useMotor) {
                            Hjoint.useMotor = true;
                        }
                        else {
                            Hjoint.useMotor = false;
                        } 
                        //PlayOpen
                        FindObjectOfType<AudioManager>().Play("DoorOpening");
                        doorIsLocked = false;
                    }
                    else {
                        //PlayLocked
                        FindObjectOfType<AudioManager>().Play("DoorLocked");
                    }
                }
                else {
                    if (!Hjoint.useMotor) {
                        Hjoint.useMotor = true;
                    }
                    else {
                        Hjoint.useMotor = false;
                    } 
                    //PlayOpen
                    FindObjectOfType<AudioManager>().Play("DoorOpening");
                }  
            }
        }
    }
    
    public void InteractLever() {
        if (isLever){
            if (!Hjoint.useMotor) {
                Hjoint.useMotor = true;
            }
            else {
                Hjoint.useMotor = false;
            }
            doorWithLever.GetComponent<InteractiveObject>().InteractLeverDoor();
            //PlayLeverAudio
        }
    }
    public void InteractLeverDoor() {
        doorIsLocked = false;
        if (isDoor) {
            if (!Hjoint.useMotor) {
                Hjoint.useMotor = true;
            }
            else { Hjoint.useMotor = false;
            }
            FindObjectOfType<AudioManager>().Play("DoorOpening");
        }
    }
    public void InteractKey() {
        player.GetComponent<PlayerScript>().keys += 1;
        Destroy(this.gameObject);
    }

}
