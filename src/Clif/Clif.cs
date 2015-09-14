using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Clif
{
    public class Clif
    {
        public Clif()
        {
            RegisterCommands();
        }

        protected List<Command> Commands { get; set; }

        protected IEnumerable<Type> Modules
        {
            get
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                return assemblies.SelectMany(a => a.GetTypes().Where(x => x.IsSubclassOf(typeof(CommandModule))));
            }
        }

        protected void RegisterCommands()
        {
            foreach (var module in Modules)
            {
                var instance = Activator.CreateInstance(module) as CommandModule;
                Commands = instance.Commands;
            }
        }

        private ExpandoObject DictionaryToExpando(IDictionary<string, object> dictionary)
        {
            var expando = new ExpandoObject();
            var expandoDictionary = (ICollection<KeyValuePair<string, object>>)expando;

            foreach(var kvp in dictionary)
            {
                expandoDictionary.Add(kvp);
            }

            return expando;
        }

        public void Resolve(string[] args)
        {
            foreach (var command in Commands)
            {
                var result = command.Match(args);
                if (result.Matches)
                {
                    command.Action.Invoke(DictionaryToExpando(result.CapturedArguments), DictionaryToExpando(result.CapturedOptions));
                    return;
                }
            }
            Console.WriteLine($"No Match! {string.Join(" ", args)}");
        }
    }
}
