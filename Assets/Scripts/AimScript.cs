using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimScript : MonoBehaviour
{
    //Public VAR
    public float MouseSens = 100f;
    public float minAngle, MaxAngle;
    public Transform player;
    public Text intText;
    
    
    //Private VAR
    private float xRotation = 0f;
    private float distanceOfInteract = 4;
    private RaycastHit ray;
    private Vector3 rayVec;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSens * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp( xRotation,minAngle, MaxAngle);
        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        player.Rotate(Vector3.up * mouseX);
        
        //Desenha um Ray do tamano do raycast
        Debug.DrawRay(Camera.main.transform.position,transform.forward*distanceOfInteract,Color.red);
        
        rayVec = Camera.main.transform.position;
        if (Physics.Raycast(rayVec, transform.forward, out ray)) {
            if (ray.distance < distanceOfInteract ){
                if (ray.collider.gameObject.CompareTag("InteractiveObject")){
                    //Esta Colidindo com o Objeto
                    Interact();
                }
                else {
                    //Esse objeto nao e interativo.
                    NotInteract();
                    return; }
            }
            else {
                //Esta Colidindo mas esta muito longe.
                NotInteract();
                return; }
        }
        
        
        
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Interact()
    {
        intText.enabled = true;
        Debug.Log("Hit");
    }
    void NotInteract()
    {
        intText.enabled = false;
        Debug.Log("Esse objeto nao e interativo ou esta muito longe.");

    }
    
}
