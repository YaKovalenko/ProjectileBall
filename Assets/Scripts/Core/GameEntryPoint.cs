using Core.FSM;
using Core.Services.InputService;
using Core.States;
using UnityEngine;
using VContainer.Unity;

namespace Core
{
    public class GameEntryPoint : IStartable, ITickable, IFixedTickable
    {
        private readonly string _gameStateId = FiniteStateMachine.GetStateID<GameplayState>();
        private readonly string _winStateId = FiniteStateMachine.GetStateID<WinState>();
        private readonly string _loseStateId = FiniteStateMachine.GetStateID<LoseState>();
        
        private readonly FiniteStateMachine _stateMachine;
        private string _currentStateId;

        public GameEntryPoint(IInputService inputService)
        {
            _stateMachine = CreateStateMachine(inputService);
        }
        
        private FiniteStateMachine CreateStateMachine(IInputService inputService)
        {
            var gameState = new GameplayState(inputService);
            
            var winState = new WinState();
            
            var loseState = new LoseState();

            var transitions = new Transition[]
            {
                new(() => string.Equals(_currentStateId, _gameStateId), winState, gameState),
                new(() => string.Equals(_currentStateId, _gameStateId), loseState, gameState),

                new(() => string.Equals(_currentStateId, _winStateId), gameState, winState),
                new(() => string.Equals(_currentStateId, _loseStateId), gameState, loseState)
                
            };

            return new FiniteStateMachine(transitions, gameState);
        } 

        public async void Start()
        {
            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Game");
            _stateMachine.Start();
        }

        public void Tick()
        {
            _stateMachine.Tick(Time.deltaTime);
        }

        public void FixedTick()
        {
        }
    }
}