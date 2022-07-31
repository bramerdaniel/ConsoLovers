# Version 4.0.0
### 1. Support for async application and commands

You can now start your application async, to be able to use async apis easier.

```c#
   public static async Task Main()
   {
      var app = await ConsoleApplicationManager
         .For<PlaygroundApp>()
         .RunAsync(CancellationToken.None);
   }
```
You can now user the IAsyncCommand interface when you are working with commands.


```c#
public class DeleteUserCommand : IAsyncCommand<DeleteUserArgs>
{
   public asynnc Task ExecuteAsync(CancellationToken cancellationToken)
   {
      return await usermanager.DeleteUserAsync(Arguments.UserName);
   }

   public DeleteUserArgs Arguments { get; set; }
}
```

### 2. Support nested commands to support more complex applications
### 3. Support for internal and private argument classes and properties

Now argument classes like this are possible
```c#
internal class DeleteUserArgs
{
   [Argument("username", "user")]
   internal string UserName { get; private set; }
}
```

### 4. Added InputBox from console toolkit 

# Version 3.0.0
