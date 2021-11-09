using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoHandler
{
    class FileTrans
    {
        public string fileConvert()
        {
            string filePath = null;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "选择图片";
            dialog.Filter = "所有文件(*.png/jpg)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
            }
            dialog.Dispose();
            return filePath;
        }

        public string fileSave()
        {
            string filePath = null;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "选择路径";
            dialog.Filter = "所有文件(*.png/jpg)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
            }
            return filePath;
        }
    }
}
