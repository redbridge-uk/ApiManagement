using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Redbridge.DependencyInjection;

namespace Redbridge.ApiManagement
{
    public abstract class UnitOfWorkMethodApi<TResponse, TUnitOfWork, TContext> : ApiMethod<TResponse, TContext>
    where TUnitOfWork : class, IWorkUnit
    where TContext: IApiCallContext
    {
        private readonly TUnitOfWork _unitOfWorkFactory;

        protected UnitOfWorkMethodApi(TUnitOfWork unitOfWork, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
            : base(logger, contextProvider, authority)
        {
            _unitOfWorkFactory = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        
        protected override async Task<TResponse> OnInvoke(TContext context)
        {
            var result = await OnInvoke(_unitOfWorkFactory, context);
            try
            {
                await _unitOfWorkFactory.SaveChangesAsync();
                await OnCommitCompleted(result, context);
                return result;
            }
            catch (Exception e)
            {
                await OnCommitFailed(e, result, context);
                throw;
            }
        }

        protected virtual Task OnCommitCompleted(TResponse result, TContext context)
        {
            return Task.FromResult(true);
        }

        protected virtual Task OnCommitFailed(Exception exception, TResponse result, TContext context)
        {
            return Task.FromResult(true);
        }

        protected abstract Task<TResponse> OnInvoke(TUnitOfWork unit, TContext context);
    }

    public abstract class UnitOfWorkMethodApi<TIn1, TResponse, TUnitOfWork, TContext> : ApiMethod<TIn1, TResponse, TContext>
    where TUnitOfWork : class, IWorkUnit
    where TContext: IApiCallContext
    {
        private readonly TUnitOfWork _unitOfWork;

        protected UnitOfWorkMethodApi(TUnitOfWork unitOfWork, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
            : base(logger, contextProvider, authority)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task<TResponse> OnInvoke(TIn1 in1, TContext context)
        {
            var result = await OnInvoke(_unitOfWork, in1, context);

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await OnCommitCompleted(in1, result, context);
            }
            catch (Exception e)
            {
                await OnCommitFailed(e, in1, result, context);
                throw;
            }

            return result;
        }

        protected virtual Task OnCommitCompleted(TIn1 in1, TResponse result, TContext context)
        {
            return Task.FromResult(true);
        }

        protected virtual Task OnCommitFailed(Exception exception, TIn1 in1, TResponse result, TContext context)
        {
            return Task.FromResult(true);
        }

        protected abstract Task<TResponse> OnInvoke(TUnitOfWork unit, TIn1 in1, TContext context);
    }

    public abstract class UnitOfWorkMethodApi<TIn1, TIn2, TResponse, TUnitOfWork, TContext> : ApiMethod<TIn1, TIn2, TResponse, TContext>
    where TUnitOfWork : class, IWorkUnit
    where TContext: IApiCallContext
    {
        private readonly TUnitOfWork _unitOfWork;

        protected UnitOfWorkMethodApi(TUnitOfWork unitOfWork, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
            : base(logger, contextProvider, authority)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        
        protected override async Task<TResponse> OnInvoke(TIn1 in1, TIn2 in2, TContext context)
        {
            var result = await OnInvoke(_unitOfWork, in1, in2, context);
            try
            {
                await _unitOfWork.SaveChangesAsync();
                await OnCommitCompleted(in1, in2, result, context);
                return result;
            }
            catch (Exception e)
            {
                await OnCommitFailed(e, in1, in2, result, context);
                throw;
            }
        }

        protected virtual Task OnCommitCompleted(TIn1 in1, TIn2 in2, TResponse result, TContext context)
        {
            return Task.FromResult(true);
        }

        protected virtual Task OnCommitFailed(Exception exception, TIn1 in1, TIn2 in2, TResponse result, TContext context)
        {
            return Task.FromResult(true);
        }

        protected abstract Task<TResponse> OnInvoke(TUnitOfWork unit, TIn1 in1, TIn2 in2, TContext context);
    }

    public abstract class UnitOfWorkMethodApi<TIn1, TIn2, TIn3, TResponse, TUnitOfWork, TContext> : ApiMethod<TIn1, TIn2, TIn3, TResponse, TContext>
    where TUnitOfWork : class, IWorkUnit
    where TContext: IApiCallContext
    {
        private readonly TUnitOfWork _unitOfWork;

        protected UnitOfWorkMethodApi(TUnitOfWork unitOfWork, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
            : base(logger, contextProvider, authority)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task<TResponse> OnInvoke(TIn1 in1, TIn2 in2, TIn3 in3, TContext context)
        {
                var result = await OnInvoke(_unitOfWork, in1, in2, in3, context);
                await _unitOfWork.SaveChangesAsync();
                return result;
        }

        protected abstract Task<TResponse> OnInvoke(TUnitOfWork unit, TIn1 in1, TIn2 in2, TIn3 in3, TContext context);
    }

    public abstract class UnitOfWorkMethodApi<TIn1, TIn2, TIn3, TIn4, TResponse, TUnitOfWork, TContext> : ApiMethod<TIn1, TIn2, TIn3, TIn4, TResponse, TContext>
        where TUnitOfWork : class, IWorkUnit
        where TContext: IApiCallContext
    {
        private readonly TUnitOfWork _unitOfWork;

        protected UnitOfWorkMethodApi(TUnitOfWork unitOfWorkFactory, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
            : base(logger, contextProvider, authority)
        {
            _unitOfWork = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        protected override async Task<TResponse> OnInvoke(TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TContext context)
        {
            var result = await OnInvoke(_unitOfWork, in1, in2, in3, in4, context);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        protected abstract Task<TResponse> OnInvoke(TUnitOfWork unit, TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TContext context);
    }
}
