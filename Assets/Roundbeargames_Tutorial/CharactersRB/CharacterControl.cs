using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
	public enum TransitionParameter
	{
		Move,
        Jump,
        ForceTransition,
	}

	public class CharacterControl : MonoBehaviour
    {
        public float speed = 8;
        public Animator animator;
		public Material material;

        public bool moveLeft;
        public bool moveRight;
        public bool jump;

        public void ChangeMaterial()
		{
            if (material == null)
			{
				Debug.LogError("No material specified");
				return;
			}

			Renderer[] arrMaterials = this.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in arrMaterials)
			{
                if (r.gameObject != this.gameObject)
				{
					r.material = material;
				}
			}
		}
    }
}
