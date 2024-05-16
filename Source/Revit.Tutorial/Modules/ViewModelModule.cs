// Copyright © 2024 Olaf Meyer
// Copyright © 2024 scotec Software Solutions AB, www.scotec-software.com
// This file is licensed to you under the MIT license.

using Autofac;
using Revit.Tutorial.ViewModels;

namespace Revit.Tutorial.Modules;

public class ViewModelModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<AddInInfoViewModel>()
               .InstancePerDependency();
    }
}
