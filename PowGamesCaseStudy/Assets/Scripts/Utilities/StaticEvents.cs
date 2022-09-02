using System;
using UnityEngine;

namespace CubeMatch
{
    public static class StaticEvents
    {
        public static Action levelFailed;

        public static Action<Cube> onCubePicked;
    }
}