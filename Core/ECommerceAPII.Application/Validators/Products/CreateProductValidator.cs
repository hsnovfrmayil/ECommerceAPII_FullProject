using System;
using ECommerceAPII.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceAPII.Application.Validators.Products;

public class CreateProductValidator :AbstractValidator<VM_Create_Product>
{
     public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Zəhmət olmasa, məhsul adını boş qoymayın.")
            .MaximumLength(150)
            .MinimumLength(5);

        RuleFor(p => p.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stok miqdarı mənfi ola bilməz.");

        RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithMessage("Qiymət sıfırdan böyük olmalıdır.");
    }
}

 