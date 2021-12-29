using System;

namespace TMTTimeKeeper.Helpers
{
    public class LogicExeption : Exception
    {
        public LogicExeption()
        {
        }

        public LogicExeption(string message)
            : base(message)
        {
        }

        public LogicExeption(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
