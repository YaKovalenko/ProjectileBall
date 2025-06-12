namespace Core.FSM
{
    public abstract class State
    {
        public virtual void Enter()
        {
        }

        public virtual void Update(float deltaTime)
        {
        }

        public virtual void FixedUpdate(float deltaTime)
        {
        }

        public virtual void Exit()
        {
        }
    }
}