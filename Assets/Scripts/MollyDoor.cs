using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MollyDoor : MonoBehaviour
{

    public bool painting;
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        painting = false;
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (painting)
        {
            Debug.Log("DESTROY DOOR");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        // Check if the player collided with the door
        if (col.CompareTag("Player") && !painting)
        {
            text.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player") && !painting)
        {
            text.SetActive(false);
        }
    }
}
