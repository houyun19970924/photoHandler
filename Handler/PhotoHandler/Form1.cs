using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoHandler
{
    public partial class Form1 : Form
    {
        private static FileTrans fileTrans = new FileTrans();
        private static PhotoManger photomanger = new PhotoManger();
        private static Gauss gauss = new Gauss();
        private string filepath;
        private Bitmap firstBitmap;
        private Bitmap bitmap;

        public Form1()
        {
            InitializeComponent();
        }

        //灰度
        
        private void radioButton1_Click(object sender, EventArgs e)
        {
            setVisible(0);
            if (emptyPicBoxCheck())
                pictureBox1.Image = photomanger.grayHandler((Bitmap)bitmap.Clone());
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            setVisible(1);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            setVisible(2);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            setVisible(3);
        }
        
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获得选中的文件的路径
            filepath = fileTrans.fileConvert();
            try
            {
                //创建两个bitmap变量来读取文件
                Bitmap bmp = new Bitmap(filepath);
                Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppRgb);
                Graphics draw = Graphics.FromImage(bmp2);
                draw.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
                //读取bmp2到picturebox 
                pictureBox1.Image = bmp2;
                //设定返回与还原所需图源
                bitmap = (Bitmap)(pictureBox1.Image.Clone());
                firstBitmap = (Bitmap)(pictureBox1.Image.Clone());
                //释放bmp文件资源
                draw.Dispose();
                bmp.Dispose();
            }
            catch (Exception){}
        }

        //红色色相
        private void 红ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectFliter(2);
        }

        //黄色色相
        private void 黄ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectFliter(1);
        }

        //选取蓝色色相
        private void 蓝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectFliter(0);
        }

        //保存
        private void button1_Click(object sender, EventArgs e)
        {
            resetTrackBar();
            if (emptyPicBoxCheck())
            {
                bitmap = (Bitmap)(pictureBox1.Image.Clone());
                MessageBox.Show("执行成功");
            }
        }

        //取消
        private void button2_Click(object sender, EventArgs e)
        {
            resetTrackBar();
            pictureBox1.Image = bitmap;
        }

        //还原
        private void button3_Click(object sender, EventArgs e)
        {
            resetTrackBar();
            pictureBox1.Image = firstBitmap;
        }

        //左右翻转
        private void bt3_Click(object sender, EventArgs e)
        {
            if (emptyPicBoxCheck())
            {
                bitmap = photomanger.RevPicW(bitmap);
                storeProcess();
            }   
        }

        //上下翻转
        private void bt4_Click(object sender, EventArgs e)
        {
            if (emptyPicBoxCheck())
            {
                bitmap = photomanger.RevPicH(bitmap);
                storeProcess();
            }
        }

        //顺时针旋转
        private void bt1_Click(object sender, EventArgs e)
        {
            if (emptyPicBoxCheck())
            {
                bitmap = photomanger.rotPicClo(bitmap);
                storeProcess();
            }
        }

        //逆时针旋转
        private void bt2_Click(object sender, EventArgs e)
        {
            if (emptyPicBoxCheck())
            {
                bitmap = photomanger.rotPicUnClo(bitmap);
                storeProcess();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            selectRGBModel(redText, trackBar1);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            selectRGBModel(greenText,trackBar2);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            selectRGBModel(blueText, trackBar3);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (emptyPicBoxCheck())
            {
                contrastText.Text = Convert.ToString(trackBar4.Value);
                pictureBox1.Image = photomanger.setContrast(trackBar4.Value, bitmap);
            }
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            if (emptyPicBoxCheck())
            {
                brightNess.Text = Convert.ToString(trackBar5.Value);
                pictureBox1.Image = photomanger.setBrightness(trackBar5.Value, bitmap);
            }
        }

        private void trackBar6_MouseUp(object sender, MouseEventArgs e)
        {
            if (emptyPicBoxCheck())
            {
                gaussText.Text = Convert.ToString(trackBar6.Value);
                pictureBox1.Image = gauss.GaussianFilt(bitmap, trackBar6.Value);
            }
        }

        private void 保存更改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (emptyPicBoxCheck())
            {
                bitmap.Save(filepath);
                MessageBox.Show("保存成功");
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (emptyPicBoxCheck())
            {
                string filePath = fileTrans.fileSave();
                bitmap.Save(filePath + ".png");
                MessageBox.Show("保存成功");
            }
        }

        private void 顺时针旋转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bt1_Click(sender, e);
        }

        private void 逆时针旋转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bt2_Click(sender, e);
        }

        private void 左右翻转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bt3_Click(sender, e);
        }

        private void 上下翻转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bt4_Click(sender, e);
        }

        private void 灰度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            radioButton1_Click(sender, e);
        }

        //亮度
        private void 高斯模糊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            radioButton4_CheckedChanged(sender, e);
        }

        //对比度
        private void 径向模糊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            radioButton4_CheckedChanged(sender, e);
        }

        private void rGB键入ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            radioButton2_CheckedChanged(sender, e);
        }

        private void 高斯模糊ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            radioButton3_CheckedChanged(sender, e);
        }

        private void setVisible(int index)
        {
            GroupBox[] tempGroup = { groupBox8, groupBox5, groupBox6, groupBox7 };
            for (int j = 0; j < tempGroup.Length; j++)
            {
                //设置groupbox的可见
                if (j == index)
                    tempGroup[j].Visible = true;
                else
                    tempGroup[j].Visible = false;
            }
        }


        //检查picturebox是否为空
        private bool emptyPicBoxCheck()
        {
            if (bitmap != null)
                return true;
            else
            {
                MessageBox.Show("请先打开一张图片");
                return false;
            }
        }

        //选择色相模式
        private void selectFliter(int index)
        {
            if (emptyPicBoxCheck())
                pictureBox1.Image = photomanger.setColorFilter(index, (Bitmap)bitmap.Clone());
        }

        //旋转反转即使未保存也会储存进度
        private void storeProcess()
        {
            pictureBox1.Image = bitmap;
        }

        //选取RGB
        private void selectRGBModel(Label selectLabel, TrackBar selectTrackBar)
        {
            if (emptyPicBoxCheck())
            {
                selectLabel.Text = Convert.ToString(selectTrackBar.Value);
                int[] Index = { trackBar3.Value, trackBar2.Value, trackBar1.Value };
                pictureBox1.Image = photomanger.setRGBColor(Index, bitmap);
            }
        }

        //恢复trackbar
        private void resetTrackBar()
        {
            TrackBar[] trackBars = { trackBar1, trackBar2, trackBar3, trackBar4, trackBar5, trackBar6};
            foreach(TrackBar tempBar in trackBars)
            {
                tempBar.Value = 0;
            }
            Label[] labels = { redText, greenText, blueText, brightNess, contrastText };
            foreach(Label tempLabel in labels)
            {
                tempLabel.Text = "0";
            } 
        }
    }
}
