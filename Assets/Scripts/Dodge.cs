using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dodge : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform playerCam;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dodging")]
    [SerializeField] float dodgeForce;
    [SerializeField] float dodgeUpwardForce;
    [SerializeField] float dodgeDuration;

    [Header("Settings")]
    public bool allowAllDirections = true;


    [Header("Cooldown")]
    [SerializeField] float dodgeCD;
    private float dodgeCDTimer;

    [Header("Input")]
    PlayerControlls playerControls;
    private InputAction dodge;

    private void Awake()
    {
        playerControls = new PlayerControlls();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        dodge = playerControls.Player.Dodge;
        dodge.Enable();
        dodge.performed += IADodge;
    }

    private void OnDisable()
    {
        dodge.Disable();
    }

    private void Update()
    {
        if (dodgeCDTimer > 0)
            dodgeCDTimer -= Time.deltaTime;
    }

    void IADodge(InputAction.CallbackContext context)
    {
        if (context.performed && pm.state != PlayerMovement.MovementState.air)
        {
            PlayerDodge();
        }
    }

    void PlayerDodge()
    {
        if (dodgeCDTimer > 0) return;
        else dodgeCDTimer = dodgeCD;

        pm.dodging = true;

        Vector3 dir = GetDirection(orientation);

        Vector3 forceToApply = dir * dodgeForce + orientation.up * dodgeUpwardForce;

        delayedForceToApply = forceToApply;

        Invoke(nameof(DelayedDodgeForce), 0.025f);

        Invoke(nameof(ResetPlayerDodge), dodgeDuration);
    }

    private Vector3 delayedForceToApply;

    void DelayedDodgeForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    void ResetPlayerDodge()
    {
        pm.dodging = false;
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;

        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }
}
