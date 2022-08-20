// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainerSetup.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using ConsoLovers.ConsoleToolkit.Core.DIContainer;

using FluentSetups;

using Microsoft.Extensions.DependencyInjection;

[FluentSetup(typeof(Container))]
internal partial class ContainerSetup : ServiceCollection
{

}