using CodeBase.Locations;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(WaypointMarker))]
    public class WaypointMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(WaypointMarker waypointMarker, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(waypointMarker.transform.position, 0.5f);
        }
    }
}
