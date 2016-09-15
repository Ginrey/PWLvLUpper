using OOGLibrary.IO.PacketBase.Server;
using OOGLibrary.Network;
using PWLvlUpper.PWBot;
using PWLvlUpper.PWBot.OOGTools;
using PWLvlUpper.PWStuct;
//using SkinSoft.VisualStyler;
//using SkinSoft.VisualStyler.Licensing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UtilsLib;
using xNet.Net;

namespace PWLvlUpper
{
    partial class Form1 : System.Windows.Forms.Form
    {
        private string resultPath;

        private ProxyFactory proxyFactory;

        private System.Collections.Generic.List<GameServer> servers;

        private GameServer selectedServer;

        private int minLvl;

        private int accountsCount;

        private int accountsCountValid;

        private int accountsCountInvalid;

        private int accountsMeditation;

        private MultiWorker<PWGameAccount> worker;

        private PropertySaver propertySaver;
        

        
        private ProxyFactory createProxyFactory()
        {
            ProxyFactory.ProxyType proxyType = 0;
            if (this.radioButtonProxySocks4.Checked)
            {
                proxyType = (ProxyFactory.ProxyType) 1;
            }
            else if (this.radioButtonProxySocks5.Checked)
            {
                proxyType = (ProxyFactory.ProxyType) 2;
            }
            else if (this.radioButtonProxyHTTP.Checked)
            {
                proxyType = (ProxyFactory.ProxyType) 3;
            }
            if (proxyType != null)
            {
                string text = null;
                string text2 = null;
                if (!string.IsNullOrWhiteSpace(this.textBoxProxyLogin.Text))
                {
                    text = this.textBoxProxyLogin.Text;
                    text2 = this.textBoxProxyPassword.Text;
                }
                ProxyFactory.ProxyListType proxyListType;
                string text3;
                if (this.checkBoxProxyUrl.Checked)
                {
                    proxyListType = (ProxyFactory.ProxyListType) 1;
                    text3 = this.textBoxProxyUrl.Text;
                }
                else
                {
                    proxyListType = 0;
                    text3 = this.textBoxProxyPath.Text;
                }
                return new ProxyFactory(proxyType, proxyListType, text3, text, text2);
            }
            return new ProxyFactory();
        }

        private System.Collections.Generic.List<PWGameAccount> createAccounts()
        {
            System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>> source = Misc.LoadAccounts(this.textBoxAccsPath.Text, true, false);
            return (from x in source
                    select new PWGameAccount(x.Key, x.Value)).ToList<PWGameAccount>();
        }

        private void createServers()
        {
            this.servers = new System.Collections.Generic.List<GameServer>();
            string[] array = (from x in System.IO.File.ReadAllLines("config\\serverlist.txt")
                              where !string.IsNullOrWhiteSpace(x)
                              select x).ToArray<string>();
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text = array2[i];
                string[] array3 = text.Split(new char[]
                {
                    '\t'
                });
                string[] array4 = array3[1].Trim().Split(new char[]
                {
                    ':'
                });
                if (array3.Length >= 2)
                {
                    this.servers.Add(new GameServer(array3[0].Trim(), array4[1], int.Parse(array4[0])));
                }
            }
            this.comboBoxServer.Items.AddRange((from x in this.servers
                                                select x.Name).ToArray<string>());
        }

        private bool processAccount(PWGameAccount pwAccount)
        {
            ProxyClient proxyClient = null;
            LvlChecker lvlChecker = new LvlChecker(this.selectedServer, pwAccount, this.minLvl);
            bool result;
            try
            {
                if (proxyClient == null)
                {
                    proxyClient = this.proxyFactory.GetProxy();
                }
                lvlChecker.Start(proxyClient);
                lvlChecker.Disconnect();
                result = true;
            }
            catch (PacketException ex)
            {
                lvlChecker.Disconnect();
                if (!(ex.ErrorPacket is ErrorInfoS05))
                {
                    proxyClient = this.proxyFactory.GetProxy();
                   
                }
                ErrorInfoS05 errorInfoS = (ErrorInfoS05)ex.ErrorPacket;
                if (errorInfoS.ErrorType == 8 || errorInfoS.ErrorType == 32 || errorInfoS.ErrorType == 135)
                {
                    result = false;
                }
                else
                {
                    pwAccount.Error = true;
                    result = true;
                }
            }
            catch (System.Exception ex2)
            {
                if (ex2.Message == "Auth" || ex2.Message == "wrong" || ex2.Message == "no exists" || ex2.Message == "user is blocked")
                {
                    pwAccount.Error = true;
                    result = true;
                }
                else
                {
                    lvlChecker.Disconnect();
                    result = false;
                }
            }
            return result;
        }

