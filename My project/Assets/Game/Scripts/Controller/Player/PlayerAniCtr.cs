using System;
using UnityEngine;

namespace Game.Scripts.Interface
{
    public class PlayerAniCtr : MonoBehaviour
    {
        public PlayParameter P;

        private void Start()
        {
            P.ts.localScale = new Vector3(1, 1, 1);
            P.faceRight = true;
        }

        private void Update()
        {
            FaceForce();
        }

        void FaceForce()
        {
            if (P.faceRight)
            {
                P.ts.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                P.ts.localScale = new Vector3(-1, 1, 1);
            }
            
        }
    }
}