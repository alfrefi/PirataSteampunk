using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurorialButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goHome()
    {
        SceneManager.LoadScene(sceneName: "Menu");
    }

    public void goNext()
    {
        SceneManager.LoadScene(sceneName: "Tutorial-02");
    }

    public void goBack()
    {
        SceneManager.LoadScene(sceneName: "Tutorial");
    }
}
