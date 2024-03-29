﻿using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Redbridge.ApiManagement
{
    public abstract class ApiCall : IApiCall
    {
        protected ILogger Logger { get; }

        protected ApiCall(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual string ApiName
        {
            get
            {
                var type = GetType();
                var apiAttribute = type.GetCustomAttribute<ApiAttribute>();
                return apiAttribute == null ? GetType().Name : string.IsNullOrWhiteSpace(apiAttribute.Name) ? type.Name : apiAttribute.Name;
            }
        }

        public virtual bool RequiresAuthentication => true;
		public virtual string RequiredAction => null;
	}
}
