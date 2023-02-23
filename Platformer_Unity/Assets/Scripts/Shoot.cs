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

public class Shoot : EnemyAction
{
    public GameObject projectile;
 
    public override void OnStart()
    {
        GetComponent<ShootProjectile>().Shoot(projectile, transform.position);
    }
}
