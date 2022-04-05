using MovementWithHook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAnimationController : MonoBehaviour
{

    public GameObject Pivot;
    public Vector3 PivotPositionOnLand;
    public Vector3 PivotPositionOnAir;
    public Vector3 PivotPositionHooked;

    public List<SpriteRenderer> armSprites;
    public List<SpriteRenderer> hookSprites;
    public Collider2D hookCollider;

    private Vector3 currentPivotPosition;
    private Animator animator;
    private SpriteRenderer sprite;

    private GrapplingGun grapplingGun;
    private LineRenderer rope;

    private bool hookArmIsShown;
    private bool armIsShown;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        currentPivotPosition = Pivot.transform.localPosition;

        grapplingGun = Pivot.GetComponentInChildren<GrapplingGun>();
        rope = grapplingGun.GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var newPosition = currentPivotPosition;
        if ( sprite.flipX )
        {
            newPosition.x *= -1;
        }

        Pivot.transform.localPosition = newPosition;
    }

    public void SetLandPosition()
    {
        currentPivotPosition = PivotPositionOnLand;
    }

    public void SetAirPosition()
    {
        currentPivotPosition = PivotPositionOnAir;
    }

    public void SetHookedPosition()
    {
        currentPivotPosition = PivotPositionHooked;
    }

    public void ShowHookArm()
    {
        if ( !hookArmIsShown )
        {
            hookArmIsShown = true;
            armIsShown = true;
            hookSprites.ForEach(h => h.enabled = true);
            hookCollider.enabled = true;
        }
    }

    public void HideHookArm()
    {
        if ( hookArmIsShown )
        {
            hookArmIsShown = false;
            armSprites.ForEach(h => h.enabled = false);
            hookSprites.ForEach(h => h.enabled = false);
            hookCollider.enabled = false;
        }
    }

    public void HideArm()
    {
        if ( armIsShown )
        {
            armIsShown = false;
            armSprites.ForEach(h => h.enabled = false);
            hookCollider.enabled = false;
        }
    }
}
