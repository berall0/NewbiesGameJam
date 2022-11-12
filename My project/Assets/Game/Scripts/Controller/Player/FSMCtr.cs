using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Interface
{
    public enum StateEnum
    {
        Idle,Run,Jump,Die,Dash
    }

    public enum PlayerStateEnum
    {
        
    }
    [Serializable]
    public class Parameter
    {
        //行走速度
        public float speed;
        //跳跃力度
        public float jumpForce;
        //冲刺时间
        public float dashTime;
        //冲刺力度
        public float dashForce;

        public Animator Animator;
        public Rigidbody2D rb;
    }
    
    
    public class FSMCtr : MonoBehaviour
    {
        public Parameter Parameter;
        private IState currentState;
        private Dictionary<StateEnum, IState> _states = new Dictionary<StateEnum, IState>();

        private void Start()
        {
            _states.Add(StateEnum.Idle,new IdleState(this));
            _states.Add(StateEnum.Die,new DieState(this));
            _states.Add(StateEnum.Dash,new DashState(this));
            _states.Add(StateEnum.Jump,new JumpState(this));
            _states.Add(StateEnum.Run,new RunState(this));
            
            TranslateState(StateEnum.Idle);
        }

        private void Update()
        {
            currentState.OnUpdate();
        }


        public void TranslateState(StateEnum stateEnum)
        {
            if (currentState!= null)
            {
                currentState.OnExit();
            }

            currentState = _states[stateEnum];
            currentState.OnEnter();
        }
    }
    // public abstract class FSMCtr : MonoBehaviour
    // {
    //     public abstract Enum CurrentState { get; set; }
    //     
    //     //当前状态对象
    //     protected StateBase currentStateObj;
    //
    //     private List<StateBase> stateList = new List<StateBase>();
    //
    //     //状态发生改变
    //     public void TranslateState(Enum newState, bool reCurrentState = false)
    //     {
    //         if (Equals(newState, CurrentState) && !reCurrentState)return;
    //         if (currentStateObj != null)
    //         {
    //             currentStateObj.OnExit();
    //         }
    //
    //         currentStateObj = GetStateObj(newState);
    //         currentStateObj.OnEnter();
    //
    //     }
    //
    //     private StateBase GetStateObj(Enum stateType)
    //     {
    //         for (int i = 0; i < stateList.Count; i++)
    //         {
    //             if (Equals(stateList[i].StateType, stateType))
    //             {
    //                 return stateList[i];
    //             }
    //             
    //         }
    //         StateBase state = Activator.CreateInstance(Type.GetType(stateType.ToString())) as StateBase;
    //         state.OnInit(stateType);
    //         stateList.Add(state);
    //         return state;
    //     }
    // }
    
    
}