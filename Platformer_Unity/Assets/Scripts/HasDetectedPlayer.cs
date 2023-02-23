using BehaviorDesigner.Runtime;
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

public class HasDetectedPlayer : EnemyConditional
{
    public bool checkIfPlayerDetected;

    public override TaskStatus OnUpdate()
    {
        if (checkIfPlayerDetected)
            return gameObject.GetComponentInChildren<PlayerDetection>().playerDetected ? TaskStatus.Success : TaskStatus.Failure;
        else
            return gameObject.GetComponentInChildren<PlayerDetection>().playerDetected ? TaskStatus.Failure : TaskStatus.Success;
    }
}
