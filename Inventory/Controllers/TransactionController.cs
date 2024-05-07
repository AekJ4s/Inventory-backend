using Inventory.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Inventory.Models;
[ApiController]
[Authorize]
[Route("transaction")]
public class TransactionController : ControllerBase
{

    private YourDbContextClassName _db = new YourDbContextClassName();
    private readonly ILogger<TransactionController> _logger;

    public TransactionController(ILogger<TransactionController> logger)
    {
        _logger = logger;
    }

    public struct TransactionCreate
    {
        
        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

    }
        [HttpPost( "BUYING",Name = "BuyProduct")]

        public ActionResult BuyProduct([FromBody] TransactionCreate transactionCreate)
        {
            Transaction transaction = new Transaction
            {
                ProductId = transactionCreate.ProductId,
                Quantity = transactionCreate.Quantity,
                TransactionTypeId = 1
            };

            
            
            transaction = Transaction.Create( _db, transaction);
            
            
            transaction.Product = _db.Products.Find(transaction.ProductId);

             if(transaction.Product != null ){ 
            transaction.Product.StockQuantity = transaction.Product.StockQuantity-transactionCreate.Quantity ;
            Product.Update(_db, transaction.Product); 
            }
            return Ok(transaction);
        }
        [HttpPost("FILLPRODUCT",Name = "FillProduct")]

        public ActionResult FillProduct([FromBody] TransactionCreate transactionCreate)
        {
            Transaction transaction = new Transaction
            {
                ProductId = transactionCreate.ProductId,
                Quantity = transactionCreate.Quantity,
                TransactionTypeId = 2 
            };

            
            
            transaction = Transaction.Create( _db, transaction);
            
            transaction.Product = _db.Products.Find(transaction.ProductId);

            if(transaction.Product != null ){ 
            transaction.Product.StockQuantity = transaction.Product.StockQuantity+transactionCreate.Quantity ;
            Product.Update(_db, transaction.Product); 
            }
            return Ok(transaction);
        }
        [HttpPost("OUTOFDATE",Name = "OutofDate")]

        public ActionResult OutofDate([FromBody] TransactionCreate transactionCreate)
        {
            Transaction transaction = new Transaction
            {
                ProductId = transactionCreate.ProductId,
                Quantity = transactionCreate.Quantity,
                TransactionTypeId = 3
            };


            
            
            
            transaction = Transaction.Create( _db, transaction);
        
            transaction.Product = _db.Products.Find(transaction.ProductId);
            if(transaction.Product != null ){ 
            transaction.Product.StockQuantity = transaction.Product.StockQuantity-transactionCreate.Quantity ;
            Product.Update(_db, transaction.Product); 
            }
            
            
            return Ok(transaction);


        }

        
        [HttpGet(Name = "ViewTransection")]
        public ActionResult ViewTransection()
            {
                List<Transaction> transactions = Transaction.GetAll(_db);
                
                return Ok( transactions );
            }
    




    // [HttpGet("GetAll",Name = "GetAllEmployees")]

    // public ActionResult GetAllEmployees()
    // {
    //     // .OrderBy(q => q.Salary) เรียงจากน้อยไปมาก
    //     // .OrderByDescending(q => q.Salary) เรียงจากมากไปน้อย
    //     List<Employee> employees = Employee.GetAll(_db).OrderByDescending(q => q.Salary).ToList();
    //     return Ok(new Response

    //     {
    //         Code = 200,
    //         Message = "Success",
    //         Data = employees

    //     });
    // }

    [HttpGet("{id}",Name = "GetTransactionsByID")]

    public ActionResult GetEmployeeById(int id)
    {
        Transaction transaction = Transaction.GetById(_db, id);
        return Ok(transaction);
    }

    // [HttpPut("UpdateEveryThing", Name = "UpdateEmployee")]

    // public ActionResult UpdateEmployee([FromBody]Employee employee)
    // {
    //     bool employeeExists = _db.Employees.Any(e => e.Id == employee.Id && e.IsDelete != true);
    //     // กรองว่ามีข้อมูล ID มาไหม
    //     int IntofData = _db.Employees.Where(e =>e.Id == employee.Id).AsNoTracking().ToList().Count();

