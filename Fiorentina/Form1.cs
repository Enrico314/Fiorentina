using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Threading;

namespace Fiorentina
{
   
    public partial class Form1 : Form
    {
        public static SpeechSynthesizer _ss = new SpeechSynthesizer();





        public Form1()
        {
            InitializeComponent();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            //   SpeechSynthesizer _ss = new SpeechSynthesizer();

            // Griglia g1 = new Griglia(1, _ss);

            //TextBox.CheckForIllegalCrossThreadCalls = false;
            Control.CheckForIllegalCrossThreadCalls = false;

            //textBoxP1_s1.CheckForIllegalCrossThreadCalls = false;

            //Thread.Sleep(3000);
            Thread t1 = new Thread(new ThreadStart(run_g1)); t1.Start();
            //Thread.Sleep(3000);
            Thread t2 = new Thread(new ThreadStart(run_g2)); t2.Start();
            //Thread.Sleep(3000);
            Thread t3 = new Thread(new ThreadStart(run_g3)); t3.Start();
            //Thread.Sleep(3000);
            Thread t4 = new Thread(new ThreadStart(run_g4)); t4.Start();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            //radioButton_P1_s1.Checked = false;



        }
        // detlay between starts of the piastre
        //const int deltay = (45*2); OLD 2022
        const int deltay = (50); // new 2023
        //const int deltay = (5); // debug 

        public void run_g1() // starts at delay*0
        {
            Griglia g1 = new Griglia(1, _ss, textBoxP1_s1, textBoxP1_s2, textBoxP1_s3, 0, checkBox1, checkBox2, checkBox3, textBox1);
        }

        public void run_g2() // starts at deplay*1
        {
            Griglia g2 = new Griglia(2, _ss, textBoxP2_s1, textBoxP2_s2, textBoxP2_s3, deltay, checkBox4, checkBox5, checkBox6, textBox2);
        }

        public void run_g3()
        {
            Griglia g3 = new Griglia(3, _ss, textBoxP3_s1, textBoxP3_s2, textBoxP3_s3, deltay*2, checkBox7, checkBox8, checkBox9, textBox3);
        }

        public void run_g4()
        {
            Griglia g4 = new Griglia(4, _ss, textBoxP4_s1, textBoxP4_s2, textBoxP4_s3, deltay*3, checkBox10, checkBox11, checkBox12, textBox4);
        }


