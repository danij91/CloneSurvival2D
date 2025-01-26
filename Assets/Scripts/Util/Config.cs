using UnityEngine;

public static class Config
{
    public static class System
    {
        public static readonly float MOVE_SPEED = 8f;
        public static readonly float CORRECTION_VALUE = 0.5f;
        public static readonly float DEFAULT_CANVAS_WIDTH = 720f;
        public static readonly float DEFAULT_CANVAS_HEIGHT = 1280f;
        public static readonly Vector3 DIRECTION_RIGHT = new Vector3(1, 0, 1);
        public static readonly Vector3 DIRECTION_LEFT = new Vector3(-1, 0, 1);
    }
}