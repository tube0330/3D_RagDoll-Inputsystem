using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaxCtrl : MonoBehaviour
{
    new Transform transform;
    Animator ani;
    Vector3 moveDir;

    void Start()
    {
        transform = GetComponent<Transform>();    
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if(moveDir != Vector3.zero)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 3.0f);
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();
        moveDir = new Vector3(dir.x, 0f, dir.y);
        //ani.SetFloat("Speed", dir.magnitude);
    }
}
