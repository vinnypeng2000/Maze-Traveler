using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{

    public MollyDoor door;
    public GameObject text;
    public bool interacted;
    public AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        interacted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (interacted && Input.GetKeyDown("e"))
        {
            door.painting = true;
            Debug.Log("E PRESSED");
            text.SetActive(false);
            sfx.Play();
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        // Check if the player collided with the door
        if (col.CompareTag("Player"))
        {
            text.SetActive(true);
            interacted = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            text.SetActive(false);
        }
    }
}
