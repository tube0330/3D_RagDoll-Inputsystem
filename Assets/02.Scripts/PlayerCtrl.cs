# pragma warning disable IDE0051
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    new Transform transform;
    Animator ani;
    Vector3 moveDir;

    readonly int hashAttack = Animator.StringToHash("Attack");
    readonly int hashMovement = Animator.StringToHash("Movement");

    [Header("PlayerInput_C#Events")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] InputActionMap mainActionMap;
    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction attackAction;

    void Start()
    {
        ani = GetComponent<Animator>();
        transform = GetComponent<Transform>();

        #region C# Events
        playerInput = GetComponent<PlayerInput>();
        mainActionMap = playerInput.actions.FindActionMap("playerActions");
        moveAction = mainActionMap.FindAction("Move");
        attackAction = mainActionMap.FindAction("Attack");

        moveAction.performed += context =>
        {
            Vector2 dir = context.ReadValue<Vector2>();
            moveDir = new Vector3(dir.x, 0, dir.y);
            ani.SetFloat(hashMovement, dir.magnitude);
        };
        moveAction.canceled += context =>
        {
            moveDir = Vector3.zero;
            ani.SetFloat(hashMovement, 0.0f);
        };

        attackAction.performed += context =>
        {
            Debug.Log("Attack by C# Events");
            ani.SetTrigger(hashAttack);
        };
        #endregion
    }

    void Update()
    {
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);  //진행 방향으로 회전
            transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);   //이동
        }
    }

    #region SendMessage 방식
    void OnMove(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();
        Debug.Log($"Move = ({dir.x}, {dir.y})");

        /* transform.position += new Vector3(dir.x, 0, dir.y) * Time.deltaTime * 5.0f; */
        moveDir = new Vector3(dir.x, 0, dir.y);
        ani.SetFloat(hashMovement, dir.magnitude);
    }

    void OnAttack()
    {
        Debug.Log($"Attack");
        ani.SetTrigger(hashAttack);
    }
    #endregion

    #region Invoke UNITY_EVENTS
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        moveDir = new Vector3(dir.x, 0, dir.y);
        ani.SetFloat(hashMovement, dir.magnitude);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log($"context.phase = {context.phase}");

        if (context.performed)
        {
            Debug.Log($"Attack");
            ani.SetTrigger(hashAttack);
        }
    }
    #endregion
}
