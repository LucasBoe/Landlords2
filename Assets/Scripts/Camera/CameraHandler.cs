using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : Singleton<CameraHandler>
{
    float dockInWorldDistance = 5;

    public DockLocation GetDockLocationInWorld()
    {
        Camera child = GetComponentInChildren<Camera>();

        Vector3 leftTop = child.ScreenPointToRay(new Vector2(0, Screen.height * 0.05f)).GetPoint(dockInWorldDistance);
        Vector3 leftBot = child.ScreenPointToRay(new Vector2(0, 0)).GetPoint(dockInWorldDistance);
        Vector3 RightTop = child.ScreenPointToRay(new Vector2(Screen.width, Screen.height * 0.05f)).GetPoint(dockInWorldDistance);
        Vector3 RightBot = child.ScreenPointToRay(new Vector2(Screen.width, 0)).GetPoint(dockInWorldDistance);

        return new DockLocation(leftTop, leftBot, RightTop, RightBot);
    }

    private void OnDrawGizmos()
    {
        DockLocation inWorldDock = GetDockLocationInWorld();
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(inWorldDock.LeftTop, inWorldDock.LeftBot);
        Gizmos.DrawLine(inWorldDock.LeftBot, inWorldDock.RightBot);
        Gizmos.DrawLine(inWorldDock.RightBot, inWorldDock.RightTop);
        Gizmos.DrawLine(inWorldDock.RightTop, inWorldDock.LeftTop);
    }
}

public struct DockLocation
{
    public Vector3 LeftTop, LeftBot, RightTop, RightBot;

    public DockLocation(Vector3 lt, Vector3 lb, Vector3 rt,  Vector3 rb)
    {
        LeftTop = lt;
        LeftBot = lb;
        RightTop = rt;
        RightBot = rb;
    }
}
