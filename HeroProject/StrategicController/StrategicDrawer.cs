using UnityEngine;

namespace StrategicManagement
{
    [DisallowMultipleComponent]
    public class StrategicDrawer
    {
        private Texture2D _whiteTexture;

        public void Init()
        {
            CreateTexture();
        }

        public void DrawScreenRect(Rect rect, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(rect, _whiteTexture);
            GUI.color = Color.white;
        }

        public void DrawScreenRectBorder(Rect rect, float thickness, Color color)
        {
            //up
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
            //left
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
            //right
            DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
            //down
            DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
        }

        public Rect GetScreenRect(Vector2 startPos, Vector2 endPos)
        {
            startPos.y = Screen.height - startPos.y;
            endPos.y = Screen.height - endPos.y;

            var topLeft = Vector2.Min(startPos, endPos);
            var bottomRight = Vector2.Max(startPos, endPos);

            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }

        private void CreateTexture()
        {
            _whiteTexture = new Texture2D(1, 1);
            _whiteTexture.SetPixel(0, 0, Color.white);
            _whiteTexture.Apply();
        }
    }
}