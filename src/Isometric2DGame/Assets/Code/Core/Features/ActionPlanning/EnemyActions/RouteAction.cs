using System.Collections;
using Code.Infrastructure;
using UnityEngine;

namespace Code.Core.Features.ActionPlanning.EnemyActions
{
    public class RouteAction : Action
    {
        private ICoroutineRunner _coroutineRunner;
        private bool _waited;
        
        public RouteAction(GameEntity entity, string name, ICoroutineRunner coroutineRunner) : base(entity, name)
        {
            _coroutineRunner = coroutineRunner;
        }

        public override void Start()
        {
            _waited = false;
            _coroutineRunner.StartCoroutine(Wait());
        }

        protected override float UpdateWeightInternal(GameEntity entity)
        {
            var hasPlayer = entity.isPlayerInRadius ? 0 : 0.5f;
            
            return hasPlayer;
        }
        
        protected override bool IsCompleteInternal(GameEntity entity) => _waited;

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(4);
            _waited = true;
        }
    }
}