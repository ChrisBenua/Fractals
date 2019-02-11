using System;
using System.Drawing;
using System.Windows.Forms;

namespace Benua_21
{
    partial class Form1
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
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.iterationsUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.startColorButton = new System.Windows.Forms.Button();
            this.endColorButton = new System.Windows.Forms.Button();
            this.fractalComboBox = new System.Windows.Forms.ComboBox();
            this.selectZoomComboBox = new System.Windows.Forms.ComboBox();
            this.saveImageButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // picCanvas
            // 
            this.picCanvas.Location = new System.Drawing.Point(142, 0);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(953, 594);
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            // 
            // iterationsUpDown
            // 
            this.iterationsUpDown.Location = new System.Drawing.Point(1, 94);
            this.iterationsUpDown.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.iterationsUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.iterationsUpDown.Name = "iterationsUpDown";
            this.iterationsUpDown.Size = new System.Drawing.Size(135, 31);
            this.iterationsUpDown.TabIndex = 1;
            this.iterationsUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.iterationsUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.iterationsUpDown.ValueChanged += new System.EventHandler(this.IterationsUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Iterations";
            // 
            // startColorButton
            // 
            this.startColorButton.Location = new System.Drawing.Point(1, 170);
            this.startColorButton.Name = "startColorButton";
            this.startColorButton.Size = new System.Drawing.Size(135, 41);
            this.startColorButton.TabIndex = 3;
            this.startColorButton.Text = "start color";
            this.startColorButton.UseVisualStyleBackColor = true;
            this.startColorButton.Click += new System.EventHandler(this.StartColorButton_Click);
            // 
            // endColorButton
            // 
            this.endColorButton.Location = new System.Drawing.Point(1, 256);
            this.endColorButton.Name = "endColorButton";
            this.endColorButton.Size = new System.Drawing.Size(135, 41);
            this.endColorButton.TabIndex = 4;
            this.endColorButton.Text = "end color";
            this.endColorButton.UseVisualStyleBackColor = true;
            this.endColorButton.Click += new System.EventHandler(this.StartEndColorButton_Click);
            // 
            // fractalComboBox
            // 
            this.fractalComboBox.FormattingEnabled = true;
            this.fractalComboBox.Items.AddRange(new object[] {
            "Koch Curve",
            "H-Fractal",
            "CircleFractal"});
            this.fractalComboBox.Location = new System.Drawing.Point(1, 338);
            this.fractalComboBox.Name = "fractalComboBox";
            this.fractalComboBox.Size = new System.Drawing.Size(135, 33);
            this.fractalComboBox.TabIndex = 5;
            // 
            // selectZoomComboBox
            // 
            this.selectZoomComboBox.FormattingEnabled = true;
            this.selectZoomComboBox.Location = new System.Drawing.Point(1, 412);
            this.selectZoomComboBox.Name = "selectZoomComboBox";
            this.selectZoomComboBox.Size = new System.Drawing.Size(135, 33);
            this.selectZoomComboBox.TabIndex = 6;
            // 
            // saveImageButton
            // 
            this.saveImageButton.Location = new System.Drawing.Point(1, 486);
            this.saveImageButton.Name = "saveImageButton";
            this.saveImageButton.Size = new System.Drawing.Size(135, 47);
            this.saveImageButton.TabIndex = 7;
            this.saveImageButton.Text = "Save Image";
            this.saveImageButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 590);
            this.Controls.Add(this.saveImageButton);
            this.Controls.Add(this.selectZoomComboBox);
            this.Controls.Add(this.fractalComboBox);
            this.Controls.Add(this.endColorButton);
            this.Controls.Add(this.startColorButton);
            this.Controls.Add(this.iterationsUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picCanvas);
            this.MinimumSize = new System.Drawing.Size(400, 600);
            this.Name = "Form1";
            this.Text = "Fractals";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.NumericUpDown iterationsUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startColorButton;
        private System.Windows.Forms.Button endColorButton;
        private System.Windows.Forms.ComboBox fractalComboBox;
        private System.Windows.Forms.ComboBox selectZoomComboBox;
        private System.Windows.Forms.Button saveImageButton;
    }
}

