﻿using UnityEngine;


namespace BeastHunter
{
    public sealed class AttackingFromLeftState : CharacterBaseState
    {
        #region Constants

        private const float TIME_PART_TO_ENABLE_WEAPON = 0f;

        #endregion


        #region Fields

        private float _currentAttackTime;
        private int _currentAttackIndex;

        #endregion


        #region ClassLifeCycle

        public AttackingFromLeftState(GameContext context, CharacterStateMachine stateMachine) : base(context, stateMachine)
        {
            Type = StateType.Battle;
            IsTargeting = false;
            IsAttacking = true;
            CanExit = false;
            CanBeOverriden = false;
            _currentAttackIndex = 0;
        }

        #endregion


        #region Methods

        public override void Initialize()
        {
            _currentAttackIndex = Random.Range(0, _characterModel.LeftHandWeapon.AttacksLeft.Length);
            _characterModel.LeftHandWeapon.CurrentAttack = _characterModel.LeftHandWeapon.AttacksLeft[_currentAttackIndex];
            _currentAttackTime = _characterModel.LeftHandWeapon.CurrentAttack.Time;
            _animationController.PlayAttackAnimation(_characterModel.LeftHandWeapon.SimpleAttackFromLeftkAnimationHash, 
                _currentAttackIndex);
            TimeRemaining enableWeapon = new TimeRemaining(EnableWeapon, TIME_PART_TO_ENABLE_WEAPON * _currentAttackTime);
            enableWeapon.AddTimeRemaining(TIME_PART_TO_ENABLE_WEAPON * _currentAttackTime);
            CanExit = false;
        }

        public override void Execute()
        {       
            ExitCheck();
            LookAtEnemy();
            StayInBattle();
        }

        public override void OnExit()
        {
            _characterModel.LeftWeaponBehavior.IsInteractable = false;
        }

        public override void OnTearDown()
        {
        }

        private void ExitCheck()
        {
            if (_currentAttackTime > 0)
            {
                _currentAttackTime -= Time.deltaTime;
            }
            else
            {
                CanExit = true;

                if(_stateMachine.PreviousState == _stateMachine.CharacterStates[CharacterStatesEnum.BattleTargetMovement])
                {
                    _stateMachine.ReturnState();
                }
            }
        }

        private void LookAtEnemy()
        {
            if (_characterModel.ClosestEnemy != null && _stateMachine.PreviousState.IsTargeting)
            {
                _characterModel.CharacterTransform.LookAt(_characterModel.ClosestEnemy.transform);
            }
        }

        private void StayInBattle()
        {
            _characterModel.IsInBattleMode = true;
        }

        private void EnableWeapon()
        {
            _characterModel.LeftWeaponBehavior.IsInteractable = true;
        }

        #endregion
    }
}
