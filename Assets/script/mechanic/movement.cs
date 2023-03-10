using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class movement : MonoBehaviour
{   
     [SerializeField] public float movementspeed;
    [SerializeField] private float rotationspeed=500f;
    Touch touch;
    Vector3 touchdown;
     public Vector3 direction;
    private Camera Cam;
    [SerializeField] private float playerSpeed;
    Vector3 touchup;
    private bool dragstarted,ismoving;

   void Update()
    {
        if(Input.touchCount>0){

            touch=Input.GetTouch(0);
            if(touch.phase==TouchPhase.Began){

                dragstarted=true;
                ismoving=true;
                touchdown=touch.position;
                touchup=touch.position;
            }
        }
        if(dragstarted){
            if(touch.phase==TouchPhase.Moved)
            {
                touchdown=touch.position;
            }
            if(touch.phase==TouchPhase.Ended)
            {
                touchdown=touch.position;
                dragstarted=false;
                ismoving=false;
            }
            gameObject.transform.rotation=Quaternion.RotateTowards(transform.rotation,CalculateRotation(),rotationspeed*Time.deltaTime);
            gameObject.transform.Translate(Vector3.forward*Time.deltaTime*movementspeed);
            if(this.gameObject.tag=="Player"){
             var anim =this.gameObject.GetComponent<Animator>();
            anim.SetBool("yürü",true);
            }
           
        }
        else {
        if(this.gameObject.tag=="Player"){
             var anim =this.gameObject.GetComponent<Animator>();
            anim.SetBool("yürü",false);
            }         
        }
        
    }
    Vector3 CalculateDirection(){
        Vector3 temp=(touchdown-touchup).normalized;
        temp.z=temp.y;
        temp.y=0;
        return temp;
    }
    Quaternion CalculateRotation(){
        Quaternion temp=Quaternion.LookRotation(CalculateDirection(),Vector3.up);
        return temp;
    }

}
