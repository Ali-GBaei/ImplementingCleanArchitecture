using StoreWebsite.Application.Common;
using StoreWebsite.Application.DTOs;

namespace StoreWebsite.Application.Interfaces;

public interface IProductUseCaseService
{
    Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<OperationResult> CreateAsync(ProductInputDto input, CancellationToken cancellationToken = default);
    Task<OperationResult> UpdateAsync(int id, ProductInputDto input, CancellationToken cancellationToken = default);
    Task<OperationResult> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
