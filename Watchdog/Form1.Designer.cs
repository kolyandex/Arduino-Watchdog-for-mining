﻿namespace Watchdog
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.SerialTimer = new System.Windows.Forms.Timer(this.components);
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.AutorunCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(12, 9);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(51, 13);
            this.InfoLabel.TabIndex = 1;
            this.InfoLabel.Text = "InfoLabel";
            // 
            // SerialTimer
            // 
            this.SerialTimer.Enabled = true;
            this.SerialTimer.Interval = 1000;
            this.SerialTimer.Tick += new System.EventHandler(this.SerialTimer_Tick);
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(252, 12);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(14, 13);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // AutorunCheckBox
            // 
            this.AutorunCheckBox.AutoSize = true;
            this.AutorunCheckBox.Location = new System.Drawing.Point(12, 39);
            this.AutorunCheckBox.Name = "AutorunCheckBox";
            this.AutorunCheckBox.Size = new System.Drawing.Size(117, 17);
            this.AutorunCheckBox.TabIndex = 6;
            this.AutorunCheckBox.Text = "Start with Windows";
            this.AutorunCheckBox.UseVisualStyleBackColor = true;
            this.AutorunCheckBox.CheckedChanged += new System.EventHandler(this.AutorunCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 73);
            this.Controls.Add(this.AutorunCheckBox);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.InfoLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Arduino watchdog";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.Timer SerialTimer;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.CheckBox AutorunCheckBox;
    }
}

