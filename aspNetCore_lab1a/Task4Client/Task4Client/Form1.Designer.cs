namespace Task4Client;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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

    private void InitializeComponent()
    {
        button1 = new Button();
        textBox1 = new TextBox();
        textBox2 = new TextBox();
        textBox3 = new TextBox();
        label1 = new Label();
        label2 = new Label();
        textBox4 = new TextBox();
        SuspendLayout();
        // 
        // button1
        // 
        button1.Location = new Point(305, 141);
        button1.Margin = new Padding(2);
        button1.Name = "button1";
        button1.Size = new Size(76, 21);
        button1.TabIndex = 0;
        button1.Text = "=";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // textBox1
        // 
        textBox1.Location = new Point(11, 139);
        textBox1.Margin = new Padding(2);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(102, 23);
        textBox1.TabIndex = 1;
        textBox1.TextAlign = HorizontalAlignment.Center;
        textBox1.TextChanged += textBox1_TextChanged;
        // 
        // textBox2
        // 
        textBox2.Location = new Point(166, 139);
        textBox2.Margin = new Padding(2);
        textBox2.Name = "textBox2";
        textBox2.Size = new Size(102, 23);
        textBox2.TabIndex = 2;
        textBox2.TextAlign = HorizontalAlignment.Center;
        // 
        // textBox3
        // 
        textBox3.BackColor = SystemColors.Window;
        textBox3.Location = new Point(411, 141);
        textBox3.Margin = new Padding(2);
        textBox3.Name = "textBox3";
        textBox3.Size = new Size(189, 23);
        textBox3.TabIndex = 3;
        textBox3.TextAlign = HorizontalAlignment.Center;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(131, 144);
        label1.Name = "label1";
        label1.Size = new Size(15, 15);
        label1.TabIndex = 4;
        label1.Text = "+";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(206, 105);
        label2.Name = "label2";
        label2.Size = new Size(14, 15);
        label2.TabIndex = 5;
        label2.Text = "Y";
        // 
        // textBox4
        // 
        textBox4.Location = new Point(41, 102);
        textBox4.Name = "textBox4";
        textBox4.Size = new Size(33, 23);
        textBox4.TabIndex = 6;
        textBox4.Text = "X";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(622, 321);
        Controls.Add(textBox4);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(textBox3);
        Controls.Add(textBox2);
        Controls.Add(textBox1);
        Controls.Add(button1);
        Margin = new Padding(2);
        Name = "Form1";
        Text = "Task4WinClient";
        ResumeLayout(false);
        PerformLayout();
    }

    private Button button1;
    private TextBox textBox1;
    private TextBox textBox2;
    private TextBox textBox3;
    private Label label1;
    private Label label2;
    private TextBox textBox4;
}
