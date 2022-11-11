
using LearnMUSIC.Common;
using System;

namespace LearnMUSIC.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public int CurrentYear => DateTime.Now.Year;

        public DateTime Now => DateTime.Now;

        public DateTime Today => DateTime.Today;
    }
}
