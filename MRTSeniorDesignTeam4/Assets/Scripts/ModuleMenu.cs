﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;


public class ModuleMenu : Singleton<ModuleMenu>{
    GameObject currentPanel;
    GameObject modulePanel;

    // Use this for initialization
    void Start () {

        currentPanel = this.transform.FindChild("RoomSelection").gameObject;
        modulePanel = this.transform.FindChild("InModule").gameObject;
        modulePanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RunKitchenScene()
    {
        if(SpaceUnderstanding.horizontal != null && SpaceUnderstanding.vertical != null)
        {
            // Load kitchen objects and swap menu
            RoomAssetManager.Instance.GenerateItemsInWorld(SpaceUnderstanding.horizontal, SpaceUnderstanding.vertical, ModuleType.Kitchen);
            MenuSwap();
        }
    }

    public void RunLivingRoonScene()
    {
        if (SpaceUnderstanding.horizontal != null && SpaceUnderstanding.vertical != null)
        {
            // Load living room objects and swap menu
            RoomAssetManager.Instance.GenerateItemsInWorld(SpaceUnderstanding.horizontal, SpaceUnderstanding.vertical, ModuleType.LivingRoom);
            MenuSwap();
        }
    }

    // Swap the current menu with the in module menu 
    void MenuSwap()
    {
        modulePanel.SetActive(true);
        modulePanel.transform.position = currentPanel.transform.position;
        currentPanel.GetComponent<MeshRenderer>().enabled = false;
    }
}
