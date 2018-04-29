using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InteractionMode
{
    Bomb,
    Weapon,
    Build
}

public class ModeChange : MonoBehaviour
{
    public BuildMode BM;
    public WeaponController WPC;
    public BombThrow BT;
    public Text CurrentModeDisplay;

    // Use this for initialization
    private void Start()
    {
        switchMode(InteractionMode.Weapon);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            switchMode(InteractionMode.Build);
            BM.InitBuilding(buildMode.buildModeGround);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            switchMode(InteractionMode.Build);
            BM.InitBuilding(buildMode.buildModeWall);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            switchMode(InteractionMode.Build);
            BM.InitBuilding(buildMode.buildModeRamp);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            BM.InitBuilding(buildMode.buildModeOff);
            switchMode(InteractionMode.Weapon);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            BM.InitBuilding(buildMode.buildModeOff);
            switchMode(InteractionMode.Bomb);
        }
    }

    private void switchMode(InteractionMode mode)
    {
        switch (mode)
        {
            case InteractionMode.Bomb:
                BM.gameObject.SetActive(false);
                WPC.gameObject.SetActive(false);
                BT.gameObject.SetActive(true);
                CurrentModeDisplay.text = "Bomb Mode";
                break;

            case InteractionMode.Weapon:
                BM.gameObject.SetActive(false);
                WPC.gameObject.SetActive(true);
                BT.gameObject.SetActive(false);
                CurrentModeDisplay.text = "Weapon Mode";
                break;

            case InteractionMode.Build:
                BM.gameObject.SetActive(true);
                WPC.gameObject.SetActive(false);
                BT.gameObject.SetActive(false);
                CurrentModeDisplay.text = "Build Mode";
                break;

            default:
                Debug.Log("This Should not happen #ModeChangeSwichtCase");
                break;
        }
    }
}