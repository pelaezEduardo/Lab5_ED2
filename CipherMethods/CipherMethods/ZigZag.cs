﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace CipherMethods
{
    public class ZigZag
    {
        string text;
        string fileName { get; set; }
        int rails { get; set; }
        public void calculate(int rails, StringBuilder input, string fileName) {
            this.fileName = fileName;
            this.rails = rails;
            text = input.ToString();
            //eliminar el caracter \r\n que agrega el stringbuilder al final de todo el texto
            text = text.Remove(text.Length - 1);
            text = text.Remove(text.Length - 1);
            float charQty = 1 + 1 + (2 * (rails - 2)); //cantidad de caracteres por ola 
            float unit = charQty / (charQty * charQty); //decimal correspondiente a un caracter
            float auxLength = text.Length;
            float waves = auxLength / charQty;
            double rest = waves  - Math.Truncate(waves); //parte decimal de las olas 
            
            rest = 1 - rest; //completar 
            rest = rest / unit; //cantidad de caracteres especiales que hay que completar

            for (int i = 0; i < rest; i++)
            {
                text += "#"; //para completar la ola
            }

            fillMatrix(rails, text);

        }
        public void fillMatrix(int rails, string text) {
            string[,] matrix = new string[rails, text.Length];
            int cont = 0;
            while (cont != text.Length) //recorrer toda la cadena
            {                
                for (int j = 0; j < rails; j++)
                {
                    matrix[j, cont] = text[cont].ToString();
                    cont++;
                }

                for (int i = rails - 2; i > 0; i--)
                {
                    matrix[i, cont] = text[cont].ToString();
                    cont++;
                }
                                            
            }

            cipher(matrix);
        }

        public void cipher(string[,] matrix) {
            string encryptedText = "";
            
            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < text.Length; j++)
                {
                    if (matrix[i, j] != "")
                    {
                        encryptedText += matrix[i, j];
                    }
                }
            }

            //escribir archivo 
            string folder = @"C:\Cipher\";
            string fullPath = folder + fileName;
            // crear el directorio
            DirectoryInfo directory = Directory.CreateDirectory(folder);

            using (StreamWriter file = new StreamWriter(fullPath))
            {
                file.WriteLine(encryptedText);
                file.Close();
            }

        }
    }
}
