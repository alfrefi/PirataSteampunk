using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    public float chargeTime;
    public float positioningSpeed;

    public Orientation attackOrientation;

    public float horizontalDistanceFromPlayer;
    public float VerticalDistanceFromPlayer;

    public GameObject horizontalLaser;
    public GameObject verticalLaser;

    public GameObject player;
    private bool isShooting;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
                    target = new Vector2(player.transform.position.x + horizontalDistanceFromPlayer, player.transform.position.y);
                    rotation = Quaternion.Euler(0, 0, -90);
                    break;
            }

            transform.position = Vector2.Lerp(position, target, Time.deltaTime * positioningSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * positioningSpeed);
        }

        StartCoroutine(ShootLaser());
    }

    IEnumerator ShootLaser()
    {
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
    }

    public void DoneLasering()
    {
        isShooting = false;
        enabled = false;
    }
}
