using Microsoft.AspNetCore.Mvc;

namespace WebApp.CommandPattern.Commands
{
    public class FileCreateInvoker
    {
        private ITableActionCommand _actionCommand;
        private List<ITableActionCommand> _commands = new List<ITableActionCommand>();

        public void SetCommand(ITableActionCommand actionCommand)
        {
            _actionCommand = actionCommand;
        }

        public void AddCommend(ITableActionCommand tableActionCommand)
        {
            _commands.Add(tableActionCommand);
        }

        public IActionResult CreateFile()
        {
            // Loglama kayıtları buradan alınabilir.
            return _actionCommand.Execute();
        }

        public List<IActionResult> CreateFiles()
        {
            // Loglama kayıtları buradan alınabilir.
            return _commands.Select(x=>x.Execute()).ToList();
        }
    }
}
