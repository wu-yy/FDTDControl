using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing; //Point


namespace FDTDControl
{
    class Program
    {


        private static void LeftMouseClick(MouseHookHelper.POINT pointInfo)
        {

            //先移动鼠标到指定位置
            MouseHookHelper.SetCursorPos(pointInfo.X, pointInfo.Y);

            //按下鼠标左键
            MouseHookHelper.mouse_event(MouseHookHelper.MOUSEEVENTF_LEFTDOWN,
                        pointInfo.X,
                        pointInfo.Y, 0, 0);

            //松开鼠标左键
            MouseHookHelper.mouse_event(MouseHookHelper.MOUSEEVENTF_LEFTUP,
                        pointInfo.X ,
                        pointInfo.Y , 0, 0);

        }

        //实时打印鼠标的相对（x，y）位置
        private static void PrintMousePoint(int x,int y)
        {
            int x1 = x;
            int y1 = y;
            while (true)
            {

                Point p = new Point(1, 1);//定义存放获取坐标的point变量   
                MouseHookHelper.GetCursorPos(ref p);

                if (x1 != p.X && y1 != p.Y)
                {
                    System.Console.WriteLine("相对于父类窗口 dx:");
                    System.Console.WriteLine(p.X - x);
                    System.Console.WriteLine("相对于父类窗口 dy:");
                    System.Console.WriteLine(p.Y - y);
                    x1 = p.X;
                    y1 = p.Y;
                }


            }
        }

        static void Main(string[] args)
        {

            IntPtr awin = MouseHookHelper.FindWindow("HwndWrapper[DefaultDomain;;6e277d51-7be9-45b5-879e-37e195024ec9]", 
                "FDTDControl - Microsoft Visual Studio(管理员)");
            if (awin == IntPtr.Zero)
            {
                MessageBox.Show("没有找到窗体");

         
                return;
            }
            else
            {
                //MessageBox.Show(String.Format("{0:D}",awin));

                MouseHookHelper.RECT rc = new MouseHookHelper.RECT();

                MouseHookHelper.GetWindowRect(awin,ref rc);

                int width = rc.Right - rc.Left;  //窗口的宽度
                int height = rc.Bottom - rc.Top; //窗口的高度
                int x = rc.Left;
                int y = rc.Top;



                MouseHookHelper.SetForegroundWindow(awin);  // 设置当前窗口置前
                MouseHookHelper.ShowWindow(awin, MouseHookHelper.SW_SHOWNOACTIVATE);//4、5

                LeftMouseClick(new MouseHookHelper.POINT()  //点击鼠标
                {
                    X = x,
                    Y = y,
                });

             
                /*
                IntPtr editBtn= MouseHookHelper.FindWindowEx(awin, IntPtr.Zero, null, null);
                MouseHookHelper.RECT rc2 = new MouseHookHelper.RECT();
                MouseHookHelper.GetWindowRect(editBtn, ref rc2);
                int x2 = rc2.Left;
                int y2 = rc2.Top;
                LeftMouseClick(new MouseHookHelper.POINT()  //点击鼠标
                {
                    X = x2,
                    Y = y2,
                });
                */


                
                // 实时获取鼠标的位置
                //PrintMousePoint(x, y);

                LeftMouseClick(new MouseHookHelper.POINT()  //点击鼠标(x+466,y+466)
                {
                    X = x+466,
                    Y = y+466,
                });

                // 在鼠标点击的地方打印数字
                SendKeys.SendWait("12");
                return;

            }
        }
    }
}