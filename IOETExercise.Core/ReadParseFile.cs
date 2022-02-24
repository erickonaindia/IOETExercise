using IOETExercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOETExercise.Core
{
    /// <summary>
    /// Read Parse File Class
    /// </summary>
    public static class ReadParseFile
    {
        /// <summary>
        /// Process File
        /// </summary>
        /// <returns></returns>
        public static async Task ProcessFileAsync()
        {
            try
            {
                var lines = await System.IO.File.ReadAllLinesAsync(@"Data\data.txt");

                if (lines.Length < 5)
                {
                    Console.WriteLine("The file has not the format required..!!");
                    return;
                }

                foreach (string line in lines)
                {
                    var infoFile = new DataObjectFiles();
                    int index = line.IndexOf('=') + 1;
                    if (index > 0)
                    {
                        string[] words = line.Substring(index).Split(',');
                        infoFile.Name = line.Substring(0, index).Replace('=', ' ').Trim();

                        foreach (var item in words)
                        {
                            var _detail = item.Split('-');

                            var _details = new Details()
                            {
                                WeekDay = (WeekDays)Enum.Parse(typeof(WeekDays), _detail[0].Substring(0, 2)),
                                StartTime = DateTime.ParseExact($"1900-01-01 {_detail[0].Substring(2)}", "yyyy-MM-dd HH:mm", null),
                                EndTime = DateTime.ParseExact($"1900-01-01 {_detail[1]}", "yyyy-MM-dd HH:mm", null)
                            };

                            infoFile.Details.Add(_details);
                        }
                        Console.WriteLine(CalculateAmount(infoFile));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Calculate Amount
        /// </summary>
        /// <param name="input">input</param>
        public static string CalculateAmount(DataObjectFiles input)
        {
            double result = 0;
            TimeSpan initTime;
            TimeSpan endTime;
            try
            {
                foreach (var item in input.Details)
                {
                    switch (item.WeekDay)
                    {
                        case WeekDays.MO:
                        case WeekDays.TU:
                        case WeekDays.WE:
                        case WeekDays.TH:
                        case WeekDays.FR:
                            initTime = item.StartTime.TimeOfDay;
                            endTime = item.EndTime.TimeOfDay;

                            result += GetHours(initTime,
                                endTime);
                            break;
                        case WeekDays.SA:
                        case WeekDays.SU:
                            initTime = item.StartTime.TimeOfDay;
                            endTime = item.EndTime.TimeOfDay;

                            result += GetHours(initTime,
                                endTime,
                                true);
                            break;
                    }
                }
                return $"The amount to pay {input.Name} is: {result} USD";
            }
            catch (Exception)
            {
                throw;
            }            
        }

        /// <summary>
        /// Get Hours
        /// </summary>
        /// <param name="start">start hour</param>
        /// <param name="end">end hour</param>
        /// <param name="isWeekend">is weekend</param>
        /// <returns></returns>
        static double GetHours(TimeSpan start,
            TimeSpan end,
            bool isWeekend = false)
        {
            try
            {
                #region Scheduled Hours
                var infoFile = new List<ScheduledDaysAmount>()
                {
                    new ScheduledDaysAmount
                    {
                        InitTime = TimeSpan.Parse("00:01"),
                        EndTime = TimeSpan.Parse("09:00"),
                        Amount = 25,
                        Weekend = false
                    },
                    new ScheduledDaysAmount
                    {
                        InitTime = TimeSpan.Parse("09:01"),
                        EndTime = TimeSpan.Parse("18:00"),
                        Amount = 15,
                        Weekend = false
                    },
                    new ScheduledDaysAmount
                    {
                        InitTime = TimeSpan.Parse("18:01"),
                        EndTime = TimeSpan.Parse("00:00"),
                        Amount = 20,
                        Weekend = false
                    },
                    new ScheduledDaysAmount
                    {
                        InitTime = TimeSpan.Parse("00:01"),
                        EndTime = TimeSpan.Parse("09:00"),
                        Amount = 30,
                        Weekend = true
                    },
                    new ScheduledDaysAmount
                    {
                        InitTime = TimeSpan.Parse("09:01"),
                        EndTime = TimeSpan.Parse("18:00"),
                        Amount = 20,
                        Weekend = true
                    },
                    new ScheduledDaysAmount
                    {
                        InitTime = TimeSpan.Parse("18:01"),
                        EndTime = TimeSpan.Parse("23:59"),
                        Amount = 25,
                        Weekend = true
                    },
                };
                #endregion

                double subTotal = 0;

                foreach (var item in infoFile.Where(x => x.Weekend == isWeekend).ToList())
                {
                    if (start >= item.InitTime && end <= item.EndTime)
                    {
                        subTotal = (end - start).TotalHours * item.Amount;
                    };
                }
                return subTotal;
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
