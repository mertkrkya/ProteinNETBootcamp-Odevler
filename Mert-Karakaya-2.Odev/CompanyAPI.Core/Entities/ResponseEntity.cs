using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CompanyAPI.Core.Entities
{
    public class ResponseEntity
    {
        public bool isSuccess { get; set; }
        public string errorMessage { get; set; }
        public object data { get; set; }
        public ResponseEntity(object data)
        {
            isSuccess = true;
            errorMessage = null;
            this.data = data;
        }
        public ResponseEntity(string errorMessage)
        {
            isSuccess = false;
            this.errorMessage = errorMessage;
            data = null;
        }
    }
}
