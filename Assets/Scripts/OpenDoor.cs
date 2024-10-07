using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    public float xAngle, yAngle, zAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log("TOUCH");
            if (Input.GetKeyDown("o"))
            {
                // Debug.Log("Pressed O");
                transform.Rotate(xAngle, yAngle, zAngle);
            }
        }
    }
}
