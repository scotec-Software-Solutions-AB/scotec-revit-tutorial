// Copyright © 2024 Olaf Meyer
// Copyright © 2024 scotec Software Solutions AB, www.scotec-software.com
// This file is licensed to you under the MIT license.

using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Scotec.Revit;

namespace Revit.Tutorial.Commands;

[Scotec.Revit.Isolation.RevitCommandAvailabilityIsolation]
public class TestCommandAvailability : RevitCommandAvailability
{
    protected override bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories,
                                               IServiceProvider services)
    {
        return true;
    }
}
