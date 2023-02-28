using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Core.AI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Skill : EnemyAction
{
    public GameObject attackCollision;
    public Vector2 skillSize;
    public Vector2 skillOffset;
    private Vector2 baseSize;
    private Vector2 baseOffset;
    private bool isSkill = true;

    public override void OnStart()
    {
        baseSize = attackCollision.GetComponent<HitDetection>().colliderSize;
        baseOffset = attackCollision.GetComponent<BoxCollider2D>().offset;
        anim.SetTrigger("Skill");
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.4f);
        isSkill = true;
        attackCollision.GetComponent<HitDetection>().colliderSize = skillSize;
        attackCollision.GetComponent<BoxCollider2D>().size = skillSize;
        attackCollision.GetComponent<BoxCollider2D>().offset = skillOffset;
        StartCoroutine(AttackTimer());
    }

    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(0.3f);
        attackCollision.GetComponent<HitDetection>().colliderSize = baseSize;
        attackCollision.GetComponent<BoxCollider2D>().size = baseSize;
        attackCollision.GetComponent<BoxCollider2D>().offset = baseOffset;
        isSkill = false;
    }

    public override TaskStatus OnUpdate()
    {
        if (isSkill)
        {
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
}
