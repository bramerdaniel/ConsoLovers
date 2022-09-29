## The ConsoLovers.ConsoleToolkit ![GitHub](https://img.shields.io/github/license/bramerdaniel/ConsoLovers?style=plastic) ![Nuget](https://img.shields.io/nuget/dt/ConsoLovers.ConsoleToolkit?style=plastic)
The ConsoLovers.ConsoleToolkit(.Core) is a collection of several independant tools that can be used to write console applications (better and faster).


Check out he [Wiki](https://github.com/bramerdaniel/ConsoLovers/wiki) for detailed support of the most features.   
To get an idea of the newest features and chages, have a look at the [latest changes](LatestChanges.md)

## Main features are:
* Commandline parsing, mapping and execution engine
* Customizable execution pipeline with many build in features like, exception and exit code handling  
* Customizable dependency injection
* A Customizable [ConsoleMenu](https://github.com/bramerdaniel/ConsoLovers/wiki/ConsoleMenu) that supports Keyboard and Mouse selection.
* A [InputBox](https://github.com/bramerdaniel/ConsoLovers/wiki/InputBox) class that can be used for password scenarios.




## Usage

The toolkit is also available as nuget package. Eee the links below.

Package  | Current version | Build | Content
-------- | -------- | -------- | --------
ConsoLovers.Toolkit.Core   | ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/ConsoLovers.ConsoleToolkit.Core?style=plastic) | [![ConsoLovers.Toolkit.Core](https://github.com/bramerdaniel/ConsoLovers/actions/workflows/build-Toolkit.Core.yml/badge.svg)](https://github.com/bramerdaniel/ConsoLovers/actions/workflows/build-Toolkit.Core.yml) | command line handling, execution engine
ConsoLovers.Toolkit   | ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/ConsoLovers.ConsoleToolkit?style=plastic) | [![Consolovers.Toolkit](https://github.com/bramerdaniel/ConsoLovers/actions/workflows/build-Toolkit.yml/badge.svg)](https://github.com/bramerdaniel/ConsoLovers/actions/workflows/build-Toolkit.yml) | ConsoleMenu

## Related packages

If you nee inter-process communication between multiple processes, you should have check out the packages below

Package  | Version | Description
-------- | -------- | --------
ConsoLovers.Ipc.Server   | [![NuGet version (ConsoLovers.Ipc.Server)](https://img.shields.io/nuget/v/ConsoLovers.Ipc.Server.svg?style=flat)](https://www.nuget.org/packages/ConsoLovers.Ipc.Server/) | Package for the process that hosts the gRPC server
ConsoLovers.Ipc.Client   | [![NuGet version (ConsoLovers.Ipc.Client)](https://img.shields.io/nuget/v/ConsoLovers.Ipc.Client.svg?style=flat)](https://www.nuget.org/packages/ConsoLovers.Ipc.Client/)  | Package for a client process that wants to communicate with a server 
ConsoLovers.Ipc.ProcessMonitoring.Server   | [![NuGet version (ConsoLovers.Ipc.ProcessMonitoring.Server)](https://img.shields.io/nuget/v/ConsoLovers.Ipc.ProcessMonitoring.Server.svg?style=flat)](https://www.nuget.org/packages/ConsoLovers.Ipc.ProcessMonitoring.Server/) | Server package for a process that should be monitored
ConsoLovers.Ipc.ProcessMonitoring.Client   | [![NuGet version (ConsoLovers.Ipc.ProcessMonitoring.Client)](https://img.shields.io/nuget/v/ConsoLovers.Ipc.ProcessMonitoring.Client.svg?style=flat)](https://www.nuget.org/packages/ConsoLovers.Ipc.ProcessMonitoring.Client/)  | Client package for applications that want to monitor processes hosting the ConsoLovers.Ipc.ProcessMonitoring.Server package services

### Contribution
Feel free to create a pull request to include new features. 
For development Visual Studio 2022 is required. (or any tool for compiling C#10)

### License
The software is licensed under the [MIT](LICENSE) license.
