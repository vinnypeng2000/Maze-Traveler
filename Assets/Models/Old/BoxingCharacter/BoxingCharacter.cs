using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoxingCharacter : MonoBehaviour
{
    public Animator boxingAnim;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            boxingAnim.SetTrigger("Boxing");
        }
    }
}
