﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlacementPosition
{
    Floor = 1,
    HighWall = 2,
    MidWall = 3,
    LowWall = 4,
    WallFloor = 5
}

public enum ModuleType
{
    Kitchen = 1,
    LivingRoom = 2
}

public class Assessable : MonoBehaviour {

    [Tooltip("Type of plane that the object has been classified as.")]
    public PlacementPosition placement = PlacementPosition.MidWall;

    public ModuleType module = ModuleType.Kitchen;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