        private void updateCb(System.Collections.Generic.List<PWGameAccount> items, MultiWorker<PWGameAccount>.WorkerState State)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            System.Text.StringBuilder stringBuilder2 = new System.Text.StringBuilder();
            foreach (PWGameAccount current in items)
            {
                if (!current.Error)
                {
                    this.accountsCountValid++;
                    stringBuilder.AppendLine(current.Login + ":" + current.Password);
                }
                else
                {
                    this.accountsCountInvalid++;
                    stringBuilder2.AppendLine(current.Login + ":" + current.Password);
                }
            }
            if (items.Count != 0)
            {
                System.IO.File.AppendAllText(this.resultPath + "valids.txt", stringBuilder.ToString());
                System.IO.File.AppendAllText(this.resultPath + "invalids.txt", stringBuilder2.ToString());
            }
            this.progressBar1.Maximum = this.accountsCount;
            this.progressBar1.Value = this.accountsCountValid + this.accountsCountInvalid;
            this.labelValids.Text = this.accountsCountValid.ToString();
            this.labelInvalids.Text = this.accountsCountInvalid.ToString();
            this.labelProgress.Text = this.progressBar1.Value.ToString() + "/" + this.progressBar1.Maximum.ToString();
            this.labelProxyCount.Text = this.proxyFactory.ProxyUsedCount + "/" + this.proxyFactory.ProxyCount;
            if (State == null)
            {
                System.Collections.Generic.List<PWGameAccount> undoneAccounts = this.worker.GetUndoneAccounts();
                if (undoneAccounts.Count != 0)
                {
                    System.IO.File.WriteAllLines(this.resultPath + "undone.txt", from x in undoneAccounts
                                                                                 select x.Login + ":" + x.Password);
                }
                this.buttonStart.Enabled = true;
                this.buttonStart.Text = "Начать";
            }
        }

        private void buttonStart_Click(object sender, System.EventArgs e)
        {
            if (this.buttonStart.Text == "Начать")
            {
                this.selectedServer = this.servers[this.comboBoxServer.SelectedIndex];
                this.minLvl = (int)this.numericMinLvl.Value;
                this.resultPath = Misc.GetPathForResult(this.textBoxPathSave.Text);
                this.proxyFactory = this.createProxyFactory();
                System.Collections.Generic.List<PWGameAccount> list = this.createAccounts();
                this.accountsCount = list.Count;
                this.accountsCountValid = 0;
                this.accountsCountInvalid = 0;
                this.worker = new MultiWorker<PWGameAccount>(new MultiWorker<PWGameAccount>.ProcessItem(this.processAccount), new MultiWorker<PWGameAccount>.UpdateCallback(this.updateCb));
                this.worker.Start(list, (int)this.numericThreadsCount.Value, 2000);
                this.buttonStart.Text = "Остановить";
                this.updateCb(new System.Collections.Generic.List<PWGameAccount>(), (MultiWorker<PWGameAccount>.WorkerState) 1);
                return;
            }
            if (this.buttonStart.Text == "Остановить" && this.worker.Stop())
            {
                this.buttonStart.Enabled = false;
                this.buttonStart.Text = "Остановка..";
            }
        }

        public Form1()
        {
            if (GlobalConfig.ServerStrings == null)
            {
                return;
            }
            this.InitializeComponent();
            this.Text = this.Text + " " + GlobalConfig.Version;
            this.createServers();
            this.propertySaver = new PropertySaver(this, "config\\settingsDefault.cfg", "config\\settings.cfg");
            this.propertySaver.Load();
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
            this.checkBox_Trigger(null, null);
            TestMail testMail = new TestMail();
            testMail.Test();
        }

        private void Form1_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            this.propertySaver.Save();
        }

        private void buttonSettings_Click(object sender, System.EventArgs e)
        {
            if (sender == this.buttonOpenAccs)
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "Txt файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.textBoxAccsPath.Text = openFileDialog.FileName;
                }
            }
            if (sender == this.buttonOpenProxy)
            {
                System.Windows.Forms.OpenFileDialog openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog2.Filter = "Txt файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.textBoxProxyPath.Text = openFileDialog2.FileName;
                }
            }
            if (sender == this.buttonSaveDb)
            {
                System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.textBoxPathSave.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void radioButton_Click(object sender, System.EventArgs e)
        {
            ((System.Windows.Forms.RadioButton)sender).Checked = true;
        }

        private void checkBox_Trigger(object sender, System.EventArgs e)
        {
            this.textBoxProxyUrl.Enabled = this.checkBoxProxyUrl.Checked;
            this.textBoxProxyPath.Enabled = !this.checkBoxProxyUrl.Checked;
            this.buttonOpenProxy.Enabled = !this.checkBoxProxyUrl.Checked;
        }
       
    }
}

