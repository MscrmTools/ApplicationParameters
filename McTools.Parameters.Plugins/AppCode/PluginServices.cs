using System;
using Microsoft.Xrm.Sdk;

namespace McTools.Parameters.Plugins.AppCode
{
    public class PluginServices
    {
        #region Variables

        /// <summary>
        /// Service factory
        /// </summary>
        private readonly IOrganizationServiceFactory _serviceFactory;

        /// <summary>
        /// Tracing service
        /// </summary>
        private readonly ITracingService _tracingService;

        /// <summary>
        /// Plugin Execution Context
        /// </summary>
        private readonly IPluginExecutionContext _executionContext;


        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class PluginServices
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        public PluginServices(IServiceProvider serviceProvider)
        {
            // Instanciation des services
            _executionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            _serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            _tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets Plugin execution context
        /// </summary>
        public IPluginExecutionContext Context
        {
            get { return _executionContext; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the organization service
        /// </summary>
        /// <param name="getAdminService">Indicates if the service must be instantiated as the service account</param>
        /// <returns>Organization service</returns>
        public IOrganizationService GetIOrganizationService(bool getAdminService)
        {
            return getAdminService
                       ? _serviceFactory.CreateOrganizationService(null)
                       : _serviceFactory.CreateOrganizationService(_executionContext.UserId);
        }

        /// <summary>
        /// Add a message in the trace
        /// </summary>
        /// <param name="message">Message to add</param>
        public void Trace(string message)
        {
            _tracingService.Trace(message);
        }

        #endregion Methods
    }
}
