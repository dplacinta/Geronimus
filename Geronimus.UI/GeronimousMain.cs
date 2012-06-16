namespace Geronimus.UI
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using BusinessLogic;
    using DataObjects;
    using Properties;

    public partial class GeronimousMain : Form
    {
        private const string NumberFormat = "{0,-7:0.#####}";
        private const string Asterix = "*      ";
        private readonly Settings settings;
        private GeronimusParameter geronimousParameter;

        public GeronimousMain()
        {
            InitializeComponent();
            settings = new Settings();
            new Geronimus(settings.LogFilePath);
        }

        private void BtnLoadInitialDataClick(object sender, EventArgs e)
        {
            ClearTextboxes();

            geronimousParameter = new GeronimusParameter
                                      {
                                          InitialMatrix = MatrixLoader.Load(settings.InitialMatrixPath),
                                          PassengerFlow = MatrixLoader.Load(settings.PassengerFlow),
                                          WaitingTime = MatrixLoader.LoadVector(settings.WaitingTimeMatrix),
                                          Capacity = settings.Capacity,
                                          MaxInterval = settings.MaxInterval,
                                          PassangerArrivalCoeficient = settings.PassengerArrivalCoeficient,
                                          TimeInterval = settings.TimeInterval,
                                          TransferTime = settings.TransferTime,
                                          UnuniformityCoeficient = settings.UnuniformityCoeficient
                                      };

            txtInitialMatrix.Text = MatrixLoader.GetString(geronimousParameter.InitialMatrix);
            txtPassengerFlow.Text = MatrixLoader.GetString(geronimousParameter.PassengerFlow);
            txtWaitingTime.Text = MatrixLoader.GetString(geronimousParameter.WaitingTime);

            txtCapacity.Text = geronimousParameter.Capacity.ToString();
            txtMaxInterval.Text = geronimousParameter.MaxInterval.ToString();
            txtPassengerArrivalCoeficient.Text = settings.PassengerArrivalCoeficient.ToString();
            txtInterval.Text = settings.TimeInterval.ToString();
            txtUniformityCoeficient.Text = settings.UnuniformityCoeficient.ToString();
            btnSaveSettings.Enabled = false;
            btnCalculateShortestPaths.Enabled = true;
        }

        private void ClearTextboxes()
        {
            foreach (TextBox textBox in grbResult.Controls.OfType<TextBox>())
            {
                textBox.Text = string.Empty;
            }
        }

        private void BtnCalculateShortestPathsClick(object sender, EventArgs e)
        {
            ClearTextboxes();
            geronimousParameter.NodesMatrix = FloydAlgorithm.Process(geronimousParameter.InitialMatrix);
            ShortestPathsResult result = Geronimus.CalculateShortestPaths(geronimousParameter);


            txtShortestPaths.Text = MatrixLoader.GetString(geronimousParameter.NodesMatrix);
            AppendShortestPaths(result.ShortestPaths, txtShortestPathsList);
            AppendShortestPaths(result.NeighbouringPaths, txtShortestPathsList);
            AppendShortestPaths(result.AdditionalPaths, txtNewPaths);
            AppendShortestPaths(result.ResultedNodesMatrix, txtInitialRoutesShortestPaths);
            AppendShortestPaths(result.ResultedInitialMatrix, txtInitialRoutesMatrix);
            AppendShortestPaths(result.ConfirmedAdditionalPaths, txtConfirmedAdditionalPaths);
            txtTotalTime.Text = string.Format(NumberFormat, result.TotalTime);
            tabControl.SelectTab(1);
        }

        private static void AppendShortestPaths(IList<double[]> matrix, Control textbox)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix.Count; j++)
                {
                    textbox.Text += double.IsInfinity(matrix[i][j])
                                        ? Asterix
                                        : string.Format(NumberFormat, matrix[i][j]);
                }
                textbox.Text.Trim();
                textbox.Text += Environment.NewLine;
            }
        }

        private static void AppendShortestPaths(IList<Node[]> nodesMatrix, Control textbox)
        {
            for (int i = 0; i < nodesMatrix.Count; i++)
            {
                for (int j = 0; j < nodesMatrix.Count; j++)
                {
                    textbox.Text += string.Format(NumberFormat, nodesMatrix[i][j].ShortestPath);
                }
                textbox.Text.Trim();
                textbox.Text += Environment.NewLine;
            }
        }

        private static void AppendShortestPaths(IEnumerable<IList<int>> shortestPaths, Control textbox)
        {
            foreach (var shortestPath in shortestPaths)
            {
                foreach (int i in shortestPath)
                {
                    textbox.Text += string.Format(NumberFormat, i + 1);
                }
                textbox.Text.Trim();
                textbox.Text += Environment.NewLine;
            }
        }

        private void GeronimousMainResize(object sender, EventArgs e)
        {
            TabControl container = tabControl;
            txtInitialMatrix.Width = container.Width*40/100;
            txtPassengerFlow.Width = container.Width*40/100;
            txtPassengerFlow.Location = new Point(container.Width/2 + 80, txtPassengerFlow.Location.Y);
            lblPassengerFlowText.Location = new Point(container.Width/2 + 80, lblPassengerFlowText.Location.Y);
            txtShortestPathsList.Width = Width*20/100;
            txtNewPaths.Width = Width*25/100;
            txtShortestPathsList.Location = new Point(txtShortestPaths.Location.X + txtShortestPaths.Width + 20,
                                                      txtShortestPathsList.Location.Y);
            txtNewPaths.Location = new Point(txtShortestPathsList.Location.X + txtShortestPathsList.Width + 20,
                                             txtNewPaths.Location.Y);
            lblInitialNetwork.Location = new Point(txtShortestPathsList.Location.X, lblInitialNetwork.Location.Y);
            lblNewPaths.Location = new Point(txtNewPaths.Location.X, lblNewPaths.Location.Y);
        }

        private void BtnSaveSettingsClick(object sender, EventArgs e)
        {
            try
            {
                settings.Capacity = int.Parse(txtCapacity.Text);
                settings.MaxInterval = double.Parse(txtMaxInterval.Text);
                settings.PassengerArrivalCoeficient = double.Parse(txtPassengerArrivalCoeficient.Text);
                settings.TimeInterval = double.Parse(txtInterval.Text);
                settings.UnuniformityCoeficient = double.Parse(txtUniformityCoeficient.Text);
                SaveMatrixes();
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.GeronimousMain_WrongData, Resources.GeronimousMain_Error, MessageBoxButtons.OK,
                                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            settings.Save();
            BtnLoadInitialDataClick(sender, e);
            btnSaveSettings.Enabled = false;
        }

        private void SaveMatrixes()
        {
            MatrixLoader.SaveMatrix(txtInitialMatrix.Text, settings.InitialMatrixPath);
            MatrixLoader.SaveMatrix(txtPassengerFlow.Text, settings.PassengerFlow);
            MatrixLoader.SaveMatrix(txtWaitingTime.Text, settings.WaitingTimeMatrix);
        }

        private void SettingValueChanged(object sender, EventArgs e)
        {
            btnSaveSettings.Enabled = true;
        }
    }
}