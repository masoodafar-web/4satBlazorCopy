using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newFace.Utility
{
    public class ModelStateHelper
    {
        public List<string> GetModelStateErrors(ModelStateDictionary ModelState)
        {
            List<string> ListMsg = new List<string>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var modelError in modelState.Errors)
                {
                    ListMsg.Add(modelError.ErrorMessage);
                }
            }
            return ListMsg;
        }

        public string GetModelStateError(ModelStateDictionary ModelState)
        {
            List<string> ListMsg = new List<string>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var modelError in modelState.Errors)
                {
                    ListMsg.Add(modelError.ErrorMessage);
                }
            }
            return ListMsg.FirstOrDefault();
        }

        public bool TryValidate(object @object, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(@object, serviceProvider: null, items: null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(
                @object, context, results,
                validateAllProperties: true
            );
        }

    }
}