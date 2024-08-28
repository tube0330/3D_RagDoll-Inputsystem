using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollCtrl : MonoBehaviour
{
    public Rigidbody[] rbodys;
    public Animator ani;


    void Start()
    {
        rbodys = GetComponentsInChildren<Rigidbody>();
        ani = GetComponent<Animator>();
        SetRagDoll(false);  //RagDoll을 안쓸거면 Rigidbody의 Kinematic이 true가 되어야함
    }

    void SetRagDoll(bool isEnable)
    {
        foreach (Rigidbody rbody in rbodys)
            rbody.isKinematic = !isEnable;
    }

    #region Collision Detection
    /* void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
            StartCoroutine(WakeUpRagDoll());
    }

    IEnumerator WakeUpRagDoll()
    {
        yield return new WaitForSeconds(0.2f);
        ani.enabled = false;    //Animator 비활성화 시키면 RagDoll 동작
        SetRagDoll(true);
    } */
    #endregion
    #region Trigger Detection
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ani.enabled = false;
            SetRagDoll(true);
        }
    }
    #endregion
}
