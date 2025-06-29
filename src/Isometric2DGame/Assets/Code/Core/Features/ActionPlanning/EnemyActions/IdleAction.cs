using System.Collections;
using Code.Infrastructure;
using UnityEngine;

namespace Code.Core.Features.ActionPlanning.EnemyActions
{
    public class IdleAction : Action
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private bool _waited;

        public IdleAction(GameEntity entity, string name, ICoroutineRunner coroutineRunner) : base(entity, name)
        {
            _coroutineRunner = coroutineRunner;
        }

        public override void Start()
        {
            _waited = false;
            _coroutineRunner.StartCoroutine(Wait());
        }

        protected override float UpdateWeightInternal(GameEntity entity) => Random.value;
        protected override bool IsCompleteInternal(GameEntity entity) => _waited;

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(1);
            _waited = true;
        }
    }
}