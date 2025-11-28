using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    public class Error 
    {
     
        public string Code { get;  }
        public string Description { get;  }
        public ErrorType Type { get; }

        private Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }


        #region Static Factory Method

        public static Error Failure(string Code = "General.Failure", string Description = "General Faillure Has Occurred")
        {
            return new Error(Code , Description , ErrorType.Failure);
        }
        public static Error Validation(string Code = "General.Validation", string Description = " One or more validation errors occurred")
        {
            return new Error(Code , Description , ErrorType.Validation);
        }
        public static Error NotFound(string Code = "NotFound.Validation", string Description = " The Requested Resource Was Not Found")
        {
            return new Error(Code , Description , ErrorType.NotFound);
        }
        public static Error Unauthorized(string Code = "NotFound.Unauthorized", string Description = " You Are Not Authorized To ")
        {
            return new Error(Code , Description , ErrorType.Unauthorized);
        }
        public static Error Forbidden(string Code = "NotFound.Forbidden", string Description = " You do not have permission to perform this action ")
        {
            return new Error(Code , Description , ErrorType.Forbidden);
        }
        public static Error InvalidCrendentials(string Code = "NotFound.InvalidCrendentials", string Description = " Invalid username or password ")
        {
            return new Error(Code , Description , ErrorType.InvalidCrendentials);
        }


        #endregion
    }
}
