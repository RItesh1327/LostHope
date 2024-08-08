using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public GameObject collector;
   
    public TMP_Text Objective_Text;

    private int collectorCount;

    private bool capsulefound, basefound;
    private void Start()
    {
        capsulefound = false;
        basefound = false;
        collectorCount = collector.transform.childCount;  
    }
    private void Update()
    {
        if (collector.transform.childCount == collectorCount - 1)
        {
            capsulefound = true;
        }
        if(collector.transform.childCount == 0)
        {
            basefound = true;
            Mars_ShipReturn();
        }


    }
    /// <summary> Mars Objectives
    public void Mars_CapsuleFind()
    {
        Objective_Text.text = "Find the Capsule Location";
    }
    public void Mars_CapsulePicking()
    {
        if (capsulefound)
        {
            Objective_Text.text = "Collect the Capsule";
            capsulefound = false;
        }
    }

    public void Mars_CollectivesFinding()
    {
        Objective_Text.text = "Find the abandoned base";
    }
    public void Mars_CollectivePicking()
    {
        if(basefound)
            Objective_Text.text = "Retrieve all the collectives";
    }
    public void Mars_ShipReturn()
    {
        Objective_Text.text = "Return to the ship";
    }
    
    ///
    ///<summary> Jupiter Objectives
    ///

    public void Jupiter_collectCrystals()
    {
        Objective_Text.text = "Collect 10 Crystals";
    }
    public void Jupiter_Return()
    {
        Objective_Text.text = "Return to Spaceship";
    }
    public void Jupiter_Phosphorous()
    {
        Objective_Text.text = "Collect phosphorous";
    }
    public void Jupiter_Ignite()
    {
        Objective_Text.text = "Ignite the rocks";
    }
    public void Jupiter_savePlace()
    {
        Objective_Text.text = "Go to a safe place";
    }
    ///
    ///<summary> TopGun Objectives
    ///
    public void Titan_nitrogen()
    {
        Objective_Text.text = "Collect nitrogen";
    }
}
