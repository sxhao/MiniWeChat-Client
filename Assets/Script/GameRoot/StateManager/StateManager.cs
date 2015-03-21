﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MiniWeChat
{

    public class StateManager : Singleton<StateManager>
    {
        private Stack<BaseState> _stateStack;

        public override void Init()
        {
            _stateStack = new Stack<BaseState>();

            GameObject welcomePanel = UIManager.GetInstance().GetSingleUI(EUIType.WelcomePanel);
            PushState<WelcomePanel>(welcomePanel);
        }

        public void PushState<T>(GameObject go, object param = null) where T : BaseState
        {
            T nextState = go.GetComponent<T>();

            if (_stateStack.Count != 0)
            {
                BaseState curState = _stateStack.Peek();
                curState.OnHide();
            }
            nextState.OnEnter(param);
            _stateStack.Push(nextState);
        }

        public void PopState()
        {
            if (_stateStack.Count != 0)
            {
                BaseState curState = _stateStack.Pop();
                curState.OnExit();
            }

            if (_stateStack.Count != 0)
            {
                BaseState lastState = _stateStack.Peek();
                lastState.OnShow();
            }
        }

        public void ReplaceState<T>(GameObject go, object param = null) where T : BaseState
        {
            T nextState = go.GetComponent<T>();

            if (_stateStack.Count != 0)
            {
                BaseState curState = _stateStack.Pop();
                curState.OnExit();
            }
            nextState.OnEnter(param);
            _stateStack.Push(nextState);
        }
    }

    public class BaseState : MonoBehaviour
    {
        public virtual void OnEnter(object param = null)
        {

        }

        public virtual void OnExit()
        {

        }

        public virtual void OnHide()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnShow()
        {
            gameObject.SetActive(true);
        }
    }

}