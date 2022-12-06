using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Redbridge.DependencyInjection;

namespace Redbridge.ApiManagement
{
	public abstract class UnitOfWorkActionApi<TUnitOfWork, TContext> : ApiAction<TContext>
		where TUnitOfWork : class, IWorkUnit
        where TContext: IApiCallContext
	{
		private readonly TUnitOfWork _unit;

		protected UnitOfWorkActionApi(TUnitOfWork unit, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
			: base(logger, contextProvider, authority)
		{
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
		}

		protected override async Task OnInvoke(TContext context)
		{
				await OnInvoke(_unit, context);
				try
				{
					await _unit.SaveChangesAsync();
					await OnCommitCompleted(context);
				}
				catch (Exception e)
				{
					await OnCommitFailed(e, context);
					throw;
				}
		}

		protected virtual Task OnCommitCompleted(TContext context)
		{
			return Task.FromResult(true);
		}

		protected virtual Task OnCommitFailed(Exception exception, TContext context)
		{
			return Task.FromResult(true);
		}

		protected abstract Task OnInvoke(TUnitOfWork unit, TContext context);
	}

	public abstract class UnitOfWorkActionApi<TIn1, TUnitOfWork, TContext> : ApiAction<TIn1, TContext>
	where TUnitOfWork : class, IWorkUnit
    where TContext: IApiCallContext
	{
		private readonly TUnitOfWork _unit;

		protected UnitOfWorkActionApi(TUnitOfWork unit, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
			: base(logger, contextProvider, authority)
		{
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
		}

		protected override async Task OnInvoke(TIn1 in1, TContext context)
		{
			await OnInvoke(_unit, in1, context);
			try
			{
				await _unit.SaveChangesAsync();
				await OnCommitCompleted(_unit, in1, context);
			}
			catch (Exception e)
			{
				await OnCommitFailed(_unit, e, in1, context);
				throw;
			}
		}

		protected virtual Task OnCommitCompleted(TUnitOfWork workUnit, TIn1 in1, TContext context)
		{
			return Task.FromResult(true);
		}

		protected virtual Task OnCommitFailed(TUnitOfWork workUnit, Exception exception, TIn1 in1, TContext context)
		{
			return Task.FromResult(true);
		}

		protected abstract Task OnInvoke(TUnitOfWork unit, TIn1 in1, TContext context);
	}

    public abstract class UnitOfWorkActionApi<TIn1, TIn2, TUnitOfWork, TContext> : ApiAction<TIn1, TIn2, TContext>
        where TUnitOfWork : class, IWorkUnit
        where TContext: IApiCallContext
    {
        private readonly TUnitOfWork _unit;

        protected UnitOfWorkActionApi(TUnitOfWork unit, ILogger logger, IApiContextProvider<TContext> contextProvider, IApiContextAuthorizer<TContext> authority)
            : base(logger, contextProvider, authority)
        {
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        protected override async Task OnInvoke(TIn1 in1, TIn2 in2, TContext context)
        {
                await OnInvoke(_unit, in1, in2, context);
                try
                {
                    await _unit.SaveChangesAsync();
                    await OnCommitCompleted(_unit, in1, in2, context);
                }
                catch (Exception e)
                {
                    await OnCommitFailed(_unit, e, in1, in2, context);
                    throw;
                }
        }

        protected virtual Task OnCommitCompleted(TUnitOfWork workUnit, TIn1 in1, TIn2 in2, TContext context)
        {
            return Task.FromResult(true);
        }

        protected virtual Task OnCommitFailed(TUnitOfWork workUnit, Exception exception, TIn1 in1, TIn2 in2, TContext context)
        {
            return Task.FromResult(true);
        }

        protected abstract Task OnInvoke(TUnitOfWork unit, TIn1 in1, TIn2 in2, TContext context);
    }
}
