using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsScripts : MonoBehaviour
{
    public Animator[] anim;
    int random; 
    void Start()
    {
        for (int i = 0; i < 0; i++)
        {

            random = Random.Range(0, 101);
            if (random <= 60) {
                anim[i].SetBool("isOn", true);
            }
            else if (random > 60 && random <= 75) {
                anim[i].SetBool("isOff", true);
            }
            else {
                anim[i].SetBool("isBlinking", true);
            }
        }
    }
}
