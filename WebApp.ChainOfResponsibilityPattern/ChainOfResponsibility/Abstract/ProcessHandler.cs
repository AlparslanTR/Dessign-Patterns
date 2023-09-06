namespace WebApp.ChainOfResponsibilityPattern.ChainOfResponsibility.Abstract
{
    public abstract class ProcessHandler : IProcessHandler
    {
        private IProcessHandler _processHandler;

        public virtual object handle(object o)
        {
            if (_processHandler is not null)
            {
                return _processHandler.handle(o);
            }
            return null;
        }

        public IProcessHandler SetNext(IProcessHandler processHandler)
        {
           _processHandler = processHandler;
            return _processHandler;
        }
    }
}
