using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSpawnHandler : Singleton<ResultSpawnHandler>
{
    [SerializeField] UIUseableEntityList useableEntityListPrefab;
    [SerializeField] AnimationCurve lerpToDockCurve;
    [SerializeField] Vector3 SpawnOffsetFromOrigin;

    internal void Spawn(UseableEntity[] useableEntities, Transform origin)
    {
        StartCoroutine(MoveResultsRoutine(useableEntities, origin));
    }

    IEnumerator MoveResultsRoutine (UseableEntity[] useableEntities, Transform origin)
    {
        UIUseableEntityList listUI = Instantiate(useableEntityListPrefab, Game.UIHandler.InWorldCanvas.transform);
        listUI.Init(new SubscribableList<UseableEntity>(useableEntities));

        float duration = lerpToDockCurve.keys[lerpToDockCurve.length - 1].time;
        float dockOffsetLerpAmount = Game.Dock.GetFilledFloat();
        float t = 0;

        while ((t+= Time.deltaTime) < duration)
        {
            DockLocation dockL = Game.Camera.GetDockLocationInWorld();
            listUI.transform.position = Vector3.Lerp(origin.position + SpawnOffsetFromOrigin, Vector3.Lerp(dockL.LeftBot, dockL.RightBot, dockOffsetLerpAmount), lerpToDockCurve.Evaluate(t));
            yield return null;
        }

        Game.Dock.Add(useableEntities);
        Destroy(listUI.gameObject);
    }
}
