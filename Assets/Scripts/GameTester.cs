using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTester : MonoBehaviour
{
    [SerializeField] List<UseableEntityData> spawnOnStart;

    [Button]
    private void Start()
    {
        StartCoroutine(TestRoutine());
    }

    IEnumerator TestRoutine()
    {
        yield return new WaitForEndOfFrame();
        Game.ResultSpawnHandler.Spawn(UseableEntity.CreateArrayFromDataArray(spawnOnStart), transform);
    }
}
