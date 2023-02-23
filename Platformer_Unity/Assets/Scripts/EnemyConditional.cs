using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    public class EnemyConditional : Conditional
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