using Microsoft.AspNetCore.Mvc;
using StoreWebsite.Application.DTOs;
using StoreWebsite.Application.Interfaces;
using StoreWebsite.Web.Models.Products;

namespace StoreWebsite.Web.Controllers;

public sealed class ProductsController : Controller
{
    private readonly IProductUseCaseService _productUseCaseService;

    public ProductsController(IProductUseCaseService productUseCaseService)
    {
        _productUseCaseService = productUseCaseService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var products = await _productUseCaseService.GetAllAsync(cancellationToken);

        var viewModel = products
            .Select(product => new ProductListItemViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                LastModifiedAtUtc = product.LastModifiedAtUtc
            })
            .ToList();

        return View(viewModel);
    }

    public IActionResult Create()
    {
        return View(new ProductFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductFormViewModel form, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var result = await _productUseCaseService.CreateAsync(Map(form), cancellationToken);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Unable to create product.");
            return View(form);
        }

        TempData["SuccessMessage"] = "Product created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var product = await _productUseCaseService.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            return NotFound();
        }

        var form = new ProductFormViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity
        };

        return View(form);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductFormViewModel form, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var result = await _productUseCaseService.UpdateAsync(form.Id, Map(form), cancellationToken);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Unable to update product.");
            return View(form);
        }

        TempData["SuccessMessage"] = "Product updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var product = await _productUseCaseService.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            return NotFound();
        }

        var form = new ProductFormViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity
        };

        return View(form);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmDelete(int id, CancellationToken cancellationToken)
    {
        var result = await _productUseCaseService.DeleteAsync(id, cancellationToken);
        if (!result.Succeeded)
        {
            TempData["ErrorMessage"] = result.Error ?? "Unable to delete product.";
        }
        else
        {
            TempData["SuccessMessage"] = "Product deleted successfully.";
        }

        return RedirectToAction(nameof(Index));
    }

    private static ProductInputDto Map(ProductFormViewModel form)
    {
        return new ProductInputDto
        {
            Name = form.Name,
            Description = form.Description,
            Price = form.Price,
            StockQuantity = form.StockQuantity
        };
    }
}
