using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Messages;

namespace MassTransitPublishApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationPublisherController : ControllerBase
    {
        private readonly ILogger<NotificationPublisherController> _logger;
        private readonly IBusControl _bus;

        public NotificationPublisherController(ILogger<NotificationPublisherController> logger, IBusControl bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpGet]
        public IEnumerable<Task> Get()
        {
            var task = _bus.Publish(new TestMessage { Body = "body 1", Title = "title 1", To = "fNSbPuo4IE8:APA91bFqP2mmKEEsqI8pnzrqpiWtJmiT-YviswUAFkgaCh_Z6ZjwaaScJjIEM7de06X0T2DwAhynDZQBkWSGAlUR-SHoThAOAvs2UqTL5UcXtl8R53L7W-UwjJyb1KW0eYHk5UPrlag0" });
            Console.WriteLine(task);
            yield return task;

        }
    }
}
