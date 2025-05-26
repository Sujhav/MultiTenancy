using Application.Authentication.Command.Register;
using Application.Common.Dtos;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class ValidationPipeLineBehaviours<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {

        private readonly IValidator<TRequest?> _validator;
        public ValidationPipeLineBehaviours(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validator is null)
            {
                return await next();
            }
            var ValidationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (ValidationResult.IsValid)
            {
                return await next();
            }
            var errors = ValidationResult.Errors.ToList();
            return (dynamic)errors;
        }
    }
}
