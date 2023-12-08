# Gesture Recognizers
[![openupm](https://img.shields.io/npm/v/com.gilzoide.gesture-recognizers?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.gilzoide.gesture-recognizers/)

Touch/pointer gesture recognizer scripts based on [EventSystem handlers](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/SupportedEvents.html) or [Input](https://docs.unity3d.com/ScriptReference/Input.html).

Implemented gestures:
- Tap (configurable number of touches and taps)
- Long press (configurable number of touches, press duration)
- Pan (configurable number of touches)
- Pinch (configurable number of touches, at least 2)
- Twist (configurable number of touches, at least 2)
- Swipe (configurable number of touches, supported directions, minimum distance, minimum velocity)
- Edge pan (configurable number of touches, supported edges, maximum distance from edge)

Gesture recognizers are implemented as pure C# classes and can be used with your own touch input data.

Recognizers based on [EventSystem](Runtime/EventSystem) can be used in uGUI-based UIs, as well as physics objects if your Camera has `PhysicsRaycaster` or `Physics2DRaycaster` components.

Recognizers based on [Input](Runtime/Input) detect gestures anywhere in the screen or a configurable portion of it.


## How to install
This package is available on the [openupm registry](https://openupm.com/) and can be installed using the [openupm-cli](https://github.com/openupm/openupm-cli):
```
openupm add com.gilzoide.gesture-recognizers
```

Otherwise, you can install directly using the [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui-giturl.html) with the following URL:
```
https://github.com/gilzoide/unity-gesture-recognizers.git#1.0.0
```