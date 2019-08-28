namespace RainbowMage.ActLocalizer
{
    partial class ConfigPanel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboLocale = new System.Windows.Forms.ComboBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonExportRN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Locale:";
            // 
            // comboLocale
            // 
            this.comboLocale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLocale.FormattingEnabled = true;
            this.comboLocale.Location = new System.Drawing.Point(49, 13);
            this.comboLocale.Name = "comboLocale";
            this.comboLocale.Size = new System.Drawing.Size(121, 20);
            this.comboLocale.TabIndex = 2;
            this.comboLocale.SelectedIndexChanged += new System.EventHandler(this.comboLocale_SelectedIndexChanged);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(5, 85);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(323, 50);
            this.buttonExport.TabIndex = 3;
            this.buttonExport.Text = "Export original translation files on next boot\r\n(Overwrite if exists)";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(3, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "* You need to restart the ACT to take effect.";
            // 
            // buttonExportRN
            // 
            this.buttonExportRN.Location = new System.Drawing.Point(5, 142);
            this.buttonExportRN.Name = "buttonExportRN";
            this.buttonExportRN.Size = new System.Drawing.Size(323, 50);
            this.buttonExportRN.TabIndex = 5;
            this.buttonExportRN.Text = "Export current plugin translation files right now\r\n(Overwrite if exists)";
            this.buttonExportRN.UseVisualStyleBackColor = true;
            this.buttonExportRN.Click += new System.EventHandler(this.buttonExportRN_Click);
            // 
            // ConfigPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonExportRN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.comboLocale);
            this.Controls.Add(this.label1);
            this.Name = "ConfigPanel";
            this.Size = new System.Drawing.Size(382, 309);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboLocale;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonExportRN;
    }
}
