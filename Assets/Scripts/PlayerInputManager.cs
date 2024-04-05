using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour,IInputManager
{
    private Transform camT;
    public float sens = 0.1f;
    public float attackCooldown = 0.5f;
    private float _attackTime;
    [SerializeField] Vector2 mouseVector;
    public event Action<Vector2> OnMoveRecieved = delegate(Vector2 vector2){};
    public event Action OnJumpPressed = delegate{};
    public event Action OnHitPressed = delegate{};
    public event Action OnSwitchWeaponPressed = delegate{};
    public Vector2 Rotation{get;private set;}
    public Vector2 Movement{get;private set;}

    void Awake(){
        _attackTime = Time.time+attackCooldown;
        camT = transform.GetChild(0);
    }

    void OnMoveCamera(InputValue value){
        Rotation = value.Get<Vector2>()*sens;
        camT.Rotate(-value.Get<Vector2>().y*sens,0f,0f);
    }

    void OnMove(InputValue value){
        Movement = value.Get<Vector2>();
    }    
    void OnJump(){
        OnJumpPressed();
    }
    void OnHit(){
        if(Time.time < _attackTime) return;
        _attackTime = Time.time+attackCooldown;
        OnHitPressed();
    }
    void OnSwitchWeapon(){
        OnSwitchWeaponPressed();
    }

    void recieveDamage(){
        //Emit a noise?
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().DamagePlayer();
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "EnemyAttack"){
            recieveDamage();
        }
    }
}
