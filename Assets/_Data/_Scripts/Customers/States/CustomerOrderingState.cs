using Assets.SimpleLocalization.Scripts;
using TableAndChair;
using UnityEngine;

namespace Customers
{
    public class CustomerOrderingState : CustomerState
    {
        private bool _isOrder = false;
        public CustomerOrderingState(StateMachine stateMachine, string animBoolName, CustomerBehaviour customer) : base(stateMachine, animBoolName, customer)
        {
        }

        public override void Enter()
        {
            base.Enter();
            if (!_isOrder)
            {
                _isOrder = true;
                FoodData foodData = MenuManager.Instance.GetRandomFoodFromMenu();
                string text = UpdateNameText(foodData);
                customer.targetTable.AddFood(foodData);
                customer.SetupPopupText(text, true);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(isExitingState) return;
            if (customer.targetTable != null && customer.targetTable.TableStatus == TableStatus.Occupied)
            {
                stateMachine.ChangeState(customer.SittingState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _isOrder = false;
            customer.SetupPopupText("", false);
        }

        private string UpdateNameText(FoodData foodData)
        {
            switch (foodData.id)
            {
                case 1:
                    return LocalizationManager.Localize("Bread.Name");
                    break;
                case 2:
                    return LocalizationManager.Localize("BrokenRice.Name");
                    break;
                case 3:
                    return LocalizationManager.Localize("Pho.Name");

                    break;
                case 4:
                    return LocalizationManager.Localize("Kimbap.Name");

                    break;
                case 5:
                    return LocalizationManager.Localize("Kimchi.Name");

                    break;
                case 6:
                    return LocalizationManager.Localize("Tokbokki.Name");

                    break;
                case 7:
                    return LocalizationManager.Localize("Bibimbap.Name");

                    break;

                default:
                    return "";

            }
        }
    }
}