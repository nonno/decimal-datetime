using System;
using System.Globalization;
using DecimalDateTimeApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DecimalDateTimeApi.Controllers
{
    [Route("api/[controller]")]
    public class DecimalDateTimeController : Controller
    {
        private readonly CultureInfo[] cultureInfo = new CultureInfo[]
        {
            new CultureInfo("it"),
            new CultureInfo("en-US"),
            new CultureInfo("en-GB"),
            new CultureInfo("fr")
        };

        // GET: api/datetime
        [HttpGet]
        public JsonResult Now()
        {
            return RevolutionaryDateTimeToJson(Pallettaro.Revo.DateTime.Now);
        }

        // GET: api/datetime/xxxx
        [HttpGet("{timestamp}")]
        public JsonResult Parse(String timestamp)
        {
            try {
                DateTime parsed = ParseDateTime(timestamp);
                if (parsed.Millisecond == 0 && parsed.Second == 0 && parsed.Minute == 0 && parsed.Hour == 0) {
                    // gestisce il caso di parse della sola data, che viene convertita alle informazioni del giorno precedente
                    parsed = parsed.AddMilliseconds(1);
                }

                return RevolutionaryDateTimeToJson(new Pallettaro.Revo.DateTime(parsed));
            }
            catch (Exception exc) {
                return Json(exc);
            }
        }

        private DateTime ParseDateTime(String timestamp, int cultureIndex = 0)
        {
            try {
                return DateTime.Parse(timestamp, cultureInfo[cultureIndex]);
            }
            catch (FormatException exc) {
                if (cultureInfo.Length > cultureIndex + 1) {
                    return ParseDateTime(timestamp, cultureIndex + 1);
                }
                throw exc;
            }
        }

        private JsonResult RevolutionaryDateTimeToJson(Pallettaro.Revo.DateTime datetime)
        {
            return Json(new DecimalDateTimeJsonResult(datetime));
        }
    }
}