using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRApi.DAL;
using SignalRApi.Model;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks; // Eklenen using ifadesi

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //public class VisitorController : ControllerBase
    //{
    //    private readonly VisitorService _visitorService;
    //    private readonly Random _random;

    //    public VisitorController(VisitorService visitorService)
    //    {
    //        _visitorService = visitorService;
    //        _random = new Random();
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> CreateVisitor()
    //    {
    //        var cityValues = Enum.GetValues(typeof(ECity));
    //        var cityCount = cityValues.Length;

    //        var tasks = Enumerable.Range(1, 100000).Select(async x =>
    //        {
    //            foreach (ECity item in Enum.GetValues(typeof(ECity)))
    //            {
    //                var newVisitor = new Visitor
    //                {
    //                    City = item,
    //                    CityVisitCount = _random.Next(100, 2000),
    //                    VisitDate = DateTime.Now.AddDays(x + (int)item * 100000) // Farklı VisitDate üretimi
    //                };                  
    //                    await _visitorService.SaveVisitor(newVisitor);
    //                    await Task.Delay(10);                    
    //            }
    //        });
    //        await Task.WhenAll(tasks);

    //        return Ok("Ziyaretçiler başarılı bir şekilde eklendi");
    //    }
    //}
    public class VisitorController : ControllerBase
    {
        private readonly VisitorService _visitorService;
        public VisitorController(VisitorService visitorService)
        {
            _visitorService = visitorService;
        }
        [HttpGet]
        public async Task<IActionResult> CreateVisitor()
        {
            Random random = new Random();
            Enumerable.Range(1, 100000).ToList().ForEach(x =>
            {
                foreach (ECity item in Enum.GetValues(typeof(ECity)))
                {
                    var newVisitor = new Visitor
                    {
                        City = item,
                        CityVisitCount = random.Next(100, 2000),
                        VisitDate = DateTime.Now.AddDays(x)
                    };
                    _visitorService.SaveVisitor(newVisitor).Wait();
                    System.Threading.Thread.Sleep(3);
                }
            });
           return Ok("Ziyaretçiler başarılı bir şekilde eklendi");
        }
    }

}
