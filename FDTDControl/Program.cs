using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing; //Point
using System.Threading;
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

        private static void RightMouseClick(MouseHookHelper.POINT pointInfo)
        {

            //先移动鼠标到指定位置
            MouseHookHelper.SetCursorPos(pointInfo.X, pointInfo.Y);

            //按下鼠标鼠标右键
            MouseHookHelper.mouse_event(MouseHookHelper.MOUSEEVENTF_RIGHTDOWN,
                        pointInfo.X,
                        pointInfo.Y, 0, 0);

            //松开鼠标右键
            MouseHookHelper.mouse_event(MouseHookHelper.MOUSEEVENTF_RIGHTUP,
                        pointInfo.X,
                        pointInfo.Y, 0, 0);

            // MouseHookHelper.SetCursorPos(424, 393);

            // MouseHookHelper.mouse_event(MouseHookHelper.MOUSEEVENTF_RIGHTDOWN,
            //           629,
            //           391, 0, 0);

            //松开鼠标右键
            //MouseHookHelper.mouse_event(MouseHookHelper.MOUSEEVENTF_RIGHTUP,
            //            pointInfo.X,
            //            pointInfo.Y, 0, 0);

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

        public static void CopyFolder(string strFromPath, string strToPath, string filename,string to_filename)
        {
            string[] strFiles = { strFromPath + "\\" + filename };
            //循环拷贝文件
            for (int i = 0; i < strFiles.Length; i++)
            {
                //取得拷贝的文件名，只取文件名，地址截掉。
                //string strFileName = strFiles[i].Substring(strFiles[i].LastIndexOf("\\") + 1, strFiles[i].Length - strFiles[i].LastIndexOf("\\") - 1);
                //开始拷贝文件,true表示覆盖同名文件
                 if (! System.IO.Directory.Exists(strToPath))     // 返回bool类型，存在返回true，不存在返回false
                 {
                     System.IO.Directory.CreateDirectory(strToPath);      //不存在则创建路径
                 }
                System.IO.File.Copy(strFiles[i], strToPath + "\\" + to_filename, true);
            }
        }

        public static void DingTime()
        {
            while (true)
            {
                string curr = DateTime.Now.ToString("HH-mm-ss");
                // 判断是不是执行到当前的时间
                if (curr == "00-48-10")
                {
                    Console.WriteLine("current is now!");
                    break;
                }
            }
            Console.ReadKey();
        }

        public static void OpenExe()
        {
            System.Diagnostics.Process firstProcess = new System.Diagnostics.Process();
            firstProcess.StartInfo.FileName = "C:\\国盛证券同花顺新一代\\hexin.exe"; //字符串是 文件url地址
            firstProcess.StartInfo.UseShellExecute = false;
            firstProcess.StartInfo.RedirectStandardInput = true;
            firstProcess.StartInfo.RedirectStandardOutput = true;
            firstProcess.StartInfo.RedirectStandardError = true;
            firstProcess.StartInfo.CreateNoWindow = true;
            firstProcess.Start(); //启动线程

            Thread.Sleep(3000);
            LeftMouseClick(new MouseHookHelper.POINT()
            {
                X = 761,
                Y = 273,
            });
            Thread.Sleep(15000);
        }

        // 打印相对于桌面的坐标
        static void PrintScreenXY()
        {
            while (true)
            {
                Point screenPoint = Control.MousePosition;//鼠标相对于屏幕左上角的坐标

                Console.WriteLine(screenPoint.X);
                Console.WriteLine(screenPoint.Y);
                Thread.Sleep(2000);
            }
        }

        static void Main(string[] args)
        {
            // PrintScreenXY();
            while (true)
           {
               if (DateTime.Now.ToString("HH-mm-ss") != "09-30-00")
               {
                   // Console.WriteLine(DateTime.Now.ToString("HH-mm-ss"));
                   continue;
               }
                // 打开程序
                OpenExe();

                // 获取窗口
                IntPtr awin = IntPtr.Zero;
                WindowHookHelper.WindowInfo[] infos = WindowHookHelper.GetAllDesktopWindows();
                for (int i = 0; i < infos.Length; ++i)
                {
                    // Console.WriteLine(infos[i].szWindowName);
                    if (infos[i].szClassName.StartsWith("Afx:400000:b:10003") && infos[i].szWindowName.StartsWith("国盛证券"))
                    {
                        Console.WriteLine(infos[i].szWindowName);
                        awin = infos[i].hWnd;
                        break;
                    }
                }      

                if (awin == IntPtr.Zero)
                {
                    MessageBox.Show("没有找到窗体");
                    return;
                }
                else
                {
                    //MessageBox.Show(String.Format("{0:D}",awin));

                    MouseHookHelper.RECT rc = new MouseHookHelper.RECT();

                    MouseHookHelper.GetWindowRect(awin, ref rc);

                    int width = rc.Right - rc.Left;  //窗口的宽度
                    int height = rc.Bottom - rc.Top; //窗口的高度
                    int x = rc.Left;
                    int y = rc.Top;
                    x = 0;
                    y = 0;
                    Console.WriteLine(x);
                    Console.WriteLine(y);

                    MouseHookHelper.SetForegroundWindow(awin);  // 设置当前窗口置前
                    // 设置窗口最大
                    MouseHookHelper.ShowWindow(awin, MouseHookHelper.SW_SHOWMAXIMIZED);//4、5                   

                    LeftMouseClick(new MouseHookHelper.POINT()  //激活当前窗口
                    {
                        X = 2,
                        Y = 2,
                    });
                    Console.WriteLine("设置当前窗口置前");
                   

                    // 关闭推荐的咨询中心
                    Console.WriteLine("关闭推荐的咨询中心");
                    Thread.Sleep(1000);
                    LeftMouseClick(new MouseHookHelper.POINT()
                    {
                        X = 1332,
                        Y = 102,
                    });                 
                   
                    // 点个股
                    Console.WriteLine("点击个股");
                    LeftMouseClick(new MouseHookHelper.POINT()
                    {
                        X = 527,
                        Y = 78,
                    });

                    Thread.Sleep(1000);
                   
                    // 打印鼠标位置
                    // PrintMousePoint(x, y);
                    int max_count = 7800;
                    int count = 0;
                    while (count < max_count)
                    {
                        RightMouseClick(new MouseHookHelper.POINT()  //点击鼠标(x+466,y+466)
                        {
                            X = x + 363,
                            Y = y + 278,
                        });

                        LeftMouseClick(new MouseHookHelper.POINT()
                        {
                            X = x + 383,
                            Y = y + 393,
                        });

                        LeftMouseClick(new MouseHookHelper.POINT()
                        {
                            X = x + 629,
                            Y = y + 393,
                        });

                        int next_x = 809, next_y = 524;
                        Thread.Sleep(1000);
                        // 下一步点击
                        LeftMouseClick(new MouseHookHelper.POINT()
                        {
                            X = x + next_x,
                            Y = y + next_y,
                        });

                        LeftMouseClick(new MouseHookHelper.POINT()
                        {
                            X = x + next_x,
                            Y = y + next_y,
                        });

                        Thread.Sleep(1000);

                        // 下一步点击
                        LeftMouseClick(new MouseHookHelper.POINT()
                        {
                            X = x + next_x,
                            Y = y + next_y,
                        });

                        // 睡眠5秒，点击完成
                        Thread.Sleep(7000);//睡眠5000毫秒，也就是5秒

                        // 点击完成
                        LeftMouseClick(new MouseHookHelper.POINT()
                        {
                            X = x + next_x,
                            Y = y + next_y,
                        });

                        Thread.Sleep(1000);//睡眠5000毫秒，也就是5秒

                        // 点个股
                        LeftMouseClick(new MouseHookHelper.POINT()
                        {
                            X = x + 527,
                            Y = y + 78,
                        });

                        Thread.Sleep(1000);//睡眠5000毫秒，也就是5秒

                        // 点一下修正
                        LeftMouseClick(new MouseHookHelper.POINT()
                        {
                            X = x + 113,
                            Y = y + 70,
                        });

                        // 获取当前的时间戳
                        string to_folder = "F:\\tonghuashun\\" + DateTime.Now.ToString("yyyy-MM-dd");
                        string to_filename = DateTime.Now.ToString("HH-mm-ss") + ".xls";
                        CopyFolder("C:\\Documents and Settings\\Administrator\\桌面", to_folder,
                            "Table.xls", to_filename);

                        Thread.Sleep(1000 * 30);//睡眠半分钟
                        // 下午三点半关闭程序
                        if (DateTime.Now.ToString("HH-mm-ss") == "15-30-00")                        
                        {
                            // 下午三点半，停止程序
                            // 关闭程序
                            LeftMouseClick(new MouseHookHelper.POINT()
                            {
                                X = x + 1345,
                                Y = y + 17,
                            });
                            Thread.Sleep(1000 * 30);//睡眠半分钟
                            break;
                        }
                    }

            } // end while 

                // 在鼠标点击的地方打印数字
                //SendKeys.SendWait("{BS}");
                //SendKeys.SendWait("{BS}");
         
                //SendKeys.SendWait("123");
                //SendKeys.SendWait("123");
                return;

            }
        }
    }
}
