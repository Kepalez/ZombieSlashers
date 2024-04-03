using System;
using UnityEngine;

public class CharacterMover : MonoBehaviour{
    Rigidbody rb;
    Animator animator;
    public float movementSpeed = 50f;
    [SerializeField] Vector2 m_direction;
    [SerializeField] Vector2 currMovement;
    [SerializeField]float _timeElapsed = 0f;
    [SerializeField]float _animInterpolationDuration = 1f;
    [SerializeField]float _jumpForce = 300f;

    IInputManager inputManager;
    private bool isGrounded{
        get{
            return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
        }
    }
    void Awake(){
        inputManager = GetComponent<IInputManager>();
        inputManager.OnJumpPressed += OnJumpPressed;
        inputManager.OnHitPressed += OnHitPressed;
        inputManager.OnSwitchWeaponPressed += OnSwitchWeaponPressed;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void OnDisable(){
        inputManager.OnJumpPressed -= OnJumpPressed;
        inputManager.OnHitPressed -= OnHitPressed;
        inputManager.OnSwitchWeaponPressed -= OnSwitchWeaponPressed;
    }

    private void OnSwitchWeaponPressed()
    {
        animator.SetBool("Armed",!animator.GetBool("Armed"));
    }

    private void OnHitPressed()
    {
        if(isGrounded){
            animator.SetTrigger("Hit");
        }
    }

    private void OnJumpPressed()
    {
        if(isGrounded){
            rb.AddForce(Vector2.up*_jumpForce);
        }
    }

    void Start()
    {
        m_direction = currMovement = inputManager.Movement;
    }

    void Update()
        {
            HandleRotation();
            HandleHorizontalMovement();
            HandleVerticalMovement();
        }

    private void HandleVerticalMovement(){
        animator.SetBool("Grounded",isGrounded);
    }

    private void HandleRotation()
    {
        transform.Rotate(0f,inputManager.Rotation.x,0f);
    }

    private void HandleHorizontalMovement()
    {
        if(currMovement != inputManager.Movement) _timeElapsed = 0f;
            currMovement = inputManager.Movement;
            if(_timeElapsed < _animInterpolationDuration){
                m_direction.x = Mathf.Lerp(m_direction.x,inputManager.Movement.x,_timeElapsed/_animInterpolationDuration);
                m_direction.y = Mathf.Lerp(m_direction.y,inputManager.Movement.y,_timeElapsed/_animInterpolationDuration);
                _timeElapsed += Time.deltaTime;
            }else{
                m_direction = inputManager.Movement;
            }
            rb.velocity = new Vector3(0f,rb.velocity.y,0f)+movementSpeed*((transform.forward*m_direction.y) + (transform.right*m_direction.x));
            //print(rb.velocity);
            animator.SetFloat("X",m_direction.x);
            animator.SetFloat("Y",m_direction.y);
            animator.SetFloat("Speed",MathF.Max(m_direction.x,m_direction.y));    
        }
}