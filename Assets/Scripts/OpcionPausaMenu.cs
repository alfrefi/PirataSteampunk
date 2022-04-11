using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OpcionPausaMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    public Image image;
    void Start()
    {
       //text = this.transform.GetComponentInChildren<Text>();
       text = this.transform.GetChild(0).GetComponent<Text>();
       image = this.transform.parent.GetComponent<Image>();
    }
    public void goToMenu()
    {
        //Debug.Log("REGRESA AL JUEGO");
        //Debug.Log("nombre parent: " + this.transform.parent.name);
        image.enabled = false;
        text.enabled = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName: "Menu");
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
