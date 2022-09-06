using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace _7
{
    class Chart
    {
        /// <summary>
        /// Объявление переменных
        /// </summary>
        private String nazv;
        private Int32 gol;
        private Double prec;

        /// <summary>
        /// Присваивание переменным значения.
        /// </summary>
        /// <param name="nazv">Название браузера.</param>
        /// <param name="kolvo">Частота пользования.</param>
        /// <param name="sum">Все голоса.</param>
        /// <param name="prec">Процент.</param>
        public Chart(String nazv, int kolvo)
        {
            this.nazv = nazv;
            this.gol = kolvo;
            this.prec = 0.0;
        }
        /// <summary>
        /// Метод получения значения из переменной nazv.
        /// </summary>
        public String Nazv
        {
            get { return nazv; }
        }
        /// <summary>
        /// Метод получения значения из переменной prec.
        /// </summary>
        public Double Prec
        {
            get
            { return prec; }
            set
            { prec = value; }
        }

        /// <summary>
        /// Метод создания массива с процентом.
        /// </summary>
        /// <param name="Z">Массив.</param>
        public static void procent(Chart[] Z)
        {
            int sum = 0;
            for (int i = 0; i < Z.Length; i++)
            {
                sum += Z[i].gol;
            }
            for (int i = 0; i < Z.Length; i++)
            {
                Z[i].prec = ((((Double)Z[i].gol) * 100) / sum);
            }
        }
        /// <summary>
        /// Метод ввода знаечний в массив.
        /// </summary>
        /// <param name="Z">массив.</param>
        /// <param name="table">DataGridView.</param>
        public static void Show(Chart[] Z, DataGridView table)
        {
            table.ColumnCount = 3;
            table.RowCount = 5;
            table.Columns[0].Name = "Вирус";
            table.Columns[1].Name = "Количество голосов";
            table.Columns[2].Name = "Процент";
            table.Rows[0].Cells[0].Value = "Conficker";
            table.Rows[1].Cells[0].Value = "My Doom";
            table.Rows[2].Cells[0].Value = "Sasser";
            table.Rows[3].Cells[0].Value = "Nimba";
            table.Rows[4].Cells[0].Value = "Другой";

            for (int i = 0; i < 5; i++)
            {
                table.Rows[i].Cells[1].Value = Z[i].gol;
                table.Rows[i].Cells[2].Value = Math.Round(Z[i].prec, 2) + " %";
            }
        }
        /// <summary>
        /// Метод сохранения данных из dataGridView.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="Z">Массив.</param>
        /// <param name="File_Name">Имя файла.</param>
        public static void SaveToFile(string text, Chart[] Z, string File_Name)
        {
            try
            {
                StreamWriter sw = new StreamWriter(File_Name, false, Encoding.UTF8);

                sw.WriteLine(text);
                sw.WriteLine(Z.Length);
                for (int i = 0; i < Z.Length; i++)
                {
                    sw.WriteLine(Z[i].nazv);
                    sw.WriteLine(Z[i].gol);
                    sw.WriteLine(Z[i].prec);
                    sw.WriteLine();
                }
                sw.Close();
            }
            catch
            {
                throw new Exception("Ошибка доступа к файлу");
            }
        }
        /// <summary>
        /// Метод открытия из текстового файла.
        /// </summary>
        /// <param name="NameDiagram">Имя диаграмма.</param>
        /// <param name="Z">Массив.</param>
        /// <param name="File_Name">Файл.</param>
        public static void OpenFile(string NameDiagram, ref Chart[] Z, string File_Name)
        {
            try
            {
                int n = 0;
                StreamReader sr = new StreamReader(File_Name, Encoding.GetEncoding(1251));
                NameDiagram = sr.ReadLine();
                n = Convert.ToInt32(sr.ReadLine());
                for (int i = 0; i < n; i++)
                {
                    Z[i].nazv = sr.ReadLine();
                    Z[i].gol = Convert.ToInt32(sr.ReadLine());
                    Z[i].prec = Convert.ToDouble(sr.ReadLine());
                    sr.ReadLine();
                }
                sr.Close();
            }
            catch
            {
                throw new Exception("Ошибка чтения файла");
            }
        }


    }
}
