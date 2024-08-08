using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
public class Crystal : MonoBehaviour
{
    private XRBaseInteractor grab;
    public TMP_Text grabbed;
    public AudioSource collect;
    private void Start()
    {
        grab = GetComponent<XRBaseInteractor>();
    }
    void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "hand")
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0 || OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0)
            {
                collect.Play();
                Destroy(gameObject);
            }
        }
    }
}
