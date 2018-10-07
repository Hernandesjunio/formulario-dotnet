using Formulario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Class
{
    public class CompositeCommand : ICommandPattern
    {
        List<ICommandPattern> lst = new List<ICommandPattern>();

        public void AddCommand(ICommandPattern cmd)
        {
            lst.Add(cmd);
        }

        public void Execute()
        {
            foreach (var item in lst)
            {
                item.Execute();
            }

            lst.Clear();
        }
    }

    public class CommandImpl : ICommandPattern
    {
        Action callback = () => { };
        public CommandImpl(Action callback)
        {
            this.callback = callback;
        }
        public void Execute()
        {
            callback();
        }
    }
}
