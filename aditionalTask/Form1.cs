using System;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace aditionalTask
{
    public partial class Form1 : Form
    {
        Func<bool> myDel;
        IAsyncResult asyncResult;
        public Form1()
        {
            InitializeComponent();
            myDel = new Func<bool>(OpenImage);
        }

        private bool OpenImage()
        {
            try
            {
                this.pictureBox.Image = Properties.Resources.Hello_World;
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        private void CallbackMethod(IAsyncResult asyncResult)
        {
            Func<bool> func = (Func<bool>)(asyncResult as AsyncResult).AsyncDelegate;
            
            if (func.EndInvoke(asyncResult))
                label1.Text = "result: successfully!";
            else
                label1.Text = "result: failed!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            asyncResult = myDel.BeginInvoke(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            if (asyncResult.IsCompleted)
                label1.Text = "result: completed!";
            else
                label1.Text = "result: not completed!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            asyncResult = myDel.BeginInvoke(null, null);
            bool result = myDel.EndInvoke(asyncResult);
            if (result)
                label1.Text = "result: successfully!";
            else
                label1.Text = "result: failed!";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            asyncResult = myDel.BeginInvoke(CallbackMethod, null);
        }
    }
}
