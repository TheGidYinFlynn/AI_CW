using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
namespace caverns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string Flocation = Microsoft.VisualBasic.Interaction.InputBox("Please enter the path to the file", "file explorer");

            try
            {
                String inputcheck = File.ReadAllText(Flocation);
                 
            }




            catch
            {
                MessageBox.Show("There was a problem with either the file please reload to try again");
                System.Environment.Exit(1);
             
            }

            DateTime startTime, endTime;
            startTime = DateTime.Now;
            char a = ',';
            String input = File.ReadAllText(Flocation);
            string[] raw = input.Split(a);
            int nextnode = 0;
            lblOutput.Content = "this program is starting";
            object content = lblOutput.Content;
            int[] data = new int[raw.Length];
            ;
            for (int i = 0; i < raw.Length; i++)
            {
                data[i] = Convert.ToInt32(raw[i]);
            }
            
            double[] distance = new double[data[0]];
            int[] visited = new int[data[0]];
            int[] preD = new int[data[0]];


            int[,] points = new int[data[0], 2];

            int[,] connections = new int[data[0], data[0]];

            int currentPoint = 0;
            int XorY = 0;


            for (int i = 0; i < (data[0] * 2); i++)
            {
                points[currentPoint, XorY] = data[i + 1];

                if (XorY == 0)
                {
                    XorY = 1;
                }
                else
                {
                    XorY = 0;
                    currentPoint++;
                }

            }

            int pointer = (2 * data[0] + 1);

            for (int i = 0; i < data[0]; i++)
            {
                for (int j = 0; j < data[0]; j++)
                {


                    connections[j, i] = data[pointer];
                    pointer++;

                }
            }


            double[,] dist = new double[data[0], data[0]];

            for (int i = 0; i < data[0]; i++)
            {
                for (int j = 0; j < data[0]; j++)
                {
                    if (connections[i, j] == 0)
                    {
                        dist[i, j] = 999;
                    }
                    else
                    {
                        int A;
                        int B;

                        A = points[i, 0] - points[j, 0];
                        A = Convert.ToInt32(Math.Sqrt(A * A));

                        B = points[i, 1] - points[j, 1];
                        B = Convert.ToInt32(Math.Sqrt(B * B));

                        dist[i, j] = Math.Sqrt(A * A + B * B);





                    }
                }
            }

            for (int i = 0; i < data[0]; i++)
            {
                double current = dist[0, i];

                distance[i] = dist[0,i];

            }
            distance[0] = 0;
            visited[0] = 1;

            for (int i = 0; i < data[0]; i++)
            {

                double min = 999;

                for (int j = 0; j < data[0]; j++)
                {
                    if (min > distance[j] && visited[j] != 1)
                    {
                        min = distance[j];
                        nextnode = j;
                    }
                    
                }

                visited[nextnode] = 1;

                for (int k = 0; k < data[0]; k++)
                {

                    if (visited[k] != 1)
                    {
                        if (min + dist[nextnode, k] < distance[k])
                        {
                            distance[k] = min + dist[nextnode, k];
                            preD[k] = nextnode;

                        }
                    }
                }

           
            

             
            
         }
             

             for (int i = 0; i < data[0]; i++)
             {
                 int j;


                 lblOutput.Content = content + Environment.NewLine + "Path = " + (i + 1) + " ";
                 content = lblOutput.Content;

                 j = i;

                 do
                 {
                     j = preD[j];

                     lblOutput.Content = content + "<--" + (j + 1);
                     content = lblOutput.Content;

                 } while (j != 0);

                 


             }

             lblOutput.Content = content + Environment.NewLine + " parth is read Goal <-- point <-- point <-- starting point" + Environment.NewLine; 
             content = lblOutput.Content;
            for (int i = 0; i < data[0]; i++)
            {
                lblOutput.Content = content + Environment.NewLine + "  path {" + i + "} distance  = " + distance[i] + " ";
                content = lblOutput.Content;

            }

             endTime = DateTime.Now;
             double elapsedtime = ((TimeSpan)(endTime - startTime)).TotalMilliseconds;

             lblOutput.Content = content + Environment.NewLine + Environment.NewLine +  " " + elapsedtime + " Milliseconds";

        }

       
        
        
        
    }
}
