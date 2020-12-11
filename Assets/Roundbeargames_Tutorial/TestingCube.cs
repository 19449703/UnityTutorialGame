using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class TestingCube : MonoBehaviour
    {
        void Update()
        {
            if (VirtualInputManager.instance.moveLeft && VirtualInputManager.instance.moveRight)
            {
                return;
            }

            if (VirtualInputManager.instance.moveLeft)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
                this.transform.Translate(Vector3.forward * 10 * Time.deltaTime);
                
            }
            if (VirtualInputManager.instance.moveRight)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.transform.Translate(Vector3.forward * 10 * Time.deltaTime);
            }
        }
    }

}
