// Copyright © 2024 Olaf Meyer
// Copyright © 2024 scotec Software Solutions AB, www.scotec-software.com
// This file is licensed to you under the MIT license.

using Autofac;
using Revit.Tutorial.Views;

namespace Revit.Tutorial.Modules;

public class ViewModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<AddInInfoWindow>()
               .InstancePerDependency();
    }
}
