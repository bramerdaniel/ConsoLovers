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
To support command lines like `usermanager.exe create user -name=Robert` you can now defined nested commands.   
This would look like this.

```c#
// Parent command arg class
public class ApplicationArgs
{
   [Command("create")]
   internal CommandGroup<CreateArgs> Create { get; set; } 
   
   [Command("delete")]
   internal CommandGroup<DeleteArgs> Delete { get; set; }   

   [Command("modify")]
   internal CommandGroup<ModifyArgs> Modify{ get; set; }  
}

// child command arg class
public class CreateArgs
{
   [Command("role")]
   public CreateRoleCommand Role { get; set; }

   [Command("user")]
   public CreateUserCommand User { get; set; }
}

```



### 3. Support for internal and private argument classes and properties

Now argument classes like this are possible
```c#
internal class DeleteUserArgs
{
   [Argument("username", "user")]
   internal string UserName { get; private set; }
}
```

### 4. Easy help text header customisation with the ICustomizedHeader interface

To simplify the customization of a command header in the help, I introduzed the `ICustomizedHeader` interface.   
Just umplement this interface in your args class.

```c#
public class SomeArgs : ICustomizedHeader
{
   public void WriteHeader(IConsole console)
   {
      console.WriteLine("HELP FOR THE CREATE COMMAND");
      console.WriteLine();
   }
}
```

### 5. Added InputBox from console toolkit 

# Version 3.0.0
