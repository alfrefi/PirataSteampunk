using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public Image image;
    public Text[] text;

    public Button[] buttons;

    //public GameObject pauseMenuUi;
    //public static bool GameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
        /*text = image.GetComponentsInChildren<Text>();
        text[0].enabled = false;
        text[1].enabled = false;
        text[2].enabled = false;
        text[3].enabled = false;*/
        buttons = image.GetComponentsInChildren<Button>();
        buttons[0].enabled = false;
        buttons[0].GetComponentInChildren<Text>().enabled = false;
        buttons[1].enabled = false;
        buttons[1].GetComponentInChildren<Text>().enabled = false;
        buttons[2].enabled = false;
        buttons[2].GetComponentInChildren<Text>().enabled = false;
        //Debug.Log("hijos del pergamino: " + image.transform.childCount);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }*/

        if(Input.GetKeyDown(KeyCode.Return) && GameManager.Instance.hasMuerto == false)
        {
            if (Time.timeScale == 0)
            {
                ResumeGame();
                image.enabled = false;
                /*text[0].enabled = false;
                text[1].enabled = false;
                text[2].enabled = false;
                text[3].enabled = false;*/
                buttons[0].enabled = false;
                buttons[0].GetComponentInChildren<Text>().enabled = false;
                buttons[1].enabled = false;
                buttons[1].GetComponentInChildren<Text>().enabled = false;
                buttons[2].enabled = false;
                buttons[2].GetComponentInChildren<Text>().enabled = false;
            }
            else
            {
                PauseGame();
                image.enabled = true;
                /*text[0].enabled = true;
                text[1].enabled = true;
                text[2].enabled = true;
                text[3].enabled = true;*/
                buttons[0].enabled = true;
                buttons[0].GetComponentInChildren<Text>().enabled = true;
                buttons[1].enabled = true;
                buttons[1].GetComponentInChildren<Text>().enabled = true;
                buttons[2].enabled = true;
                buttons[2].GetComponentInChildren<Text>().enabled = true;
            }
        }
        
    }

    void PauseGame()
    {
        //pauseMenuUi.SetActive(true);
        Time.timeScale = 0;
        //GameIsPaused = true;
    }

    void ResumeGame()
    {
        //pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        //GameIsPaused = false;
    }

    void OnMouseOver()
    {
        //m_SpriteRenderer.color =  new Color(250f/255f, 4f/255f, 21f/255f);
        //text.color = Color.red;
        Debug.Log("OnMouseOver");
    }

    void OnMouseExit()
    {
        //m_SpriteRenderer.color =  new Color(255f/255f, 255f/255f, 255f/255f);
        Debug.Log("OnMouseExit");
    }

    private void OnMouseDown() 
    {
        //Time.timeScale = 1;
        Debug.Log("OnMouseDown");
    }
}
