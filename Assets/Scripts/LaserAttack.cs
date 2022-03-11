using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    public float chargeTime;
    public float positioningSpeed;

    public Orientation attackOrientation;
    public Direction horizontalAttackDirection;

    public float horizontalDistanceFromPlayer;
    public float VerticalDistanceFromPlayer;

    public GameObject horizontalLaser;
    public GameObject verticalLaser;

    public GameObject player;
    private Animator animator;
    private bool isShooting;
    private bool charginStarted;
    private bool doneLasering;

    private GlowingBehavior glowingBehavior;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        glowingBehavior = GetComponent<GlowingBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( !isShooting ) 
        { 
            var position = transform.position;
            Vector2 target = Vector2.zero;
            Quaternion rotation = Quaternion.identity;
            
            switch ( attackOrientation )
            {
                case Orientation.Vertical:
                    target = new Vector2(player.transform.position.x, player.transform.position.y + VerticalDistanceFromPlayer);
                    break;
                case Orientation.Horizontal:
                    if ( player.transform.position.x < transform.position.x )
                    {
                        target = new Vector2(player.transform.position.x + horizontalDistanceFromPlayer, player.transform.position.y);
                        rotation = Quaternion.Euler(0, 0, -90);
                        horizontalAttackDirection = Direction.ToLeft;
                    }
                    else
                    {
                        target = new Vector2(player.transform.position.x - horizontalDistanceFromPlayer, player.transform.position.y);
                        rotation = Quaternion.Euler(0, 0, 90);
                        horizontalAttackDirection = Direction.ToRight;
                    }
                    break;
            }

            transform.position = Vector2.Lerp(position, target, Time.deltaTime * positioningSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * positioningSpeed);
        }

        if (!charginStarted)
        {
            StartCoroutine(ShootLaser());
        }

        if (doneLasering)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * positioningSpeed);

            if ( transform.rotation.eulerAngles.z <= 1 )
            {
                isShooting = false;
                doneLasering = false;
                charginStarted = false;
                enabled = false;
            }
        }
    }

    IEnumerator ShootLaser()
    {
        charginStarted = true;
        animator.SetBool("IsChargingLaser", true);
        glowingBehavior.DoGlow = true;

        yield return new WaitForSeconds(chargeTime);

        isShooting = true;

        GameObject laser;
        if (attackOrientation == Orientation.Horizontal )
        {
            laser = horizontalLaser;
        } 
        else
        {
            laser = verticalLaser;
        }

        var laserInstance = GameObject.Instantiate(laser, transform.position, Quaternion.identity);
        laserInstance.GetComponent<LaserScript>().laserAttack = this;
        
        //Voltea el laser cuando se dispara desde la izquierda hacia la derecha
        if ( attackOrientation == Orientation.Horizontal && horizontalAttackDirection == Direction.ToRight )
        {
            laserInstance.transform.localScale = new Vector3(-1, 1, 1); ;
        }

        animator.SetBool("IsAttacking", true);
    }

    public void DoneLasering()
    {
        glowingBehavior.DoDeGlow = true;
        animator.SetBool("IsAttacking", false);
        doneLasering = true;
    }
}
