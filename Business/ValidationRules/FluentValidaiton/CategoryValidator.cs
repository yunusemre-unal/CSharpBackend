using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidaiton
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(p => p.CategoryName).NotEmpty();
            RuleFor(p => p.CategoryName).Length(1, 50);
            RuleFor(p => p.CategoryName).Must(NotStartWithDigit).WithMessage("Category adı rakam ile başlayamaz");

        }
        private bool NotStartWithDigit(string arg)
        {
            return !char.IsDigit(arg[0]);
        }
    }
}
