using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public bool isDoor;
    public bool isLever;
    public HingeJoint Hjoint;

    public void InteractDoor()
    {
        //Caso seja uma porta
        if (isDoor || isLever) {
            if (!Hjoint.useMotor) {
                Hjoint.useMotor = true;
            }
            else {
                Hjoint.useMotor = false;
            }
        }
        
    }

}
