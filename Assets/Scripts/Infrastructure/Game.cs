﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game instance;
    private static Game Instance
    {
        get {
            if (instance == null)
                instance = FindObjectOfType<Game>();

            if (instance == null)
                new GameObject("GAME").AddComponent<Game>();

            return instance;
        }
    }

    public static CameraHandler Camera {
        get {
            return CameraHandler.GetInstance(usePrefab: true);
        }
    }
    public static UIHandler UIHandler
    {
        get
        {
            return UIHandler.GetInstance(usePrefab: true);
        }
    }

    public static ResultSpawnHandler ResultSpawnHandler
    {
        get
        {
            return ResultSpawnHandler.GetInstance(usePrefab: true);
        }
    }

    public static Dock Dock
    {
        get
        {
            return Dock.GetInstance(usePrefab: true);
        }
    }

    public static UseableEntityDataHandler UseableEntityDataHandler {
        get {
            return UseableEntityDataHandler.GetInstance();
        }
    }

    public static Game GetInstance () {
        return Instance;
    }
}
