using System;
using UnityEngine;

namespace CubeMatch
{
    public static class StaticEvents
    {
        public static Action<Cube> onCubePicked;
        public static Action<CubeInfo> onCubeHinted;
    }
}