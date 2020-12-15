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
        Grounded,
	}

	public class CharacterControl : MonoBehaviour
    {
        public Animator animator;

        public bool moveLeft;
        public bool moveRight;
        public bool jump;

        private Rigidbody rigid;
        public Rigidbody RIGID_BODY
        {
            get
            {
                if (rigid == null)
                {
                    rigid = GetComponent<Rigidbody>();
                }
                return rigid;
            }
        }

        public GameObject colliderEdgePrefab;
        public List<GameObject> bottomSpheres = new List<GameObject>();
        public List<GameObject> frontSpheres = new List<GameObject>();

        public float gravityMultiplier;
        public float pullMultiplier;

        private void Awake()
        {
            BoxCollider box = GetComponent<BoxCollider>();

            float top = box.bounds.center.y + box.bounds.extents.y;
            float bottom = box.bounds.center.y - box.bounds.extents.y;
            float front = box.bounds.center.z + box.bounds.extents.z;
            float back = box.bounds.center.z - box.bounds.extents.z;

            GameObject bottomFront = CreateEdgeSphere(new Vector3(0, bottom, front));
            GameObject bottomBack = CreateEdgeSphere(new Vector3(0, bottom, back));
            bottomFront.transform.SetParent(this.transform);
            bottomBack.transform.SetParent(this.transform);
            bottomSpheres.Add(bottomFront);
            bottomSpheres.Add(bottomBack);

            GameObject topFront = CreateEdgeSphere(new Vector3(0, top, front));
            topFront.transform.SetParent(this.transform);
            frontSpheres.Add(topFront);

            float horSec = Vector3.Distance(bottomFront.transform.localPosition, bottomBack.transform.localPosition) / 5;
            CreateMiddleSpheres(bottomFront, -this.transform.forward, horSec, 4, bottomSpheres);

            float verSec = Vector3.Distance(bottomFront.transform.localPosition, topFront.transform.localPosition) / 10;
            CreateMiddleSpheres(bottomFront, this.transform.up, verSec, 9, frontSpheres);
        }

        private void FixedUpdate()
        {
            if (RIGID_BODY.velocity.y < 0f)
            {
                //Debug.Log("gravity");
                RIGID_BODY.velocity += -Vector3.up * gravityMultiplier;
            }

            if (RIGID_BODY.velocity.y > 0f && !jump)
            {
                //Debug.Log("pull");
                RIGID_BODY.velocity += -Vector3.up * pullMultiplier;
            }
        }
        public void CreateMiddleSpheres(GameObject start, Vector3 dir, float sec,
            int iterations, List<GameObject> spheresList)
        {
            for (int i = 0; i < iterations; i++)
            {
                Vector3 pos = start.transform.position + dir * sec * (i + 1);
                GameObject obj = CreateEdgeSphere(pos);
                obj.transform.SetParent(this.transform);
                spheresList.Add(obj);
            }
        }

        public GameObject CreateEdgeSphere(Vector3 pos)
        {
            GameObject obj = Instantiate(colliderEdgePrefab, pos, Quaternion.identity);
            return obj;
        }

#if UNITY_EDITOR_OSX
        public Material material;
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
#endif
}
