using System.Collections.Generic;
using System.Linq;
using Code.Core.GOAP;
using Code.Infrastructure.Loggers.Unity;
using UnityEngine;

namespace Code.Core.Features.Enemies
{
    public class EnemyAgent : IGoapAgent
    {
        private readonly IGoapPlanner _goapPlanner;
        private readonly GameEntity _entity;
        private Dictionary<string, AgentBelief> _beliefs;
        private HashSet<AgentGoal> _goals;
        private AgentGoal _currentGoal;
        private AgentGoal _lastGoal;
        private ActionPlan _actionPlan;
        private AgentAction _currentAction;

        public HashSet<AgentAction> Actions { get; private set; }

        public EnemyAgent(IGoapFactory goapFactory, GameEntity entity)
        {
            _goapPlanner = goapFactory.CreatePlanner();
            _entity = entity;
            SetupBeliefs();
            SetupActions();
            SetupGoals();
        }

        private void Update()
        {
            if (_currentAction == null)
            {
                "Calculating any potential new plan".Log();
                CalculatePlan();

                if (_actionPlan != null && _actionPlan.Actions.Count > 0)
                {
                    _currentGoal = _actionPlan.AgentGoal;
                    $"Goal: {_currentGoal.Name} with {_actionPlan.Actions.Count} actions in plan".Log();
                    _currentAction = _actionPlan.Actions.Pop();
                    $"Popped action: {_currentAction.Name}".Log();
                    if (_currentAction.Preconditions.All(b => b.Evaluate()))
                    {
                        _currentAction.Start();
                    }
                    else
                    {
                        "Preconditions not met, clearing current action and goal".Log();
                        _currentAction = null;
                        _currentGoal = null;
                    }
                }
            }
            
            if (_actionPlan != null && _currentAction != null)
            {
                _currentAction.Update(Time.deltaTime);

                if (_currentAction.Complete)
                {
                    $"{_currentAction.Name} complete".Log();
                    _currentAction.Stop();
                    _currentAction = null;

                    if (_actionPlan.Actions.Count == 0)
                    {
                        "Plan complete".Log();
                        _lastGoal = _currentGoal;
                        _currentGoal = null;
                    }
                }
            }
        }

        private void SetupBeliefs()
        {
            _beliefs = new Dictionary<string, AgentBelief>();

            _beliefs.Add("Nothing", new AgentBelief.Builder("Nothing")
                    .WithCondition( () => false)
                    .Build());
            
            _beliefs.Add("AgentIdle", new AgentBelief.Builder("AgentIdle")
                .WithCondition(() => _entity.isMoving == false)
                .Build());
            _beliefs.Add("AgentMoving", new AgentBelief.Builder("AgentMoving")
                .WithCondition(() => _entity.isMoving)
                .WithLocation(() => _entity.MovementPoint)
                .Build());
        }

        private void SetupActions()
        {
            Actions = new HashSet<AgentAction>();

            Actions.Add(new AgentAction.Builder("Relax")
                .WithStrategy(new IdleStrategy(5))
                .AddEffect(_beliefs["Nothing"])
                .Build());

            Actions.Add(new AgentAction.Builder("Wander Around")
                .WithStrategy(new RoutingStrategy(_entity))
                .AddEffect(_beliefs["AgentMoving"])
                .Build());

            Actions.Add(new AgentAction.Builder("ChasePlayer")
                .WithStrategy(new MoveStrategy(_entity, () => _beliefs["PlayerInChaseRange"].Location))
                .AddPrecondition(_beliefs["PlayerInChaseRange"])
                .AddEffect(_beliefs["PlayerInAttackRange"])
                .Build());

            Actions.Add(new AgentAction.Builder("AttackPlayer")
                .WithStrategy(new AttackStrategy())
                .AddPrecondition(_beliefs["PlayerInAttackRange"])
                .AddEffect(_beliefs["AttackingPlayer"])
                .Build());
        }

        private void SetupGoals()
        {
            _goals = new HashSet<AgentGoal>();

            _goals.Add(new AgentGoal.Builder("Chill Out")
                .WithPriority(1)
                .WithDesiredEffect(_beliefs["Nothing"])
                .Build());

            _goals.Add(new AgentGoal.Builder("Wander")
                .WithPriority(1)
                .WithDesiredEffect(_beliefs["AgentMoving"])
                .Build());

            _goals.Add(new AgentGoal.Builder("SeekAndDestroy")
                .WithPriority(3)
                .WithDesiredEffect(_beliefs["AttackingPlayer"])
                .Build());
        }

        private void CalculatePlan()
        {
            var priorityLevel = _currentGoal?.Priority ?? 0;
            var goalsToCheck = _goals;
            
            if (_currentGoal != null)
            {
                "Current goal exists, checking goals with higher priority".Log();
                goalsToCheck = new HashSet<AgentGoal>(_goals.Where(g => g.Priority > priorityLevel));
            }

            var potentialPlan = _goapPlanner.Plan(this, goalsToCheck, _lastGoal);
            if (potentialPlan != null)
            {
                _actionPlan = potentialPlan;
            }
        }
    }
}