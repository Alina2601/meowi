<<<<<<< HEAD
=======
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
>>>>>>> feature/Novruzova/ids
using Alina.Models;

namespace to.Models
{
    public class lab1Data
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string Product { get; set;}
        public string Material { get; set;}
        public int Content { get; set;}
        public int Price { get; set;}
    public BaseModelValidationResult Validate()
        {
            var validationResult = new BaseModelValidationResult();

            if (string.IsNullOrWhiteSpace(Product))
            
                validationResult.Append($"Product cannot be empty");
          
            if (string.IsNullOrWhiteSpace(Material))
            
                validationResult.Append($"Material cannot be empty");
            
            if (!(84 < Content && Content < 10001)) 
                validationResult.Append($"Content {Content} cannot be <85");
           
            if (!(0<Price))
            
                validationResult.Append($"Price cannot be <0");
            
              return validationResult;
        }

        public override string ToString()
        {
            return $"{Product} {Material} ptoba {Content} price {Price}";
        }

    }
}