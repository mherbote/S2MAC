using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Automation;
using System.Threading.Channels;
using System.Reflection.Metadata;
using System.Windows.Media.TextFormatting;

namespace Z80_S2MAC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool endLine = false;
        public string lineMAC;
        struct testStruct
        {
            public int WAHLi;
            public string TESTi, CHA1i, CHA2i;
        }
        testStruct[] testc = { new testStruct { WAHLi=1, TESTi="TITL", CHA1i="TITLE", CHA2i="" },
                               new testStruct { WAHLi=1, TESTi="DEF", CHA1i="EQU", CHA2i="" },
                               new testStruct { WAHLi=1, TESTi="BER", CHA1i="DS", CHA2i="" },
                               new testStruct { WAHLi=1, TESTi="CMP", CHA1i="CP", CHA2i="" },
                               new testStruct { WAHLi=1, TESTi="JMP", CHA1i="JP", CHA2i="" },
                               new testStruct { WAHLi=1, TESTi="EXAF", CHA1i="EX\tAF,AF'", CHA2i="" },
                               new testStruct { WAHLi=1, TESTi="RZ", CHA1i="RET\tZ", CHA2i="" },
                               new testStruct { WAHLi=1, TESTi="RNZ", CHA1i="RET\tNZ", CHA2i="" },

                               new testStruct { WAHLi=2, TESTi="EQU", CHA1i="EQU", CHA2i="" },

                               new testStruct { WAHLi=3, TESTi="ADD", CHA1i="ADD", CHA2i="" },

                               new testStruct { WAHLi=4, TESTi="OUT", CHA1i="OUT", CHA2i="" },

                               new testStruct { WAHLi=5, TESTi="IN", CHA1i="", CHA2i="" },

                               new testStruct { WAHLi=6, TESTi="JR", CHA1i="", CHA2i="" },

                               new testStruct { WAHLi=7, TESTi="JPZ", CHA1i="JP", CHA2i="Z," },
                               new testStruct { WAHLi=7, TESTi="JPNZ", CHA1i="JP", CHA2i="NZ," },
                               new testStruct { WAHLi=7, TESTi="JPC", CHA1i="JP", CHA2i="C," },
                               new testStruct { WAHLi=7, TESTi="JPNC", CHA1i="JP", CHA2i="NC," },
                               new testStruct { WAHLi=7, TESTi="JPPO", CHA1i="JP", CHA2i="PO," },

                               new testStruct { WAHLi=8, TESTi="JRZ", CHA1i="JR", CHA2i="Z," },
                               new testStruct { WAHLi=8, TESTi="JRNZ", CHA1i="JR", CHA2i="NZ," },
                               new testStruct { WAHLi=8, TESTi="JRC", CHA1i="JR", CHA2i="C," },
                               new testStruct { WAHLi=8, TESTi="JRNC", CHA1i="JR", CHA2i="NC," },

                               new testStruct { WAHLi=9, TESTi="DJNZ", CHA1i="", CHA2i="" },

                               new testStruct { WAHLi=10, TESTi="DA", CHA1i="DW", CHA2i="" },

                               new testStruct { WAHLi=11, TESTi="CAZ", CHA1i="CALL", CHA2i="Z," },
                               new testStruct { WAHLi=11, TESTi="CANZ", CHA1i="CALL", CHA2i="NZ," },
                               new testStruct { WAHLi=11, TESTi="CAC", CHA1i="CALL", CHA2i="C," },
                               new testStruct { WAHLi=11, TESTi="CANC", CHA1i="CALL", CHA2i="NC," },
                               new testStruct { WAHLi=11, TESTi="CAM", CHA1i="CALL", CHA2i="M," },
                               new testStruct { WAHLi=11, TESTi="CAP", CHA1i="CALL", CHA2i="P," },
                               new testStruct { WAHLi=11, TESTi="JPM", CHA1i="JP", CHA2i="M," },
                               new testStruct { WAHLi=11, TESTi="JPP", CHA1i="JP", CHA2i="P," },
                               new testStruct { WAHLi=11, TESTi="JPPE", CHA1i="JP", CHA2i="PE," },
                               new testStruct { WAHLi=11, TESTi="RZ", CHA1i="RET", CHA2i="Z," },
                               new testStruct { WAHLi=11, TESTi="RNZ", CHA1i="RET", CHA2i="NZ," },
                               new testStruct { WAHLi=11, TESTi="RC", CHA1i="RET", CHA2i="C," },
                               new testStruct { WAHLi=11, TESTi="RNC", CHA1i="RET", CHA2i="NC," },
                               new testStruct { WAHLi=11, TESTi="SBC\tA", CHA1i="SBC", CHA2i="A," },
                               new testStruct { WAHLi=11, TESTi="ADC\tA", CHA1i="ADC", CHA2i="A," },

                               new testStruct { WAHLi=12, TESTi="PN", CHA1i="PN", CHA2i=";" },

                               new testStruct { WAHLi=13, TESTi="SBC\tHL", CHA1i="SBC", CHA2i="HL," },
                               new testStruct { WAHLi=13, TESTi="ADC\tHL", CHA1i="ADC", CHA2i="HL," },

                               new testStruct { WAHLi=101, TESTi="EXAF", CHA1i="EX\tAF,AF'", CHA2i="" },
                               new testStruct { WAHLi=101, TESTi="RZ", CHA1i="RET\tZ", CHA2i="" },
                               new testStruct { WAHLi=101, TESTi="RNZ", CHA1i="RET\tNZ", CHA2i="" },
                               new testStruct { WAHLi=101, TESTi="RC", CHA1i="RET\tC", CHA2i="" },
                               new testStruct { WAHLi=101, TESTi="RNC", CHA1i="RET\tNC", CHA2i="" },
                               new testStruct { WAHLi=101, TESTi="RP", CHA1i="RET\tP", CHA2i="" },

                               new testStruct { WAHLi=201, TESTi="EJEC", CHA1i="PAGE", CHA2i="" },
                               new testStruct { WAHLi=201, TESTi="ENIF", CHA1i="ENDIF", CHA2i="" },
                               new testStruct { WAHLi=201, TESTi="IM", CHA1i="IM\t", CHA2i="" },

                               new testStruct { WAHLi=202, TESTi="IF", CHA1i="", CHA2i="" },

                               new testStruct { WAHLi=203, TESTi="ENDM", CHA1i="", CHA2i="" },
                               new testStruct { WAHLi=204, TESTi="END", CHA1i="", CHA2i="" }
                             };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Quelle_Click(object sender, RoutedEventArgs e)
        {
            bool comment = false;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            // openFileDialog.InitialDirectory 
            openFileDialog.Filter = "S-Files|*.s";
            if (openFileDialog.ShowDialog() == true)
            {
                Quelle.Text = openFileDialog.FileName;

                // Zieldateiname erstellen
                lineMAC = openFileDialog.FileName;
                Ziel.Text = lineMAC.Substring (0,lineMAC.Length -1)+ "MAC";
                //S_Quelle.Text = File.ReadAllText(openFileDialog.FileName);                
                //MAC_Ziel.Text  = S_Quelle.Text;

                // Verarbeitungsschleife
                S_Quelle.Text = "";
                MAC_Ziel.Text = "";
                foreach (string line in System.IO.File.ReadLines(openFileDialog.FileName ))
                {
                    //System.Console.WriteLine(line);
                    S_Quelle.Text = S_Quelle.Text + line + "\n";

                    if (!endLine)
                    { 
                        lineMAC = line;
                    }
                    else
                    {
                        lineMAC = "";
                    }

                    if (lineMAC.Length != 0)                                                // Leerzeilen
                    {
                        if (SemiOnAnf(lineMAC))                                             // Kommentarzeilen
                        {
                            comment = true;
                        }
                        else
                        {
                            comment = false;
                            foreach (testStruct test1 in testc)
                            {
                                if (inString(test1.WAHLi, test1.TESTi, test1.CHA1i, test1.CHA2i)) { break; }
                            }
                        }
                    }

                    //if (!comment)
                    if (lineMAC.IndexOf("\tEQU\t", 0) > 0)
                    {
                        lineMAC = lineMAC.Replace(":", "");
                    }

                    if (endLine & lineMAC == "")
                    { }
                    else
                    {
                        MAC_Ziel.Text = MAC_Ziel.Text + lineMAC + "\r";
                    }
                }
                //File.Create(Ziel.Text);
                File.WriteAllText (Ziel.Text, MAC_Ziel.Text);
            }
        }

        private bool inString(int wahl, string TEST, string Change1, string Change2)
        {
            if (wahl < 100)
            {

                if (OnAnf(lineMAC, "\t" + TEST + "\t") | lineMAC.IndexOf("\t" + TEST + "\t", 0) > 0)
                {
                    switch (wahl)
                    {
                        case 1:
                            lineMAC = lineMAC.Replace("\t" + TEST + "\t", "\t" + Change1 + "\t");
                            return true;
                        case 2:
                            lineMAC = lineMAC.Replace("\t" + TEST + "\t#", "\t" + Change1 + "\t$");
                            return true;
                        case 3:
                            if (lineMAC.IndexOf("\tHL,", 0) > 0 |
                                lineMAC.IndexOf("\tIX,", 0) > 0 |
                                lineMAC.IndexOf("\tIY,", 0) > 0
                               ) { }
                            else
                            {
                                lineMAC = lineMAC.Replace("\t" + TEST + "\t", "\t" + TEST + "\tA,");
                            }
                            return true;
                        case 4:
                            lineMAC = lineMAC.Replace("\t" + TEST + "\t", "\t" + Change1 + "\t(C),");
                            return true;
                        case 5:
                            lineMAC = lineMAC + ",(C)";
                            return true;
                        case 6:
                            lineMAC = replLine(lineMAC);
                            return true;
                        case 7:
                            lineMAC = lineMAC.Replace("\t" + TEST + "\t", "\t" + Change1 + "\t" + Change2);
                            lineMAC = lineMAC.Replace("-#", "");
                            return true;
                        case 8:
                            lineMAC = lineMAC.Replace("\t" + TEST + "\t", "\t" + Change1 + "\t" + Change2);
                            lineMAC = replLine(lineMAC);
                            return true;
                        case 9:
                            lineMAC = replLine(lineMAC);
                            return true;
                        case 10:
                            lineMAC = lineMAC.Replace("\t" + TEST + "\t", "\t" + Change1 + "\t");
                            lineMAC = lineMAC.Replace("#", "$");
                            return true;
                        case 11:
                            lineMAC = lineMAC.Replace("\t" + TEST + "\t", "\t" + Change1 + "\t" + Change2);
                            return true;
                        case 12:
                            lineMAC = lineMAC.Replace("\t" + TEST + "\t", Change2 + "\t" + Change1 + "\t");
                            return true;
                        case 13:
                            lineMAC = lineMAC.Replace("\t" + TEST , Change1 + "\t" + Change2);
                            return true;
                    }
                }
            }
            else
            {
                if (wahl < 200)                                                                                         // wahl 101 ... 199
                {
                    if (OnAnf(lineMAC, "\t" + TEST) | lineMAC.IndexOf("\t" + TEST, 0) > 0)
                    {
                        switch (wahl)
                        {
                            case 101:
                                lineMAC = lineMAC.Replace("\t" + TEST, "\t" + Change1);
                                return true;
                        }
                    }
                }
                else 
                {   if (OnAnf(lineMAC, "\t"+ TEST))                                                                     // wahl 201 ...
                    {
                        switch (wahl)
                        {
                            case 201:
                                lineMAC = lineMAC.Replace("\t" + TEST, "\t" + Change1);
                                return true;
                            case 202:
                                lineMAC = lineMAC.Replace(".", " ");
                                return true;
                            case 203:
                                return true;
                            case 204:
                                endLine = true;
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool SemiOnAnf(string line1)
        {
            char[] arr;
            arr = line1.ToCharArray();
            foreach (char c in arr)
            {
                if (c == ';')
                {
                    return true;
                }
                if (c != '\t' & c != ' ')
                {
                    return false;
                }
            }
            return false;
        }
        private bool OnAnf(string line1, string vergl)
        {
            if (line1.IndexOf(vergl) == 0)
            { 
                return true;
            }
            else
            {
                return false;
            }
        }
        private string replLine (string line1)
        {
            int PosSemi,PosChar;
            string lineOut;
            lineOut = line1;
            PosSemi = lineOut.IndexOf(';');

            //if (lineOut.IndexOf("-#", 0) > 0)
            //{
            //    lineOut = lineOut.Replace("-#", "");
            //}
            //else
            //{
            //    lineOut = lineOut.Replace("-", "$-");
            //}

            lineOut = lineOut.Replace("-#", "");

            if (PosSemi == -1)
            {
                PosChar = lineOut.IndexOf('+');
                if (PosChar > -1)
                {
                    if (lineOut.Substring(PosChar - 1, 1) == "\t" |
                        lineOut.Substring(PosChar - 1, 1) == ",")
                    {
                        lineOut = lineOut.Replace("+", "$+");
                    }
                }

                PosChar = lineOut.IndexOf('-');
                if (PosChar > -1)
                {
                    if (lineOut.Substring(PosChar - 1, 1) == "\t" |
                        lineOut.Substring(PosChar - 1, 1) == ",")
                    {
                        lineOut = lineOut.Replace("-", "$-");
                    }
                }
            }
            else
            {
                PosChar = lineOut.IndexOf('+');
                if (PosChar > -1)
                {
                    if ((lineOut.Substring(PosChar - 1, 1) == "\t" |
                         lineOut.Substring(PosChar - 1, 1) == ",")
                        & PosChar < PosSemi)
                    {
                        lineOut = lineOut.Replace("+", "$+");
                    }
                }

                PosChar = lineOut.IndexOf('-');
                if (PosChar > -1)
                {
                    if ((lineOut.Substring(PosChar - 1, 1) == "\t" |
                         lineOut.Substring(PosChar - 1, 1) == ",")
                        & PosChar < PosSemi)
                    {
                        lineOut = lineOut.Replace("-", "$-");
                    }
                }
            }

            return lineOut;
        }
    }
}
