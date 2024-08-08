using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CollectObject : MonoBehaviour
{
    // The name of the button that needs to be pressed to collect the object
    public string buttonName;

    // The name of the object to be collected
    public string objectName;

    public TMP_Text title;
    public TMP_Text info;
    public TMP_Text ButtonInfo;
    public Image displayicon;
    public GameObject infopanel;
    public SoundManager soundmanager;
    // Details of the object
    public string objectInfo;

    // Icon of the Object
    public Sprite Icon;

    // A reference to the player character
    public GameObject player;

    // A reference to the inventory script
    public Inventory inventory;

    // A flag indicating whether the player is currently in the trigger zone
    private bool playerInTriggerZone;

    // Start is called before the first frame update
    void Start()
    {
        soundmanager = GetComponent<SoundManager>();
        playerInTriggerZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is in the trigger zone and the collect button is pressed...
        if (playerInTriggerZone && OVRInput.Get(OVRInput.Button.Two))
        {
            // ... collect the object
            Collect();
        }
    }

    // Function to collect the object
    void Collect()
    {
        // Add the object to the inventory
        inventory.AddItem(gameObject);

        // Disable the object's gameObject
       

        // Display a message indicating that the object has been collected
        Debug.Log("Collected " + objectName);

        infopanel.SetActive(false);
        playerInTriggerZone = false;
      //  soundmanager.PlayPickupSound();
        Destroy(this.gameObject);
        
    }

    private IEnumerator DisplayInfo()
    {
        displayicon.sprite = Icon;
        title.text = objectName;
        info.text = objectInfo;
        yield return new WaitForSeconds(3);
        playerInTriggerZone = true;
        ButtonInfo.text ="Press the Button "+ buttonName;
    }
    // Function called when the player enters the trigger zone
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            infopanel.SetActive(true);
            StartCoroutine(DisplayInfo());
            
        }
    }

    // Function called when the player exits the trigger zone
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            infopanel.SetActive(false);
            playerInTriggerZone = false;
        }
    }
}
