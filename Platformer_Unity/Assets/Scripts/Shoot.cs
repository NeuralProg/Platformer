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

public class Shoot : EnemyAction
{
    public float speed;
    public float addedStopDistance = 0f;
    public Transform checkEdge;
    public bool movingRight;

    public override void OnStart()
    {
        if (movingRight && transform.localScale.x < 0 || !movingRight && transform.localScale.x > 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public override void OnFixedUpdate()
    {
        rb.velocity = new Vector2(speed * (transform.localScale.x / 4), rb.velocity.y);
    }

    public override TaskStatus OnUpdate()
    {
        var goingToGround = Physics2D.OverlapCircle(checkEdge.position + new Vector3(addedStopDistance * (transform.localScale.x / 4), checkEdge.position.y, checkEdge.position.z), 0.04f, GetComponent<CyborgEnemy>().whatIsGround);

        return !goingToGround ? TaskStatus.Success : TaskStatus.Running;
    }
}
