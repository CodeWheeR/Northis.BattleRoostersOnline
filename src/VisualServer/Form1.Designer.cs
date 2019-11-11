namespace VisualServer
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
			this.StartButton = new System.Windows.Forms.Button();
			this.StopButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// StartButton
			// 
			this.StartButton.Location = new System.Drawing.Point(12, 12);
			this.StartButton.Name = "StartButton";
			this.StartButton.Size = new System.Drawing.Size(176, 23);
			this.StartButton.TabIndex = 0;
			this.StartButton.Text = "Запустить";
			this.StartButton.UseVisualStyleBackColor = true;
			this.StartButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// StopButton
			// 
			this.StopButton.Location = new System.Drawing.Point(194, 12);
			this.StopButton.Name = "StopButton";
			this.StopButton.Size = new System.Drawing.Size(176, 23);
			this.StopButton.TabIndex = 1;
			this.StopButton.Text = "Остановить";
			this.StopButton.UseVisualStyleBackColor = true;
			this.StopButton.Click += new System.EventHandler(this.button2_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(377, 45);
			this.Controls.Add(this.StopButton);
			this.Controls.Add(this.StartButton);
			this.Name = "Form1";
			this.Text = "Сервер";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button StartButton;
		private System.Windows.Forms.Button StopButton;
	}
}

