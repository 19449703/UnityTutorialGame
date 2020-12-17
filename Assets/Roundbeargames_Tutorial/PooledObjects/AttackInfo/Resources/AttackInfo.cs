﻿using System.Collections;
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

        public void ResetInfo(Attack attack, CharacterControl control)
        {
            isRegisterd = false;
            isFinished = false;
            attackAbility = attack;
            attacker = control;
        }

        public void Register(Attack attack)
        {
            isRegisterd = true;

            attackAbility = attack;
            colliderNames = attack.colliderNames;
            mustCollider = attack.mustCollider;
            mustFaceAttacker = attack.mustFaceAttacker;
            lethalRange = attack.lethalRange;
            maxHits = attack.maxHits;
            currentHits = 0;
        }

        private void OnDisable()
        {
            isFinished = true;
        }
    }
}