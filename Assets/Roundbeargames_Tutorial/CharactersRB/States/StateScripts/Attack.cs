using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New State(Attack)", menuName = "Roundbeargames/AbilityData/Attack")]
    public class Attack : StateData
    {
        public float startAttackTime;
        public float endAttackTime;
        public List<string> colliderNames = new List<string>();
        public bool mustCollider;
        public bool mustFaceAttacker;
        public float lethalRange;
        public int maxHits;
        public List<RuntimeAnimatorController> deathAnimators = new List<RuntimeAnimatorController>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);

            GameObject obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject))) as GameObject;
            AttackInfo info = obj.GetComponent<AttackInfo>();
            info.ResetInfo(this);

            if (!AttackManager.instance.currentAttcks.Contains(info))
            {
                AttackManager.instance.currentAttcks.Add(info);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterAttack(characterState, animator, stateInfo);
            DeregisterAttack(characterState, animator, stateInfo);
        }

        public void RegisterAttack(CharacterState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= startAttackTime && stateInfo.normalizedTime < endAttackTime)
            {
                foreach (AttackInfo info in AttackManager.instance.currentAttcks)
                {
                    if (info == null)
                        continue;

                    if (!info.isRegisterd && info.attackAbility == this)
                    {
                        info.Register(this, state.GetCharacterControl(animator));
                    }
                }
            }
        }

        public void DeregisterAttack(CharacterState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= endAttackTime)
            {
                foreach (AttackInfo info in AttackManager.instance.currentAttcks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if (info.attackAbility == this && !info.isFinished)
                    {
                        info.isFinished = true;
                        Destroy(info.gameObject);
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ClearAttack();
        }

        public void ClearAttack()
        {
            for (int i = 0; i < AttackManager.instance.currentAttcks.Count; i++)
            {
                AttackManager.instance.currentAttcks.RemoveAt(i);
            }
        }
    }
}
