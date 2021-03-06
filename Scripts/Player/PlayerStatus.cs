﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public enum Season { none, spring, summer, fall, winter };
    private static Season inSeason = Season.none;

    public static bool canControl = true;


    //===Status===
    public static bool isCanMoveInput = true;
    public static bool isLanding = false;
    public static bool isSkilling = false;
    public static bool isIceSpecialSkilling = false;
    public static bool isWaterSpecialSkilling = false;
    public static bool isGasSpecialSkilling = false;
    public static bool isChanging = false;
    public static bool isHitRecover = false;
    public static bool isWallSticking = false;
    public static bool isInInteractTrigger = false;
    public static bool isWaterPassing = false;
    public static bool isSleeping = false;

    //===CanDoWhat===
    public static bool canMove = true;
    public static bool canFlip = true;
    public static bool canJump = true;
    public static bool canSkill = true;
    public static bool canChange = true;
    public static bool canBeHurt = true;

    //===InputMode===
    private static bool isJoystickConnected = false;
    private static bool keyboardInput = true;   //若否則為搖桿

    private void Start()
    {
        isJoystickConnected = Input.GetJoystickNames().Length > 0;

        StatusReset();
    }

    private void Update()
    {
        if (!canControl)
        {
            canMove = canJump = false;
            canFlip = false;
            canSkill = false;
            canChange = false;
            canBeHurt = false;
        }
        else
        {
            canMove = canJump = true;
            canFlip = true;
            canSkill = true;
            canChange = true;
            canBeHurt = true;

            if (!isCanMoveInput)
            {
                canMove = false;
            }
            if (isSkilling)
            {
                canMove = canJump = false;
                canChange = false;
                canFlip = false;
            }
            if (isIceSpecialSkilling || isWaterSpecialSkilling)
            {
                canMove = canJump = false;
                canChange = false;
            }
            if (isGasSpecialSkilling)
            {
                canMove = canJump = false;
                canChange = false;
                canFlip = false;
            }
            if (isChanging)
            {
                canMove = canJump = false;
                canFlip = false;
                canSkill = false;
                canChange = false;
            }
            if (isHitRecover)
            {
                canMove = canJump = false;
                canFlip = false;
                canSkill = false;
                canBeHurt = false;
                canChange = false;
            }
            if (isWallSticking)
            {
                canFlip = false;
                canSkill = false;
                canChange = false;
            }
            if (isInInteractTrigger)
            {
                canJump = false;
            }
            if (isWaterPassing)
            {
                canMove = canJump = false;
                canFlip = false;
                canSkill = false;
                canChange = false;
                canBeHurt = false;
            }
            if (isSleeping)
            {
                canMove = canJump = false;
                canFlip = false;
                canSkill = false;
                canChange = false;
                canBeHurt = false;
            }
        }
    }

    public static void StatusReset()
    {
        isLanding = false;
        isSkilling = false;
        isIceSpecialSkilling = isWaterSpecialSkilling = isGasSpecialSkilling = false;
        isChanging = false;
        isHitRecover = false;
        isWallSticking = false;
        isInInteractTrigger = false;
        isWaterPassing = false;
        isSleeping = false;
    }

    //所處季節
    static public void set_inSeason(Season season)
    {
        if (season != inSeason) { inSeason = season; }
    }
    static public Season get_inSeason() { return (inSeason); }

    #region inputMode

    //OnGUI自動Update，不須放入Update函式
    void OnGUI()
    {
        if (!isJoystickConnected) return;
        switch (keyboardInput)
        {
            case true:
                if (isJoystick()) keyboardInput = false;
                break;
            case false:
                if (isKeyboard()) keyboardInput = true;
                break;
        }
    }
    private bool isKeyboard()
    {
        // mouse & keyboard buttons
        if (Event.current.isKey ||
            Event.current.isMouse)
        {
            return true;
        }
        //// mouse movement
        //if (Input.GetAxis("Mouse X") != 0.0f ||
        //    Input.GetAxis("Mouse Y") != 0.0f)
        //{
        //    return true;
        //}
        return false;
    }
    private bool isJoystick()
    {
        // joystick buttons
        if (Input.GetKey(KeyCode.JoystickButton0) ||
           Input.GetKey(KeyCode.JoystickButton1) ||
           Input.GetKey(KeyCode.JoystickButton2) ||
           Input.GetKey(KeyCode.JoystickButton3) ||
           Input.GetKey(KeyCode.JoystickButton4) ||
           Input.GetKey(KeyCode.JoystickButton5) ||
           Input.GetKey(KeyCode.JoystickButton6) ||
           Input.GetKey(KeyCode.JoystickButton7) ||
           Input.GetKey(KeyCode.JoystickButton8) ||
           Input.GetKey(KeyCode.JoystickButton9) ||
           Input.GetKey(KeyCode.JoystickButton10))
        {
            return true;
        }
        // joystick axis
        if (Input.GetAxis("XBOX_Horizontal") != 0.0f ||
           Input.GetAxis("XBOX_Vertical") != 0.0f ||
           Input.GetAxis("XBOX_Change") != 0.0f)
        {
            return true;
        }
        return false;
    }
    static public bool Get_isKeyboard()
    {
        return keyboardInput;
    }

#endregion
}
