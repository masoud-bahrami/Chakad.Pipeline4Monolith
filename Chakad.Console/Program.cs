using System;
using Chakad.Bootstraper;
using Chakad.MessageHandler.EventSubscribers;
using Chakad.Messages.Command;
using Chakad.Messages.Events;
using Chakad.Messages.Query;
using Chakad.Pipeline;
using Chakad.Pipeline.Core;
using Chakad.Pipeline.Core.Exceptions;

namespace Chakad.Console
{
    class Program
    {
        private static IPipeline Pipeline
        {
            get {return ChakadServiceBus.Pipeline; }
        }

        static void Main()
        {
            System.Console.Title = "Chakad ChakadMessage Bus Simulations";

            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Starting .....");
            System.Console.ForegroundColor = ConsoleColor.White;

            ChakadBootstraper.Run();

            //Pipeline.Send(new GetOutgoingFaxQuery());

            System.Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.WriteLine("Send Activity Command ");
            try
            {
                Pipeline.Send(new StartActivityCommand1());
            }
            catch (ChakadPipelineNotFoundHandler exHandler)
            {
                System.Console.WriteLine(exHandler);
            }
            

            var res = Pipeline.Send(new StartActivityCommand
            {
                Message = "",
                Id = Guid.NewGuid()
            },new TimeSpan(0,0,0,1));

            if (res.Exception != null)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("ChakadPipelineTimeoutException");
                System.Console.WriteLine(res.Exception);


                System.Console.ForegroundColor = ConsoleColor.Magenta;
            }

            res = Pipeline.Send(new StartActivityCommand
            {
                Message = "",
                Id = Guid.NewGuid()
            });

            PrintBreakeLine(2);

            System.Console.ForegroundColor = ConsoleColor.Blue;

            System.Console.WriteLine("Raise MyDomainevent. It should be received By subscribers in unorder manner");
            var res1 = Pipeline.Publish(new MyDomainEvent
            {
                FirstName = "Mahyar",
                LastName = "Mehrnoosh"
            });

            PrintBreakeLine(2);
            System.Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.WriteLine("Try to set order MyDomainEvent's Subscribers");
            ChakadBootstraper.ReorderEvents();

            ChakadServiceBus.Pipeline.Publish(new MyDomainEvent
            {
                FirstName = "Mahyar",
                LastName = "Mehrnoosh"
            });
            PrintBreakeLine(2);
            System.Console.ForegroundColor = ConsoleColor.DarkYellow;
            System.Console.WriteLine("Try to unsubscribe MyDomainEventHandler 0");
            new MyEventSubscriber().ReluctanceTo(typeof(MyDomainEvent));
            ChakadServiceBus.Pipeline.Publish(new MyDomainEvent
            {
                FirstName = "Mahyar",
                LastName = "Mehrnoosh"
            });
            
            System.Console.ReadKey();
        }

        private static void PrintBreakeLine(int count)
        {
            for (int i = 0; i < count; i++)
            {
                System.Console.WriteLine();
            }
        }
    }
}
