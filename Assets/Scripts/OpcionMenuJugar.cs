using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpcionMenuJugar : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseOver()
    {
        m_SpriteRenderer.color =  new Color(250f/255f, 4f/255f, 21f/255f);
    }

    void OnMouseExit()
    {
        m_SpriteRenderer.color =  new Color(255f/255f, 255f/255f, 255f/255f);
    }

    private void OnMouseDown() 
    {
        SceneManager.LoadScene(sceneName: "BossFight");
    }
    
}
