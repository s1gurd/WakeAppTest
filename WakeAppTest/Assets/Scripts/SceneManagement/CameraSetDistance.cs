using System;
using DG.Tweening;
using Scripts.Hybrid;
using UnityEngine;

namespace Scripts.SceneManagement
{
    public class CameraSetDistance : MonoBehaviour
    {
        private Camera cam;
        private bool changing;

        private Vector3 camPos;
        private bool ready = false;

        public void SetDistance()
        {
            if (cam == null)
            {
                cam = gameObject.GetComponent<Camera>();
            }

            var levelBounds = Bootstrap.Level.levelBounds;

            var topLeft = cam.WorldToScreenPoint(levelBounds.TopLeft);
            var topRight = cam.WorldToScreenPoint(levelBounds.TopRight);
            var bottomLeft = cam.WorldToScreenPoint(levelBounds.BottomLeft);
            var bottomRight = cam.WorldToScreenPoint(levelBounds.BottomRight);
            var sceneScreenWidth = bottomRight.x - topLeft.x;
            var sceneScreenHeight = bottomLeft.y - topRight.y;
            var margin = Bootstrap.Level.CameraSettings.LevelMargin;
            var horizontalRatio = (sceneScreenWidth + margin) / Screen.width;
            var verticalRatio = (sceneScreenHeight + margin) / Screen.height;
            var liftRatio = Mathf.Max(horizontalRatio, verticalRatio);

            var levelCenter = new Vector2((topRight.x + topLeft.x) / 2, (topLeft.y + bottomRight.y) / 2);
            var perspRatio = (levelBounds.BottomRight.x - levelBounds.TopLeft.x) / (bottomRight.x - topLeft.x);
            var planeShift = new Vector2(Screen.width / 2f - levelCenter.x,
                                 levelCenter.y - Screen.height / 2f) * perspRatio;

            camPos = cam.transform.position;
            camPos.x += planeShift.x;
            camPos.z += planeShift.y;
            camPos.y *= liftRatio;
            ready = true;
        }

        public void MoveCamera()
        {
            if (!ready) return;
            var t = Bootstrap.Level.CameraSettings.MoveTime;
            cam.transform.DOMove(camPos,t);
        }
    }
}