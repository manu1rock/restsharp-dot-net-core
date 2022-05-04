using BoDi;
using System;
using System.Collections.Generic;
using System.Globalization;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.Tracing;
using TechTalk.SpecFlow.UnitTestProvider;

namespace CustomTracer.SpecflowPlugin
{
    public class CustomTracerPlugin : IRuntimePlugin
    {
        public void RegisterDependencies(ObjectContainer container)
        {

        }
        /*
        public void RegisterCustomizations(ObjectContainer container, RuntimeConfiguration runtimeConfiguration)
        {
            container.RegisterTypeAs<CustomTracer, ITestTracer>();
        }

        public void RegisterConfigurationDefaults(RuntimeConfiguration runtimeConfiguration)
        {

        }
        */

        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomTracer : ITestTracer
    {
        // Your implementation here
        public void TraceBindingError(BindingException ex)
        {
            throw new NotImplementedException();
        }

        public void TraceDuration(TimeSpan elapsed, IBindingMethod method, object[] arguments)
        {
            throw new NotImplementedException();
        }

        public void TraceDuration(TimeSpan elapsed, string text)
        {
            throw new NotImplementedException();
        }

        public void TraceError(Exception ex, TimeSpan duration)
        {
            throw new NotImplementedException();
        }

        public void TraceNoMatchingStepDefinition(StepInstance stepInstance, ProgrammingLanguage targetLanguage, CultureInfo bindingCulture, List<BindingMatch> matchesWithoutScopeCheck)
        {
            throw new NotImplementedException();
        }

        public void TraceStep(StepInstance stepInstance, bool showAdditionalArguments)
        {
            throw new NotImplementedException();
        }

        public void TraceStepDone(BindingMatch match, object[] arguments, TimeSpan duration)
        {
            throw new NotImplementedException();
        }

        public void TraceStepPending(BindingMatch match, object[] arguments)
        {
            throw new NotImplementedException();
        }

        public void TraceStepSkipped()
        {
            throw new NotImplementedException();
        }

        public void TraceWarning(string text)
        {
            throw new NotImplementedException();
        }
    }
}
