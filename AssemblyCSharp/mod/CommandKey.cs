using AssemblyCSharp.mod.functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp.mod
{

    [AttributeUsage(AttributeTargets.Method)]
    public class KeyMethod : Attribute
    {
        private string[] names;
        public string[] Names => names;

        public KeyMethod(params string[] names)
        {
            this.names = names;
        }
    }
    public class CommandKey
    {
        public CommandKey()
        {
            
        }
        public static bool Autorun(int key)
        {
            switch (key)
            {
                case 'b':
                    callmethod("csdb");
                    break; 
                case 'd':
                    callmethod("dapdo");
                    break;
                case 'q':
                    callmethod("exitall");
                     break;
                case 'p':
                    callmethod("sd");
                    break;
                case 'm':
                    callmethod("menu");
                    break;
            }
            return false;
        }
        public static void callmethod(string attributeName, params object[] parameters)
        {
            GameScr.info1.addInfo($"Quang quằng quoại", 0);

            Type functionsChatType = typeof(usedKey);

            var methods = functionsChatType.GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(KeyMethod), false)
                .OfType<KeyMethod>()
                .Any(attr => attr.Names.Contains(attributeName)))
                .ToList();

            if (methods.Count > 0)
            {
                foreach (var method in methods)
                {
                    try
                    {
                        method.Invoke(null, parameters);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
