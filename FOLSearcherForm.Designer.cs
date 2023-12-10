namespace FYP_FC_Evaluator
{
	partial class FOLSearcherForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.queryBox = new System.Windows.Forms.TextBox();
			this.materialBox = new System.Windows.Forms.RichTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.topDownSearchButton = new System.Windows.Forms.Button();
			this.resultsGridView = new System.Windows.Forms.DataGridView();
			this.solutionNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.variableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.variableValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bottomUpSearchButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.resultsGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Query:";
			// 
			// queryBox
			// 
			this.queryBox.Location = new System.Drawing.Point(55, 13);
			this.queryBox.Name = "queryBox";
			this.queryBox.Size = new System.Drawing.Size(601, 20);
			this.queryBox.TabIndex = 1;
			// 
			// materialBox
			// 
			this.materialBox.Location = new System.Drawing.Point(16, 56);
			this.materialBox.Name = "materialBox";
			this.materialBox.Size = new System.Drawing.Size(640, 403);
			this.materialBox.TabIndex = 2;
			this.materialBox.Text = "";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Search Material:";
			// 
			// topDownSearchButton
			// 
			this.topDownSearchButton.Location = new System.Drawing.Point(662, 465);
			this.topDownSearchButton.Name = "topDownSearchButton";
			this.topDownSearchButton.Size = new System.Drawing.Size(236, 23);
			this.topDownSearchButton.TabIndex = 4;
			this.topDownSearchButton.Text = "Top Down Search";
			this.topDownSearchButton.UseVisualStyleBackColor = true;
			this.topDownSearchButton.Click += new System.EventHandler(this.TopDownSearchButton_Click);
			// 
			// resultsGridView
			// 
			this.resultsGridView.AllowUserToAddRows = false;
			this.resultsGridView.AllowUserToDeleteRows = false;
			this.resultsGridView.AllowUserToOrderColumns = true;
			this.resultsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.resultsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.solutionNumber,
            this.variableName,
            this.variableValue});
			this.resultsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.resultsGridView.Location = new System.Drawing.Point(662, 13);
			this.resultsGridView.Name = "resultsGridView";
			this.resultsGridView.ReadOnly = true;
			this.resultsGridView.RowHeadersVisible = false;
			this.resultsGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.resultsGridView.Size = new System.Drawing.Size(478, 446);
			this.resultsGridView.TabIndex = 5;
			// 
			// solutionNumber
			// 
			this.solutionNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.solutionNumber.HeaderText = "Solution Number";
			this.solutionNumber.Name = "solutionNumber";
			this.solutionNumber.ReadOnly = true;
			this.solutionNumber.Width = 101;
			// 
			// variableName
			// 
			this.variableName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.variableName.HeaderText = "Variable Name";
			this.variableName.Name = "variableName";
			this.variableName.ReadOnly = true;
			// 
			// variableValue
			// 
			this.variableValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.variableValue.HeaderText = "Value";
			this.variableValue.Name = "variableValue";
			this.variableValue.ReadOnly = true;
			// 
			// bottomUpSearchButton
			// 
			this.bottomUpSearchButton.Location = new System.Drawing.Point(904, 465);
			this.bottomUpSearchButton.Name = "bottomUpSearchButton";
			this.bottomUpSearchButton.Size = new System.Drawing.Size(236, 23);
			this.bottomUpSearchButton.TabIndex = 6;
			this.bottomUpSearchButton.Text = "Bottom Up Search";
			this.bottomUpSearchButton.UseVisualStyleBackColor = true;
			this.bottomUpSearchButton.Click += new System.EventHandler(this.BottomUpSearchButton_Click);
			// 
			// FOLSearcherForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1152, 500);
			this.Controls.Add(this.bottomUpSearchButton);
			this.Controls.Add(this.resultsGridView);
			this.Controls.Add(this.topDownSearchButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.materialBox);
			this.Controls.Add(this.queryBox);
			this.Controls.Add(this.label1);
			this.Name = "FOLSearcherForm";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.resultsGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox queryBox;
		private System.Windows.Forms.RichTextBox materialBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button topDownSearchButton;
		private System.Windows.Forms.DataGridView resultsGridView;
		private System.Windows.Forms.Button bottomUpSearchButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn solutionNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn variableName;
		private System.Windows.Forms.DataGridViewTextBoxColumn variableValue;
	}
}

