using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace Tech_test_API.Controllers
{
    public class NumToWordsController : Controller
    {
        public class ConversionRequest
        {
            public string number { get; set; }
        }

        private static string[] ones = {
            "ZERO", "ONE", "TWO", "THREE", "FOUR",
            "FIVE", "SIX", "SEVEN", "EIGHT", "NINE"
        };

        private static string[] teens = {
            "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN",
            "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };

        private static string[] tens = {
            "TWENTY", "THIRTY", "FORTY", "FIFTY",
            "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };

        public static string ConvertToWords(decimal number)
        {
            string result = "";

            long dollars = (long)number;
            int cents = (int)((number - dollars) * 100);

            if (dollars > 0)
            {
                result += ConvertThreeDigitGroup((int)(dollars / 1000000), "MILLION") + " ";
                result += ConvertThreeDigitGroup((int)((dollars / 1000) % 1000), "THOUSAND") + " ";
                result += ConvertThreeDigitGroup((int)(dollars % 1000), "");
                result += (dollars == 1 ? " DOLLAR" : " DOLLARS");
            }
            else
            {
                result += "ZERO DOLLARS";
            }

            if (cents > 0)
            {
                result += " AND ";
                result += ConvertTwoDigitGroup(cents);
                result += (cents == 1 ? " CENT" : " CENTS");
            }

            return result;
        }

        private static string ConvertThreeDigitGroup(int number, string groupLabel)
        {
            if (number == 0)
            {
                return "";
            }

            string groupWords = "";

            int hundreds = number / 100;
            int remainder = number % 100;

            if (hundreds > 0)
            {
                groupWords += ones[hundreds] + " HUNDRED";
            }

            if (remainder > 0)
            {
                if (hundreds > 0)
                {
                    groupWords += " AND ";
                }

                if (remainder < 10)
                {
                    groupWords += ones[remainder];
                }
                else if (remainder < 20)
                {
                    groupWords += teens[remainder - 10];
                }
                else
                {
                    groupWords += tens[remainder / 10 - 2];
                    if (remainder % 10 > 0)
                    {
                        groupWords += "-" + ones[remainder % 10];
                    }
                }
            }

            if (!string.IsNullOrEmpty(groupLabel))
            {
                groupWords += " " + groupLabel;
            }

            return groupWords;
        }

        private static string ConvertTwoDigitGroup(int number)
        {
            if (number < 10)
            {
                return ones[number];
            }
            else if (number < 20)
            {
                return teens[number - 10];
            }
            else
            {
                string result = tens[number / 10 - 2];
                if (number % 10 > 0)
                {
                    result += "-" + ones[number % 10];
                }
                return result;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("convert")]
        public async Task<IActionResult> Convert()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string requestBody = await reader.ReadToEndAsync();

                try
                {
                    ConversionRequest body = JsonSerializer.Deserialize<ConversionRequest>(requestBody);

                    string words = ConvertToWords(decimal.Parse(body.number));
                    return Json(new { words });
                }
                catch (JsonException)
                {
                    return BadRequest("Invalid input.");
                }
            }
        }

    }
}
