using System;
using UnityEngine;

namespace AsteroidsCode.Core
{
    public static class SpaceMetrics
    {
        private static float aspect = (float)Screen.width / Screen.height;
        public static float worldHeight = Camera.main.orthographicSize;
        public static float worldWidth = worldHeight * aspect;

        public static event Action<int> OnScoreChange;

        public static int Score 
        {
            get
            {
                return score;
            }
            set
            {
                score = value;

                OnScoreChange?.Invoke(value);
            }
        }

        private static int score;
    }
}
