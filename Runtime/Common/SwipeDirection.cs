using System;

namespace Gilzoide.GestureRecognizers.Common
{
    [Flags]
    public enum SwipeDirection
    {
        None = 0,
        Left = 1 << 0,
        Right = 1 << 1,
        Up = 1 << 2,
        Down = 1 << 3,
        Everything = Left | Right | Up | Down,
    }
}
