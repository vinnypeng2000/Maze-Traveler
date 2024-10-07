using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySceneController : MonoBehaviour
{
    public Vector3 defaultStartPosition = new Vector3(-2.22f,0.8f,0.12f);

    private void Start()
    {
        // PlayerPrefs.DeleteAll();

        // Find the player in the scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Check if the PlayerPrefs contains the saved player position
            if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
            {
                // Retrieve the stored player position from PlayerPrefs
                float posX = PlayerPrefs.GetFloat("PlayerPosX");
                float posY = PlayerPrefs.GetFloat("PlayerPosY");
                float posZ = PlayerPrefs.GetFloat("PlayerPosZ");
                Vector3 storedPosition = new Vector3(posX, posY, posZ);

                // Set the player's position to the stored value
                player.transform.position = storedPosition;
            }
            else
            {
                // If no position is stored, place the player at the default starting position
                player.transform.position = defaultStartPosition;
            }
        }
    }
}
