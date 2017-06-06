using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Multi_PirmasDarbas_AG
{


    public partial class Form1 : Form
    {

        private string kelias;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                kelias = fbd.SelectedPath;
                txtPath.Text = kelias;
            }
            else
            {
                MessageBox.Show("Neteisingas pasirinkimas");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Thread thread = new Thread(new ParameterizedThreadStart(PaieskaFailo));

            if (txtFailas.Text != "" && kelias != "")
            {
                thread.Start(kelias);
            }
            else
            {
                MessageBox.Show("Pirma įveskite ieškomo failo pavadinimą");
            }
        }

        private void PaieskaFailo(object kelias)
        {

            Stopwatch sw = Stopwatch.StartNew();

            Invoke(new EventHandler(delegate { txtdir2.Clear(); }));
            Invoke(new EventHandler(delegate { txtInfo.Clear(); }));

            string ieskomas = txtFailas.Text;

            string[] dirs = Directory.GetFiles((string)kelias, ieskomas + "*.*", SearchOption.AllDirectories);

            int current = 0;


            foreach (string dir in dirs)
            {
                try
                {
                    current++;
                    Invoke(new EventHandler(delegate { txtdir2.Text += "Failas: " + new DirectoryInfo(dir).Name + "\n"; }));
                    Invoke(new EventHandler(delegate { txtdir2.Text += "Failo direktorija: " + dir + "\n"; }));
                    Invoke(new EventHandler(delegate { txtdir2.Text += "\n"; }));
                    Invoke(new EventHandler(delegate { progressBar1.Value = (100 * current / dirs.Length); }));

                    Invoke(new EventHandler(delegate { label6.Text = progressBar1.Value.ToString() + "%"; }));
                    Thread.Sleep(100);

                    if (dirs.Length <= 0)
                    {
                        Invoke(new EventHandler(delegate { txtInfo.Text = "Nerasta tokių failų"; }));
                    }
                    else
                    {
                        Invoke(new EventHandler(delegate { txtInfo.Text = "Direktorijų kiekis, kuriose rastas failas: " + dirs.Length; }));
                    }
                }

                catch (UnauthorizedAccessException e)
                {
                    MessageBox.Show(e.Message);
                }

            }

            sw.Stop();
            BeginInvoke((MethodInvoker)delegate { label5.Text = sw.Elapsed.Milliseconds + "ms"; });

            //---------------------------------------REKURSIJA---------------------------------------------------------------------------------

            //Stopwatch sw = Stopwatch.StartNew();

            //string ieskomas = txtFailas.Text;
            //int current = 0;
            //string[] dirs = Directory.GetDirectories((string)kelias);
            //int counter;

            //foreach (string dir in dirs)
            //{

            //    try
            //    {
            //        foreach (string file in Directory.GetFiles(dir, ieskomas + "*.*", SearchOption.AllDirectories))
            //        {
            //            Invoke(new EventHandler(delegate { txtdir2.Text += "Failas: " + new DirectoryInfo(file).Name + "\n"; }));
            //            Invoke(new EventHandler(delegate { txtdir2.Text += "Failo direktorija: " + dir + "\n"; }));
            //            Invoke(new EventHandler(delegate { txtdir2.Text += "\n"; }));

            //        }
            //    }
            //    catch (UnauthorizedAccessException)
            //    {
            //        continue;
            //    }

            //    //Thread.Sleep(200);

            //    PaieskaFailo(dir);
            //    // counter = +Convert.ToInt32(dirs.Length);
            //    current++;
            //    Invoke(new EventHandler(delegate { progressBar1.Value = (100 * current / dirs.Length); }));
            //    Invoke(new EventHandler(delegate { label6.Text = progressBar1.Value.ToString() + "%"; }));
            //}

            //Invoke(new EventHandler(delegate { txtInfo.Text = "Direktorijų kiekis, kuriose rastas failas: " + dirs.Length; }));

            //sw.Stop();
            //Invoke(new EventHandler(delegate { label5.Text = sw.Elapsed.Milliseconds + "ms"; }));

        }



        private void PaieskaDirektorijos(object kelias)
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                Invoke(new EventHandler(delegate { txtdir2.Clear(); }));
                Invoke(new EventHandler(delegate { txtInfo.Clear(); }));

                string ieskomas = txtDirektorija.Text;

                string[] dirs = Directory.GetFiles((string)kelias, ieskomas + "*.*", SearchOption.AllDirectories);

                int current = 0;

                foreach (string dir in dirs)
                {

                    try
                    {
                        current++;
                        //Invoke(new EventHandler(delegate { txtdir2.Text += "Failas: " + new DirectoryInfo(dir).Name + "\n"; }));
                        Invoke(new EventHandler(delegate { txtdir2.Text += "Failo direktorija: " + dir + "\n"; }));
                        Invoke(new EventHandler(delegate { txtdir2.Text += "\n"; }));
                        Invoke(new EventHandler(delegate { progressBar1.Value = (100 * current / dirs.Length); }));

                        Invoke(new EventHandler(delegate { label6.Text = progressBar1.Value.ToString() + "%"; }));
                        //Thread.Sleep(100);

                        if (dirs.Length == 0)
                        {
                            Invoke(new EventHandler(delegate { txtInfo.Text = "Nerasta tokių direktorijų"; }));
                        }
                        else
                        {
                            Invoke(new EventHandler(delegate { txtInfo.Text = "Rastas direktorijų kiekis: " + dirs.Length; }));
                        }
                    }

                    catch (UnauthorizedAccessException)
                    {
                        //continue;
                    }

                }

                sw.Stop();
                BeginInvoke((MethodInvoker)delegate { label5.Text = sw.Elapsed.Milliseconds + "ms"; });

            }
            catch (UnauthorizedAccessException)
            {

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(PaieskaDirektorijos));

            if (txtDirektorija.Text != "" && kelias != "")
            {

                thread.Start(kelias);
            }
            else
            {
                MessageBox.Show("Pirma įveskite ieškomo failo pavadinimą");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(200);
            }
        }
    }
}
