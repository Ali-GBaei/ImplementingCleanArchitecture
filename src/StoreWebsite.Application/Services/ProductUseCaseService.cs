using StoreWebsite.Application.Common;
using StoreWebsite.Application.DTOs;
using StoreWebsite.Application.Interfaces;
using StoreWebsite.Domain.Entities;
using StoreWebsite.Domain.Interfaces;

namespace StoreWebsite.Application.Services;

public sealed class ProductUseCaseService : IProductUseCaseService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductUseCaseService(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        return products
            .Select(Map)
            .ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        return product is null ? null : Map(product);
    }

    public async Task<OperationResult> CreateAsync(ProductInputDto input, CancellationToken cancellationToken = default)
    {
        try
        {
            var product = Product.Create(input.Name, input.Description, input.Price, input.StockQuantity);
            await _productRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return OperationResult.Success();
        }
        catch (ArgumentException exception)
        {
            return OperationResult.Failure(exception.Message);
        }
    }

    public async Task<OperationResult> UpdateAsync(int id, ProductInputDto input, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            return OperationResult.Failure("Product not found.");
        }

        try
        {
            product.Update(input.Name, input.Description, input.Price, input.StockQuantity);
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return OperationResult.Success();
        }
        catch (ArgumentException exception)
        {
            return OperationResult.Failure(exception.Message);
        }
    }

    public async Task<OperationResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            return OperationResult.Failure("Product not found.");
        }

        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }

    private static ProductDto Map(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            LastModifiedAtUtc = product.LastModifiedAtUtc
        };
    }
}
