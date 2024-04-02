using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace WaiterChefBoss.ModelBinders
{
    public class DoubleModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (result != ValueProviderResult.None && !string.IsNullOrEmpty(result.FirstValue))
            {
                double res = 0;
                bool success = false;
                try
                {
                    string val = result.FirstValue;
                    val = val.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    val = val.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    res = Convert.ToDouble(val, CultureInfo.CurrentCulture);
                    success = true;
                }
                catch (FormatException ex)
                {

                    bindingContext.ModelState.AddModelError(bindingContext.ModelName,ex, bindingContext.ModelMetadata);
                }
                if (success)
                {
                    bindingContext.Result = ModelBindingResult.Success(res);
                }
            }

            return Task.CompletedTask;
        }
    }
}
