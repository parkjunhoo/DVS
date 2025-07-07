using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Events;
using DevExpress.XtraSpreadsheet;
using System.IO;

namespace DVS.Forms
{
    public partial class CheckPriceForm : XtraForm
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        string invoiceExcelPath = "";
        string itemListExcelPath = "";

        public CheckPriceForm()
        {
            InitializeComponent();

            //엑셀 형식 지정
            openFileDialog.Filter = "Excel Files (*.xlsx; *.xls)|*.xlsx;*.xls|All Files (*.*)|*.*";
            openFileDialog.Title = "거래명세서 열기";

            //스프레드시트 설정
            ssInvoice.PopupMenuShowing += SpreadsheetControl_PopupMenuShowing;
            //ssInvoice.Visible = false;
            ssItemList.PopupMenuShowing += SpreadsheetControl_PopupMenuShowing;
            //ssItemList.Visible = false;

        }


        //스프레드시트 우클릭 커스텀 이벤트
        private void SpreadsheetControl_PopupMenuShowing(object sender, DevExpress.XtraSpreadsheet.PopupMenuShowingEventArgs e)
        {
            // 뒤에서부터 돌면서 작업 (인덱스 밀림 방지)
            for (int i = e.Menu.Items.Count - 1; i >= 0; i--)
            {
                var item = e.Menu.Items[i];

                // Cut/Copy/Paste만 남기고 나머지 제거
                if (item.Caption == "Cut" || item.Caption == "Copy" || item.Caption == "Paste")
                {
                    // 캡션을 한글로 바꾸기
                    switch (item.Caption)
                    {
                        case "Cut":
                            item.Caption = "잘라내기";
                            break;
                        case "Copy":
                            item.Caption = "복사하기";
                            break;
                        case "Paste":
                            item.Caption = "붙여넣기";
                            break;
                    }
                }
                else
                {
                    // 나머지는 제거
                    e.Menu.Items.RemoveAt(i);
                }
            }
        }
        //스프레드시트 저장 다른이른으로
        private void SpreadsheetControl_KeyDown(object sender, KeyEventArgs e)
        {
            SpreadsheetControl ssc = sender as SpreadsheetControl;
            // Ctrl+S가 눌렸을 때
            if (e.Control && e.KeyCode == Keys.S)
            {
                // 다른 이름으로 저장 대화상자 표시
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel 파일 (*.xlsx)|*.xlsx|모든 파일 (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 선택된 경로로 저장
                    ssc.SaveDocument(saveFileDialog.FileName, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
                }

                // 기본 저장 동작을 취소
                e.Handled = true;
            }
        }




        //거래명세서 열기
        private void btnOpenExcelClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //디자이너에서 캡션안에 invoice, itemList 넣어놓고 해당 값으로 분기처리
                string caption = (string)e.Item.Tag;

                //ofd로 open 한 파일경로
                string originPath = openFileDialog.FileName;

                //tempPath로 만들 폴더 만들기
                string todayDate = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = Path.GetFileName(originPath);
                string newFileName = $"{todayDate}{Path.GetExtension(fileName)}";

                //오늘날짜로 만듬
                string tempPath = Path.Combine(Application.StartupPath, "temp", caption);
                //없으면 temp폴더 만들기
                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }

                tempPath = Path.Combine(Application.StartupPath, "temp", caption, newFileName);
                //temp 폴더로 원본 파일 복사
                try
                {
                    File.Copy(originPath, tempPath, true);
                }
                catch
                {

                }

                if (caption == "invoice")
                {
                    invoiceExcelPath = tempPath;
                    ssInvoice.LoadDocument(invoiceExcelPath);
                    ssInvoice.Visible = true;
                }
                if (caption == "itemList")
                {
                    itemListExcelPath = tempPath;
                    ssItemList.LoadDocument(itemListExcelPath);
                    ssItemList.Visible = true;
                }
            }
        }

        private void CheckPriceForm_Load(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = this.Width / 2;
        }

        //(TEST)
        private void btnSearchItemName_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedRange = ssInvoice.ActiveWorksheet.Selection;
            if (selectedRange != null)
            {
                var firstCell = selectedRange[0, 0];
                var cellValue = firstCell.Value.ToString();

                selectedRange.FillColor = Color.Yellow;
                XtraMessageBox.Show($"{cellValue}", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
