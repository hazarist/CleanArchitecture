using CelanArchitecture.Core.ServiceWrapper;
using CelanArchitecture.ServiceProcessors;
using Microsoft.AspNetCore.Http;
using System;

namespace CelanArchitecture.Core.ServiceProcessors
{
    public class ServiceProcessor : IServiceProcessor
    {

        private readonly IHttpContextAccessor httpContextAccessor;
        public ServiceProcessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public ServiceResponse Call<TResult>(Func<TResult> action)
        {
            object result = null;
            Exception exception = null;
            try
            {
                result = action();
            }
            catch (Exception e)
            {
                exception = e;
            }

            return CreateResponse(result, exception);
        }

        public ServiceResponse Call<T, TResult>(Func<T, TResult> action, T parameter)
        {
            object result = null;
            Exception exception = null;
            try
            {
                result = action(parameter);
            }
            catch (Exception e)
            {
                exception = e;
            }

            return CreateResponse(result, exception);
        }

        public ServiceResponse Call<T, T2, TResult>(Func<T, T2, TResult> action, T parameter, T2 parameter2)
        {
            object result = null;
            Exception exception = null;
            try
            {
                result = action(parameter, parameter2);
            }
            catch (Exception e)
            {
                exception = e;
            }

            return CreateResponse(result, exception);
        }

        public ServiceResponse Call<T, T2, T3, TResult>(Func<T, T2, T3, TResult> action, T parameter, T2 parameter2, T3 parameter3)
        {
            object result = null;
            Exception exception = null;
            try
            {
                result = action(parameter, parameter2, parameter3);
            }
            catch (Exception e)
            {
                exception = e;
            }

            return CreateResponse(result, exception);
        }


        public ServiceResponse Call<T>(Action<T> action, T parameter)
        {
            object result = null;
            Exception exception = null;

            try
            {
                action(parameter);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return CreateResponse(result, exception);
        }

        public ServiceResponse Call<T, T2>(Action<T, T2> action, T parameter, T2 parameter2)
        {
            object result = null;
            Exception exception = null;

            try
            {
                action(parameter, parameter2);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return CreateResponse(result, exception);
        }


        public ServiceResponse Call<T, T2, T3>(Action<T, T2, T3> action, T parameter, T2 parameter2, T3 parameter3)
        {
            object result = null;
            Exception exception = null;

            try
            {
                action(parameter, parameter2, parameter3);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return CreateResponse(result, exception);
        }

        private ServiceResponse CreateResponse(object result, Exception exception)
        {
            var response = new ServiceResponse()
            {
                Succeeded = true,
                AccessToken = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "")
            };

            if (exception != null)
            {
                response.Succeeded = false;
                response.Message = exception.Message;
                response.Content = null;
            }
            else
            {
                response.Content = result;
                response.Message = "Success";
            }

            return response;
        }
    }
}
