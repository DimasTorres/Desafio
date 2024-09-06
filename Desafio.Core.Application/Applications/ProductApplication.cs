using AutoMapper;
using Desafio.Core.Application.Contracts.Product.Request;
using Desafio.Core.Application.Contracts.Product.Response;
using Desafio.Core.Application.Interfaces.Applications;
using Desafio.Core.Application.Interfaces.Services;
using Desafio.Core.Application.Validators;
using Desafio.Core.Application.Validators.Base;
using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Applications;

public class ProductApplication : IProductApplication
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductApplication(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public async Task<Response<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var validate = new CreateProductRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return Response.Unprocessable<CreateProductResponse>(validateErrors.ReportErrors);

        try
        {
            var productEntity = _mapper.Map<ProductEntity>(request);
            var response = await _productService.CreateAsync(productEntity);

            CreateProductResponse responseOk = new() { Id = response.Data };

            return Response.OK(responseOk);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable<CreateProductResponse>(responseError);
        }
    }

    public async Task<Response> DeleteAsync(int id)
    {
        try
        {
            var exists = await _productService.GetByIdAsync(id);
            if (exists.Data is null || exists.Data.Id == 0)
            {
                return Response.Unprocessable(ReportError.Create($"Product {id} not found."));
            }

            return await _productService.DeleteAsync(id);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }

    }

    public async Task<Response<List<ProductResponse>>> GetAllAsync()
    {
        try
        {            
            var result = await _productService.GetAllAsync();

            if (result.ReportErrors.Any())
                return Response.Unprocessable<List<ProductResponse>>(result.ReportErrors);

            var response = _mapper.Map<List<ProductResponse>>(result.Data);

            return Response.OK(response);
        }
        catch (Exception e)
        {
            List<ReportError> listError = [ReportError.Create(e.Message)];
            return Response.Unprocessable<List<ProductResponse>>(listError);
        }
    }

    public async Task<Response> GetByIdAsync(int id)
    {
        try
        {
            return await _productService.GetByIdAsync(id);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }

    public async Task<Response> UpdateAsync(UpdateProductRequest request)
    {
        var validate = new UpdateProductRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return validateErrors;

        try
        {
            var productEntity = _mapper.Map<ProductEntity>(request);

            return await _productService.UpdateAsync(productEntity);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }
}