using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    public class EnemyAction : Action
    {
        protected Rigidbody2D rb;
        protected Animator anim;
        
        public override void OnAwake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = gameObject.GetComponentInChildren<Animator>();
        }
    }
}