        private void textBoxP1_s1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int totali = int.Parse(textBox1.Text) + int.Parse(textBox2.Text) + int.Parse(textBox3.Text) + int.Parse(textBox4.Text);
            textBox7.Text = totali.ToString();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // FIXME, terminate the threads
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String to_speak = "Moglia Gay e Gay chi non lo dice";
            _ss.Speak(to_speak);
        }
    }




    public class Griglia
    {
        int number; // identifier of the class
        int status; // status

        TextBox t1, t2, t3;
        //RadioButton r1, r2, r3;

        CheckBox c1, c2, c3;
        // tempo lato 1
        int time_l1 = (3 * 60 + 40);
        //int time_l1 = (10);
        // tempo lato 2
        int time_l2 = (3 * 60 + 40);
        //int time_l2 = (10);

        // tempo lato 3
        int time_l3 = (3 * 60);
        //int time_l3 = (5);

        TextBox cicl;

        // tempo da lato 3 a girare
        //int time_l4 = (1*60) ;

        // 0 initialization state
        // 1 lato 1 in cottura
        // 2 lato 2 in cottura
        // 3 lato 3 in cottura
        // 4 lato 3 in cottura e nuova fiorentina carica in cottura
        SpeechSynthesizer _ss;
        System.Timers.Timer aTimer = new System.Timers.Timer();

        public class Costata
        {
            int t1 = (3 * 60 + 40);
            int t2 = (3 * 60 + 40);
            int t3 = (3 * 60);
            //int t1 = 15;
            //int t2 = 15;
            //int t3 = 10;
            public Costata(int index_costata, int index_piastra, int delay, SpeechSynthesizer _ss, TextBox textbox_t1, TextBox textbox_t2, TextBox textbox_t3)
            {
                bool first_cycle = true;
                do
                {
                    bool exception = false;
                    try
                    {
                        // something
                        t1 = Int32.Parse(textbox_t1.Text);
                        t2 = Int32.Parse(textbox_t2.Text);
                        t3 = Int32.Parse(textbox_t3.Text);
                    }
                    catch (Exception e)
                    {
                        exception = true;
                    }
                    finally
                    {
                        if (!exception)
                        {

                        }
                    }

                    String to_speak = "";
                    // if first, time, wait for the delay
                    if (first_cycle)
                    {
                        System.Threading.Thread.Sleep(delay * 1000);
                        to_speak = "Piastra " + index_piastra.ToString() + " carica costata " + index_costata.ToString();
                        _ss.Speak(to_speak);
                        System.Threading.Thread.Sleep(t1 * 1000);
                        first_cycle = false;
                    }

                    exception = false;
                    try
                    {
                        // something
                        t1 = Int32.Parse(textbox_t1.Text);
                        t2 = Int32.Parse(textbox_t2.Text);
                        t3 = Int32.Parse(textbox_t3.Text);
                    }
                    catch (Exception e)
                    {
                        exception = true;
                    }
                    finally
                    {
                        if (!exception)
                        {

                        }
                    }


                    to_speak = "Piastra " + index_piastra.ToString() + " gira costata " + index_costata.ToString();
                    _ss.Speak(to_speak);
                    System.Threading.Thread.Sleep(t2 * 1000);
                    to_speak = "Piastra " + index_piastra.ToString() + " in piedi costata " + index_costata.ToString() +" e carica nuova " +index_costata.ToString();
                    _ss.Speak(to_speak);
                    exception = false;
                    try
                    {
                        // something
                        t1 = Int32.Parse(textbox_t1.Text);
                        t2 = Int32.Parse(textbox_t2.Text);
                        t3 = Int32.Parse(textbox_t3.Text);
                    }
                    catch (Exception e)
                    {
                        exception = true;
                    }
                    finally
                    {
                        if (!exception)
                        {

                        }
                    }
                    System.Threading.Thread.Sleep(t1 * 1000);
                } while (true);

            }
        }

        public void runCostata(
            int index_piastra,
            int index_costata,
            int delay,
            SpeechSynthesizer _ss,
            TextBox tbt1,
            TextBox tbt2,
            TextBox tbt3
            )
        {
            Costata C = new Costata(index_costata, index_piastra, delay, _ss, tbt1, tbt2, tbt3);
        }

        // griglia class
        public Griglia(int number, SpeechSynthesizer _ss, TextBox t1, TextBox t2, TextBox t3, int init_deltay, CheckBox c1, CheckBox c2, CheckBox c3, TextBox cicl)
        {
            this.status = 0;

            this.number = number;
            this._ss = _ss;

            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;

            this.t1.Text = time_l1.ToString();
            this.t2.Text = time_l2.ToString();
            this.t3.Text = time_l3.ToString();

            this.c1 = c1;
            this.c2 = c2;
            this.c3 = c3;

            this.cicl = cicl;
            this.cicl.Text = "0";
            //System.Console.WriteLine("Class constructor");
            System.Threading.Thread.Sleep(init_deltay*1000);

            // start 3 costate
            time_l1 = Int32.Parse(this.t1.Text);
            time_l2 = Int32.Parse(this.t2.Text);
            time_l3 = Int32.Parse(this.t3.Text);
            //int time_tot = time_l1 + time_l2 + time_l3;


            StartTheThreadCostata(number, 1, 0, _ss, t1, t2, t3);
            StartTheThreadCostata(number, 2, 30 * 4, _ss, t1, t2, t3); //was 50
            StartTheThreadCostata(number, 3, 30 * 8, _ss, t1, t2, t3);
            
            
            //Costata c = new Costata(1, 1, 0, _ss);

            //run_cycle();
        }


        public Thread StartTheThreadCostata
            (int index_piastra,
            int index_costata,
            int delay,
            SpeechSynthesizer _ss,
            TextBox tbt1,
            TextBox tbt2,
            TextBox tbt3

            )
        {
            var t = new Thread(() => runCostata(index_piastra, index_costata, delay, _ss, tbt1, tbt2, tbt3));
            t.Start();
            return t;
        }




        void run_cycle()
        {
            int ciclo_index = 1;
            bool primo_ciclo = true;
            bool secondo_ciclo = false;
            do
            {

                if (primo_ciclo)
                {
                    primo_ciclo = false;
                    secondo_ciclo = true;

                    String to_speak = "Piastra " + number.ToString() + " carica";
                    cicl.Text = ciclo_index.ToString();
                    status = 1;
                    this._ss.Speak(to_speak);
                    System.Console.WriteLine("Wait t1");
                    c1.Checked = true;
                    time_l1 = Int32.Parse(this.t1.Text);
                    time_l2 = Int32.Parse(this.t2.Text);
                    time_l3 = Int32.Parse(this.t3.Text);
                    System.Threading.Thread.Sleep(time_l1 * 1000);

                    // fiorentina cariata, apsetto la cottura per time_l1
                    //System.Threading.Thread.Sleep(time_l1);
                    // giro la fiorentina
                    to_speak = "Piastra " + number.ToString() + " gira";
                    status = 2;
                    c2.Checked = true;
                    c1.Checked = false;
                    this._ss.Speak(to_speak);
                    //this._ss.Speak(to_speak);
                    System.Console.WriteLine(to_speak);
                    System.Console.WriteLine("Wait t2");
                    time_l1 = Int32.Parse(this.t1.Text);
                    time_l2 = Int32.Parse(this.t2.Text);
                    time_l3 = Int32.Parse(this.t3.Text);
                    System.Threading.Thread.Sleep(time_l2 * 1000);





                }
                else if (secondo_ciclo)
                {
                    secondo_ciclo = false;
                }
                else 
                {
                    // secondo ciclo
                    
                    String to_speak = "Piastra " + number.ToString() + " in piedi e carica";

                    //this._ss.Speak(to_speak);
                    
                    this._ss.Speak(to_speak);
                    System.Console.WriteLine(to_speak);
                    System.Console.WriteLine("Wait t3");
                    c1.Checked = true;
                    c2.Checked = false;
                    c3.Checked = true;
                    time_l1 = Int32.Parse(this.t1.Text);
                    time_l2 = Int32.Parse(this.t2.Text);
                    time_l3 = Int32.Parse(this.t3.Text);
                    System.Threading.Thread.Sleep(time_l3*1000);

                    //to_speak = "Piastra " + number.ToString() + " controlla cottura";
                    ciclo_index++;
                    cicl.Text = ciclo_index.ToString();

                    //this._ss.Speak(to_speak);

                    //this._ss.Speak(to_speak);
                    //System.Console.WriteLine(to_speak);
                    //System.Console.WriteLine("Wait t4");
                    c1.Checked = true;
                    c2.Checked = false;
                    c3.Checked = false;
                    time_l1 = Int32.Parse(this.t1.Text);
                    time_l2 = Int32.Parse(this.t2.Text);
                    time_l3 = Int32.Parse(this.t3.Text);
                    if (time_l1 - time_l3 > 0)
                    {
                        System.Threading.Thread.Sleep((time_l1 - time_l3)*1000);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(1 * 1000);
                    }
                    

                    to_speak = "Piastra " + number.ToString() + " gira ";

                    //this._ss.Speak(to_speak);
                    
                    this._ss.Speak(to_speak);
                    System.Console.WriteLine(to_speak);
                    System.Console.WriteLine("Wait t1");
                    c1.Checked = false;
                    c2.Checked = true;
                    c3.Checked = false;
                    time_l1 = Int32.Parse(this.t1.Text);
                    time_l2 = Int32.Parse(this.t2.Text);
                    time_l3 = Int32.Parse(this.t3.Text);
                    System.Threading.Thread.Sleep(time_l1* 1000);

                }
                

            } while (true);

        }

    }
}
