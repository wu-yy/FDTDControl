using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing; //Point
using System.Drawing.Imaging;

namespace FDTDControl
{
    class Program
    {


        private static void LeftMouseClick(int x,int y)
        {
            MouseHookHelper.POINT pointInfo =new MouseHookHelper.POINT(){X=x,Y=y};
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

        private static void MouseMove(int x,int y)
        {
            MouseHookHelper.POINT pointInfo = new MouseHookHelper.POINT() { X = x, Y = y };
            //移动鼠标
            MouseHookHelper.mouse_event(MouseHookHelper.MOUSEEVENTF_MOVE,
                        pointInfo.X,
                        pointInfo.Y, 0, 0);
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

        //键盘按键删除
        private static void DelText(int a)
        {
            for (int i = 1; i <= a; i++)
            {
                MouseHookHelper.keybd_event(8, 0, 0, 0);
                MouseHookHelper.keybd_event(8, 0, 2, 0);
            }
        }

        // 通过键盘输入数据
        private static void InputText(string testStr)
        {
            for (int counter = 0; counter < testStr.Length; counter++)
            {
                char sc=testStr[counter];
                if (sc == '0' || sc == '1' || sc == '2' || sc == '3' || sc == '4' || sc == '5' || sc == '6'
                    || sc == '7' || sc == '8' || sc == '9')
                {
                    int a1 = int.Parse(testStr[counter].ToString()) + 48;
                    MouseHookHelper.keybd_event((byte)a1, 0, 0, 0);
                    MouseHookHelper.keybd_event((byte)a1, 0, 2, 0);
                    Console.WriteLine(a1);
                }
                else
                {
                    int a1 = 110;
                    MouseHookHelper.keybd_event((byte)a1, 0, 0, 0);
                    MouseHookHelper.keybd_event((byte)a1, 0, 2, 0);
                    Console.WriteLine('.');
                }
            }
        }

        static void Main(string[] args)
        {
            ScreenCapture screen = new ScreenCapture(); //用于截图
            IntPtr awin = MouseHookHelper.FindWindow("QWidget",
                "Lumerical FDTD Solutions - Layout - test.fsp [C:/Users/Administrator/Desktop]");
            if (awin == IntPtr.Zero)
            {
                MessageBox.Show("没有找到窗体");        
                return;
            }
            else
            {
                MouseHookHelper.RECT rc = new MouseHookHelper.RECT();

                MouseHookHelper.GetWindowRect(awin,ref rc);

                int width = rc.Right - rc.Left;  //窗口的宽度
                int height = rc.Bottom - rc.Top; //窗口的高度
                int x = rc.Left;     //窗口的x坐标值
                int y = rc.Top;      // 窗口的y 坐标值


                MouseHookHelper.SetForegroundWindow(awin);  // 设置当前窗口置前
                //MouseHookHelper.ShowWindow(awin, MouseHookHelper.SW_SHOWNOACTIVATE);//4、5
                
                // 点击界面让界面置在最前面
                LeftMouseClick(x,y);             
                
                // 实时获取鼠标的位置
                //PrintMousePoint(x, y);

                //左键点击 Object
                LeftMouseClick( x + 99,y + 227);
                
                // 左键点击 Edit
                LeftMouseClick(x + 60, y + 42);                               
       
                //左键点击 Edit Properties
                LeftMouseClick(x + 90,y + 190);
     
                Point p = new Point(1, 1);//定义存放获取坐标的point变量 
                //点击激活窗口
                LeftMouseClick(x + 357,y + 229);
               
                //移动鼠标获取输入框
                MouseMove(72,38);
                MouseHookHelper.GetCursorPos(ref p);
                LeftMouseClick(p.X,p.Y);
                //删除之前的数据
                DelText(4);
                //输入新的数据
                InputText("2.1");
                   
                //全屏截图保存
                screen.CaptureScreenToFile("1.jpg", ImageFormat.Jpeg);
                return;
            }
        }
    }
}