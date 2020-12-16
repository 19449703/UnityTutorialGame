using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class DamageDecetor : MonoBehaviour
    {
        CharacterControl control;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        private void Update()
        {
            if (AttackManager.instance.currentAttcks.Count > 0)
            {
                CheckAttack();
            }
        }

        private void CheckAttack()
        {
            foreach(var info in AttackManager.instance.currentAttcks)
            {
                if (info == null || !info.isRegisterd || info.isFinished)
                {
                    continue;
                }

                if (info.mustCollider)
                {
                    if (IsCollided(info))
                    {
                        TakeDamage(info);
                    }
                }
            }
        }

        private bool IsCollided(AttackInfo info)
        {
            foreach(Collider collider in control.collidingParts)
            {
                foreach(string name in info.colliderNames)
                {
                    if (name == collider.gameObject.name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void TakeDamage(AttackInfo info)
        {
            Debug.Log(info.attacker.gameObject.name + " hits：" + this.gameObject.name);
        }
    }
}
