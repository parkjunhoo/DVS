namespace DVS.Forms
{
    partial class CheckPriceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckPriceForm));
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            btnOpenInvoice = new DevExpress.XtraBars.BarButtonItem();
            btnOpenItemList = new DevExpress.XtraBars.BarButtonItem();
            btnSearchItemName = new DevExpress.XtraBars.BarButtonItem();
            btnPriceValidation = new DevExpress.XtraBars.BarButtonItem();
            btnPriceVEdit = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ssInvoice = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            ssItemList = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            SuspendLayout();
            // 
            // ribbonControl1
            // 
            ribbonControl1.AllowMinimizeRibbon = false;
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, btnOpenInvoice, btnOpenItemList, btnSearchItemName, btnPriceValidation, btnPriceVEdit });
            ribbonControl1.Location = new Point(0, 0);
            ribbonControl1.MaxItemId = 6;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.OptionsExpandCollapseMenu.ShowQuickAccessToolbarItem = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.OptionsExpandCollapseMenu.ShowRibbonGroup = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.OptionsExpandCollapseMenu.ShowRibbonLayoutGroup = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.OptionsTouch.ShowTouchUISelectorInSearchMenu = false;
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.ShowMoreCommandsButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            ribbonControl1.ShowPageKeyTipsMode = DevExpress.XtraBars.Ribbon.ShowPageKeyTipsMode.Hide;
            ribbonControl1.ShowQatLocationSelector = false;
            ribbonControl1.ShowToolbarCustomizeItem = false;
            ribbonControl1.Size = new Size(800, 151);
            ribbonControl1.Toolbar.ShowCustomizeItem = false;
            ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // btnOpenInvoice
            // 
            btnOpenInvoice.Caption = "거래명세서 열기";
            btnOpenInvoice.Id = 1;
            btnOpenInvoice.ImageOptions.Image = (Image)resources.GetObject("btnOpenInvoice.ImageOptions.Image");
            btnOpenInvoice.Name = "btnOpenInvoice";
            btnOpenInvoice.Tag = "invoice";
            btnOpenInvoice.ItemClick += btnOpenExcelClick;
            // 
            // btnOpenItemList
            // 
            btnOpenItemList.Caption = "품목표 열기";
            btnOpenItemList.Id = 2;
            btnOpenItemList.ImageOptions.Image = (Image)resources.GetObject("btnOpenItemList.ImageOptions.Image");
            btnOpenItemList.Name = "btnOpenItemList";
            btnOpenItemList.Tag = "itemList";
            btnOpenItemList.ItemClick += btnOpenExcelClick;
            // 
            // btnSearchItemName
            // 
            btnSearchItemName.Caption = "품목 검색";
            btnSearchItemName.Id = 3;
            btnSearchItemName.ImageOptions.Image = (Image)resources.GetObject("btnSearchItemName.ImageOptions.Image");
            btnSearchItemName.Name = "btnSearchItemName";
            btnSearchItemName.ItemClick += btnSearchItemName_ItemClick;
            // 
            // btnPriceValidation
            // 
            btnPriceValidation.Caption = "단가 검증하기";
            btnPriceValidation.Id = 4;
            btnPriceValidation.ImageOptions.Image = (Image)resources.GetObject("btnPriceValidation.ImageOptions.Image");
            btnPriceValidation.Name = "btnPriceValidation";
            btnPriceValidation.ItemClick += btnPriceValidation_ItemClick;
            // 
            // btnPriceVEdit
            // 
            btnPriceVEdit.Caption = "단가 검증&수정";
            btnPriceVEdit.Id = 5;
            btnPriceVEdit.ImageOptions.Image = (Image)resources.GetObject("barButtonItem1.ImageOptions.Image");
            btnPriceVEdit.Name = "btnPriceVEdit";
            btnPriceVEdit.ItemClick += btnPriceVEdit_ItemClick;
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1, ribbonPageGroup2 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(btnOpenInvoice);
            ribbonPageGroup1.ItemLinks.Add(btnOpenItemList);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "파일열기";
            // 
            // ribbonPageGroup2
            // 
            ribbonPageGroup2.ItemLinks.Add(btnSearchItemName);
            ribbonPageGroup2.ItemLinks.Add(btnPriceValidation);
            ribbonPageGroup2.ItemLinks.Add(btnPriceVEdit);
            ribbonPageGroup2.Name = "ribbonPageGroup2";
            ribbonPageGroup2.Text = "기능";
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainerControl1.Location = new Point(0, 138);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Controls.Add(ssInvoice);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(ssItemList);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(800, 433);
            splitContainerControl1.SplitterPosition = 447;
            splitContainerControl1.TabIndex = 1;
            // 
            // ssInvoice
            // 
            ssInvoice.Dock = DockStyle.Fill;
            ssInvoice.Location = new Point(0, 0);
            ssInvoice.MenuManager = ribbonControl1;
            ssInvoice.Name = "ssInvoice";
            ssInvoice.Size = new Size(447, 433);
            ssInvoice.TabIndex = 0;
            ssInvoice.Text = "spreadsheetControl1";
            ssInvoice.KeyDown += SpreadsheetControl_KeyDown;
            // 
            // ssItemList
            // 
            ssItemList.Dock = DockStyle.Fill;
            ssItemList.Location = new Point(0, 0);
            ssItemList.MenuManager = ribbonControl1;
            ssItemList.Name = "ssItemList";
            ssItemList.Size = new Size(347, 433);
            ssItemList.TabIndex = 1;
            ssItemList.Text = "spreadsheetControl2";
            ssItemList.KeyDown += SpreadsheetControl_KeyDown;
            // 
            // CheckPriceForm
            // 
            Appearance.Options.UseFont = true;
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 571);
            Controls.Add(splitContainerControl1);
            Controls.Add(ribbonControl1);
            IconOptions.Image = (Image)resources.GetObject("CheckPriceForm.IconOptions.Image");
            Name = "CheckPriceForm";
            Text = "거래명세서 단가 비교";
            Load += CheckPriceForm_Load;
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnOpenInvoice;
        private DevExpress.XtraBars.BarButtonItem btnOpenItemList;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl ssInvoice;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl ssItemList;
        private DevExpress.XtraBars.BarButtonItem btnSearchItemName;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem btnPriceValidation;
        private DevExpress.XtraBars.BarButtonItem btnPriceVEdit;
    }
}