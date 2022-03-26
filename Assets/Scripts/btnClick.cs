using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class btnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    void Start()
    {
       Debug.Log("hijos: " + this.transform.childCount);
       text = this.transform.GetComponentInChildren<Text>();
    }
    public void Check()
    {
        //elementosColocados  = new List<string>(); 

        Debug.Log("REGRESA AL JUEGO");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //do stuff
        text.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //do stuff
        text.color = Color.black;
    }
}
