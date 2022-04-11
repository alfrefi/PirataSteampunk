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
    private MissileAttackBehavior missileAttackBehavior;
    private AngryShakeBehavior angryShakeBehavior;

    public GameObject[] lifePoints;
    public int vidaBoss;
    public GameObject[] armorsGO;
    public CircleCollider2D circuloCollider;

    public PulpoStances stance = PulpoStances.Vulnerable;

    public bool ProtectedArmorsOnAttack { get; private set; }

    public ParticleSystem Explosion;
    private ParticleSystem explosionPS;
    [SerializeField, ColorUsage(showAlpha: true, hdr: true)]
    Color FullExplosionColor;
    private bool startExplosionGlow;

    void Awake()
    {
        flyingBehavior = GetComponent<FlyingBehavior>();
        bashAttack = GetComponent<BashAttack>();
        laserAttack = GetComponent<LaserAttack>();
        missileAttackBehavior = GetComponent<MissileAttackBehavior>();
        angryShakeBehavior = GetComponent<AngryShakeBehavior>();

        circuloCollider = GetComponent<CircleCollider2D>();
        circuloCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        lifePoints = GameObject.FindGameObjectsWithTag("BossLifeUnit");
        vidaBoss = lifePoints.Length;

        if ( startExplosionGlow )
        {
            StartCoroutine(ExplosionGlow());
        }

        if(vidaBoss == 0)
        {
            return;
        }

        if ( !flyingBehavior.enabled && !bashAttack.enabled && !laserAttack.enabled && !angryShakeBehavior.enabled)
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

        ChangeArmorProtection(ProtectedArmorsOnAttack);

        int attack = UnityEngine.Random.Range(1, 3);
        
        if (flyingBehavior != null )
        {
            flyingBehavior.enabled = false;
        }

        switch (attack)
        {
            case 1:
                if ( bashAttack != null )
                {
                    bashAttack.enabled = true;
                    //laserAttack.enabled = true;
                }
                break;
            case 2:
                if ( laserAttack != null )
                {
                    laserAttack.enabled = true;
                }
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
        if (collision.name == "Hook")
        {
            if ( armorsGO.Length == 0 && lifePoints.Length > 0)
            {
                Destroy(lifePoints[0]);
            }
        }

        if ( lifePoints.Length == 0 ) 
        {
            DisablePulpo();
            if ( Explosion != null )
            {
                explosionPS = Instantiate(Explosion, transform.position, Quaternion.identity);
                explosionPS.Play();
                startExplosionGlow = true;
            }
        }
    }

    private IEnumerator ExplosionGlow()
    {
        startExplosionGlow = false;
        var sprite = GetComponent<SpriteRenderer>();
        Color currentColor = sprite.material.GetColor("_HDRColor");

        float elapsedTime = 0f;
        float timeForFullExplosion = 8f;
        yield return new WaitForSeconds(2);

        while ( elapsedTime < timeForFullExplosion )
        {
            elapsedTime += Time.deltaTime;
            currentColor = sprite.material.GetColor("_HDRColor");

            int luminosity = (int)Mathf.Lerp(0, 100, elapsedTime / timeForFullExplosion);
            //Color color = Color.LerpUnclamped(startColor, FullExplosionColor, elapsedTime/timeForFullExplosion);

            float factor = Mathf.Pow(2,luminosity);
            Color color = new Color(currentColor.r*factor,currentColor.g*factor,currentColor.b*factor);

            sprite.material.SetColor("_HDRColor", color);
            //elapsedTime += Time.deltaTime;
            yield return false;
        }
        GameManager.Instance.juegoTerminado = true;
        //explosionPS.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        yield return false;

        //Destroy(explosionPS.gameObject);
        //yield return false;
    }

    private void DisablePulpo()
    {
        //TODO Arreglar que al deshabilitar (con IsActive?) deje de hacer el comportamiento
        circuloCollider.enabled = false;
        //flyingBehavior.enabled = false;
        //bashAttack.enabled = false;
        //laserAttack.enabled = false;
        //missileAttackBehavior.enabled = false;
        //angryShakeBehavior.enabled = false;

        circuloCollider.enabled = false;
        Destroy(flyingBehavior);
        Destroy(bashAttack);
        Destroy(laserAttack);
        Destroy(missileAttackBehavior);
        Destroy(angryShakeBehavior);
    }

    internal void RemoveArmor(ArmorScript armorScript)
    {
        armors.Remove(armorScript);

        if (angryShakeBehavior != null )
        {
            angryShakeBehavior.enabled = true;
        }

        ChangeStance(PulpoStances.Aggressive);
        flyingBehavior.SetPositionToBottom();
    }

    public void ChangeStance(PulpoStances newStance)
    {
        //No hace nada si los ataques 
        if ( bashAttack == null && laserAttack == null && missileAttackBehavior == null) { return; } 

        // Hacer algo para esperar a que termine de hacer un ataque si es que lo esta haciendo antes de cambiar de estado
        if ( (bashAttack != null && bashAttack.enabled) || (laserAttack != null && laserAttack.enabled) ) { return; }

        stance = newStance;
        if (stance == PulpoStances.Aggressive)
        {
            ProtectedArmorsOnAttack = true;
            laserAttack.attackOrientation = Orientation.Horizontal;

            if ( missileAttackBehavior != null )
            {
                missileAttackBehavior.enabled = true;
            }
        } 
        else if (stance == PulpoStances.Vulnerable)
        {
            ProtectedArmorsOnAttack = false;
            laserAttack.attackOrientation = Orientation.Vertical;
            
            if ( missileAttackBehavior != null )
            {
                missileAttackBehavior.enabled = false;
            }
        }
    }

    public bool AttackInProgress()
    {
        return laserAttack.enabled || bashAttack.enabled;
    }
}
