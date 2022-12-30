using System;

namespace Gilzoide.GestureRecognizers.Recognizers.Common
{
    [Flags]
    public enum RectEdge
    {
        None = 0,
        Left = 1 << 0,
        Right = 1 << 1,
        Top = 1 << 2,
        Bottom = 1 << 3,
        Everything = Left | Right | Top | Bottom,
    }
}
