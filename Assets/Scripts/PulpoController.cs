using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PulpoController : MonoBehaviour
{
    public float timeBetweenAttacks;
    public List<ArmorScript> armors;

    private FlyingBehavior flyingBehavior;
    private BashAttack bashAttack;
    private LaserAttack laserAttack;

    public GameObject[] lifePoints;
    public int vidaBoss;
    public GameObject[] armorsGO;
    public CircleCollider2D circuloCollider;

    
    void Awake()
    {
        flyingBehavior = GetComponent<FlyingBehavior>();
        bashAttack = GetComponent<BashAttack>();
        laserAttack = GetComponent<LaserAttack>();
        circuloCollider = GetComponent<CircleCollider2D>();
        circuloCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        lifePoints = GameObject.FindGameObjectsWithTag("BossLifeUnit");
        vidaBoss = lifePoints.Length;
        if(vidaBoss == 0)
        {
            GameManager.Instance.juegoTerminado = true;
        }
        if ( !flyingBehavior.enabled && !bashAttack.enabled && !laserAttack.enabled )
        {
            ChangeArmorProtection(true);

            flyingBehavior.enabled = true;
            StartCoroutine(DoAttack());
        }
        if(armors.Count == 0)
        {
            circuloCollider.enabled = true;
        }
    }

    private IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        
        ChangeArmorProtection(false);

        int attack = UnityEngine.Random.Range(1, 3);

        flyingBehavior.enabled = false;
        switch (attack)
        {
            case 1:
                bashAttack.enabled = true;
                break;
            case 2:
                laserAttack.enabled = true;
                break;
        }
    }

    private void ChangeArmorProtection(bool isProtected)
    {
        armors.ForEach(a => a.isProtected = isProtected);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ME PEGAN, SOY SOLO UN PULO!");
        //substractLife();
        armorsGO = GameObject.FindGameObjectsWithTag("armor");
        Debug.Log("Armaduras restantes: " + armorsGO.Length);
        //lifePoints = GameObject.FindGameObjectsWithTag("BossLifeUnit");
        //vidaBoss = lifePoints.Length;
        int count = 1;
        if(armorsGO.Length == 0)
        {
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
}
