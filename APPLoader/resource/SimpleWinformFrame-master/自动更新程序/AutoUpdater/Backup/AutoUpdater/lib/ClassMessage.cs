using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace AutoUpdater.lib
{
    class ClassMessage
    {
        /// <summary> 
        /// 弹出提示框 
        /// </summary> 
        /// <param name="txt">输入提示信息</param> 
        public void messageInfoBox(string strMessage)
        {
            MessageBox.Show(
                strMessage,
                "提示信息",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
