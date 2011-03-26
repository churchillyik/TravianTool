namespace Stran.DockingPanel
{
	partial class ResourceShow
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
			if(disposing && (components != null))
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
			this.label5 = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.resourceLabel1 = new Stran.ResourceLabel();
			this.resourceLabel2 = new Stran.ResourceLabel();
			this.resourceLabel3 = new Stran.ResourceLabel();
			this.resourceLabel4 = new Stran.ResourceLabel();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Ivory;
			this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(0, 56);
			this.label5.Margin = new System.Windows.Forms.Padding(10, 4, 10, 4);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(472, 32);
			this.label5.TabIndex = 21;
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.Controls.Add(this.resourceLabel1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.resourceLabel2, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.resourceLabel3, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.resourceLabel4, 3, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(472, 56);
			this.tableLayoutPanel2.TabIndex = 20;
			// 
			// resourceLabel1
			// 
			this.resourceLabel1.BackColor = System.Drawing.Color.LightYellow;
			this.resourceLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resourceLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.resourceLabel1.Location = new System.Drawing.Point(3, 3);
			this.resourceLabel1.Name = "resourceLabel1";
			this.resourceLabel1.Res = null;
			this.resourceLabel1.Size = new System.Drawing.Size(112, 50);
			this.resourceLabel1.TabIndex = 20;
			// 
			// resourceLabel2
			// 
			this.resourceLabel2.BackColor = System.Drawing.Color.LightYellow;
			this.resourceLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resourceLabel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.resourceLabel2.Location = new System.Drawing.Point(121, 3);
			this.resourceLabel2.Name = "resourceLabel2";
			this.resourceLabel2.Res = null;
			this.resourceLabel2.Size = new System.Drawing.Size(112, 50);
			this.resourceLabel2.TabIndex = 21;
			// 
			// resourceLabel3
			// 
			this.resourceLabel3.BackColor = System.Drawing.Color.LightYellow;
			this.resourceLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resourceLabel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.resourceLabel3.Location = new System.Drawing.Point(239, 3);
			this.resourceLabel3.Name = "resourceLabel3";
			this.resourceLabel3.Res = null;
			this.resourceLabel3.Size = new System.Drawing.Size(112, 50);
			this.resourceLabel3.TabIndex = 22;
			// 
			// resourceLabel4
			// 
			this.resourceLabel4.BackColor = System.Drawing.Color.LightYellow;
			this.resourceLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resourceLabel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.resourceLabel4.Location = new System.Drawing.Point(357, 3);
			this.resourceLabel4.Name = "resourceLabel4";
			this.resourceLabel4.Res = null;
			this.resourceLabel4.Size = new System.Drawing.Size(112, 50);
			this.resourceLabel4.TabIndex = 23;
			// 
			// ResourceShow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(472, 88);
			this.CloseButton = false;
			this.Controls.Add(this.tableLayoutPanel2);
			this.Controls.Add(this.label5);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ResourceShow";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockTop;
			this.TabText = "ResourceShow";
			this.Tag = "resourceshow";
			this.Text = "ResourceShow";
			this.Load += new System.EventHandler(this.ResourceShow_Load);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		public System.Windows.Forms.Label label5;
		public ResourceLabel resourceLabel1;
		public ResourceLabel resourceLabel2;
		public ResourceLabel resourceLabel3;
		public ResourceLabel resourceLabel4;
	}
}