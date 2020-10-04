using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Datadog.Trace;


namespace weatherapi.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
    
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chillys", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            
            var scope = Tracer.Instance.ActiveScope;
            var rng = new Random();
            using (var parentScope =
       Tracer.Instance.StartActive("weatherforecast.get"))
            {
            if (parentScope != null)
                {
                     //https://docs.datadoghq.com/tracing/custom_instrumentation/dotnet/
                     //https://docs.datadoghq.com/tracing/guide/metrics_namespace/
                    //parentScope.Span.SetTag("http.status_class", HttpContext.Request.Method.ToString());
                    parentScope.Span.SetTag("http.method", HttpContext.Request.Method.ToString());
                    parentScope.Span.SetTag("http.status_code", HttpContext.Response.StatusCode.ToString());
                    parentScope.Span.SetTag("host_port", HttpContext.Request.Host.ToString());
                    parentScope.Span.SetTag("http.request.headers.host", HttpContext.Request.Host.ToString());
                    parentScope.Span.SetTag("http.request.protocol", HttpContext.Request.Protocol.ToString());
                    parentScope.Span.SetTag("http.request.isHTTPS", HttpContext.Request.IsHttps.ToString());
                    parentScope.Span.SetTag("http.request.scheme", HttpContext.Request.Scheme.ToString());
                    //parentScope.Span.SetTag("http.request.contenttype", HttpContext.Request.ContentType.ToString());
                    //parentScope.Span.SetTag("http.request.contentlength", HttpContext.Request.ContentLength.Value.ToString());
                    parentScope.Span.SetTag("http.response.headers", HttpContext.Response.Headers.Values.ToString());
                    parentScope.Span.SetTag("http.response.cookies", HttpContext.Response.Cookies.ToString());
                    //parentScope.Span.SetTag("http.response.bodylength", HttpContext.Response.Body.Length.ToString());
                    parentScope.Span.SetTag("http.response.contentlength", HttpContext.Response.ContentLength.HasValue.ToString());
                    //parentScope.Span.SetTag("http.user.identity", HttpContext.User.Identity.Name.ToString());
                    //http.url


                    parentScope.Span.SetTag("resource", HttpContext.Request.Method.ToString());
                    parentScope.Span.SetTag("host_port", HttpContext.Request.Host.ToString());
                   // parentScope.Span.SetTag("success", HttpContext.Request.Body.ToString());
                    parentScope.Span.SetTag("http.status_code", HttpContext.Response.StatusCode.ToString());
                    parentScope.Span.SetTag("user", HttpContext.User.ToString());
                   

                }

                    // parentScope.Span.SetTag("method", parentScope.Span.ServiceName.;
                
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {

                    Date = DateTime.Now.AddDays(index),

                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();

                 

                
                
            }
        }
    }
}


