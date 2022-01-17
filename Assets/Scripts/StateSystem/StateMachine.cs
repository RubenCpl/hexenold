using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.StateSystem
{
    public class StateMachine<TState>
        where TState : IState<TState>
    {
        private Dictionary<string, TState> _states = new Dictionary<string, TState>();

        private string _currentStateName = "";

        public TState CurrentState
        {
            get
            {
                if (_states.ContainsKey(_currentStateName))
                    return _states[_currentStateName];
                else
                    return default (TState);
            }
        }

        public string InitialState
        {
            set
            {
                _currentStateName = value;
                CurrentState?.OnEnter();
            }
        }

        public void MoveToState(string stateName)
        {
            CurrentState?.OnExit();

            _currentStateName = stateName;

            CurrentState?.OnEnter();
        }

        public void Register(string stateName, TState state)
        {
            if (_states.ContainsKey(stateName))
                throw new ArgumentException($"{nameof(stateName)} alread exists");

            _states[stateName] = state;
        }


    }
}
