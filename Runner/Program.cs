using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using ProcessManager;
using ProcessManager.Messages;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("examples");

            var creditBureau = actorSystem.ActorOf(CreditBureau.DefaultProps(), "creditBureau");
            var bank1 = actorSystem.ActorOf(Bank.DefaultProps("bank1", 2.75, 0.30), "bank1");
            var bank2 = actorSystem.ActorOf(Bank.DefaultProps("bank2", 2.73, 0.31), "bank2");
            var bank3 = actorSystem.ActorOf(Bank.DefaultProps("bank3", 2.80, 0.29), "bank3");
            var loanBroker = actorSystem.ActorOf(LoanBroker.DefaultProps(creditBureau, new[] {bank1, bank2, bank3}), "loanBroker");
            loanBroker.Tell(new QuoteBestLoanRate("111-11-1111", 100000, 84));
            Console.ReadLine();
        }
    }
}
