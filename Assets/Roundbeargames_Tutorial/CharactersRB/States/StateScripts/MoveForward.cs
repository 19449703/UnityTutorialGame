using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        public AnimationCurve speedGraph;
        public float speed;
        public float blockDistance;
        private bool self;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.jump)
            {
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
            }

            if (control.moveLeft && control.moveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (!control.moveLeft && !control.moveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (control.moveLeft)
            {
                if (!CheckFront(control))
                {
                    control.transform.rotation = Quaternion.Euler(0, 180, 0);
                    control.transform.Translate(Vector3.forward * speed * speedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);
                }
            }
            if (control.moveRight)
            {
                if (!CheckFront(control))
                {
                    control.transform.rotation = Quaternion.Euler(0, 0, 0);
                    control.transform.Translate(Vector3.forward * speed * speedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
            
        bool CheckFront(CharacterControl control)
        {
            foreach (var o in control.frontSpheres)
            {
                self = false;

                Debug.DrawRay(o.transform.position, control.transform.forward * 0.3f, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, blockDistance))
                {
                    foreach (Collider c in control.ragdollParts)
                    {
                        if (c.gameObject == hit.collider.gameObject)
                        {
                            self = true;
                            break;
                        }
                    }

                    if (!self)
                        return true;
                }
            }

            return false;
        }
    }
}
