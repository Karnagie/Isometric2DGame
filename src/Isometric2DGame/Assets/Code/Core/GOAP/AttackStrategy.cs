namespace Code.Core.GOAP
{
    public class AttackStrategy : IActionStrategy 
    {
        public bool CanPerform => true;
        public bool Complete { get; private set; }

        private readonly CountdownTimer _timer;

        public AttackStrategy() 
        {
            _timer = new CountdownTimer(1f);
            _timer.OnTimerStart += () => Complete = false;
            _timer.OnTimerStop += () => Complete = true;
        }
    
        public void Start() 
        {
            _timer.Start();
        }
    
        public void Update(float deltaTime) => _timer.Tick(deltaTime);
    }
}