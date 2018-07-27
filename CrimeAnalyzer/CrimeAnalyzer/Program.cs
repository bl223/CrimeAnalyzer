using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CrimeAnalyzer
{
    public class CrimeStats
    {
        public int Year;
        public int Population;
        public int ViolentCrime;
        public int Murder;
        public int Rape;
        public int Robbery;
        public int AggAssault;
        public int PropertyCrime;
        public int Burglary;
        public int Theft;
        public int MotorVehicleTheft;

        internal static void Add(CrimeStats CrimeStats)
        {
            throw new NotImplementedException();
        }
    }


    class program
    {
 //       private static StreamWriter report;

        private static void Main(string[] args)
        {

            string cFile = args[0]; //csvFile
            string iFile = args[1]; //infomationFileReport

            if (args.Length != 2) //checks number of arguments
            {
                Console.WriteLine("Incorrect number of arguments. Re-enter source file and report file in correct format");
                Environment.Exit(-1);
            }

            List<CrimeStats> CrimeData = new List<CrimeStats>();

            StreamWriter streamWrite = new StreamWriter(iFile);
            StreamReader streamRead  = new StreamReader(cFile);

            StreamReader sourceFile = null;

            int count = 1;

            try
            {
                sourceFile = new StreamReader(cFile);
                sourceFile.ReadLine();
                while (!sourceFile.EndOfStream)
                {
                    string row = sourceFile.ReadLine();
                    string[] column = row.Split(',');

                    int Year = Convert.ToInt32(column[0]);
                    int Population = Convert.ToInt32(column[1]);
                    int ViolentCrime = Convert.ToInt32(column[2]);
                    int Murder = Convert.ToInt32(column[3]);
                    int Rape = Convert.ToInt32(column[4]);
                    int Robbery = Convert.ToInt32(column[5]);
                    int AggAssault = Convert.ToInt32(column[6]);
                    int PropertyCrime = Convert.ToInt32(column[7]);
                    int Burglary = Convert.ToInt32(column[8]);
                    int Theft = Convert.ToInt32(column[9]);
                    int MotorVehicleTheft = Convert.ToInt32(column[10]);


                    CrimeData.Add(new CrimeStats()
                    {
                        Year = Year,
                        Population = Population,
                        ViolentCrime = ViolentCrime,
                        Murder = Murder,
                        Rape = Rape,
                        Robbery = Robbery,
                        AggAssault = AggAssault,
                        PropertyCrime = PropertyCrime,
                        Burglary = Burglary,
                        Theft = Theft,
                        MotorVehicleTheft = MotorVehicleTheft
                    });

                    count++;

                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Source file contains data that is not of the right type");
                return;
            }

            catch (Exception)
            {
                Console.WriteLine("Source file cannot be opened");
                return;
            }

            finally
            {
                if (sourceFile != null)
                {
                    sourceFile.Close();
                }
            }
            //title
            Console.WriteLine("Crime Analyzer Report \n\n");

            //Stores Output Info
          //  string report = "";

            var years = from CrimeStats in CrimeData select CrimeStats.Year;
            foreach (var i in years)
            {
                count++;
            }

            //Year range of Data
            var YearRange = from CrimeStats in CrimeData where CrimeStats.Year >= 1994 && CrimeStats.Year < 2014 select CrimeStats.Year;
            //Finds Year Range'20 years'
            streamWrite.WriteLine($"Period 1994 - 2014  ({YearRange.Max() - YearRange.Min()} years)");
        
            //Years murders per year < 15000 count 
            var Murders15k = from CrimeStats in CrimeData where CrimeStats.Murder < 15000 select CrimeStats.Year;
            streamWrite.Write($"Years murders per year< 15000: ");

            foreach (int i in Murders15k)
            {
                streamWrite.Write($"{i}, ");
            }
            streamWrite.WriteLine("\n");

            //Robberies per year > 500000

            var Rob50k = from CrimeStats in CrimeData where CrimeStats.Robbery > 1 select CrimeStats.Robbery;

            int x = Rob50k.Max();

            var Rob50kII = from CrimeStats in CrimeData where CrimeStats.Robbery == x select CrimeStats.Year;
            foreach (int i in Rob50kII) 
            {
                streamWrite.Write($"{i}, ");
            }
            streamWrite.WriteLine("\n");

            //Violent crime per capita rate (2010)
            var ViolentCrimeCapita = from CrimeStats in CrimeData where CrimeStats.Year == 2010 select CrimeStats.ViolentCrime;
            var Capita = from CrimeStats in CrimeData where CrimeStats.Year == 2010 select CrimeStats.Population;

            streamWrite.Write("Violent crime per capita rate(2010): ");

            foreach (double i in ViolentCrimeCapita)
            {
                streamWrite.Write(i);
            }
            streamWrite.WriteLine("\n");

            foreach (double i in Capita)
            {
                streamWrite.Write(i);
            }
            streamWrite.WriteLine("\n");

            //Average murder per year (all years)
            var MurderPerYear = from CrimeStats in CrimeData select CrimeStats.Murder;

            double murderAvg1 = MurderPerYear.Average(); //Finds Mean
            //writeline
            streamWrite.WriteLine($"Average murder per year (all years): {murderAvg1}");

            //Average murder per year (1994-1997)
            var Murder9497 = from CrimeStats in CrimeData where CrimeStats.Year >= 1994 && CrimeStats.Year <= 1997 select CrimeStats.Murder;

            double murderAvg2 = Murder9497.Average();
            //writeline
            streamWrite.WriteLine($"Average murder per year (1994-1997) {murderAvg2}");

            //Average murder per year (2010-2014)
            var Murder1014 = from CrimeStats in CrimeData where CrimeStats.Year >= 2010 && CrimeStats.Year <= 2014 select CrimeStats.Murder;

            double murderAvg3 = Murder1014.Average();
            //writeline
            streamWrite.WriteLine($"Average murder per year (2010-2014) {murderAvg3}");

            //Minimum thefts per year(1999 - 2004)
            var minThefts9904 = from CrimeStats in CrimeData where CrimeStats.Year >= 1999 && CrimeStats.Year <= 2004 select CrimeStats.Theft;
            //writeline
            streamWrite.WriteLine($"Minimum thefts per year (1999-2004): {minThefts9904.Min()}");//finds min

            //Maximum thefts per year (1999-2004)
            var maxThefts9904 = from CrimeStats in CrimeData where CrimeStats.Year >= 1999 && CrimeStats.Year <= 2004 select CrimeStats.Theft;
            //writeline
            streamWrite.WriteLine($"Maximum thefts per year (1999-2004): {maxThefts9904.Max()}");//Finds Max

            //Year of highest number of motor vehicle thefts
            var mvTheft = from CrimeStats in CrimeData where CrimeStats.MotorVehicleTheft > 1 select CrimeStats.MotorVehicleTheft;

            int y = mvTheft.Max();

            var mvTheftII = from CrimeStats in CrimeData where CrimeStats.MotorVehicleTheft == y select CrimeStats.Year;
            foreach (int i in mvTheftII)
            {

                streamWrite.WriteLine($"Year of highest number of motor vehicle thefts {i}");
                break;
            }


            streamWrite.Close();
            streamRead.Close();
        }
    }
}