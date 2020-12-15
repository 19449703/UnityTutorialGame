using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        [Range(0.01f, 1f)]
        public float checkTime;
        public float distance;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (stateInfo.normalizedTime >= checkTime)
            {
                animator.SetBool(TransitionParameter.Grounded.ToString(), IsGrounded(control));
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        bool IsGrounded(CharacterControl control)
        {
            if (Mathf.Abs(control.RIGID_BODY.velocity.y) < 0.01f)
                return true;

            foreach(var o in control.bottomSpheres)
            {
                Debug.DrawRay(o.transform.position, -Vector3.up * 0.7f, Color.yellow);
                if (Physics.Raycast(o.transform.position, -Vector3.up, distance))
                    return true;
            }

            return false;
        }
    }
}
