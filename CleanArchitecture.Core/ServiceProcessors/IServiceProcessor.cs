using CelanArchitecture.Core.ServiceWrapper;
using System;

namespace CelanArchitecture.ServiceProcessors
{
    public interface IServiceProcessor
    {
        ServiceResponse Call<TResult>(Func<TResult> action);
        ServiceResponse Call<T, TResult>(Func<T, TResult> action, T parameter);
        ServiceResponse Call<T, T2, TResult>(Func<T, T2, TResult> action, T parameter, T2 parameter2);
        ServiceResponse Call<T, T2, T3, TResult>(Func<T, T2, T3, TResult> action, T parameter, T2 parameter2, T3 parameter3);
        //for void function
        ServiceResponse Call<T>(Action<T> action, T parameter);
        ServiceResponse Call<T, T2>(Action<T, T2> action, T parameter, T2 parameter2);
        ServiceResponse Call<T, T2, T3>(Action<T, T2, T3> action, T parameter, T2 parameter2, T3 parameter3);
    }
}
