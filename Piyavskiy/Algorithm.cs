using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piyavskiy
{

    internal class Algorithm
    {
        
        double l;
        public double L
        {
            get
            { 
                return l;
            }
            set
            {
                l = value;
            } 
        }
        double e;
        public double E
        {
            get
            {
                return e;
            }
            set
            {
                e = value;
            }
        }

        double left;
        public double Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }
        double right;
        public double Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }
        
        public  int Iteration(int STEP)
        {
            return (int)((right - left) * 10+STEP);
        }

        public static double Function(double x)
        {
            string NumberFunc="7";
            switch(NumberFunc)
            {
                case "1": return (Math.Pow(x, 2));
                case "2": return (Math.Pow(x, 3));
                case "3": return (Math.Pow(x, 3)+Math.Pow(x, 2));
                case "4": return (Math.Pow(x, 2)+Math.Pow(x, 4));
                case "5": return (Math.Cos(x));
                case "6": return (Math.Cos(Math.Pow(x, 2)));
                case "7": return (Math.Sin(x)+Math.Sin(10*x/3));
                case "8": return (Math.Sin(Math.Pow(x, 2))); 
                case "9": return (Math.Pow(x, 2)+ Math.Pow(x, 3)- Math.Pow(x, 4));
                case "10": return (Math.Pow(x, 3)+ Math.Pow(x, 4)- Math.Pow(x, 5)+ Math.Pow(x, 6));
                case "11": return (Math.Pow(x, 4)- Math.Pow(x, 5)+ Math.Pow(x, 6)- Math.Pow(x, 7));
                case "12": return (Math.Pow(x, 5)- Math.Pow(x, 6)- Math.Pow(x, 7)- Math.Pow(x, 8));
                case "13": return (Math.Pow(x, 6)+ Math.Pow(x, 7)+ Math.Pow(x, 8)+ Math.Pow(x, 9));
                case "14": return (Math.Pow(x, 7)+ Math.Pow(x, 8)+ Math.Pow(x, 9)+ Math.Pow(x, 10)+ Math.Pow(x, 11));
                case "15": return (Math.Pow(x, 8)- Math.Pow(x, 9)- Math.Pow(x, 10)- Math.Pow(x, 11)- Math.Pow(x, 12));
                case "16": return (Math.Pow(x, 9)+ Math.Pow(x, 10)- Math.Pow(x, 11)+ Math.Pow(x, 12)- Math.Pow(x, 13));
                case "17": return (Math.Pow(x, 10))- Math.Pow(x, 11)+ Math.Pow(x, 12)- Math.Pow(x, 13)+ Math.Pow(x, 14);
                case "18": return (Math.Tan(Math.Pow(x, 2)));
                case "19": return (Math.Pow(x, 1));
                case "20": return (Math.Pow(x, -1));
                
                default:
                return(Math.Pow(x, 2));
            }
            
        }
        public double SearchConstLipshitz()
        {
            return 0;
        }
        public  void SearchMinoranta(out double minoranta, out double xminoranta, int i,double[] X,double left,double right, double criterio,double L,int step)
        {
           // Console.WriteLine("Считаем миноранту");
            double x=left+(right - left) / (100 * step);
            minoranta = Int32.MaxValue;
            xminoranta = Int32.MinValue;

            while (x < right)
            {
                double minorantaCur = double.MinValue;
                //Console.WriteLine("Считаем миноранту тут {0}");
                for (int j = 0; j < i; j++)
                {
                   
                    double min = double.MinValue;
                    min = Algorithm.Function(x) - L * Math.Abs(x - X[j]);
                 //   Console.WriteLine("Посмотрим какое влияние оказывает константа Липшица:{0}", L * Math.Abs(x - X[j]));
                 //   Console.WriteLine("на этом шаге посмотрим миноранты {0}", min);

                    if (min > minorantaCur && min<criterio )
                    {
                        minorantaCur = min;
                   //     Console.WriteLine("Смена на {0}", minorantaCur);
                    }
                }
              //  Console.WriteLine("среди этих минорант мы выбрали большую по определению {0}", minorantaCur);
                if (minorantaCur < minoranta )
                    {
                        xminoranta = x;
                        minoranta = minorantaCur;
                 //      Console.WriteLine("Меняем миноранту тут {0} и точку миноранты тоже {1}",minoranta,xminoranta);


                    }
                
                x = x + (right-left)/(10*step);
               // Console.ReadKey();
            }
            
            


        }


     
        public void Realization()
        {
            double LeftBorder = 2.7;//левая граница компакта
            double RightBorder = 7.5;//правая граница компакта
            double ConstLipshitz =4.29;//константа липшица
            double Epsilon = 0.1;//точность
            Algorithm Pyavskiy = new Algorithm();
            Pyavskiy.left = LeftBorder;
            Pyavskiy.right = RightBorder;
            Pyavskiy.l = ConstLipshitz;
            Pyavskiy.e = Epsilon;
            int step = 1;
            bool flag = true;
            double LocalMinimum = 0;
            double LocalMinimumX = 0;
            double minoranta = double.MinValue, xminoranta = double.MinValue;
            double[] y = new double[Pyavskiy.Iteration(step)];
            double[] x = new double[Pyavskiy.Iteration(step)];
            x[0] = Pyavskiy.Left;
            y[0] = Algorithm.Function(Pyavskiy.Left);
            double maxL=0,curL = 0;
            while (flag)
            {
                


                for (int i = 1; i < Pyavskiy.Iteration(step); i++)
                {
                    if (step == 1)
                    {
                        x[i] = x[i - 1] + (Pyavskiy.Right - Pyavskiy.Left) / (Pyavskiy.Iteration(step));
                        y[i] = Algorithm.Function(x[i]);
                        curL = (y[i] - y[i - 1]) / (x[i] - x[i - 1]);
                        if (curL > maxL)
                        {
                            maxL = curL;
                        }
                    }
                    else
                    {
                        if (i == Pyavskiy.Iteration(step) - 1)
                        {
                            x[i] = xminoranta;
                            y[i] = Algorithm.Function(xminoranta);
                        }


                    }
                   
                }
                for (int i = 0; i < Pyavskiy.Iteration(step); i++)
                {
                    if (y[i] < LocalMinimum)
                    {
                        LocalMinimum = y[i];//Найден f*k
                        LocalMinimumX = x[i]; //соответствующее ему значение аргумента
                        Console.WriteLine("Найден локальный минимум y[{0}] = {1} , x[{0}] = {2}", i, LocalMinimum, LocalMinimumX);
                    }
                }

                for (int i=0; i<Pyavskiy.Iteration(step);i++)
                {
                    Console.WriteLine("x[{0}] = {1} , y[{0}] = {2}", i, x[i], y[i]);
                }
                if (step == 1)
                {
                    Console.WriteLine("мы нашли ту константу липшица,что нам надо {0}", maxL);
                    Console.ReadKey(); 
                }
                SearchMinoranta(out minoranta,out xminoranta,Pyavskiy.Iteration(step), x, LocalMinimumX- (Pyavskiy.Right - Pyavskiy.Left) / (Pyavskiy.Iteration(step)), LocalMinimumX+ (Pyavskiy.Right - Pyavskiy.Left) / (Pyavskiy.Iteration(step)), LocalMinimum,Pyavskiy.l,step);
                    
                    Console.WriteLine("Найден миноранта  в точке  со значением {0} в точке {1}", minoranta,xminoranta);
                    if (Math.Abs(LocalMinimum - minoranta) < Pyavskiy.e)
                    {
                        Console.WriteLine("Найден минимум{0} в точке {1} на шаге {2} ", LocalMinimum, LocalMinimumX, step);
                    Console.ReadKey();
                        flag = false ;
                    }
                    else
                    {
                    Console.WriteLine("НЕ Найден минимум{0} в точке {1} на шаге {2} ", LocalMinimum, LocalMinimumX, step);
                    step++;
                    //Console.ReadKey();
                    Array.Resize(ref x, Pyavskiy.Iteration(step));
                    Array.Resize(ref y, Pyavskiy.Iteration(step));
                    }
            
            }
        }
    }
    
}
