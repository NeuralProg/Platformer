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

public class Dash : EnemyAction
{
    public float dashDuration;
    public float dashForce;
    public Transform checkEdge;

    private bool isDashing;


    public override void OnStart()
    {
        isDashing = true;
        StartCoroutine(DashTimer());
    }

    public override void OnFixedUpdate()
    {
        rb.velocity = new Vector2(dashForce * (transform.localScale.x / 4) * Time.fixedDeltaTime, rb.velocity.y);
    }

    public override TaskStatus OnUpdate()
    {
        var goingToGround = Physics2D.OverlapCircle(checkEdge.position, 0.04f, GetComponent<CyborgEnemy>().whatIsGround);

        return !isDashing || !goingToGround ? TaskStatus.Success : TaskStatus.Running;
    }

    private IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }
}
