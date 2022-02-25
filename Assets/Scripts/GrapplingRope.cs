using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementWithHook
{
    public class GrapplingRope : MonoBehaviour
    {
        [Header("General Refernces:")]
        public GrapplingGun grapplingGun;
        public LineRenderer m_lineRenderer;

        [Header("General Settings:")]
        [SerializeField] private int precision = 40;
        [Range(0, 20)] [SerializeField] private float straightenLineSpeed = 5;

        [Header("Rope Animation Settings:")]
        public AnimationCurve ropeAnimationCurve;
        [Range(0.01f, 4)] [SerializeField] private float StartWaveSize = 2;
        float waveSize = 0;

        [Header("Rope Progression:")]
        public AnimationCurve ropeProgressionCurve;
        [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 1;
        float ropeSpeed;

        float moveTime = 0;

        //[HideInInspector]
        public bool isGrappling = true;
        public bool grappledToMovingPlatform = false;

        public bool straightLine = true;

        public bool isAttack = false;
        public bool finishedAttacking = false;

        //private float? firstPointXPosition = 0f;
        private float? previousGrapplePointXPosition = 0f;

        private void OnEnable()
        {
            moveTime = 0;
            m_lineRenderer.positionCount = precision;
            waveSize = isAttack ? StartWaveSize / 2 : StartWaveSize;
            ropeSpeed = isAttack ? ropeProgressionSpeed * 2 : ropeProgressionSpeed;
            straightLine = false;

            LinePointsToFirePoint();

            m_lineRenderer.enabled = true;
        }

        private void OnDisable()
        {
            grapplingGun.transform.parent.parent.SetParent(null);
            grappledToMovingPlatform = false;
            m_lineRenderer.enabled = false;
            isGrappling = false;
            isAttack = false;
            finishedAttacking = false;
            //firstPointXPosition = null;
        }

        private void LinePointsToFirePoint()
        {
            for ( int i = 0; i < precision; i++ )
            {
                m_lineRenderer.SetPosition(i, grapplingGun.firePoint.position);
            }
        }

        private void Update()
        {
            moveTime += Time.deltaTime;
            DrawRope();
        }

        void DrawRope()
        {
            if ( !straightLine )
            {
                var lastRopeXPoint = m_lineRenderer.GetPosition(precision - 1).x;
                var grapplePointXPosition = grapplingGun.grapplePoint.transform.position.x;

                if ( lastRopeXPoint == grapplePointXPosition )
                {
                    grappledToMovingPlatform = false;
                    straightLine = true;
                }
                else if ( lastRopeXPoint == previousGrapplePointXPosition )
                {
                    grappledToMovingPlatform = true;
                    straightLine = true;
                }
                else
                {
                    previousGrapplePointXPosition = grapplePointXPosition;
                    DrawRopeWaves();
                }
            }
            else
            {
                if ( !isGrappling && !isAttack )
                {
                    grapplingGun.Grapple();
                    isGrappling = true;

                    if ( grappledToMovingPlatform && grapplingGun.grapplePoint != null )
                    {
                        try { 
                            grapplingGun.grapplePoint.transform.GetComponentInParent<BatMovement>().CanMove = false;
                        }
                        catch
                        {
                            Debug.DebugBreak();
                        }
                    }

                }
                if ( waveSize > 0 )
                {
                    waveSize -= Time.deltaTime * straightenLineSpeed;
                    DrawRopeWaves();
                }
                else
                {
                    waveSize = 0;
                    if ( m_lineRenderer.positionCount != 2 ) { m_lineRenderer.positionCount = 2; }
                    DrawRopeNoWaves();
                    if (isAttack)
                    {
                        finishedAttacking = true;
                    }
                }

                //if (grappledToMovingPlatform && grapplingGun.transform.parent.parent.parent == null )
                //{
                //    rapplingGun.transform.parent.parent.SetParent(grapplingGun.grapplePoint.transform.parent);
                //}
            }
        }

        void DrawRopeWaves()
        {
            for ( int i = 0; i < precision; i++ )
            {
                float delta = (float)i / ((float)precision - 1f);
                Vector2 offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
                Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint.transform.position, delta) + offset;
                Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeSpeed);

                m_lineRenderer.SetPosition(i, currentPosition);
            }
        }

        void DrawRopeNoWaves()
        {
            m_lineRenderer.SetPosition(0, grapplingGun.firePoint.position);
            m_lineRenderer.SetPosition(1, grapplingGun.grapplePoint.transform.position);
        }
    }
}
