using System;
using UnityEngine;

public interface IInputManager
{ 
    Vector2 Movement {get;}
    Vector2 Rotation {get;}
    event Action<Vector2> OnMoveRecieved;
    event Action OnJumpPressed;
    event Action OnHitPressed;
    event Action OnSwitchWeaponPressed;
}
