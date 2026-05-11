using UnityEngine;

namespace PartyGDK.Base.Extensions
{
    public static class ColorExtensions
    {
        public static Color PreserveAlphaIfZero(this Color color, Color oldColor)
        {
            if (oldColor.a == 0)
                color.a = 0;

            return color;
        }
    }
}