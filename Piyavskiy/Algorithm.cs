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
            return (Math.Pow(x, 2));
        }
        public  double SearchMinoranta(int i,double[] X,double left,double right, double criterio,double L,int step)
        {
           // Console.WriteLine("Считаем миноранту");
            double Max=double.MinValue;
            double minoranta;
            double x=left;
           /* foreach (double t in X)
            {
                Console.WriteLine("это массив из подданых иксов {0}", t);
            }
            Console.WriteLine("это константа липшица {0}", L);*/

            while (x < right)
            {
                //Console.WriteLine("Считаем миноранту тут {0}");
                for (int j = 0; j < i; j++)
                {
                    minoranta = Algorithm.Function(X[j]) - L * Math.Abs(x - X[j]);
                    //Console.WriteLine("на этом шаге посмотрим миноранты {0}", minoranta);
                    if (minoranta <= criterio)
                    {
                     //   Console.WriteLine("удовлетворяет критерию {0}", minoranta);
                    }
                    if (minoranta > Max && minoranta <= criterio)
                    {
                       // Console.WriteLine("Меняем миноранту тут {0}",minoranta);
                        Max = minoranta;
                    }
                }
                x = x + (right-left)/(99*step);
               // Console.ReadKey();
            }
            return Max;
        }
        public double SearchX(int i, double[] X, double left, double right,double criterio,double L,int step)
        {
           // Console.WriteLine("Считаем точку миноранты");
            double Max = double.MinValue;
            double minoranta;
            double xminoranta=double.MinValue;
            double x = left;
            while (x < right)
            {
              //  Console.WriteLine("Считаем точку миноранты тут {0}");
                for (int j = 0; j < i; j++)
                {
                    
                   // Console.WriteLine("Меняем точку миноранты тут {0}", xminoranta);
                    minoranta = Algorithm.Function(X[j]) - L * Math.Abs(x - X[j]);
                    /*if (minoranta <= criterio)
                    {
                        Console.WriteLine("удовлетворяет критерию {0}", xminoranta);
                    }*/
                    if (minoranta > Max && minoranta<=criterio ) 
                    {
                        Max = minoranta;
                        xminoranta = x;
                       // Console.WriteLine("Меняем точку миноранты тут {0}",xminoranta);
                    }
                }
                x = x + (right - left) / (99 * step); 
            }
            
            return xminoranta;
        }

        public void Realization()
        {
            double LeftBorder = -2;//левая граница компакта
            double RightBorder = 2;//правая граница компакта
            double ConstLipshitz =0.1;//константа липшица
            double Epsilon = 0.0001;//точность
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
            LocalMinimum = y[0];
            //где-то здесь начнем пошаговый алгоритм
            /*double[] y = new double[Pyavskiy.Iteration(step)];
            double[] x = new double[Pyavskiy.Iteration(step)];*/
            while (flag)
            {
                


                for (int i = 1; i < Pyavskiy.Iteration(step); i++)
                {
                    if (step == 1)
                    {
                        x[i] = x[i - 1] + (Pyavskiy.Right - Pyavskiy.Left) / (Pyavskiy.Iteration(step));
                        y[i] = Algorithm.Function(x[i]);
                    }
                    else
                    {
                        if (i == Pyavskiy.Iteration(step) - 1)
                        {
                            x[i] = xminoranta;
                            y[i] = Algorithm.Function(x[i]);
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
                    minoranta = SearchMinoranta(Pyavskiy.Iteration(step), x, Pyavskiy.Left, Pyavskiy.right,LocalMinimum,Pyavskiy.l,step);
                    xminoranta = SearchX(Pyavskiy.Iteration(step), x, Pyavskiy.Left, Pyavskiy.right,LocalMinimum,Pyavskiy.l,step);
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
                    Console.ReadKey();
                    Array.Resize(ref x, Pyavskiy.Iteration(step));
                    Array.Resize(ref y, Pyavskiy.Iteration(step));
                    }
            
            }
        }
    }
    
}
