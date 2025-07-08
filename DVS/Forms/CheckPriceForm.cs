using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet;
using System.IO;
using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;

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

            //엑셀 형식 지정, xlsx , xls파일만 
            openFileDialog.Filter = "Excel Files (*.xlsx; *.xls)|*.xlsx;*.xls|All Files (*.*)|*.*";
            openFileDialog.Title = "거래명세서 열기";

            //스프레드시트 설정 엑셀 업로드안되잇으면 안보이게, 커스텀메뉴설정땜 이벤트핸들러 달아주기,
            ssInvoice.PopupMenuShowing += SpreadsheetControl_PopupMenuShowing;
            ssInvoice.Visible = false;
            ssItemList.PopupMenuShowing += SpreadsheetControl_PopupMenuShowing;
            ssItemList.Visible = false;

        }

        private void CheckPriceForm_Load(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = this.Width / 2;
        }



        //스프레드시트 우클릭 커스텀 이벤트
        private void SpreadsheetControl_PopupMenuShowing(object sender, DevExpress.XtraSpreadsheet.PopupMenuShowingEventArgs e)
        {
            // 뒤에서부터 돌면서 작업 (인덱스 밀림 방지)
            for (int i = e.Menu.Items.Count - 1; i >= 0; i--)
            {
                var item = e.Menu.Items[i];

                // 원하는기능만 남기기
                if (item.Caption == "Cut" || item.Caption == "Copy" || item.Caption == "Paste" || item.Caption == "Delete")
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
                        case "Delete":
                            item.Caption = "삭제하기";
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

        private void btnSearchItemName_ItemClick(object sender, ItemClickEventArgs e)
        {
            var invoiceWorksheet = ssInvoice.ActiveWorksheet;
            var itemListWorksheet = ssItemList.ActiveWorksheet;

            if (invoiceWorksheet == null || itemListWorksheet == null)
            {
                XtraMessageBox.Show("거래명세서와 품목리스트를 모두 불러와야 합니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 1. 선택된 범위 확인
                var selectedRange = ssInvoice.ActiveWorksheet.Selection;
                if (selectedRange == null)
                {
                    XtraMessageBox.Show("행을 선택해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2. 여러 행 선택 확인 (병합된 셀 고려)
                if (selectedRange.RowCount > 1)
                {
                    // 병합된 셀인지 확인 - 첫 번째 행에만 데이터가 있고 나머지는 비어있는지 체크
                    bool isMergedCell = true;
                    var firstRowData = "";

                    // 첫 번째 행의 데이터 확인
                    for (int col = selectedRange.LeftColumnIndex; col <= selectedRange.RightColumnIndex; col++)
                    {
                        var cellValue = invoiceWorksheet.Cells[selectedRange.TopRowIndex, col].Value?.ToString()?.Trim();
                        if (!string.IsNullOrWhiteSpace(cellValue))
                        {
                            firstRowData = cellValue;
                            break;
                        }
                    }

                    // 나머지 행들이 비어있는지 확인
                    for (int row = selectedRange.TopRowIndex + 1; row <= selectedRange.BottomRowIndex; row++)
                    {
                        for (int col = selectedRange.LeftColumnIndex; col <= selectedRange.RightColumnIndex; col++)
                        {
                            var cellValue = invoiceWorksheet.Cells[row, col].Value?.ToString()?.Trim();
                            if (!string.IsNullOrWhiteSpace(cellValue))
                            {
                                isMergedCell = false;
                                break;
                            }
                        }
                        if (!isMergedCell) break;
                    }

                    // 병합된 셀이 아니면서 여러 행이 선택된 경우 에러
                    if (!isMergedCell)
                    {
                        XtraMessageBox.Show("한 행만 선택해주세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                int selectedRow = selectedRange.TopRowIndex;
                string itemCode = "";

                // 3. 선택된 행에서 물품코드 찾기
                // 현재 행이 물품코드 행인지 확인 (첫 번째 컬럼이 숫자로만 구성)
                var firstCell = invoiceWorksheet.Cells[selectedRow, 0];
                var firstCellValue = firstCell.Value?.ToString()?.Trim();

                if (!string.IsNullOrWhiteSpace(firstCellValue) &&
                    firstCellValue.All(char.IsDigit) &&
                    firstCellValue.Length > 5)
                {
                    // 현재 행이 물품코드 행
                    itemCode = firstCellValue;
                }
                else
                {
                    // 현재 행이 제품 정보 행일 가능성이 있으므로 다음 행에서 물품코드 찾기
                    var invoiceRange = invoiceWorksheet.GetUsedRange();
                    if (selectedRow + 1 <= invoiceRange.BottomRowIndex)
                    {
                        var nextRowFirstCell = invoiceWorksheet.Cells[selectedRow + 1, 0];
                        var nextRowFirstCellValue = nextRowFirstCell.Value?.ToString()?.Trim();

                        if (!string.IsNullOrWhiteSpace(nextRowFirstCellValue) &&
                            nextRowFirstCellValue.All(char.IsDigit) &&
                            nextRowFirstCellValue.Length > 5)
                        {
                            itemCode = nextRowFirstCellValue;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(itemCode))
                {
                    XtraMessageBox.Show("선택된 행에서 물품코드를 찾을 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 4. 품목리스트에서 물품코드 열 찾기
                var (itemCodeCell, itemCodeRow) = FindCellByText(itemListWorksheet, "물품코드", 10);
                if (itemCodeCell == null)
                {
                    XtraMessageBox.Show("품목리스트에서 물품코드 열을 찾을 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int itemCodeColumnIndex = itemCodeCell.ColumnIndex;

                // 5. 품목리스트에서 해당 물품코드 찾기
                var itemListRange = itemListWorksheet.GetUsedRange();
                bool found = false;

                for (int row = itemListRange.TopRowIndex + 1; row <= itemListRange.BottomRowIndex; row++)
                {
                    var codeCell = itemListWorksheet.Cells[row, itemCodeColumnIndex];
                    var code = codeCell.Value?.ToString()?.Trim();

                    if (!string.IsNullOrWhiteSpace(code) && code == itemCode)
                    {
                        // 해당 행으로 포커스 이동
                        var targetRange = itemListWorksheet.Range.FromLTRB(itemListRange.LeftColumnIndex, row, itemListRange.RightColumnIndex, row);
                        ssItemList.ActiveWorksheet.Selection = targetRange;

                        found = true;
                        XtraMessageBox.Show($"물품코드 '{itemCode}'를 품목리스트에서 찾았습니다.", "검색 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }

                if (!found)
                {
                    XtraMessageBox.Show($"물품코드 '{itemCode}'를 품목리스트에서 찾을 수 없습니다.", "검색 결과", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"검색 중 오류가 발생했습니다: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //단가검증
        /// <summary>
        /// 지정된 텍스트를 포함한 열을 찾는 메서드
        /// </summary>
        /// <param name="worksheet">검색할 워크시트</param>
        /// <param name="searchText">검색할 텍스트</param>
        /// <param name="maxRowsToSearch">검색할 최대 행 수</param>
        /// <returns>찾은 셀 정보 (Cell, 행 인덱스), 찾지 못하면 (null, -1)</returns>
        private (Cell cell, int rowIndex) FindCellByText(DevExpress.Spreadsheet.Worksheet worksheet, string searchText, int maxRowsToSearch = 10)
        {
            var useRange = worksheet.GetUsedRange();
            if (useRange == null) return (null, -1);

            // 최대 검색 행 수 제한
            int maxRow = Math.Min(useRange.TopRowIndex + maxRowsToSearch - 1, useRange.BottomRowIndex);

            for (int row = useRange.TopRowIndex; row <= maxRow; row++)
            {
                for (int col = useRange.LeftColumnIndex; col <= useRange.RightColumnIndex; col++)
                {
                    var cell = worksheet.Cells[row, col];
                    var cellVal = cell.Value?.ToString()?.Trim();

                    if (!string.IsNullOrWhiteSpace(cellVal) && cellVal.Contains(searchText))
                    {
                        return (cell, row);
                    }
                }
            }

            return (null, -1);
        }

        private void btnPriceValidation_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 거래명세서와 품목리스트 워크시트 가져오기
            var invoiceWorksheet = ssInvoice.ActiveWorksheet;
            var itemListWorksheet = ssItemList.ActiveWorksheet;

            if (invoiceWorksheet == null || itemListWorksheet == null)
            {
                XtraMessageBox.Show("거래명세서와 품목리스트를 모두 불러와야 합니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 1. 품목리스트에서 물품코드와 단가 열 찾기
                var (itemCodeCell, itemCodeRow) = FindCellByText(itemListWorksheet, "물품코드", 10);
                var (itemPriceCell, itemPriceRow) = FindCellByText(itemListWorksheet, "단가", 10);

                if (itemCodeCell == null || itemPriceCell == null)
                {
                    XtraMessageBox.Show("품목리스트에서 물품코드 또는 단가 열을 찾을 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int itemCodeColumnIndex = itemCodeCell.ColumnIndex;
                int itemPriceColumnIndex = itemPriceCell.ColumnIndex;

                // 2. 거래명세서에서 단가 열 찾기
                var (invoicePriceCell, invoicePriceRow) = FindCellByText(invoiceWorksheet, "단가", 10);
                if (invoicePriceCell == null)
                {
                    XtraMessageBox.Show("거래명세서에서 단가 열을 찾을 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int invoicePriceColumnIndex = invoicePriceCell.ColumnIndex;

                // 3. 품목리스트에서 물품코드-단가 매핑 딕셔너리 생성
                var itemPriceMap = new Dictionary<string, string>();
                var itemListRange = itemListWorksheet.GetUsedRange();

                for (int row = itemListRange.TopRowIndex + 1; row <= itemListRange.BottomRowIndex; row++)
                {
                    var codeCell = itemListWorksheet.Cells[row, itemCodeColumnIndex];
                    var priceCell = itemListWorksheet.Cells[row, itemPriceColumnIndex];

                    var code = codeCell.Value?.ToString()?.Trim();
                    var price = priceCell.Value?.ToString()?.Trim();

                    if (!string.IsNullOrWhiteSpace(code) && !string.IsNullOrWhiteSpace(price))
                    {
                        itemPriceMap[code] = price;
                    }
                }

                // 4. 거래명세서에서 물품코드와 단가 비교
                var invoiceRange = invoiceWorksheet.GetUsedRange();
                int matchCount = 0;
                int mismatchCount = 0;

                for (int row = invoiceRange.TopRowIndex; row <= invoiceRange.BottomRowIndex; row++)
                {
                    // 물품코드 행인지 확인 (첫 번째 컬럼이 숫자로만 구성되어 있는지)
                    var firstCell = invoiceWorksheet.Cells[row, 0];
                    var firstCellValue = firstCell.Value?.ToString()?.Trim();

                    if (!string.IsNullOrWhiteSpace(firstCellValue) &&
                        firstCellValue.All(char.IsDigit) &&
                        firstCellValue.Length > 5) // 물품코드는 보통 5자리 이상
                    {
                        string itemCode = firstCellValue;

                        // 이전 행에서 단가 찾기 (물품코드 행 바로 위가 제품 정보 행)
                        if (row - 1 >= invoiceRange.TopRowIndex)
                        {
                            var prevRowPriceCell = invoiceWorksheet.Cells[row - 1, invoicePriceColumnIndex];
                            var invoicePrice = prevRowPriceCell.Value?.ToString()?.Trim();

                            if (!string.IsNullOrWhiteSpace(invoicePrice))
                            {
                                // 품목리스트에서 해당 물품코드의 단가 찾기
                                if (itemPriceMap.ContainsKey(itemCode))
                                {
                                    string itemListPrice = itemPriceMap[itemCode];

                                    // 단가 비교 (공백, 쉼표 제거 후 비교)
                                    string cleanInvoicePrice = invoicePrice.Replace(" ", "").Replace(",", "");
                                    string cleanItemListPrice = itemListPrice.Replace(" ", "").Replace(",", "");

                                    if (cleanInvoicePrice == cleanItemListPrice)
                                    {
                                        // 일치 - 초록색
                                        prevRowPriceCell.FillColor = Color.LightGreen;
                                        matchCount++;
                                    }
                                    else
                                    {
                                        // 불일치 - 빨간색
                                        prevRowPriceCell.FillColor = Color.LightCoral;
                                        mismatchCount++;
                                    }
                                }
                                else
                                {
                                    // 품목리스트에서 물품코드를 찾을 수 없음 - 노란색
                                    prevRowPriceCell.FillColor = Color.Yellow;
                                }
                            }
                        }
                    }
                }

                // 결과 메시지 출력
                string resultMessage = $"단가 검증 완료!\n\n" +
                                     $"일치: {matchCount}개 (초록색)\n" +
                                     $"불일치: {mismatchCount}개 (빨간색)\n" +
                                     $"미발견: 노란색으로 표시됨";

                XtraMessageBox.Show(resultMessage, "검증 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"단가 검증 중 오류가 발생했습니다: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPriceVEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 거래명세서와 품목리스트 워크시트 가져오기
            var invoiceWorksheet = ssInvoice.ActiveWorksheet;
            var itemListWorksheet = ssItemList.ActiveWorksheet;
            
            if (invoiceWorksheet == null || itemListWorksheet == null)
            {
                XtraMessageBox.Show("거래명세서와 품목리스트를 모두 불러와야 합니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 수정 확인
            var result = XtraMessageBox.Show("단가 검증 후 불일치하는 경우 자동으로 수정하시겠습니까?", 
                                           "단가 자동 수정", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // 1. 품목리스트에서 물품코드와 단가 열 찾기
                var (itemCodeCell, itemCodeRow) = FindCellByText(itemListWorksheet, "물품코드", 10);
                var (itemPriceCell, itemPriceRow) = FindCellByText(itemListWorksheet, "단가", 10);

                if (itemCodeCell == null || itemPriceCell == null)
                {
                    XtraMessageBox.Show("품목리스트에서 물품코드 또는 단가 열을 찾을 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int itemCodeColumnIndex = itemCodeCell.ColumnIndex;
                int itemPriceColumnIndex = itemPriceCell.ColumnIndex;

                // 2. 거래명세서에서 단가 열 찾기
                var (invoicePriceCell, invoicePriceRow) = FindCellByText(invoiceWorksheet, "단가", 10);
                if (invoicePriceCell == null)
                {
                    XtraMessageBox.Show("거래명세서에서 단가 열을 찾을 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int invoicePriceColumnIndex = invoicePriceCell.ColumnIndex;

                // 3. 품목리스트에서 물품코드-단가 매핑 딕셔너리 생성
                var itemPriceMap = new Dictionary<string, string>();
                var itemListRange = itemListWorksheet.GetUsedRange();
                
                for (int row = itemListRange.TopRowIndex + 1; row <= itemListRange.BottomRowIndex; row++)
                {
                    var codeCell = itemListWorksheet.Cells[row, itemCodeColumnIndex];
                    var priceCell = itemListWorksheet.Cells[row, itemPriceColumnIndex];
                    
                    var code = codeCell.Value?.ToString()?.Trim();
                    var price = priceCell.Value?.ToString()?.Trim();
                    
                    if (!string.IsNullOrWhiteSpace(code) && !string.IsNullOrWhiteSpace(price))
                    {
                        itemPriceMap[code] = price;
                    }
                }

                // 4. 거래명세서에서 물품코드와 단가 비교 및 수정
                var invoiceRange = invoiceWorksheet.GetUsedRange();
                int matchCount = 0;
                int editedCount = 0;
                int notFoundCount = 0;
                
                for (int row = invoiceRange.TopRowIndex; row <= invoiceRange.BottomRowIndex; row++)
                {
                    // 물품코드 행인지 확인 (첫 번째 컬럼이 숫자로만 구성되어 있는지)
                    var firstCell = invoiceWorksheet.Cells[row, 0];
                    var firstCellValue = firstCell.Value?.ToString()?.Trim();
                    
                    if (!string.IsNullOrWhiteSpace(firstCellValue) && 
                        firstCellValue.All(char.IsDigit) && 
                        firstCellValue.Length > 5) // 물품코드는 보통 5자리 이상
                    {
                        string itemCode = firstCellValue;
                        
                        // 이전 행에서 단가 찾기 (물품코드 행 바로 위가 제품 정보 행)
                        if (row - 1 >= invoiceRange.TopRowIndex)
                        {
                            var prevRowPriceCell = invoiceWorksheet.Cells[row - 1, invoicePriceColumnIndex];
                            var invoicePrice = prevRowPriceCell.Value?.ToString()?.Trim();
                            
                            if (!string.IsNullOrWhiteSpace(invoicePrice))
                            {
                                // 품목리스트에서 해당 물품코드의 단가 찾기
                                if (itemPriceMap.ContainsKey(itemCode))
                                {
                                    string itemListPrice = itemPriceMap[itemCode];
                                    
                                    // 단가 비교 (공백, 쉼표 제거 후 비교)
                                    string cleanInvoicePrice = invoicePrice.Replace(" ", "").Replace(",", "");
                                    string cleanItemListPrice = itemListPrice.Replace(" ", "").Replace(",", "");
                                    
                                    if (cleanInvoicePrice == cleanItemListPrice)
                                    {
                                        // 일치 - 초록색
                                        prevRowPriceCell.FillColor = Color.LightGreen;
                                        matchCount++;
                                    }
                                    else
                                    {
                                        // 불일치 - 자동 수정
                                        prevRowPriceCell.Value = itemListPrice;
                                        prevRowPriceCell.FillColor = Color.LightGreen; // 수정된 셀은 파란색
                                        editedCount++;
                                    }
                                }
                                else
                                {
                                    // 품목리스트에서 물품코드를 찾을 수 없음 - 노란색
                                    prevRowPriceCell.FillColor = Color.Yellow;
                                    notFoundCount++;
                                }
                            }
                        }
                    }
                }

                // 결과 메시지 출력
                string resultMessage = $"단가 검증 및 수정 완료!\n\n" +
                                     $"일치: {matchCount}개 (초록색)\n" +
                                     $"자동 수정: {editedCount}개 (파란색)\n" +
                                     $"미발견: {notFoundCount}개 (노란색)";

                XtraMessageBox.Show(resultMessage, "수정 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"단가 검증 및 수정 중 오류가 발생했습니다: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
