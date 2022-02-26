using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //text.enabled = GameManager.Instance.hasMuerto;
        if(GameManager.Instance.hasMuerto == true)
        {
            text.text = "Has Muerto";
            text.color = Color.red;
            text.enabled = true;
            PauseGame();
        }
        else
        {
            if(GameManager.Instance.juegoTerminado == true)
            {
                text.text = "Juego terminado";
                text.color = Color.green;
                text.enabled = true;
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Time.timeScale == 0 && GameManager.Instance.hasMuerto == true) 
            {
                ResumeGame();
                GameManager.Instance.hasMuerto = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
