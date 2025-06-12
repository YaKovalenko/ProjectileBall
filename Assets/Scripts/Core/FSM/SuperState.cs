namespace Core.FSM
{
    public abstract class SuperState : State
    {
        protected FiniteStateMachine FiniteStateMachine { get; set; }

        public override void Enter()
        {
            FiniteStateMachine.Start();
        }

        public override void Update(float deltaTime)
        {
            FiniteStateMachine.Tick(deltaTime);
        }

        public override void FixedUpdate(float deltaTime)
        {
            FiniteStateMachine.FixedTick(deltaTime);
        }

        public override void Exit()
        {
            FiniteStateMachine.Stop();
        }
    }
}