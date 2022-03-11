using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulpoZonesBehavior : MonoBehaviour
{
    GameObject Pulpo;
    public PulpoStances Stance;
    public bool pulpoInZone = true;

    void Awake()
    {
        Pulpo = GameObject.FindGameObjectWithTag("Pulpo");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Pulpo != null ) 
        { 
            pulpoInZone = true;
            Pulpo.GetComponent<PulpoController>().ChangeStance(Stance);
        } 
        else
        {
            Debug.LogWarning("Pulpo not assigned");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( Pulpo != null )
        {
            pulpoInZone = false;
        }
        else
        {
            Debug.LogWarning("Pulpo not assigned");
        }
    }
}
