using System;

namespace Core.FSM
{
    public readonly struct StateIdentifier : IComparable<StateIdentifier>, IEquatable<StateIdentifier>
    {
#if ORCHID_FSM_DEBUG
        private readonly string _value;
#else
        private readonly int _value;
#endif

        public static bool operator ==(StateIdentifier left, StateIdentifier right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(StateIdentifier left, StateIdentifier right)
        {
            return !left.Equals(right);
        }

        private StateIdentifier(string value)
        {
#if ORCHID_FSM_DEBUG
            _value = value;
#else
            _value = value.GetHashCode();
#endif
        }

        public int CompareTo(StateIdentifier other)
        {
#if ORCHID_FSM_DEBUG
            return string.Compare(_value, other._value, StringComparison.Ordinal);
#else
            return _value.CompareTo(other._value);
#endif
        }

        public bool Equals(StateIdentifier other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is StateIdentifier other && Equals(other);
        }

        public override int GetHashCode()
        {
#if ORCHID_FSM_DEBUG
            return _value != null ? _value.GetHashCode() : 0;
#else
            return _value;
#endif
        }

        public static StateIdentifier FromState<TState>() where TState : State
        {
            return new StateIdentifier(typeof(TState).Name);
        }
    }
}