using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState _currentState;

    private Dictionary<Type, List<Transition>> _transitions = new();
    private List<Transition> _currentTransitions;
    private List<Transition> _anyTransitions;

    private static List<Transition> EmptyTransitions = new List<Transition>(0);

    public void Tick()
    {
        var transition = GetTransition();
        if(transition != null)
        {
            SetState(transition.To);
        }
    }

    private void SetState(IState state)
    {
        if (state == _currentState)
        {
            return;
        }

        _currentState?.OnExit();
        _currentState = state;

        _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
        if (_currentTransitions == null)
        {
            _currentTransitions = EmptyTransitions;
        }

        _currentState.OnEnter();
    }

    public void AddStransition(IState from, IState to, Func<bool> condition)
    {
        if(_transitions.TryGetValue(from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
        }

        transitions.Add(new Transition(to, condition));
    }

    public void AddAnyTransition(IState state, Func<bool> condition)
    {
        _anyTransitions.Add(new Transition(state, condition));
    }

    private class Transition
    {
        public readonly IState To;
        public readonly Func<bool> Condition;

        public Transition (IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

    private Transition GetTransition()
    {
        foreach(var transition in _anyTransitions)
        {
            if(transition.Condition())
            {
                return transition;
            }
        }
        foreach(var transition in _currentTransitions)
        {
            if(transition.Condition())
            {
                return transition;
            }
        }
        return null;
    }
}


