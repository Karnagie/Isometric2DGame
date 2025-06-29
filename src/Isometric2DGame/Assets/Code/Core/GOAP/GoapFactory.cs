namespace Code.Core.GOAP
{
    public class GoapFactory : IGoapFactory
    {
        public IGoapPlanner CreatePlanner() 
        {
            return new GoapPlanner();
        }
    }
}