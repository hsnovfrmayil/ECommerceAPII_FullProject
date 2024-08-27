using System;
using ECommerceAPII.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceAPII.Application.Validators.Products;

public class UpdateProductValidator : AbstractValidator<VM_Update_Product>
{
	public UpdateProductValidator()
	{
        RuleFor(p => p.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Zəhmət olmasa, məhsulun ID-sini daxil edin.")
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("Düzgün formatda bir ID daxil edin.");

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

