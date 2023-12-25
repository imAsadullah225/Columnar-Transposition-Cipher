using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cipher
{
    public partial class ColumnarTransposition : Form
    {
        public ColumnarTransposition()
        {
            InitializeComponent();
        }

        string[] encryptedText;
        char[] decryptedText;
        
        char[] alphabates = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        int[] key;
        void getKey()
        {
            key = new int[key_TB.Text.Count()];
            
            int count = 0;

            if (key_TB.Text != "")
            {
                for (int i = 0; i < alphabates.Count(); i++)
                {
                    for (int j = 0; j < key_TB.Text.Length; j++)
                    {
                        if (key_TB.Text[j] == alphabates[i])
                        {
                            count++;
                            key[j] = count;
                        }
                    }  
                }
            }
        }

        public void doEncryption(string text)
        {
            getKey();
            encryptedText = new string[key.Count()];

            int extra = text.Length % key.Count();
            int filler = key.Count() - extra;

            if (extra != 0)
            {
                for (int i = 0; i < filler; i++)
                {
                    text += '-';

                }
            }

            int keyLength = key_TB.Text.Count();
            int row = (text.Length / keyLength);

            char[,] CharOnGrid = new char[row, keyLength];

            int c = 0;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    CharOnGrid[i, j] = text[c];
                    c++;
                }
            }

            for (int i = 0; i < key.Count(); i++)
            {
                for (int j = 0; j < row; j++)
                {
                    encryptedText[key[i]-1] += CharOnGrid[j, i];
                }

            }
        }

        public void doDecryption(string text)
        {
            getKey();
            decryptedText = new char[text.Length];

            int keyLength = key_TB.Text.Count();
            int row = (text.Length / keyLength);

            char[,] CharOnGrid = new char[row, keyLength];

            int k = 0;
            for (int i = 0; i < keyLength; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    CharOnGrid[j, Array.IndexOf(key, i + 1)] = text[k];
                    k++;
                }
            }

            int l = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    if (CharOnGrid[i, j] != '-')
                    {
                        decryptedText[l] += CharOnGrid[i, j];
                        l++;
                    }
                }
            }
        }

        private void encrypt_BTN_Click(object sender, EventArgs e)
        {
            if (key_TB.Text != "")
            {
                doEncryption(inputText_TB.Text);
                string encrypt = string.Join("", encryptedText);
                encryptedText_TB.Text = encrypt;
            }
            else
                MessageBox.Show("Key is empty please type a key.", "Error");
        }

        private void decrypt_BTN_Click(object sender, EventArgs e)
        {
            if (key_TB.Text != "")
            {
                doDecryption(encryptedText_TB.Text);
                string decrypt = string.Join("", decryptedText);
                decryptedText_TB.Text = decrypt;
            }
            else
                MessageBox.Show("Key is empty please type a key.", "Error");
        }

        private void inputText_TB_TextChanged(object sender, EventArgs e)
        {
            encryptedText_TB.Clear();
            decryptedText_TB.Clear();
        }
    }
}
