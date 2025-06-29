namespace Code.Core.Features.ActionPlanning.EnemyActions
{
    public abstract class Action
    {
        private readonly GameEntity _entity;

        public string Name { get; }
        public float Weight { get; private set; }
        public bool Complete => IsCompleteInternal(_entity);

        protected Action(GameEntity entity, string name)
        {
            _entity = entity;
            Name = name;
        }

        public void UpdateWeight() => Weight = UpdateWeightInternal(_entity);

        public virtual void Start() { }
        protected abstract float UpdateWeightInternal(GameEntity entity);
        protected abstract bool IsCompleteInternal(GameEntity entity);
    }
}