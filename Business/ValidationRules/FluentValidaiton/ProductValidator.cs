using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidaiton
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).Length(1, 50);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1);
            RuleFor(p => p.ProductName).Must(NotStartWithDigit).WithMessage("Ürün rakam ile başlayamaz");

        }
        private bool NotStartWithDigit(string arg)
        {
            return !char.IsDigit(arg[0]);
        }
    }
}
