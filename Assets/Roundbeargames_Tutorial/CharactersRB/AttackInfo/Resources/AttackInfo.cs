using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class AttackInfo : MonoBehaviour
    {
        public CharacterControl attacker = null;
        public Attack attackAbility;
        public List<string> colliderNames = new List<string>();

        public bool mustCollider;
        public bool mustFaceAttacker;
        public float lethalRange;
        public int maxHits;
        public bool isRegisterd;
        public bool isFinished;
        public int currentHits;

        public void ResetInfo(Attack attack)
        {
            isRegisterd = false;
            isFinished = false;
            attackAbility = attack;
        }

        public void Register(Attack attack, CharacterControl control)
        {
            isRegisterd = true;
            attacker = control;

            attackAbility = attack;
            colliderNames = attack.colliderNames;
            mustCollider = attack.mustCollider;
            mustFaceAttacker = attack.mustFaceAttacker;
            lethalRange = attack.lethalRange;
            maxHits = attack.maxHits;
            currentHits = 0;
        }
    }
}