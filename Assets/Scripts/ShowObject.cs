using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObject : MonoBehaviour
{

    public GameObject text;
    public GameObject obj;
    public bool interacted;
    public AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        obj.SetActive(false);
        text.SetActive(false);
        interacted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (interacted && Input.GetKey("e"))
        {
            obj.SetActive(true);
            text.SetActive(false);
            sfx.Play();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            text.SetActive(true);
            interacted = true;
        }
    }
}
