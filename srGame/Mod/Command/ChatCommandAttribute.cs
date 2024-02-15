using System.Linq;
using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ChatCommandAttribute : Attribute
{
    public string[] commands;

    public ChatCommandAttribute(params string[] commands)
    {
        this.commands = commands.Where(x => !string.IsNullOrEmpty(x)).ToArray();
    }
}
