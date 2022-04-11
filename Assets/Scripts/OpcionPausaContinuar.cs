using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpcionPausaContinuar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    public Image image;

    public Text[] textos;
    void Start()
    {
       //text = this.transform.GetComponentInChildren<Text>();
       text = this.transform.GetChild(0).GetComponent<Text>();
       textos = this.transform.parent.GetComponentsInChildren<Text>();
       image = this.transform.parent.GetComponent<Image>();
    }
    public void ResumeGame()
    {
        //Debug.Log("REGRESA AL JUEGO");
        image.enabled = false;
        text.enabled = false;
        textos[0].enabled = false;
        textos[1].enabled = false;
        textos[2].enabled = false;
        Time.timeScale = 1f;
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