using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Models
{
	public class ApiResponse
	{
        public string Title { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public int Code { get; set; }
        public object? Data { get; set; }
	}
}
