﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New State(Idle)", menuName = "Roundbeargames/AbilityData/Idle")]
    public class Idle : StateData
    {
		public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
		{
            animator.SetBool(TransitionParameter.Jump.ToString(), false);
        }

		public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
			CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.attack)
            {
                animator.SetBool(TransitionParameter.Attack.ToString(), true);
            }

            if (control.jump)
			{
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
            }

			if (control.moveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }
            if (control.moveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }
        }

		public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
		{
		}
	}
}
