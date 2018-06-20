using System;

[assembly: WebActivator.PreApplicationStartMethod(
    typeof(NNZFSC.App_Start.MySuperPackage), "PreStart")]

namespace NNZFSC.App_Start {
    public static class MySuperPackage {
        public static void PreStart() {
            MVCControlsToolkit.Core.Extensions.Register();
            System.Web.Mvc.GlobalFilters.Filters.Add(new MVCControlsToolkit.ActionFilters.PlaceJavascriptAttribute());
        }
    }
}