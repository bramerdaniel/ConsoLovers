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
Wen you are useing commands, you can now use the `IAsyncCommand` interface when you want to get invoked async.
Normal commands are ofcourse still supported.

```c#
public class DeleteUserCommand : IAsyncCommand<DeleteUserArgs>
{
   public async Task ExecuteAsync(CancellationToken cancellationToken)
   {
      return await usermanager.DeleteUserAsync(Arguments.UserName);
   }

   public DeleteUserArgs Arguments { get; set; }
}
```

### 2. Support nested commands to support more complex applications
To support commands lines like `usermanager.exe create user -name=Robert` you can now defined nested commands.   
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
Just implement this interface in your args class.

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
Like in the Consolovers.ToolKit there is now an improved InputBox class.   
The usage looks still the same.

```c#
int number = new InputBox<int>("Enter your number: ", 123).ReadLine();
```

# Version 3.0.0
