using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TiltMove : ContinuousMoveProviderBase
{
    private Vector2 _movementInput;
    private int _inputX;
    private int _inputY;

    public int inputX
    {
        set => _inputX = value;
    }

    public int inputY
    {
        set => _inputY = value;
    }

    protected override Vector2 ReadInput()
    {
        _movementInput = new Vector2(_inputX * 5, _inputY * 5);
        return _movementInput;
    }
}
