using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    private PlayerInputAction playerControl;
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance!= null &&_instance!= this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        playerControl = new PlayerInputAction();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControl.Player.Movement.ReadValue<Vector2>();
    }
    public Vector2 GetMousePos()
    {

        return playerControl.Player.MousePos.ReadValue<Vector2>();
    }
    public bool GetShottingAction()
    {
        return playerControl.Player.Shoot.WasPressedThisFrame();
    }
    public bool GetReloadingAction()
    {
        return playerControl.Player.Reload.WasPressedThisFrame();
    }


}
