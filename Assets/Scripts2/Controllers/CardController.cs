using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : Controller
{
    public CardViewVariable activeCardViewVar;
    public PlayerSystem playerSystem;
    public CardSystem cardSystem;
    public ResourceSystem resourceSystem;
    public GameEvent GiveUpControlEvent;
    public GameEvent OnCardPlacedOnTableEvent;
    private ControllerState currentControllerState;
    private bool missUpdate;

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        activeControl = false;
        missUpdate = true;
    }

    private void Reset()
    {
        Debug.Log("Card controller gives up control...");
        missUpdate = true;
        currentControllerState = null;
        activeControl = false;
    }

    public void Activate(GameObject activeCardView, Areas cardLocation)
    {
        Debug.Log("Card controller takes over...");
        if (!PlayerOwnsCard(activeCardView))
        {
            Debug.Log("Player doesn't own card - returning control to Main Controller ...");
            GiveUpControl();
            return;
        }

        if (cardLocation == Areas.Hand)
        {
            if (activeCardView.GetComponent<CardView>().cardInstance.card.type == CardType.Spell)
            {
                ConfirmChooseSpellTargetStateInit(activeCardView);
            }
            else if (activeCardView.GetComponent<CardView>().cardInstance.card.type == CardType.Soldier || activeCardView.GetComponent<CardView>().cardInstance.card.type == CardType.Blocker)
            {
                ConfirmPlaceOnTableStateInit(activeCardView);
            }

        }

        if (cardLocation == Areas.Table)
        {
            ConfirmCardAttackStateInit(activeCardView);
        }

        if (currentControllerState == null)
        {
            GiveUpControl();
        }
    }

    void ChangeCurrentState(ControllerState state)
    {
        this.currentControllerState = state;
        state.owner = this;
    }

    void ConfirmPlaceOnTableStateInit(GameObject activeCardView)
    {
        Debug.Log("Card controller switches to CardOnHandState...");
        activeCardViewVar.value = activeCardView;
        var cardView = activeCardView.GetComponent<CardView>();
        cardView.ToggleActive();
        activeControl = true;
        ChangeCurrentState(new ConfirmPlaceOnTableState());
    }

    void ConfirmCardAttackStateInit(GameObject activeCardView)
    {
        Debug.Log("Card controller switches to CardAttackConfirmState...");
        activeCardViewVar.value = activeCardView;
        var cardView = activeCardView.GetComponent<CardView>();
        cardView.ToggleActive();
        activeControl = true;
        ChangeCurrentState(new ConfirmCardAttackState());
    }

    void ConfirmChooseSpellTargetStateInit(GameObject activeCardView)
    {
        Debug.Log("Card controller switches to CardAttackConfirmState...");
        activeCardViewVar.value = activeCardView;
        var cardView = activeCardView.GetComponent<CardView>();
        cardView.ToggleActive();
        activeControl = true;
        ChangeCurrentState(new ConfirmChooseSpellTargetState());
    }

    bool PlayerOwnsCard(GameObject activeCardView)
    {
        if (activeCardView.GetComponent<CardView>().cardInstance.owner != playerSystem.currentPlayer.value)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    protected override void HandleLeftMouseClick()
    {
        var handler = currentControllerState as ILeftMouseClickHandler;
        if (handler != null)
            handler.OnLeftMouseClick();
    }

    public void GiveUpControl()
    {
        Reset();
        GiveUpControlEvent.Raise();
    }

    private interface ILeftMouseClickHandler
    {
        void OnLeftMouseClick();
    }


    private abstract class ControllerState
    {
        public CardController owner;
    }


    private class ConfirmPlaceOnTableState : ControllerState, ILeftMouseClickHandler
    {
        public void OnLeftMouseClick()
        {
            if (!owner.missUpdate)
            {
                InvokePlaceCardOnTable();
            }
            else
            {
                if (owner.missUpdate)
                {
                    owner.missUpdate = false;
                }
            }
        }

        private void InvokePlaceCardOnTable()
        {
            var objectClicked = owner.GetObjectClicked();
            if (objectClicked != null)
            {
                if (objectClicked.GetComponent<PlayerTableView>() != null)
                {
                    if (owner.activeCardViewVar.value.GetComponent<CardView>().cardInstance.owner == owner.playerSystem.currentPlayer.value)
                    {
                        owner.cardSystem.PlaceCardOnTable(owner.activeCardViewVar.value.GetComponent<CardView>(), objectClicked);
                    }
                }
            }
            Complete();
        }

        private void Complete()
        {
            owner.activeCardViewVar.value.GetComponent<CardView>().ToggleActive();
            owner.activeCardViewVar.value = null;
            owner.GiveUpControl();
        }
    }

    private class ConfirmCardAttackState : ControllerState, ILeftMouseClickHandler
    {
        public void OnLeftMouseClick()
        {
            if (!owner.missUpdate)
            {
                InvokeCardAttack();
            }
            else
            {
                owner.missUpdate = false;
            }

        }

    

        private void InvokeCardAttack()
        {
            var objectClicked = owner.GetObjectClicked();

            if (objectClicked != null)
            {
                if (objectClicked.GetComponent<PlayerView>() != null)
                {
                    if (objectClicked.GetComponent<PlayerView>().owner != owner.playerSystem.currentPlayer.value)
                    {
                        Debug.Log("Confirmed target is a player...");
                        owner.cardSystem.UseCard(owner.activeCardViewVar.value.GetComponent<CardView>().cardInstance, objectClicked.GetComponent<PlayerView>().owner);
                    }
                }

                if (objectClicked.GetComponent<CardView>() != null)
                {
                    if (objectClicked.GetComponent<CardView>().cardInstance.owner != owner.playerSystem.currentPlayer.value)
                    {
                        Debug.Log("Confirmed target is a card...");
                        owner.cardSystem.UseCard(owner.activeCardViewVar.value.GetComponent<CardView>().cardInstance, objectClicked.GetComponent<CardView>().cardInstance);
                    }

                }

            }

            Complete();
        }

        private void Complete()
        {
            owner.activeCardViewVar.value.GetComponent<CardView>().ToggleActive();
            owner.activeCardViewVar.value = null;
            owner.GiveUpControl();
        }

    }

    private class ConfirmChooseSpellTargetState : ControllerState, ILeftMouseClickHandler
    {
        private List<Target> chosenTargetsList = new List<Target>();
        private List<CardView> chosenCardsViews = new List<CardView>();
        private CardInstance card;
        SpellAbility ability;

        public void OnLeftMouseClick()
        {
            card = owner.activeCardViewVar.value.GetComponent<CardView>().cardInstance;
            ability = (SpellAbility)card.card.ability;

            if (!owner.missUpdate)
            {
                if (chosenTargetsList.Count < ability.numberOfTargets)
                {
                    AddTarget();
                }
            }
            else
            {
                owner.missUpdate = false;
            }
        }

        private void AddTarget()
        {
            var objectClicked = owner.GetObjectClicked();

            if (objectClicked != null)
            {
                if (objectClicked.GetComponent<PlayerView>() != null)
                {
                    Target target = (Target)objectClicked.GetComponent<PlayerView>().owner;
                    if (TargetingSystem.IsValid(ability.validTargets, target, true))
                    {
                        chosenTargetsList.Add(target);
                    }
                }
                else if (objectClicked.GetComponent<CardView>() != null)
                {
                    Target target = (Target)objectClicked.GetComponent<CardView>().cardInstance;
                    if (TargetingSystem.IsValid(ability.validTargets, target, true))
                    {
                        if (!chosenTargetsList.Contains(target))
                        {
                            chosenTargetsList.Add(target);
                            chosenCardsViews.Add(objectClicked.GetComponent<CardView>());
                            objectClicked.GetComponent<CardView>().ToggleActive();
                        }
                        else
                        {
                            chosenTargetsList.Remove(target);
                            chosenCardsViews.Remove(objectClicked.GetComponent<CardView>());
                            objectClicked.GetComponent<CardView>().ToggleActive();
                        }
                    }
                }
                else
                {
                    if (chosenCardsViews.Count > 0)
                    {
                        foreach (CardView cardView in chosenCardsViews)
                        {
                            cardView.ToggleActive();
                        }
                        chosenCardsViews.Clear();
                        chosenTargetsList.Clear();
                        Complete();
                    }
                }

            }
            else
            {
                Complete();
            }

            if (chosenTargetsList.Count == ability.numberOfTargets || (owner.playerSystem.enemy.table.Count < ability.numberOfTargets && chosenTargetsList.Count == owner.playerSystem.enemy.table.Count && chosenTargetsList.Count != 0))
            {

                foreach (CardView cardView in chosenCardsViews)
                {
                    cardView.ToggleActive();
                }
                Debug.Log("Activating spell ability....");
                ability.ActivateAbility(chosenTargetsList, card.abilityInstance);
                Complete();
            }

        }

        private void Complete()
        {
            owner.activeCardViewVar.value.GetComponent<CardView>().ToggleActive();
            owner.activeCardViewVar.value = null;
            owner.GiveUpControl();
        }

    }

 
}


