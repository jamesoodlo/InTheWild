using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool attack;
		public bool hvAtk;
		public bool roll;
		public bool switchWeapons;
		public bool slot1;
		public bool slot2;
		public bool pickUp;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnAttack(InputValue value)
		{
			AttackInput(value.isPressed);
		}

		public void OnHeavyAttack(InputValue value)
		{
			HeavyAttackInput(value.isPressed);
		}

		public void OnRoll(InputValue value)
		{
			RollInput(value.isPressed);
		}

		public void OnSwitch(InputValue value)
		{
			SwitchInput(value.isPressed);
		}

		public void OnPickUp(InputValue value)
		{
			PickUpInput(value.isPressed);
		}

		public void OnSlot1(InputValue value)
		{
			Slot1Input(value.isPressed);
		}

		public void OnSlot2(InputValue value)
		{
			Slot2Input(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void AttackInput(bool newAttackState)
		{
			attack = newAttackState;
		}

		public void HeavyAttackInput(bool newHeavyAttackState)
		{
			hvAtk = newHeavyAttackState;
		}

		public void RollInput(bool newRollState)
		{
			roll = newRollState;
		}

		public void SwitchInput(bool newSwitchWeaponsState)
		{
			switchWeapons = newSwitchWeaponsState;
		}

		public void PickUpInput(bool newPickUpState)
		{
			pickUp = newPickUpState;
		}

		public void Slot1Input(bool newSlot1State)
		{
			slot1 = newSlot1State;
		}

		public void Slot2Input(bool newSlot2State)
		{
			slot2 = newSlot2State;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}