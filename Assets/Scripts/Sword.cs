using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] Transform holdPoint;

    [Header("Input System")]
    PlayerControlls playerControls;
    private InputAction attack;

    private Animator swing;

    private void Awake()
    {
        playerControls = new PlayerControlls();

        transform.position = holdPoint.position;

        swing = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        attack = playerControls.Player.Fire;
        attack.Enable();
        attack.performed += Fire;
    }

    private void OnDisable()
    {
        attack.Disable();
    }

    void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //swing.Play("Swing");
            swing.SetTrigger("Swing");
            //swing.ResetTrigger("Swing");
        }
    }
}
