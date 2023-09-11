using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Vemox.Controllers
{
    public class VemoxController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitForm(string inputValue, string language)
        {
            if (language == "SQL")
            {
                // ส่ง inputValue ไปยังแอคชัน "SQLresult"
                return RedirectToAction("SQLresult", new { inputValue = inputValue });
            }
            else if (language == "MQL")
            {
                // ส่ง inputValue ไปยังแอคชัน "MQLresult"
                return RedirectToAction("MQLresult", new { inputValue = inputValue });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }





        public IActionResult SQLresult(string inputValue)
        {
            string sqlinput;
            int num1;
            int num2 = 0;
            Stopwatch stopwatch = new Stopwatch();


            if (inputValue.EndsWith(";"))
            {
                sqlinput = inputValue.Replace(";", "");
                string[] words = sqlinput.Split(' ');

                if (words[0].Equals("SELECT"))
                {
                    if (int.TryParse(words[1], out num1))
                    {
                        if (words.Length >= 3 && (words[2].Equals("+"))) 
                        {
                            if (int.TryParse(words[3], out num2)) 
                            {
                                stopwatch.Start();
                                ViewBag.outputQuery = inputValue;
                                ViewBag.outputResult = num1+num2;
                                ViewBag.status = "true";
                                stopwatch.Stop();
                                TimeSpan elapsedTime = stopwatch.Elapsed;
                                long milliseconds = (long)elapsedTime.TotalMilliseconds;
                                ViewBag.time = milliseconds;


                                return View();
                            }
                            else 
                            {
                                return RedirectToAction("resultwong");
                            }
                        }
                        else
                        {
                            stopwatch.Start();
                            ViewBag.outputQuery = inputValue;
                            ViewBag.outputResult = num1;
                            ViewBag.status = "true";
                            stopwatch.Stop();
                            TimeSpan elapsedTime = stopwatch.Elapsed;
                            long milliseconds = (long)elapsedTime.TotalMilliseconds;
                            ViewBag.time = milliseconds;

                            return View();
                        }

                            
                        
                    }
                    else
                    {
                        return RedirectToAction("resultwong");
                    }
                }
                else
                {
                    return RedirectToAction("resultwong");
                }
            }
            else
            {
                return RedirectToAction("resultwong");
            }
          
                
        }
        public IActionResult resultwong()
        {
            return View();
        }



        public IActionResult MQLresult(string inputValue)
        {
            string mqlinput;
            int num1;
            int num2 = 0;
            Stopwatch stopwatch = new Stopwatch();


            if (inputValue.StartsWith("print(") && inputValue.EndsWith(");"))
            {
                mqlinput = inputValue.Replace(";", " ");
                mqlinput = mqlinput.Replace("(", " ");
                mqlinput = mqlinput.Replace(")", " ");
                string[] words = mqlinput.Split(' ');

                if (words[0].Equals("print"))
                {
                    if (int.TryParse(words[1], out num1))
                    {
                        if (words.Length >= 3 && (words[2].Equals("+")))
                        {
                            if (int.TryParse(words[3], out num2))
                            {
                                stopwatch.Start();
                                ViewBag.outputQuery = inputValue;
                                ViewBag.outputResult = num1 + num2;
                                ViewBag.status = "true";
                                stopwatch.Stop();
                                TimeSpan elapsedTime = stopwatch.Elapsed;
                                long milliseconds = (long)elapsedTime.TotalMilliseconds;
                                ViewBag.time = milliseconds;


                                return View();
                            }
                            else
                            {
                                return RedirectToAction("resultwong");

                            }
                        }
                        else
                        {
                            stopwatch.Start();
                            ViewBag.outputQuery = inputValue;
                            ViewBag.outputResult = num1;
                            ViewBag.status = "true";
                            stopwatch.Stop();
                            TimeSpan elapsedTime = stopwatch.Elapsed;
                            long milliseconds = (long)elapsedTime.TotalMilliseconds;
                            ViewBag.time = milliseconds;

                            return View();
                        }



                    }
                    else
                    {
                        return RedirectToAction("resultwong");

                    }
                }
                else
                {
                    ViewBag.outputQuery = words[0];
                                return View(); ;
                }
            }
            else
            {
                return RedirectToAction("resultwong");

            }


        }
        
    }
}
