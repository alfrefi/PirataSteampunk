using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public bool hasMuerto;
    public bool juegoTerminado;

    private void Awake()
    {
        if ( _instance != null )
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public static GameManager Instance 
    { 
        get { 
            if (_instance == null )
            {
                _instance = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            return _instance; 
        } 
    }
}
