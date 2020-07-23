﻿namespace BeastHunter
{
    public abstract class CharacterBaseState
    {
        #region Fields

        protected readonly InputModel _inputModel;
        protected readonly CharacterModel _characterModel;
        protected readonly CharacterAnimationController _animationController;
        protected readonly CharacterStateMachine _stateMachine;

        #endregion


        #region Properties

        public CharacterBaseState NextState { get; set; }
        public StateType Type { get; protected set; }

        public bool IsTargeting { get; protected set; }
        public bool IsAttacking { get; protected set; }
        public bool CanExit { get; protected set; }
        public bool CanBeOverriden { get; protected set; }

        #endregion


        #region ClassLifeCycle

        public CharacterBaseState(GameContext context, CharacterStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _characterModel = context.CharacterModel;
            _inputModel = context.InputModel;
            _animationController = _stateMachine.AnimationController;
        }

        #endregion


        #region Methods

        public abstract void Initialize();

        public abstract void Execute();

        public abstract void OnExit();

        public abstract void OnTearDown();

        #endregion
    }
}
