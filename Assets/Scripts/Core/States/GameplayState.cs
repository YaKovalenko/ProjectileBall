using Core.FSM;
using Core.Services.InputService;
using UnityEngine.SceneManagement;

namespace Core.States
{
    public class GameplayState : State
    {
        private readonly IInputService _inputService;

        public GameplayState(IInputService inputService)
        {
            _inputService = inputService;
        }

        public override void Enter()
        {
            base.Enter();

        }
        
    }
}