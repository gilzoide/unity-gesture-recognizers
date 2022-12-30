# Gesture Recognizers
Touch/pointer gesture recognizer scripts based on [EventSystem handlers](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/SupportedEvents.html) or [Input](https://docs.unity3d.com/ScriptReference/Input.html).

Implemented gestures:
- Tap (configurable number of touches and taps)
- Long press (configurable number of touches, press duration)
- Pan (configurable number of touches)
- Pinch (configurable number of touches, at least 2)
- Twist (configurable number of touches, at least 2)
- Swipe (configurable number of touches, supported directions, minimum distance, minimum velocity)
- Edge pan (configurable number of touches, supported edges, maximum distance from edge)

Gesture recognizers are implemented as pure C# classes in the [Recognizers](Runtime/Recognizers) folder and can be used with your own touch input data.

Recognizers based on [EventSystem](Runtime/EventSystems) can be used in uGUI-based UIs, as well as physics objects if your Camera has `PhysicsRaycaster` or `Physics2DRaycaster` components.

Recognizers based on [Input](Runtime/Input) detect gestures anywhere in the screen or a configurable portion of it.


## How to install
Install via [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui-giturl.html) using the following URL:
```
https://github.com/gilzoide/unity-gesture-recognizers
```