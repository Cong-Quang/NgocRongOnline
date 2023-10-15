using AssemblyCSharp.mod.functions;
using System;
using System.Linq;
namespace AssemblyCSharp.mod
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ChatMethod : Attribute
    {
        private string[] names;
        public string[] Names => names;

        public ChatMethod(params string[] names)
        {
            this.names = names;
        }
    }
    public static class CommandChat
    {
        public static bool Autorun(string text)
        {
            string[] data = text.Split(' ');
            if (data[0] == "q") 
            {
                switch (data.Length)
                {
                    case 2:
                        callmethod(data[1]);
                        break;
                    case 3:
                        callmethod(data[1], data[2]);
                        break;
                    case 4:
                        callmethod(data[1], data[2], data[3]);
                        break;
                    case 5:
                        callmethod(data[1], data[2], data[3], data[4]);
                        break;
                    case 6:
                        callmethod(data[1], data[2], data[3], data[4], data[5]);
                        break;
                    case 7:
                        callmethod(data[1], data[2], data[3], data[4], data[5], data[6]);
                        break;
                }
                return true;
            }
            return false;
        }
        public static void callmethod(string attributeName, params object[] parameters)
        {
            Type functionsChatType = typeof(usedChat);

            var methods = functionsChatType.GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(ChatMethod), false)
                .OfType<ChatMethod>()
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
