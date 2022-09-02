using System;
using UnityEngine;

namespace CubeMatch
{
    public static class StaticEvents
    {
        public static Action onLevelFailed;

        public static Action<Cube> onCubePicked;
    }
}