    //     if ( IntofData > 0 ){
    //         Employee DataEmployee = _db.Employees.Where(e =>e.Id == employee.Id).AsNoTracking().ToList().First();
    //         if( employee.Firstname == null){
    //             employee.Firstname = DataEmployee.Firstname;
    //         }
    //         if( employee.Lastname == null){
    //             employee.Lastname = DataEmployee.Lastname;
    //         }
    //         if( employee.Salary == null){
    //             employee.Salary = DataEmployee.Salary;
    //         }
    //         employee.IsDelete = false; // 
    //         employee.CreateDate = DataEmployee.CreateDate; // วันที่สร้างอ้างอิงจากข้อมูลที่มีอยู่
    //         employee.UpdateDate = DateTime.Now; // อัพเดตข้อมูลของวันที่อัพเดตเป็นปัจจุบัน

    //     }else 
    //     {
    //         return BadRequest(new Response
    //         {
    //         Code = 400,
    //         Message = "you id is not found",
    //         Data = null
    //     });
    //     }
    //     if(!employeeExists)
    //     {
    //         return BadRequest(new Response
    //         {
    //         Code = 400,
    //         Message = "Employee not found",
    //         Data = null
    //     });
    //     }
    //     try
    //     {
    //         employee = Employee.Update(_db,employee);
    //     }
    //     catch (Exception e)
    //     {
    //         //Return 500
    //         return StatusCode(500,new Response
    //         {
    //             Code = 500,
    //             Message = e.Message,
    //             Data = null

    //         });
    //     }

    //     return Ok(new Response
    //     {
    //         Code = 200,
    //         Message = "Success",
    //         Data = employee
    //     });
    // }

    [HttpDelete("{id}",Name ="CancelTransaction")]

    public ActionResult CancelTransaction(int id)
    {
        Transaction transaction = Transaction.Delete(_db, id);
        return Ok(transaction);
    }


    //  [HttpPost("CreateEmployeeRequest",Name = "CreateEmployeeRequest")]

    // public ActionResult<Response> Post([FromBody] EmployeeCreateRequest employee)
    // {
    //     Employee newEmployee = new Employee
    //     {
    //         Firstname = employee.Firstname,
    //         Lastname = employee.Lastname,
    //         Salary = employee.Salary,

    //     };



    //     newEmployee = Employee.Create(_db, newEmployee);
    //     return Ok(new Response
    //     {
    //         Code = 200,
    //         Message = "Success",
    //         Data = newEmployee
    //     }
    //     );
    // }

    // [HttpPut("UpdateEmployeeRequest",Name = "UpdateEmployeeRequest")]

    // public ActionResult<Response> PUT([FromBody]EmployeeUpdateRequest employee)
    // {
    //     Employee newEmployee = new Employee
    //     {
    //         Id = employee.Id,
    //         Firstname = employee.Firstname,
    //         Lastname = employee.Lastname,
    //         Salary = employee.Salary,

    //     };

    //     bool employeeExists = _db.Employees.Any(e => e.Id == employee.Id && e.IsDelete != true);
    //     int IntofData = _db.Employees.Where(e =>e.Id == employee.Id).AsNoTracking().ToList().Count();
    //     if(IntofData > 0){
    //     if(newEmployee.Firstname == null){
    //         newEmployee.Firstname = employee.Firstname;
    //     }else{
    //         newEmployee.Firstname = newEmployee.Firstname;
    //     }
    //     if(newEmployee.Lastname == null){
    //         newEmployee.Lastname = employee.Lastname;
    //     }else{
    //         newEmployee.Lastname = newEmployee.Lastname;
    //     }
    //     if(newEmployee.Salary == null){
    //         newEmployee.Salary = employee.Salary;
    //     }else{
    //         newEmployee.Salary = newEmployee.Salary;
    //     }

    //     newEmployee.UpdateDate = DateTime.Now;
    //     newEmployee = Employee.Update(_db, newEmployee);
    //     }else{
    //         return BadRequest(new Response{
    //             Code = 400,
    //             Message = "Your Id is not found",
    //             Data = null
    //         });
    //     }

    //      try
    //     {
    //         newEmployee = Employee.Update(_db,newEmployee);
    //     }
    //     catch (Exception e)
    //     {
    //         //Return 500
    //         return StatusCode(500,new Response
    //         {
    //             Code = 500,
    //             Message = e.Message,
    //             Data = null

    //         });
    //     }

    //     return Ok(new Response
    //     {
    //         Code = 200,
    //         Message = "Success",
    //         Data = newEmployee
    //     }
    //     );
    // }





    // [HttpGet("search/{name}",Name = "SearchEmployeeByName")]

