using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(Waypoint))]
    public class WaypointMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(Waypoint waypoint, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(waypoint.transform.position, 0.5f);
        }
    }
}
