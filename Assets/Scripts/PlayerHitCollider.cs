using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCollider : MonoBehaviour
{
    public GameObject[] lifePoints;
    public int vidaPlayer;
    public List<string> CanDamagePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifePoints = GameObject.FindGameObjectsWithTag("LifeUnit");
        vidaPlayer = lifePoints.Length;
        if(vidaPlayer == 0)
        {
            GameManager.Instance.hasMuerto = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanDamagePlayer.Contains(collision.tag) )
        {
            substractLife();
        }
    }

    public void substractLife()
    {
        //lifePoints = GameObject.FindGameObjectsWithTag("LifeUnit");
        //vidaPlayer = lifePoints.Length;
        int count = 1;
        foreach (GameObject lifePoint in lifePoints)
        {
            if(count == 1)
            {
                Destroy(lifePoint);
            }
            count++;
        }
    }
}
