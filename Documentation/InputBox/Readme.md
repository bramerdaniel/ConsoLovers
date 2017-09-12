## InputBox

```csharp
   
   // The InputBox class can be used to get user input from the console
   // This class also supports password input (masked with a character like '*')
   var password = new InputBox<string>("Enter password : ") { IsPassword = true }.ReadLine();
```
