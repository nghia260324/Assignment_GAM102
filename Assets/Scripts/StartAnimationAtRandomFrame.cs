using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimationAtRandomFrame : MonoBehaviour
{
    private Animator m_Animator;
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        AnimatorStateInfo stateInfo =  m_Animator.GetCurrentAnimatorStateInfo(0);
        m_Animator.Play(stateInfo.fullPathHash,0,Random.Range(0.2f, 1.5f));
    }
}
