using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Server
{
	public class SampleService : ISampleService
	{
        private IHttpContextAccessor _accessor;
        public SampleService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Ping(string s)
		{
            try
            {
                Console.WriteLine(String.Format("Exec ping method with session-id {0}", _accessor.HttpContext.Session.Id));
                return s;
            }
            catch(System.Exception ex)
            {
                Console.WriteLine(String.Format("{0}, Inner: {1}", ex.Message, ex.InnerException?.Message));
                return ex.Message; 
            }			
		}

		public ComplexModelResponse PingComplexModel(ComplexModelInput inputModel)
		{
			Console.WriteLine("Input data. IntProperty: {0}, StringProperty: {1}", inputModel.IntProperty, inputModel.StringProperty);
			return new ComplexModelResponse
			{
				FloatProperty = float.MaxValue / 2,
				StringProperty = Guid.NewGuid().ToString()
			};
		}

		public void VoidMethod(out string s)
		{
			s = "Value from server";
		}

		public Task<int> AsyncMethod()
		{
			return Task.Run(() => 42);
		}

		public int? NullableMethod(bool? arg)
		{
			return null;
		}
	}
}
