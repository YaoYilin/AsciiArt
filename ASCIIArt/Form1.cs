using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ASCIIArt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 1;
        }
        private int[] ratio = new int[] { 1, 2, 10 };
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;

            fileDialog.Title = "请选择文件";
            fileDialog.Filter = @"图像文件(JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif; *.tiff; *.png| JPeg 图像文件(*.jpg;*.jpeg)
|*.jpg;*.jpeg |GIF 图像文件(*.gif)|*.gif |BMP图像文件(*.bmp)|*.bmp|Tiff图像文件(*.tif;*.tiff)|*.tif;*.tiff|Png图像文件(*.png)
| *.png |所有文件(*.*)|*.*";

            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                var srcBitmap = (Bitmap)Bitmap.FromFile(fileDialog.FileName, false);

                var v = comboBox1.SelectedIndex;
                var file = srcBitmap.ASCIIFilter(ratio[v]);

                var fileName = Environment.CurrentDirectory + "\\"+ Path.GetFileNameWithoutExtension(fileDialog.FileName) + ".txt";

                if(File.Exists(fileName))
                    File.Delete(fileName);

                FileStream fs = new FileStream(fileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(file);
                sw.Flush();
                sw.Close();
                fs.Close();
                System.Diagnostics.Process.Start("notepad.exe", fileName);
            }
        }
    }
}
