namespace DVS
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btn_openCheckPrice = new DevExpress.XtraEditors.SimpleButton();
            btn_exit = new DevExpress.XtraEditors.SimpleButton();
            SuspendLayout();
            // 
            // btn_openCheckPrice
            // 
            btn_openCheckPrice.Appearance.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_openCheckPrice.Appearance.Options.UseFont = true;
            btn_openCheckPrice.ImageOptions.Image = (Image)resources.GetObject("btn_openCheckPrice.ImageOptions.Image");
            btn_openCheckPrice.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            btn_openCheckPrice.ImageOptions.ImageToTextIndent = 20;
            btn_openCheckPrice.Location = new Point(15, 18);
            btn_openCheckPrice.Margin = new Padding(4);
            btn_openCheckPrice.Name = "btn_openCheckPrice";
            btn_openCheckPrice.Size = new Size(276, 211);
            btn_openCheckPrice.TabIndex = 0;
            btn_openCheckPrice.Text = "거래명세표 단가 비교";
            btn_openCheckPrice.Click += btn_openCheckPrice_Click;
            // 
            // btn_exit
            // 
            btn_exit.Appearance.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_exit.Appearance.Options.UseFont = true;
            btn_exit.ImageOptions.Image = (Image)resources.GetObject("btn_exit.ImageOptions.Image");
            btn_exit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            btn_exit.ImageOptions.ImageToTextIndent = 20;
            btn_exit.Location = new Point(299, 18);
            btn_exit.Margin = new Padding(4);
            btn_exit.Name = "btn_exit";
            btn_exit.Size = new Size(276, 211);
            btn_exit.TabIndex = 1;
            btn_exit.Text = "종료";
            btn_exit.Click += btn_exit_Click;
            // 
            // MainForm
            // 
            Appearance.Options.UseFont = true;
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 234);
            Controls.Add(btn_exit);
            Controls.Add(btn_openCheckPrice);
            Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            IconOptions.LargeImage = (Image)resources.GetObject("MainForm.IconOptions.LargeImage");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DVS";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_openCheckPrice;
        private DevExpress.XtraEditors.SimpleButton btn_exit;
    }
}
