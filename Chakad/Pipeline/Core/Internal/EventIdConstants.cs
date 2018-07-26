using Chakad.Logging;

namespace Chakad.Pipeline.Core.Internal
{
    internal class EventIdConstants
    {
        #region 
        public static EventId InitialiingChakad = new EventId(1000, "InitialiingChakad");
        public static EventId ChakadInitilizingTypes = new EventId(1001, "ChakadInitilizingTypes");
        public static EventId ChakadSetOrder= new EventId(1002, "ChakadSetOrder");
        public static EventId ChakadSetContainer= new EventId(1003, "ChakadSetContainer");
        public static EventId ChakadSetDefaultContainer= new EventId(1004, "ChakadSetDefaultContainer");
        public static EventId ChakadLoggerFactory= new EventId(1005, "ChakadLoggerFactory");
        public static EventId ChakadSetLoggerFactory= new EventId(1006, "ChakadSetLoggerFactory");
        public static EventId ChakadSetDefaultLoggerFactory= new EventId(1007, "ChakadSetDefaultLoggerFactory");
        public static EventId ChakadSetLoggerProvider= new EventId(1008, "ChakadSetLoggerProvider");
        public static EventId ChakadSetDefaultLoggerProvider= new EventId(1009, "ChakadSetDefaultLoggerProvider");

        #endregion
        #region Commands
        public static EventId CommandStartProcess = new EventId(2000, "CommandStartProcess");
        public static EventId CommandBaseTypeIsEmpty = new EventId(2001, "CommandBaseTypeIsEmpty");
        public static EventId CommandNotFounddHandler = new EventId(2002, "CommandNotFounddHandler");
        public static EventId CommandInitializeCircuteBreaker = new EventId(2003, "CommandInitializeCircuteBreaker");
        public static EventId CommandStartInvokinMessageHandle = new EventId(2004, "CommandStartInvokinMessageHandle");
        public static EventId CommandInvokingMessageHandleWasFaield = new EventId(2005, "CommandInvokingMessageHandleWasFaield");
        public static EventId CommandInvokingMessageHandleWasSuccessfully = new EventId(2006, "CommandInvokingMessageHandleWasSuccessfully");
        #endregion

        #region Events
        public static EventId SubscribeToEvent = new EventId(3000, "SubscribeToEvent");
        public static EventId UnSubscribeFromEvent = new EventId(3001, "UnSubscribeFromEvent  ");
        public static EventId StartPublishingEvent = new EventId(3002, "StartPublishingEvent");

        #endregion
        #region Query
        public static EventId QueryStartProcess = new EventId(4000, "QueryStartProcess");
        public static EventId QueryBaseTypeIsEmpty = new EventId(4001, "QueryBaseTypeIsEmpty");
        public static EventId QueryNotFoundHandler = new EventId(4002, "QueryNotFoundHandler");
        public static EventId QueryInitializeCircuteBreaker= new EventId(4003, "QueryInitializeCircuteBreaker");
        public static EventId QueryStartInvokinMessageHandle= new EventId(2004, "QueryStartInvokinMessageHandle");
        public static EventId QueryInvokingMessageHandleWasFaield = new EventId(4005, "QueryInvokingMessageHandleWasFaield");
        public static EventId QueryInvokingMessageHandleWasSuccessfully = new EventId(4006, "QueryInvokingMessageHandleWasSuccessfully");
        #endregion


    }
}
