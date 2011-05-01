namespace Stran
{
    partial class Rename
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
        	this.lboldVillagename = new System.Windows.Forms.Label();
        	this.buttonOK = new System.Windows.Forms.Button();
        	this.button_cancel = new System.Windows.Forms.Button();
        	this.tbnewVillagename = new System.Windows.Forms.TextBox();
        	this.SuspendLayout();
        	// 
        	// lboldVillagename
        	// 
        	this.lboldVillagename.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.lboldVillagename.Location = new System.Drawing.Point(6, 25);
        	this.lboldVillagename.Name = "lboldVillagename";
        	this.lboldVillagename.Size = new System.Drawing.Size(270, 30);
        	this.lboldVillagename.TabIndex = 3;
        	this.lboldVillagename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        	// 
        	// buttonOK
        	// 
        	this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        	this.buttonOK.Location = new System.Drawing.Point(42, 131);
        	this.buttonOK.Name = "buttonOK";
        	this.buttonOK.Size = new System.Drawing.Size(75, 23);
        	this.buttonOK.TabIndex = 1;
        	this.buttonOK.Tag = "ok";
        	this.buttonOK.Text = "OK";
        	this.buttonOK.UseVisualStyleBackColor = true;
        	this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
        	// 
        	// button_cancel
        	// 
        	this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        	this.button_cancel.Location = new System.Drawing.Point(167, 131);
        	this.button_cancel.Name = "button_cancel";
        	this.button_cancel.Size = new System.Drawing.Size(75, 23);
        	this.button_cancel.TabIndex = 2;
        	this.button_cancel.Tag = "cancel";
        	this.button_cancel.Text = "Cancel";
        	this.button_cancel.UseVisualStyleBackColor = true;
        	// 
        	// tbnewVillagename
        	// 
        	this.tbnewVillagename.Font = new System.Drawing.Font("Tahoma", 12F);
        	this.tbnewVillagename.Location = new System.Drawing.Point(42, 84);
        	this.tbnewVillagename.MaxLength = 20;
        	this.tbnewVillagename.Name = "tbnewVillagename";
        	this.tbnewVillagename.Size = new System.Drawing.Size(200, 27);
        	this.tbnewVillagename.TabIndex = 0;
        	this.tbnewVillagename.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        	// 
        	// Rename
        	// 
        	this.AcceptButton = this.buttonOK;
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.CancelButton = this.button_cancel;
        	this.ClientSize = new System.Drawing.Size(284, 162);
        	this.Controls.Add(this.tbnewVillagename);
        	this.Controls.Add(this.button_cancel);
        	this.Controls.Add(this.buttonOK);
        	this.Controls.Add(this.lboldVillagename);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        	this.MaximizeBox = false;
        	this.Name = "Rename";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        	this.Tag = "DlgRename";
        	this.Text = "Rename";
        	this.Load += new System.EventHandler(this.Rename_Load);
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lboldVillagename;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TextBox tbnewVillagename;
    }
}