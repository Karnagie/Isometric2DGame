using RSG;

namespace Code.Infrastructure.States.StateInfrastructure
{
    public class EndOfFrameExitState : IState, IUpdateable, IFixedUpdateable
    {
        private Promise _exitPromise;
        
        private bool ExitWasRequested => _exitPromise != null;

        public virtual void Enter() { }

        protected virtual void ExitOnEndOfFrame() { }

        protected virtual void OnUpdate() { }
        protected virtual void OnFixedUpdate() { }

        IPromise IExitableState.BeginExit()
        {
            _exitPromise = new Promise();
            return _exitPromise;
        }


        void IExitableState.EndExit()
        {
            ExitOnEndOfFrame();
            ClearExitPromise();
        }

        void IUpdateable.Update()
        {
            if (!ExitWasRequested)
                OnUpdate();
            
            if(ExitWasRequested)
                ResolveExitPromise();
        }

        public void FixedUpdate()
        {
            if (!ExitWasRequested)
                OnFixedUpdate();
        }

        private void ResolveExitPromise()
        {
            _exitPromise?.Resolve();
        }

        private void ClearExitPromise()
        {
            _exitPromise = null;
        }
    }
}