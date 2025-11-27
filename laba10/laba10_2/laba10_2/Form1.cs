using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace laba10_2
{
    public partial class Form1 : Form
    {
        // Вершини графу
        private readonly string[] nodes = { "a", "b", "c", "d", "e", "f" };

        // Список ребер (звідки -> куди) згідно варіанту 6
        private readonly List<Tuple<int, int>> edges = new List<Tuple<int, int>>
        {
            // Від вершини 'a' (0)
            Tuple.Create(0, 1), // a -> b
            Tuple.Create(0, 2), // a -> c
            Tuple.Create(0, 3), // a -> d
            Tuple.Create(0, 5), // a -> f
            
            // Від вершини 'b' (1)
            Tuple.Create(1, 2), // b -> c
            Tuple.Create(1, 3), // b -> d
            Tuple.Create(1, 4), // b -> e
            
            // Від вершини 'c' (2)
            Tuple.Create(2, 4), // c -> e
            Tuple.Create(2, 5), // c -> f
            
            // Від вершини 'd' (3)
            Tuple.Create(3, 4), // d -> e
            Tuple.Create(3, 5), // d -> f
            
            // Від вершини 'e' (4)
            Tuple.Create(4, 5)  // e -> f
        };

        public Form1()
        {
            InitializeCustomComponents(); // Ініціалізація GUI через код
        }

        // Метод для побудови інтерфейсу (щоб код працював при копіюванні)
        private void InitializeCustomComponents()
        {
            this.Text = "Лабораторна: Графи (Варіант 6)";
            this.Size = new Size(900, 500);

            Label lblAdj = new Label { Text = "Матриця суміжності:", Location = new Point(10, 10), AutoSize = true, Font = new Font("Arial", 10, FontStyle.Bold) };
            DataGridView dgvAdj = new DataGridView { Name = "dgvAdjacency", Location = new Point(10, 35), Size = new Size(350, 250), ReadOnly = true, AllowUserToAddRows = false, RowHeadersWidth = 50 };

            Label lblInc = new Label { Text = "Матриця інцидентності:", Location = new Point(380, 10), AutoSize = true, Font = new Font("Arial", 10, FontStyle.Bold) };
            DataGridView dgvInc = new DataGridView { Name = "dgvIncidence", Location = new Point(380, 35), Size = new Size(480, 250), ReadOnly = true, AllowUserToAddRows = false, RowHeadersWidth = 50 };

            Button btnCalc = new Button { Text = "Побудувати матриці", Location = new Point(10, 300), Size = new Size(200, 40) };
            btnCalc.Click += (s, e) => CalculateAndShow(dgvAdj, dgvInc);

            this.Controls.Add(lblAdj);
            this.Controls.Add(dgvAdj);
            this.Controls.Add(lblInc);
            this.Controls.Add(dgvInc);
            this.Controls.Add(btnCalc);
        }

        private void CalculateAndShow(DataGridView dgvAdj, DataGridView dgvInc)
        {
            int n = nodes.Length;       // Кількість вершин (6)
            int m = edges.Count;        // Кількість ребер (12)

            // --- 1. МАТРИЦЯ СУМІЖНОСТІ ---
            // A[i, j] = 1, якщо є ребро i -> j, інакше 0
            int[,] adjMatrix = new int[n, n];

            foreach (var edge in edges)
            {
                adjMatrix[edge.Item1, edge.Item2] = 1;
            }

            DisplayMatrix(dgvAdj, adjMatrix, nodes, nodes);


            // --- 2. МАТРИЦЯ ІНЦИДЕНТНОСТІ ---
            // Рядки - вершини, Стовпці - ребра.
            // -1 (або 1): виходить з вершини
            // 1 (або -1): входить у вершину
            // 0: не стосується
            int[,] incMatrix = new int[n, m];
            string[] edgeNames = new string[m];

            for (int i = 0; i < m; i++)
            {
                int u = edges[i].Item1; // Звідки
                int v = edges[i].Item2; // Куди

                incMatrix[u, i] = -1; // Витік
                incMatrix[v, i] = 1;  // Стік

                // Назва стовпця, напр "a->b"
                edgeNames[i] = $"{nodes[u]}{nodes[v]}";
            }

            DisplayMatrix(dgvInc, incMatrix, nodes, edgeNames);
        }

        // Універсальний метод виводу 2D масиву в DataGridView
        private void DisplayMatrix(DataGridView dgv, int[,] matrix, string[] rowHeaders, string[] colHeaders)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // Створення стовпців
            for (int j = 0; j < cols; j++)
            {
                dgv.Columns.Add($"col{j}", colHeaders[j]);
                dgv.Columns[j].Width = 40;
            }

            // Заповнення рядків
            for (int i = 0; i < rows; i++)
            {
                dgv.Rows.Add();
                dgv.Rows[i].HeaderCell.Value = rowHeaders[i]; // Назва рядка (a, b, c...)

                for (int j = 0; j < cols; j++)
                {
                    dgv.Rows[i].Cells[j].Value = matrix[i, j];

                    // Візуальне оформлення для матриці інцидентності
                    if (matrix[i, j] == 1) dgv.Rows[i].Cells[j].Style.BackColor = Color.LightGreen;
                    if (matrix[i, j] == -1) dgv.Rows[i].Cells[j].Style.BackColor = Color.LightSalmon;
                }
            }
        }
    }
}