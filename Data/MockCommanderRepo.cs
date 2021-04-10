using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class  MockCommanderRepo : ICommanderRepo
    {
        public void CreatCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>();
            commands.Add(new Command{Id = 1 , HowTo = "1" , Line = "1" , Platform = "1"});
            commands.Add(new Command{Id = 2 , HowTo = "2" , Line = "2" , Platform = "2"});
            commands.Add(new Command{Id = 3 , HowTo = "3" , Line = "3" , Platform = "3"});
            return commands;
        }
        
        public Command GetCommandById(int Id)
        {
          return new Command{Id = 0 , HowTo = "a" , Line = "b" , Platform = "c"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}