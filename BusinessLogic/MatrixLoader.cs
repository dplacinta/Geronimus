namespace Geronimus.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using DataObjects;

    public static class MatrixLoader
    {
        private const string NumberFormat = "{0,-7:0.#}";
        private const string Asterix = "*      ";

        public static double[][] Load(string filePath)
        {
            string[] lines = GetLines(filePath);

            var matrix = new double[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] lineData = SplitLine(lines[i]);
                matrix[i] = new double[lineData.Length];

                for (int j = 0; j < lineData.Length; j++)
                {
                    string value = lineData[j];
                    matrix[i][j] = double.Parse(value == "*" ? double.PositiveInfinity.ToString() : value);
                }
            }

            return matrix;
        }

        public static double[] LoadVector(string filePath)
        {
            string[] lines = GetLines(filePath);
            string[] lineData = SplitLine(lines[0]);

            var matrix = new double[lineData.Length];

            for (int i = 0; i < lineData.Length; i++)
            {
                matrix[i] = double.Parse(lineData[i]);
            }

            return matrix;
        }

        private static string[] GetLines(string filePath)
        {
            string fullPath = Path.Combine(Environment.CurrentDirectory + '\\' + filePath);
            return File.ReadAllLines(fullPath);
        }

        private static string[] SplitLine(string line)
        {
            return line.Split(new[] {"\t"}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ReadAllText(string path)
        {
            string fullPath = Path.Combine(Environment.CurrentDirectory + '\\' + path);
            return File.ReadAllText(fullPath);
        }

        public static string GetString(Node[][] shortestPaths)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < shortestPaths.LongLength; i++)
            {
                for (int j = 0; j < shortestPaths.Length; j++)
                {
                    sb.Append(string.Format(NumberFormat, shortestPaths[i][j].ShortestPath));
                }
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public static string GetString(double[][] matrix)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < matrix.LongLength; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    sb.Append(matrix[i][j] != double.PositiveInfinity
                                  ? string.Format(NumberFormat, matrix[i][j])
                                  : Asterix);
                }
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public static string GetString(double[] vector)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < vector.Length; i++)
            {
                sb.Append(string.Format(NumberFormat, vector[i]));
            }

            return sb.ToString();
        }

        public static void SaveMatrix(string matrixData, string path)
        {
            string matrixToWrite = matrixData.Replace(" ", "\t");
            File.WriteAllText(path, matrixToWrite);
        }

        public static double[][] BuildInitialMatrix(IEnumerable<IList<int>> pathsList, double[][] initialMatrix)
        {
            int size = initialMatrix.Length; //pathsList.Select(path => path.Select(i => i).Max()).Max() + 1;
            var matrix = new double[size][];

            for (int i = 0; i < size; i++)
            {
                matrix[i] = new double[size];
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    matrix[i][j] = double.PositiveInfinity;
                    matrix[j][i] = double.PositiveInfinity;
                }
            }

            foreach (var path in pathsList)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    matrix[path[i]][path[i + 1]] = initialMatrix[path[i]][path[i + 1]]/* + waitingTime[path[i + 1]]*/;
                    matrix[path[i + 1]][path[i]] = initialMatrix[path[i]][path[i + 1]]/* + waitingTime[path[i]]*/;
                }
            }

            return matrix;
        }
    }
}