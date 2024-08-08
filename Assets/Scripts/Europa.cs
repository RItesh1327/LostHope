using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;


public class Europa : MonoBehaviour
{
    public GameObject crystalParent;
    public int initialCrystal = 10;
    public TMP_Text objectiveText;
    public TMP_Text buttonPress;
    private int crystalsCollected = 0;
    public GameObject stones;
    public GameObject phosphorous;
    public GameObject safeplace;
    public TMP_Text situation;
    public bool crystalMission = true;
    public bool PhosphorousMission =false;
    public bool IgniteMission = false;
    public AudioSource stonesfall;

    private void Awake()
    {
        // Update the crystalsCollected variable based on the number of child GameObjects in the crystalParent GameObject
        crystalMission = true;
        initialCrystal = crystalParent.transform.childCount;
    }
    void Update()
    {
        crystalsCollected = initialCrystal -  crystalParent.transform.childCount;
        Debug.Log("Initial  " +initialCrystal );
        Debug.Log("collected  " +crystalsCollected );
        // Check if the player has collected enough crystals
        if (crystalsCollected == initialCrystal && crystalMission == true)
        {
            // Update the objective text
            objectiveText.text = "Objective complete! Return to the spaceship.";
           // this.gameObject.SetActive(false);
            stones.SetActive(true);
            stonesfall.Play();
            crystalMission = false;
            PhosphorousMission = true;
           
        }
        if (crystalsCollected != initialCrystal && crystalMission == true)
        {
            // Update the objective text
            objectiveText.text = "Collect " + (initialCrystal - crystalsCollected).ToString()  + " more crystals.";
        }

        if(phosphorous.transform.childCount == 0 && PhosphorousMission == true)
        {

            newObjective2();
            PhosphorousMission = false;
            IgniteMission = true;
        }

        if(IgniteMission && OVRInput.Get(OVRInput.Button.Two))
    //    if (IgniteMission && Input.GetMouseButton(1)) ;
        {
      //      buttonPress.text = "";
            IgniteMission = false;
            newObjective3();
        }
    }

    public void newObjective1()
    {
        objectiveText.text = "Collect Phosphorous crystals.";
        phosphorous.SetActive(true);
    }

    public void newObjective2()
    {
        objectiveText.text = "go near rocks and press B to ignite";
    }
    public void newObjective3()
    {
        objectiveText.text = "Find a safe place";
        safeplace.SetActive(true);
    }
    public void newObjective4()
    {
        objectiveText.text = "return to spaceship";
       Destroy(stones);
    }
    public void ignition()
    {
        buttonPress.text = " Press the button 'B' ";
    }
}
