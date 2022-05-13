using CodeBase;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointMarker : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(Waypoint waypoint, GizmoType gizmo)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(waypoint.transform.position, 0.5f);
    }
}
