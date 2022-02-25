using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementWithHook
{
    public class GrapplingGun : MonoBehaviour
    {
        [Header("Scripts Ref:")]
        public GrapplingRope grappleRope;

        [Header("Layers Settings:")]
        [SerializeField] private bool grappleToAll = false;
        [SerializeField] private int grappableLayerNumber = 9;

        [Header("Main Camera:")]
        public Camera m_camera;

        [Header("Transform Ref:")]
        public Transform gunHolder;
        public Transform gunPivot;
        public Transform firePoint;

        [Header("Physics Ref:")]
        public SpringJoint2D m_springJoint2D;
        public Rigidbody2D m_rigidbody;
        public Movement movement;

        [Header("Rotation:")]
        [SerializeField] private bool rotateOverTime = true;
        [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

        [Header("Distance:")]
        [SerializeField] private bool hasMaxDistance = false;
        [SerializeField] private float maxDistance = 20;

        private enum LaunchType
        {
            Transform_Launch,
            Physics_Launch
        }

        [Header("Launching:")]
        [SerializeField] private bool launchToPoint = true;
        [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
        [SerializeField] private float launchSpeed = 1;

        [Header("No Launch To Point")]
        [SerializeField] private bool autoConfigureDistance = false;
        [SerializeField] private float targetDistance = 3;
        [SerializeField] private float targetFrequncy = 1;

        [HideInInspector] public GameObject grapplePoint;
        [HideInInspector] public Vector2 grappleDistanceVector;

        public bool shotGrappleGun = false;

        public float attackDelay = 0.2f;
        public float attackDelayCounter = 0.0f;

        private void Start()
        {
            grappleRope.enabled = false;
            m_springJoint2D.enabled = false;
        }

        private void Update()
        {
            if ( Input.GetKeyUp(KeyCode.Mouse0) && !shotGrappleGun && attackDelayCounter == 0 && (!grappleRope.isGrappling) )
            {
                shotGrappleGun = true;
                movement.CanMove = false;

                SetGrapplePoint();
            }
            else if ( (shotGrappleGun && !(Input.GetKeyUp(KeyCode.Mouse0) || Input.GetButtonDown("Jump")) ) 
                   || (grappleRope.isAttack && grappleRope.finishedAttacking))
            {
                if ( grappleRope.enabled )
                {
                    RotateGun(grapplePoint.transform.position, false);
                }
                else
                {
                    Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                    RotateGun(mousePos, true);
                }

                if ( launchToPoint && grappleRope.isGrappling )
                {
                    if ( launchType == LaunchType.Transform_Launch )
                    {
                        Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                        Vector2 targetPos = (Vector2) grapplePoint.transform.position - firePointDistnace;
                        gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                        gunHolder.position *= Vector2.left;
                    }
                }

                if ( grappleRope.isAttack && attackDelayCounter >= attackDelay )
                {
                    movement.CanMove = true;

                    grappleRope.enabled = false;
                    grappleRope.isAttack = false;
                    grappleRope.finishedAttacking = false;
                    attackDelayCounter = 0;

                    m_springJoint2D.enabled = false;
                    m_rigidbody.gravityScale = 3;
                    shotGrappleGun = false;
                }
            }
            else if ( grappleRope.isGrappling && ( Input.GetKeyUp(KeyCode.Mouse0) || Input.GetButtonDown("Jump")) )
            {
                movement.CanMove = true;
                
                grappleRope.isAttack = false;
                grappleRope.enabled = false;
                attackDelayCounter = 0;
                
                m_springJoint2D.enabled = false;
                m_rigidbody.gravityScale = 3;

                var movingPlatform = grapplePoint.GetComponentInParent<BatMovement>();
                if ( movingPlatform != null )
                {
                    movingPlatform.CanMove = true;
                }

                Destroy(grapplePoint);
                shotGrappleGun = false;
            }
            else
            {
                shotGrappleGun = false;
                movement.CanMove = true;
                grappleRope.isAttack = false;
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }

            if ( grappleRope.isAttack )
            {
                attackDelayCounter += Time.deltaTime;
            }

        }

        void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
        {
            Vector3 distanceVector = lookPoint - gunPivot.position;

            float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
            if ( rotateOverTime && allowRotationOverTime )
            {
                gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
            }
            else
            {
                gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        void SetGrapplePoint()
        {

            Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
            
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if ( _hit )
            {
                if ( _hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll )
                {
                    if ( Vector2.Distance(_hit.point, firePoint.position) <= maxDistance + .25 || !hasMaxDistance )
                    {
                        if ( grapplePoint != null ) Destroy(grapplePoint);

                        grapplePoint = new GameObject("GrapplePoint");
                        grapplePoint.transform.position = _hit.point;
                        grapplePoint.transform.SetParent(_hit.transform);

                        grappleDistanceVector = (Vector2)grapplePoint.transform.position - (Vector2)gunPivot.position;
                        grappleRope.enabled = true;
                    }
                }
            }

            if (!grappleRope.enabled) 
            {
                if ( grapplePoint != null ) Destroy(grapplePoint);

                var targetPosition = (Vector2)m_camera.ScreenToWorldPoint(Input.mousePosition);
                var position = (Vector2)firePoint.position + (targetPosition - (Vector2)firePoint.position).normalized * maxDistance;

                grapplePoint = new GameObject("GrapplePoint");
                grapplePoint.transform.position = position;
                grappleDistanceVector = (Vector2)grapplePoint.transform.position - (Vector2)gunPivot.position;
                grappleRope.isAttack = true;
                grappleRope.enabled = true;
            }
        }

        public void Grapple()
        {
            m_springJoint2D.autoConfigureDistance = false;
            if ( !launchToPoint && !autoConfigureDistance )
            {
                m_springJoint2D.distance = targetDistance;
                m_springJoint2D.frequency = targetFrequncy;
            }
            if ( !launchToPoint )
            {
                if ( autoConfigureDistance || ( grapplePoint.transform.parent?.name ?? "") == "armor" )
                {
                    m_springJoint2D.autoConfigureDistance = true;
                    m_springJoint2D.frequency = 0;
                }

                m_springJoint2D.connectedAnchor = grapplePoint.transform.position;
                m_springJoint2D.enabled = true;
            }
            else
            {
                switch ( launchType )
                {
                    case LaunchType.Physics_Launch:
                        m_springJoint2D.connectedAnchor = grapplePoint.transform.position;

                        Vector2 distanceVector = firePoint.position - gunHolder.position;

                        Debug.Log(distanceVector);
                        m_springJoint2D.distance = distanceVector.magnitude;
                        m_springJoint2D.frequency = launchSpeed;
                        m_springJoint2D.enabled = true;
                        break;
                    case LaunchType.Transform_Launch:
                        m_rigidbody.gravityScale = 0;
                        m_rigidbody.velocity = Vector2.zero;
                        break;
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            if ( firePoint != null && hasMaxDistance )
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(firePoint.position, maxDistance);
            }
        }
    }
}
