using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Core.AI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Dash : EnemyAction
{
    public float dashDuration = 0.4f;
    public float dashForce = 10;

    private bool isDashing = true;
    private float dashVelocity;

    public override void OnStart()
    {
        StartCoroutine(DashTimer());
    }

    private IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    public override void OnFixedUpdate()
    {
        rb.velocity = new Vector2((transform.localScale.x / 4) * dashVelocity, rb.velocity.y);
    }

    public override TaskStatus OnUpdate()
    {
        return isDashing ? TaskStatus.Running : TaskStatus.Success;
    }
}
