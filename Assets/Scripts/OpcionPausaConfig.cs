using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OpcionPausaConfig : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        text = this.transform.GetChild(0).GetComponent<Text>();
       image = this.transform.parent.GetComponent<Image>();
    }

    // Update is called once per frame
    public void goToConfig()
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
