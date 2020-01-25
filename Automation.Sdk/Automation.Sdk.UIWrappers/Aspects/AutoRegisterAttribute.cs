namespace Automation.Sdk.UIWrappers.Aspects
{
    using System;
    using Automation.Sdk.UIWrappers.Services.Container;

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegisterAttribute : Attribute
    {
        /// <summary>
        /// Name of the interface to be registered
        /// Use empty to register as first implemented interface
        /// </summary>
        public Type Interface;

        /// <summary>
        /// Name of the registration. Can be left blank
        /// </summary>
        public object RegistrationName;

        /// <summary>
        /// Type of the registration
        /// </summary>
        public RegistrationType RegistrationType = RegistrationType.Singletone;
    }
}