using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MVC.Models.Validaciones
{
    public class AlMenosUnContacto : ValidationAttribute
    {
        private readonly string[] propiedades;

        public AlMenosUnContacto(params string[] propiedades)
        {
            this.propiedades = propiedades;
        }

        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            foreach (var propiedad in propiedades)
            {
                PropertyInfo? info =
                    validationContext.ObjectType.GetProperty(propiedad);

                if (info == null)
                    continue;

                var valor = info.GetValue(validationContext.ObjectInstance);

                if (valor is string texto &&
                    !string.IsNullOrWhiteSpace(texto))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(
                "Debe ingresar al menos un medio de contacto: email o teléfono."
            );
        }
    }
}