    // public ActionResult SearchEmployeeByName(string name)
    // {
    //     List<Employee> employees = Employee.Search(_db,name);
    //     if(employees.Count == 0)
    //     {
    //         return NotFound(new Response{
    //             Code = 404,
    //             Message = "Employee not found",
    //             Data=null

    //         });
    //     }
    //     return Ok(new Response
    //     {
    //         Code = 200,
    //         Message = "Sucess",
    //         Data = employees
    //     });
    // }

    // [HttpGet("page/{page}",Name ="GetAllEmployeeByPage")]

    // public ActionResult GetAllEmployeesByPage(int page)
    // {
    //     int pageSize = 3 ;
    //     List<Employee> employees = Employee.GetAll(_db).OrderByDescending(q => q.Salary).Skip((page -1 )* pageSize).Take(pageSize).ToList();
    //     return Ok(new Response

    //     {
    //         Code = 200,
    //         Message = "Success",
    //         Data = employees
    //     });
    // }

    // [HttpGet("export/word",Name = "ExportEmployeeToWord")]

    // public IActionResult ExportEmployeeToWord()
    // {
    //     List<Employee> employees = Employee.GetAll(_db).ToList();

    //     using (DocX document = DocX.Create("SampleDocument.docx"))
    //     {   
    //         // Add a title to the document
    //         document.InsertParagraph("List of employees").FontSize(18).Bold().Alignment = Alignment.center;

    //         // Add a Table with some data
    //         Table table = document.AddTable(1,4);
    //         table.Design = TableDesign.ColorfulList;
    //         table.Alignment = Alignment.center;
    //         table.AutoFit = AutoFit.Contents;

    //         // Add headers to the table
    //         table.Rows[0].Cells[0].Paragraphs[0].Append("ID").Bold();
    //         table.Rows[0].Cells[1].Paragraphs[0].Append("Firstname").Bold();
    //         table.Rows[0].Cells[2].Paragraphs[0].Append("Lastname").Bold();
    //         table.Rows[0].Cells[3].Paragraphs[0].Append("Salary").Bold();

    //         // Add data to table
    //         for(int i = 0 ; i < employees.Count ; i++)
    //         {
    //             table.InsertRow();
    //             table.Rows[i+1].Cells[0].Paragraphs[0].Append(employees[i].Id.ToString());
    //             table.Rows[i+1].Cells[1].Paragraphs[0].Append(employees[i].Firstname);
    //             table.Rows[i+1].Cells[2].Paragraphs[0].Append(employees[i].Lastname);
    //             table.Rows[i+1].Cells[3].Paragraphs[0].Append(employees[i].Salary.ToString()); 
    //         }

    //         document.InsertTable(table);

    //         // Save the document to a memory stream
    //         System.IO.MemoryStream stream = new System.IO.MemoryStream();
    //         document.SaveAs(stream);

    //         //Reset the Stream position
    //         stream.Position = 0 ;

    //         // Set the content type and file name for the response
    //         string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
    //         string fileName = "SampleDocument";

    //         return File(stream, contentType, fileName);


    //     }


    // }

    // [HttpGet("export/excel", Name = "ExportEmployeeToExcel")]

    // public IActionResult ExportEmployeeToExcel(){
    //     List<Employee> employees = Employee.GetAll(_db).ToList();


    //     // Create a new Excel Package
    //     using (ExcelPackage package = new ExcelPackage())
    //     {
    //         //Create a worksheet 
    //         ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employees");


    //         // set the columns headers and decorate them with styles
    //         var headerCells = worksheet.Cells["A1:D1"];
    //         headerCells.Style.Font.Bold = true;
    //         headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
    //         headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
    //         headerCells.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

    //         //set the column headers
    //         worksheet.Cells[1, 1].Value = "ID";
    //         worksheet.Cells[1, 2].Value = "First Name";
    //         worksheet.Cells[1, 3].Value = "Last Name";
    //         worksheet.Cells[1, 4].Value = "Salary";


    //         //Add data to worksheet
    //         int row = 2;
    //         foreach( var employee in employees){
    //             worksheet.Cells[row,1].Value = employee.Id;
    //             worksheet.Cells[row,2].Value = employee.Firstname;
    //             worksheet.Cells[row,3].Value = employee.Lastname;
    //             worksheet.Cells[row,4].Value = employee.Salary;


