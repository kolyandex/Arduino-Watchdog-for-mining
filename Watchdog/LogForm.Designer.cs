﻿namespace Watchdog
{
    partial class LogForm
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
            this.loggerRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // loggerRichTextBox
            // 
            this.loggerRichTextBox.Location = new System.Drawing.Point(31, 33);
            this.loggerRichTextBox.Name = "loggerRichTextBox";
            this.loggerRichTextBox.Size = new System.Drawing.Size(471, 319);
            this.loggerRichTextBox.TabIndex = 0;
            this.loggerRichTextBox.Text = "";
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 389);
            this.Controls.Add(this.loggerRichTextBox);
            this.Name = "LogForm";
            this.Text = "LogForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox loggerRichTextBox;
    }
}