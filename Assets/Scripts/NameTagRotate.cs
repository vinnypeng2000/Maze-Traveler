using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTagRotate : MonoBehaviour
{

    public Transform tf;
    public Vector3 offset;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 180, 0);
        tf = cam.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(tf);
        transform.Rotate(offset);
    }
}