    //             //Apply Cell Border
    //             worksheet.Cells[row, 1,row ,4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
    //             row++;
    //         }

    //         // Auto-fit the Column
    //         worksheet.Cells.AutoFitColumns();

    //         //Convert the Excel Package to a byte array
    //         byte[] excelBytes = package.GetAsByteArray();

    //         // Set the content type and file name for the response
    //         string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //         string fileName = "Employees.xlsx";

    //         // Return the Excel file as a file result
    //         return File(excelBytes, contentType, fileName);
    //     }
    // }

    // [HttpGet("ExportToPdf",Name = "ExportToPdf")]

    // public ActionResult ExportToPdf()
    // {
    //     // นำเข้าข้อมูลจาก DATABASE ชื่อ Employee และ Department
    //     List<Employee> employees = _db.Employees.Where(q => q.IsDelete == false).Include(q => q.Department).ToList();

    //     //Create a new PDF document
    //     PdfDocument document = new PdfDocument();

    //     // Create a new page
    //     PdfPage page = document.AddPage();

    //     //Create a graphic object for drawing on the page
    //     XGraphics gfx = XGraphics.FromPdfPage(page);

    //     // Create a font
    //     XFont font = new XFont("Arial",12,XFontStyle.Bold);

    //     //Set the Column width
    //     double[] columnWidths = {100,150,150,100};

    //     //Set the starting position for drawing the table
    //     double x = 40 , y = 40;

    //     //Calculate the header height with a buffer for potential line wrapping
    //     double headerHeight = gfx.MeasureString("Name",font).Height * 2 ;

    //     //Define the Light blue color for the header row
    //     XColor lightBlue = XColor.FromArgb(173,216,230);

    //     //Draw table header background with light blue color
    //     gfx.DrawRectangle(XBrushes.LightBlue, x, y, columnWidths.Sum(), headerHeight);

    //     //Draw table header border (optional)
    //     gfx.DrawRectangle(XPens.Black, x, y, columnWidths.Sum(), headerHeight);

    //     //Draw the table headers inside the border with vertical centering
    //     gfx.DrawString("Name", font, XBrushes.Black,new XRect(x, y, columnWidths[0],headerHeight), XStringFormats.Center);
    //     gfx.DrawString("Department", font, XBrushes.Black,new XRect(x + columnWidths[0], y, columnWidths[1], headerHeight), XStringFormats.Center);
    //     gfx.DrawString("Salary", font, XBrushes.Black,new XRect(x +  columnWidths[0] + columnWidths[1] + columnWidths[2] , y , columnWidths[3] , headerHeight), XStringFormats.Center);

    //     // Move to the next row 
    //     y += headerHeight ;


    //     // Draw the table rows
    //     for (int i = 0 ; i < employees.Count ; i++)
    //     {
    //         //Alternate row color

    //         if ( i%2==0)
    //             gfx.DrawRectangle(XBrushes.LightGray,x,y,columnWidths.Sum(),headerHeight);

    //         //Draw row border
    //         gfx.DrawRectangle(XPens.Black, x, y, columnWidths.Sum(),headerHeight);

    //         // Draw the employee information with vertical centering
    //         gfx.DrawString(employees[i].Firstname + employees[i].Lastname , font, XBrushes.Black, new XRect( x, y, columnWidths[0], headerHeight), XStringFormats.Center);
    //         // ถ้าข้อมูลในตารางไม่มี ERROR!!!
    //         // Check department if it null send the text null in to PDF File
    //        if(employees[i].Department != null){
    //         gfx.DrawString(employees[i].Department.Name, font, XBrushes.Black, new XRect(x + columnWidths[0], y, columnWidths[1], headerHeight), XStringFormats.Center);
    //         }else{
    //         gfx.DrawString("null", font, XBrushes.Black, new XRect(x + columnWidths[0], y, columnWidths[1], headerHeight), XStringFormats.Center);          
    //         }
    //         gfx.DrawString(employees[i].Salary.ToString(), font, XBrushes.Black, new XRect(x +  columnWidths[0] + columnWidths[1] + columnWidths[2] , y , columnWidths[3] , headerHeight), XStringFormats.Center);

    //         // Move to the next row
    //         y += headerHeight;
    //     }

    //     //Save the document to a memory stream
    //     MemoryStream stream = new MemoryStream();
    //     document.Save(stream);
    //     stream.Position = 0;

    //     return File(stream, "application/pdf", "EmployeeTable.pdf");
    // }


}
