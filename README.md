# Chakad.Pipeline4Monolith
Chakad is a lightweight Message Pipeline for .NET monolith solutions

You have a monolith solutions in dot net. It works very well for a while, You dont have any problems.
But after a while, you see that buisiness use case, will be propageted to everywhere, and so manage and maintain of 
this monolith will be a pain.

Now you know the advantage of Sending command and Publishing Events using a message bus or a message broker such as NServiceBus
or MassTransit, but for many reason you dont want and also you can not using this Magic library.
It is the time to Using Chakad.Pipeline4Monolith.

In your monolith, you can easily declare you command or event, and some handlers for your commands and events.

In the bootstraping and at the stratup Chakad.Pipeline4Monolith can easily scan bin path, and resolve every commands and events and 
message handlers.

You can easily send a command to a command handler, while there is no any coupling betwwen command and its handler,
```
Task<TOut> Send<TOut>(IRequest<TOut> command, TimeSpan? timeout = null,
            TaskScheduler _taskScheduler = null, SendOptions options = null) where TOut : RequestResult;
```         
```
Chakad.Pipeline()
```
get your command, resolve its handler and invoke its
```
Execute()
```
methods, and then return command result to you.

Plus, you can publish event using the Chakad.Pipeline4Monolith,so if ther is any subscriber for this event, Chakad.Pipeline4Monolith
invoke all of them...

1- First of you should configure the Chakad.Pipeline4Monolith
```
            Configure.With("your path");
```
2- For event subscribers you can specify the orders for invoking subscribers:
 ```
     Configure.With("your path").Order(typeof(YourEvent),list of ordered subscribers);
 ```
3- At any time(run time) you can easily Unsubscriber any event subscribers
   ```
         new MyEventSubscriber().ReluctanceTo(typeof(MyDomainEvent));
```            
-----------------------------------------------------
Mr Udi Dahan :) Thank you very much 
I use some idea behind NServiceBus...

I thank my valuable colleagues at http://BaridSoft.Ir , I have learned some ideas in the resulting section from theirs

And I shuld say, the core idea of the Chakad.Pipeline4Monolith were implemented by Mahyar(one of my colleagues) and me,
So I thank Mahyar a lot
This is the first version of Chakad.Pipeline4Monolith, and It is under work for future improvment.
So bu tune...
