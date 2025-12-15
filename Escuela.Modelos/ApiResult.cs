using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuela.Modelos
{
    public class ApiResult <T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ApiResult<T> Ok(T data)
        {
            return new ApiResult<T>
            {
                Success = true,
<<<<<<< HEAD
                Data = data 
=======
                Data = data
>>>>>>> 8fb5080cd313b9e59f29ff716d5a5e5d6c3ac563
            };
        }

        public static ApiResult<T> Fail(string message)
        {
            return new ApiResult<T>
            {
                Success = false,
                Message = message
            };
        }
    }
}
