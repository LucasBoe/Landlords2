using System.Collections;
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

    public static CameraHandler CameraHandler {
        get {
            return CameraHandler.GetInstance();
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
