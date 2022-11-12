using System;
using UnityEngine;

namespace Game.Scripts.Interface
{
    public class IdleState : IState
    {
        private FSMCtr _fsmCtr;
        private Parameter _parameter;

        public IdleState(FSMCtr fsmCtr)
        {
            this._fsmCtr = fsmCtr;
            this._parameter = fsmCtr.Parameter;
        }

        public void OnEnter()
         {
             _parameter.Animator.Play("Idle");
         }

         public void OnUpdate()
         {
             
         }

         public void OnExit()
         {
             
         }
    }
    
    public class RunState : IState
    {
        private FSMCtr _fsmCtr;
        private Parameter _parameter;

        public RunState(FSMCtr fsmCtr)
        {
            this._fsmCtr = fsmCtr;
            this._parameter = fsmCtr.Parameter;
        }

        public void OnEnter()
        {
            _parameter.Animator.Play("Run");
        }

        public void OnUpdate()
        {
            float playerSpeed = Input.GetAxis("Horizontal") * _parameter.speed;
            _parameter.rb.velocity = new Vector2(playerSpeed, _parameter.rb.velocity.y);
        }

        public void OnExit()
        {
             
        }
    }
    
    public class JumpState : IState
    {
        private FSMCtr _fsmCtr;
        private Parameter _parameter;

        public JumpState(FSMCtr fsmCtr)
        {
            this._fsmCtr = fsmCtr;
            this._parameter = fsmCtr.Parameter;
        }

        public void OnEnter()
        {
             _parameter.Animator.Play("Jump");
             float playerSpeed = Input.GetAxis("Horizontal") * _parameter.speed;
             _parameter.rb.velocity = new Vector2(playerSpeed, 1f * _parameter.jumpForce);
        }

        public void OnUpdate()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
    
    public class DashState : IState
    {
        private FSMCtr _fsmCtr;
        private Parameter _parameter;

        public DashState(FSMCtr fsmCtr)
        {
            this._fsmCtr = fsmCtr;
            this._parameter = fsmCtr.Parameter;
        }

        public void OnEnter()
        {
             
        }

        public void OnUpdate()
        {
             
        }

        public void OnExit()
        {
             
        }
    }
    
    public class DieState : IState
    {
        private FSMCtr _fsmCtr;
        private Parameter _parameter;

        public DieState(FSMCtr fsmCtr)
        {
            this._fsmCtr = fsmCtr;
            this._parameter = fsmCtr.Parameter;
        }

        public void OnEnter()
        {
             
        }

        public void OnUpdate()
        {
             
        }

        public void OnExit()
        {
             
        }
    }
}