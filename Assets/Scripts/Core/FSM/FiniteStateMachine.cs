using System;

namespace Core.FSM
{
    public class FiniteStateMachine : IDisposable
    {
        private readonly Transition[] _transitions;
        private readonly State _initialState;

        private State _currentState;
        private bool _isRunning;
        
        public FiniteStateMachine(Transition[] transitions, State initialState)
        {
            _transitions = transitions;
            _initialState = initialState;
        }
        
        public void Start()
        {
            if (_currentState == null)
            {
                _currentState = _initialState;
            }

            _currentState.Enter();
            _isRunning = true;
        }

        public void Stop()
        {
            _currentState?.Exit();
            _currentState = null;
            _isRunning = false;
        }

        private void SetState(State state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public virtual void Tick(float deltaTime)
        {
            if (!_isRunning)
                return;

            if (_currentState == null)
            {
                throw new ArgumentNullException(nameof(_currentState));
            }

            foreach (var transition in _transitions)
            {
                if (!transition.CanTransit(_currentState))
                    continue;

                SetState(transition.TargetState);
                return;
            }

            _currentState?.Update(deltaTime);
        }

        public virtual void FixedTick(float deltaTime)
        {
            if (!_isRunning)
                return;

            _currentState?.FixedUpdate(deltaTime);
        }

        public virtual void Dispose()
        {
        }

        public override string ToString()
        {
            return _currentState.GetType().Name;
        }

        public static string GetStateID<T>() where T : State
        {
            return typeof(T).Name;
        }
    }
}