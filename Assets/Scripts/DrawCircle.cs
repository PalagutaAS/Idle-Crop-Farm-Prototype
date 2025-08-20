namespace DefaultNamespace
{
    using UnityEngine;

    public class DrawCircle : MonoBehaviour
    {
        public int segments = 64;
        public float radius = 5f;
        public float lineWidth = 0.2f;
        public float Y = 0.1f;
    
        private LineRenderer lineRenderer;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.positionCount = segments + 1;
            lineRenderer.loop = true;
        
            CreateSquare(radius);
        }

        void CreateSquare(float size)
        {
            Vector3[] points = {
                new Vector3(-size, Y, -size),
                new Vector3(-size, Y, size),
                new Vector3(size, Y, size),
                new Vector3(size, Y, -size),
            };
            lineRenderer.positionCount = points.Length;
            lineRenderer.SetPositions(points);
        }
    }
}