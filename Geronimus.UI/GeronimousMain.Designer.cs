namespace Geronimus.UI
{
    partial class GeronimousMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grbInitialData = new System.Windows.Forms.GroupBox();
            this.txtUniformityCoeficient = new System.Windows.Forms.TextBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.txtPassengerArrivalCoeficient = new System.Windows.Forms.TextBox();
            this.txtMaxInterval = new System.Windows.Forms.TextBox();
            this.txtCapacity = new System.Windows.Forms.TextBox();
            this.lblUniformityCoeficient = new System.Windows.Forms.Label();
            this.lblInterval = new System.Windows.Forms.Label();
            this.lblPassengerFlowText = new System.Windows.Forms.Label();
            this.lblTransferTime = new System.Windows.Forms.Label();
            this.lblInputMatrix = new System.Windows.Forms.Label();
            this.txtPassengerFlow = new System.Windows.Forms.TextBox();
            this.txtInitialMatrix = new System.Windows.Forms.TextBox();
            this.txtWaitingTime = new System.Windows.Forms.TextBox();
            this.lblInitialMatrix = new System.Windows.Forms.Label();
            this.lblPassengerArrivalCoeficient = new System.Windows.Forms.Label();
            this.lblMaxInterval = new System.Windows.Forms.Label();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.btnLoadInitialData = new System.Windows.Forms.Button();
            this.btnCalculateShortestPaths = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grbResult = new System.Windows.Forms.GroupBox();
            this.lblConfirmedAdditionalPaths = new System.Windows.Forms.Label();
            this.txtConfirmedAdditionalPaths = new System.Windows.Forms.TextBox();
            this.txtInitialRoutesMatrix = new System.Windows.Forms.TextBox();
            this.lblInitialRoutesMatrix = new System.Windows.Forms.Label();
            this.txtTotalTime = new System.Windows.Forms.TextBox();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.txtInitialRoutesShortestPaths = new System.Windows.Forms.TextBox();
            this.lblInitialRoutesShortestPaths = new System.Windows.Forms.Label();
            this.lblNewPaths = new System.Windows.Forms.Label();
            this.lblInitialNetwork = new System.Windows.Forms.Label();
            this.txtNewPaths = new System.Windows.Forms.TextBox();
            this.txtShortestPathsList = new System.Windows.Forms.TextBox();
            this.txtShortestPaths = new System.Windows.Forms.TextBox();
            this.lblShortestPaths = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grbInitialData.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.grbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(11, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1330, 645);
            this.tabControl.TabIndex = 10;
            this.tabControl.SizeChanged += new System.EventHandler(this.GeronimousMainResize);
            this.tabControl.Click += new System.EventHandler(this.GeronimousMainResize);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grbInitialData);
            this.tabPage1.Controls.Add(this.btnSaveSettings);
            this.tabPage1.Controls.Add(this.btnLoadInitialData);
            this.tabPage1.Controls.Add(this.btnCalculateShortestPaths);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1322, 619);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Datele Initiale";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.GeronimousMainResize);
            // 
            // grbInitialData
            // 
            this.grbInitialData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbInitialData.Controls.Add(this.txtUniformityCoeficient);
            this.grbInitialData.Controls.Add(this.txtInterval);
            this.grbInitialData.Controls.Add(this.txtPassengerArrivalCoeficient);
            this.grbInitialData.Controls.Add(this.txtMaxInterval);
            this.grbInitialData.Controls.Add(this.txtCapacity);
            this.grbInitialData.Controls.Add(this.lblUniformityCoeficient);
            this.grbInitialData.Controls.Add(this.lblInterval);
            this.grbInitialData.Controls.Add(this.lblPassengerFlowText);
            this.grbInitialData.Controls.Add(this.lblTransferTime);
            this.grbInitialData.Controls.Add(this.lblInputMatrix);
            this.grbInitialData.Controls.Add(this.txtPassengerFlow);
            this.grbInitialData.Controls.Add(this.txtInitialMatrix);
            this.grbInitialData.Controls.Add(this.txtWaitingTime);
            this.grbInitialData.Controls.Add(this.lblInitialMatrix);
            this.grbInitialData.Controls.Add(this.lblPassengerArrivalCoeficient);
            this.grbInitialData.Controls.Add(this.lblMaxInterval);
            this.grbInitialData.Controls.Add(this.lblCapacity);
            this.grbInitialData.Location = new System.Drawing.Point(6, 6);
            this.grbInitialData.Name = "grbInitialData";
            this.grbInitialData.Size = new System.Drawing.Size(1203, 333);
            this.grbInitialData.TabIndex = 23;
            this.grbInitialData.TabStop = false;
            this.grbInitialData.Text = "Datele Initiale";
            // 
            // txtUniformityCoeficient
            // 
            this.txtUniformityCoeficient.Location = new System.Drawing.Point(150, 151);
            this.txtUniformityCoeficient.Name = "txtUniformityCoeficient";
            this.txtUniformityCoeficient.Size = new System.Drawing.Size(48, 20);
            this.txtUniformityCoeficient.TabIndex = 18;
            this.txtUniformityCoeficient.TextChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(150, 125);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(48, 20);
            this.txtInterval.TabIndex = 17;
            this.txtInterval.TextChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // txtPassengerArrivalCoeficient
            // 
            this.txtPassengerArrivalCoeficient.Location = new System.Drawing.Point(150, 89);
            this.txtPassengerArrivalCoeficient.Name = "txtPassengerArrivalCoeficient";
            this.txtPassengerArrivalCoeficient.Size = new System.Drawing.Size(48, 20);
            this.txtPassengerArrivalCoeficient.TabIndex = 16;
            this.txtPassengerArrivalCoeficient.TextChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // txtMaxInterval
            // 
            this.txtMaxInterval.Location = new System.Drawing.Point(150, 63);
            this.txtMaxInterval.Name = "txtMaxInterval";
            this.txtMaxInterval.Size = new System.Drawing.Size(48, 20);
            this.txtMaxInterval.TabIndex = 15;
            this.txtMaxInterval.TextChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // txtCapacity
            // 
            this.txtCapacity.Location = new System.Drawing.Point(150, 41);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Size = new System.Drawing.Size(48, 20);
            this.txtCapacity.TabIndex = 14;
            this.txtCapacity.TextChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // lblUniformityCoeficient
            // 
            this.lblUniformityCoeficient.AutoSize = true;
            this.lblUniformityCoeficient.Location = new System.Drawing.Point(57, 154);
            this.lblUniformityCoeficient.Name = "lblUniformityCoeficient";
            this.lblUniformityCoeficient.Size = new System.Drawing.Size(77, 26);
            this.lblUniformityCoeficient.TabIndex = 12;
            this.lblUniformityCoeficient.Text = "Coeficientul de\nneuniformitate";
            this.lblUniformityCoeficient.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(57, 128);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(87, 13);
            this.lblInterval.TabIndex = 10;
            this.lblInterval.Text = "Intervalul de timp";
            this.lblInterval.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPassengerFlowText
            // 
            this.lblPassengerFlowText.AutoSize = true;
            this.lblPassengerFlowText.Location = new System.Drawing.Point(688, 22);
            this.lblPassengerFlowText.Name = "lblPassengerFlowText";
            this.lblPassengerFlowText.Size = new System.Drawing.Size(86, 13);
            this.lblPassengerFlowText.TabIndex = 4;
            this.lblPassengerFlowText.Text = "Fluxul de calatori";
            // 
            // lblTransferTime
            // 
            this.lblTransferTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTransferTime.AutoSize = true;
            this.lblTransferTime.Location = new System.Drawing.Point(122, 264);
            this.lblTransferTime.Name = "lblTransferTime";
            this.lblTransferTime.Size = new System.Drawing.Size(91, 13);
            this.lblTransferTime.TabIndex = 4;
            this.lblTransferTime.Text = "Timpul de transfer";
            // 
            // lblInputMatrix
            // 
            this.lblInputMatrix.AutoSize = true;
            this.lblInputMatrix.Location = new System.Drawing.Point(216, 22);
            this.lblInputMatrix.Name = "lblInputMatrix";
            this.lblInputMatrix.Size = new System.Drawing.Size(80, 13);
            this.lblInputMatrix.TabIndex = 4;
            this.lblInputMatrix.Text = "Matricea initiala";
            // 
            // txtPassengerFlow
            // 
            this.txtPassengerFlow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassengerFlow.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassengerFlow.Location = new System.Drawing.Point(691, 41);
            this.txtPassengerFlow.Multiline = true;
            this.txtPassengerFlow.Name = "txtPassengerFlow";
            this.txtPassengerFlow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPassengerFlow.Size = new System.Drawing.Size(468, 211);
            this.txtPassengerFlow.TabIndex = 3;
            this.txtPassengerFlow.WordWrap = false;
            this.txtPassengerFlow.TextChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // txtInitialMatrix
            // 
            this.txtInitialMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInitialMatrix.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInitialMatrix.Location = new System.Drawing.Point(219, 41);
            this.txtInitialMatrix.Multiline = true;
            this.txtInitialMatrix.Name = "txtInitialMatrix";
            this.txtInitialMatrix.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInitialMatrix.Size = new System.Drawing.Size(468, 211);
            this.txtInitialMatrix.TabIndex = 3;
            this.txtInitialMatrix.WordWrap = false;
            this.txtInitialMatrix.TextChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // txtWaitingTime
            // 
            this.txtWaitingTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWaitingTime.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtWaitingTime.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtWaitingTime.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWaitingTime.Location = new System.Drawing.Point(219, 261);
            this.txtWaitingTime.Name = "txtWaitingTime";
            this.txtWaitingTime.Size = new System.Drawing.Size(968, 20);
            this.txtWaitingTime.TabIndex = 1;
            this.txtWaitingTime.TextChanged += new System.EventHandler(this.SettingValueChanged);
            // 
            // lblInitialMatrix
            // 
            this.lblInitialMatrix.AutoSize = true;
            this.lblInitialMatrix.Location = new System.Drawing.Point(3, 26);
            this.lblInitialMatrix.Name = "lblInitialMatrix";
            this.lblInitialMatrix.Size = new System.Drawing.Size(0, 13);
            this.lblInitialMatrix.TabIndex = 0;
            // 
            // lblPassengerArrivalCoeficient
            // 
            this.lblPassengerArrivalCoeficient.AutoSize = true;
            this.lblPassengerArrivalCoeficient.Location = new System.Drawing.Point(15, 92);
            this.lblPassengerArrivalCoeficient.Name = "lblPassengerArrivalCoeficient";
            this.lblPassengerArrivalCoeficient.Size = new System.Drawing.Size(129, 26);
            this.lblPassengerArrivalCoeficient.TabIndex = 8;
            this.lblPassengerArrivalCoeficient.Text = "Coeficientul neuniformitatii\nsosirii pasagerilor";
            this.lblPassengerArrivalCoeficient.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblMaxInterval
            // 
            this.lblMaxInterval.AutoSize = true;
            this.lblMaxInterval.Location = new System.Drawing.Point(59, 66);
            this.lblMaxInterval.Name = "lblMaxInterval";
            this.lblMaxInterval.Size = new System.Drawing.Size(85, 26);
            this.lblMaxInterval.TabIndex = 6;
            this.lblMaxInterval.Text = "Intervalul maxim \nadmisibil";
            this.lblMaxInterval.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCapacity
            // 
            this.lblCapacity.AutoSize = true;
            this.lblCapacity.Location = new System.Drawing.Point(56, 44);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(88, 13);
            this.lblCapacity.TabIndex = 4;
            this.lblCapacity.Text = "Capacitatea U.T.";
            this.lblCapacity.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSettings.Enabled = false;
            this.btnSaveSettings.Location = new System.Drawing.Point(1215, 92);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(100, 35);
            this.btnSaveSettings.TabIndex = 3;
            this.btnSaveSettings.Text = "Pastreaza Datele";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.BtnSaveSettingsClick);
            // 
            // btnLoadInitialData
            // 
            this.btnLoadInitialData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadInitialData.Location = new System.Drawing.Point(1215, 10);
            this.btnLoadInitialData.Name = "btnLoadInitialData";
            this.btnLoadInitialData.Size = new System.Drawing.Size(100, 35);
            this.btnLoadInitialData.TabIndex = 0;
            this.btnLoadInitialData.Text = "Incarca Datele Initiale";
            this.btnLoadInitialData.UseVisualStyleBackColor = true;
            this.btnLoadInitialData.Click += new System.EventHandler(this.BtnLoadInitialDataClick);
            // 
            // btnCalculateShortestPaths
            // 
            this.btnCalculateShortestPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalculateShortestPaths.Enabled = false;
            this.btnCalculateShortestPaths.Location = new System.Drawing.Point(1215, 51);
            this.btnCalculateShortestPaths.Name = "btnCalculateShortestPaths";
            this.btnCalculateShortestPaths.Size = new System.Drawing.Size(100, 35);
            this.btnCalculateShortestPaths.TabIndex = 1;
            this.btnCalculateShortestPaths.Text = "Calculeaza Drumurile Minime";
            this.btnCalculateShortestPaths.UseVisualStyleBackColor = true;
            this.btnCalculateShortestPaths.Click += new System.EventHandler(this.BtnCalculateShortestPathsClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grbResult);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1322, 619);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Rezultatele";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.GeronimousMainResize);
            // 
            // grbResult
            // 
            this.grbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbResult.Controls.Add(this.lblConfirmedAdditionalPaths);
            this.grbResult.Controls.Add(this.txtConfirmedAdditionalPaths);
            this.grbResult.Controls.Add(this.txtInitialRoutesMatrix);
            this.grbResult.Controls.Add(this.lblInitialRoutesMatrix);
            this.grbResult.Controls.Add(this.txtTotalTime);
            this.grbResult.Controls.Add(this.lblTotalTime);
            this.grbResult.Controls.Add(this.txtInitialRoutesShortestPaths);
            this.grbResult.Controls.Add(this.lblInitialRoutesShortestPaths);
            this.grbResult.Controls.Add(this.lblNewPaths);
            this.grbResult.Controls.Add(this.lblInitialNetwork);
            this.grbResult.Controls.Add(this.txtNewPaths);
            this.grbResult.Controls.Add(this.txtShortestPathsList);
            this.grbResult.Controls.Add(this.txtShortestPaths);
            this.grbResult.Controls.Add(this.lblShortestPaths);
            this.grbResult.Location = new System.Drawing.Point(6, 6);
            this.grbResult.Name = "grbResult";
            this.grbResult.Size = new System.Drawing.Size(1310, 607);
            this.grbResult.TabIndex = 11;
            this.grbResult.TabStop = false;
            this.grbResult.Text = "Rezultatele";
            // 
            // lblConfirmedAdditionalPaths
            // 
            this.lblConfirmedAdditionalPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConfirmedAdditionalPaths.AutoSize = true;
            this.lblConfirmedAdditionalPaths.Location = new System.Drawing.Point(1008, 258);
            this.lblConfirmedAdditionalPaths.Name = "lblConfirmedAdditionalPaths";
            this.lblConfirmedAdditionalPaths.Size = new System.Drawing.Size(152, 13);
            this.lblConfirmedAdditionalPaths.TabIndex = 21;
            this.lblConfirmedAdditionalPaths.Text = "Rutele suplimentare confirmate";
            // 
            // txtConfirmedAdditionalPaths
            // 
            this.txtConfirmedAdditionalPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirmedAdditionalPaths.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmedAdditionalPaths.Location = new System.Drawing.Point(1011, 274);
            this.txtConfirmedAdditionalPaths.Multiline = true;
            this.txtConfirmedAdditionalPaths.Name = "txtConfirmedAdditionalPaths";
            this.txtConfirmedAdditionalPaths.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConfirmedAdditionalPaths.Size = new System.Drawing.Size(270, 211);
            this.txtConfirmedAdditionalPaths.TabIndex = 20;
            this.txtConfirmedAdditionalPaths.WordWrap = false;
            // 
            // txtInitialRoutesMatrix
            // 
            this.txtInitialRoutesMatrix.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInitialRoutesMatrix.Location = new System.Drawing.Point(17, 274);
            this.txtInitialRoutesMatrix.Multiline = true;
            this.txtInitialRoutesMatrix.Name = "txtInitialRoutesMatrix";
            this.txtInitialRoutesMatrix.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInitialRoutesMatrix.Size = new System.Drawing.Size(501, 211);
            this.txtInitialRoutesMatrix.TabIndex = 18;
            this.txtInitialRoutesMatrix.WordWrap = false;
            // 
            // lblInitialRoutesMatrix
            // 
            this.lblInitialRoutesMatrix.AutoSize = true;
            this.lblInitialRoutesMatrix.Location = new System.Drawing.Point(14, 258);
            this.lblInitialRoutesMatrix.Name = "lblInitialRoutesMatrix";
            this.lblInitialRoutesMatrix.Size = new System.Drawing.Size(221, 13);
            this.lblInitialRoutesMatrix.TabIndex = 19;
            this.lblInitialRoutesMatrix.Text = "Matricea construita din reteaua initiala de rute";
            // 
            // txtTotalTime
            // 
            this.txtTotalTime.Location = new System.Drawing.Point(81, 493);
            this.txtTotalTime.Name = "txtTotalTime";
            this.txtTotalTime.Size = new System.Drawing.Size(437, 20);
            this.txtTotalTime.TabIndex = 17;
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.Location = new System.Drawing.Point(14, 496);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(61, 13);
            this.lblTotalTime.TabIndex = 16;
            this.lblTotalTime.Text = "Timpul total";
            // 
            // txtInitialRoutesShortestPaths
            // 
            this.txtInitialRoutesShortestPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInitialRoutesShortestPaths.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInitialRoutesShortestPaths.Location = new System.Drawing.Point(536, 274);
            this.txtInitialRoutesShortestPaths.Multiline = true;
            this.txtInitialRoutesShortestPaths.Name = "txtInitialRoutesShortestPaths";
            this.txtInitialRoutesShortestPaths.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInitialRoutesShortestPaths.Size = new System.Drawing.Size(454, 211);
            this.txtInitialRoutesShortestPaths.TabIndex = 14;
            this.txtInitialRoutesShortestPaths.WordWrap = false;
            // 
            // lblInitialRoutesShortestPaths
            // 
            this.lblInitialRoutesShortestPaths.AutoSize = true;
            this.lblInitialRoutesShortestPaths.Location = new System.Drawing.Point(533, 258);
            this.lblInitialRoutesShortestPaths.Name = "lblInitialRoutesShortestPaths";
            this.lblInitialRoutesShortestPaths.Size = new System.Drawing.Size(131, 13);
            this.lblInitialRoutesShortestPaths.TabIndex = 15;
            this.lblInitialRoutesShortestPaths.Text = "Matricea drumurilor minime";
            // 
            // lblNewPaths
            // 
            this.lblNewPaths.AutoSize = true;
            this.lblNewPaths.Location = new System.Drawing.Point(931, 16);
            this.lblNewPaths.Name = "lblNewPaths";
            this.lblNewPaths.Size = new System.Drawing.Size(100, 13);
            this.lblNewPaths.TabIndex = 13;
            this.lblNewPaths.Text = "Rutele suplimentare";
            // 
            // lblInitialNetwork
            // 
            this.lblInitialNetwork.AutoSize = true;
            this.lblInitialNetwork.Location = new System.Drawing.Point(620, 16);
            this.lblInitialNetwork.Name = "lblInitialNetwork";
            this.lblInitialNetwork.Size = new System.Drawing.Size(80, 13);
            this.lblInitialNetwork.TabIndex = 12;
            this.lblInitialNetwork.Text = "Reteaua initiala";
            // 
            // txtNewPaths
            // 
            this.txtNewPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewPaths.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPaths.Location = new System.Drawing.Point(934, 32);
            this.txtNewPaths.Multiline = true;
            this.txtNewPaths.Name = "txtNewPaths";
            this.txtNewPaths.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNewPaths.Size = new System.Drawing.Size(270, 211);
            this.txtNewPaths.TabIndex = 11;
            this.txtNewPaths.WordWrap = false;
            // 
            // txtShortestPathsList
            // 
            this.txtShortestPathsList.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtShortestPathsList.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShortestPathsList.Location = new System.Drawing.Point(623, 32);
            this.txtShortestPathsList.Multiline = true;
            this.txtShortestPathsList.Name = "txtShortestPathsList";
            this.txtShortestPathsList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtShortestPathsList.Size = new System.Drawing.Size(270, 211);
            this.txtShortestPathsList.TabIndex = 8;
            this.txtShortestPathsList.WordWrap = false;
            // 
            // txtShortestPaths
            // 
            this.txtShortestPaths.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShortestPaths.Location = new System.Drawing.Point(17, 32);
            this.txtShortestPaths.Multiline = true;
            this.txtShortestPaths.Name = "txtShortestPaths";
            this.txtShortestPaths.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtShortestPaths.Size = new System.Drawing.Size(558, 211);
            this.txtShortestPaths.TabIndex = 9;
            this.txtShortestPaths.WordWrap = false;
            // 
            // lblShortestPaths
            // 
            this.lblShortestPaths.AutoSize = true;
            this.lblShortestPaths.Location = new System.Drawing.Point(14, 16);
            this.lblShortestPaths.Name = "lblShortestPaths";
            this.lblShortestPaths.Size = new System.Drawing.Size(131, 13);
            this.lblShortestPaths.TabIndex = 10;
            this.lblShortestPaths.Text = "Matricea drumurilor minime";
            // 
            // GeronimousMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 669);
            this.Controls.Add(this.tabControl);
            this.Name = "GeronimousMain";
            this.Text = "Geronimus";
            this.Resize += new System.EventHandler(this.GeronimousMainResize);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.grbInitialData.ResumeLayout(false);
            this.grbInitialData.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.grbResult.ResumeLayout(false);
            this.grbResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox grbInitialData;
        private System.Windows.Forms.TextBox txtUniformityCoeficient;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.TextBox txtPassengerArrivalCoeficient;
        private System.Windows.Forms.TextBox txtMaxInterval;
        private System.Windows.Forms.TextBox txtCapacity;
        private System.Windows.Forms.Label lblUniformityCoeficient;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.Label lblPassengerFlowText;
        private System.Windows.Forms.Label lblTransferTime;
        private System.Windows.Forms.Label lblInputMatrix;
        private System.Windows.Forms.TextBox txtPassengerFlow;
        private System.Windows.Forms.TextBox txtInitialMatrix;
        private System.Windows.Forms.TextBox txtWaitingTime;
        private System.Windows.Forms.Label lblInitialMatrix;
        private System.Windows.Forms.Label lblPassengerArrivalCoeficient;
        private System.Windows.Forms.Label lblMaxInterval;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Button btnLoadInitialData;
        private System.Windows.Forms.Button btnCalculateShortestPaths;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox grbResult;
        private System.Windows.Forms.Label lblNewPaths;
        private System.Windows.Forms.Label lblInitialNetwork;
        private System.Windows.Forms.TextBox txtNewPaths;
        private System.Windows.Forms.TextBox txtShortestPathsList;
        private System.Windows.Forms.TextBox txtShortestPaths;
        private System.Windows.Forms.Label lblShortestPaths;
        private System.Windows.Forms.TextBox txtInitialRoutesShortestPaths;
        private System.Windows.Forms.Label lblInitialRoutesShortestPaths;
        private System.Windows.Forms.TextBox txtTotalTime;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.TextBox txtInitialRoutesMatrix;
        private System.Windows.Forms.Label lblInitialRoutesMatrix;
        private System.Windows.Forms.Label lblConfirmedAdditionalPaths;
        private System.Windows.Forms.TextBox txtConfirmedAdditionalPaths;

    }
}

