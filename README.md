## The ConsoLovers.ConsoleToolkit ![GitHub](https://img.shields.io/github/license/bramerdaniel/ConsoLovers?style=plastic)
The ConsoLovers.ConsoleToolkit(.Core) is a collection of several independant tools that can be used to write console applications (better and faster).

Check out he [Wiki](https://github.com/bramerdaniel/ConsoLovers/wiki) for detailed support of the most features.   
To get an idea of the newest features and chages, have a look at the [latest changes](LatestChanges.md)

### Main features are:
* Commandline parser and execution engine
* Customizable execution pipeline with many build in features like, exception and exit code handling  
* Customizable dependency injection
* A [ConsoleMenu](https://github.com/bramerdaniel/ConsoLovers/wiki/ConsoleMenu) that supports Keyboard and Mouse selection.
  The menu is extremely customizable, supports color themes, index navigation and much more...
* A CommandLineEngine that offers parsing of command line arguments and mapping them to properties of a class 
* A [InputBox](https://github.com/bramerdaniel/ConsoLovers/wiki/InputBox) class that can be used for password scenarios.




### Usage

The toolkit is also available as nuget package. Eee the links below.

Package  | Current version | Build
-------- | -------- | --------
ConsoLovers.Toolkit.Core   | ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/ConsoLovers.ConsoleToolkit.Core?style=plastic) | [![ConsoLovers.Toolkit.Core](https://github.com/bramerdaniel/ConsoLovers/actions/workflows/build-Toolkit.Core.yml/badge.svg)](https://github.com/bramerdaniel/ConsoLovers/actions/workflows/build-Toolkit.Core.yml)
ConsoLovers.Toolkit   | ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/ConsoLovers.ConsoleToolkit?style=plastic) | [![Consolovers.Toolkit](https://github.com/bramerdaniel/ConsoLovers/actions/workflows/build-Toolkit.yml/badge.svg)](https://github.com/bramerdaniel/ConsoLovers/actions/workflows/build-Toolkit.yml)

If a third party reference is not possible in your project, feel free to download the sources 
and just pick the classes you need (this should work in the most cases without any problems). 

### Contribution
Feel free to create a pull request to include new features. 
For development Visual Studio 2017 is required. (or any tool for compiling C#7)

### License
The software is licensed under the [MIT](LICENSE) license.
