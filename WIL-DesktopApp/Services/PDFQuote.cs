using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Transactions;
using System.Windows.Documents;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WIL_DesktopApp.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Org.BouncyCastle.Crypto.Engines;

namespace WIL_DesktopApp.Services
{
        public class PdfGenerator
        {
            public void GeneratePdfAndEmail(Request request, string subject, string emailBody)
            {
                // Create a PDF document
                using (var document = new iTextSharp.text.Document())
            {
                try
                {
                    string directoryPath = "C:/KryptonPDF/"; // Replace with the path to your directory

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    string pdfFilePath = "C:/KryptonPDF/signage.pdf";

                    // Create a PdfWriter to write to the file
                    using (var writer = PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create)))
                    {
                        document.Open();

                        // Define fonts and styles
                        BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        Font titleFont = new Font(bf, 24, Font.BOLD, BaseColor.BLACK);
                        Font quoteFont = new Font(bf, 14, Font.NORMAL, BaseColor.GRAY);
                        Font descriptionFont = new Font(bf, 10, Font.NORMAL, BaseColor.BLACK);

                        // Title
                        string titleText = "Krypton Signs Quote";
                        iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph(titleText, titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        title.SpacingBefore = 20;
                        title.SpacingAfter = 10;

                        // Quote content
                        string requestIdText = $"Request ID: {request.RequestId}";
                        string emailText = $"Email: {request.Email}";
                        string requestItemsHeader = "Request Items";
                        List<string> attItems = new List<string>();
                        List<string> itemTexts = new List<string>();

                        foreach (var item in request.RequestItems)
                        {
                            itemTexts.Add($" {item.Name}");
                            itemTexts.Add($" {item.Quantity}");
                            foreach (var att in item.Attributes)
                            {
                                itemTexts.Add(att.Name);
                            }
                        }

                        // Description
                        string descriptionText = "A 70% deposit is required before any work can commence and balance on completion. " +
                        "The Sign will remain the property of Krypton Signs until paid in full. If for any reason " +
                        "payment has not been made, we hold the right to remove signs until full payment has been received " +
                        "and any additional costs will be charged if we are required to reinstall the signs. " +
                        "Sign will be manufactured and installed within 5 to 7 working days of receipt of deposit, as " +
                        "well as signed off artwork. Please make sure spelling is correct and all logos are correct " +
                        "before signing off. Any changes after signoff will be at the cost of the Customer. " +
                        "Krypton Signs " +
                        "FNB " +
                        "Gold Business Account " +
                        "Account Number: 628 564 95330";

                        // Create a table for the request items
                        PdfPTable table = new PdfPTable(2);
                        table.WidthPercentage = 100;
                        table.SetWidths(new int[] { 50, 50 });

                        // Add the request items header to the table
                        PdfPCell headerCell = new PdfPCell(new Phrase(requestItemsHeader, quoteFont));
                        headerCell.Colspan = 2;
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(headerCell);

                        // Add the request items to the table
                        foreach (var itemText in itemTexts)
                        {
                            PdfPCell itemCell = new PdfPCell(new Phrase(itemText, quoteFont));
                            table.AddCell(itemCell);
                        }
                        foreach (var att in attItems)
                        {
                            PdfPCell attCell = new PdfPCell(new Phrase(att, quoteFont));
                            table.AddCell(attCell);
                        }

                        // Add the table to the document
                        document.Add(title);
                        document.Add(new Phrase()
                    {
                        " "
                    }); document.Add(new Phrase()
                    {
                        " "
                    });

                        // Add some space between the table and the description
                        document.Add(new iTextSharp.text.Paragraph(" ", quoteFont));

                        // Add the description to the document

                        // Add a signature line to the document


                        document.Add(table);
                        document.Add(new Phrase()
                    {
                        " "
                    }); document.Add(new Phrase()
                    {
                        " "
                    });
                        document.Add(new iTextSharp.text.Paragraph(descriptionText, descriptionFont));
                        document.Add(new Phrase()
                    {
                        " "
                    }); document.Add(new Phrase()
                    {
                        " "
                    });

                        PdfPTable table2 = new PdfPTable(2);

                        PdfPCell signatureCell = new PdfPCell(new Phrase("Signature:", quoteFont));
                        signatureCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table2.AddCell(signatureCell);

                        signatureCell = new PdfPCell(new Phrase("___________________________________", descriptionFont));
                        signatureCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        table2.AddCell(signatureCell);

                        document.Add(table2);


                        // Close the document
                        document.Close();

                        Process process = new Process();
                        process.StartInfo.FileName = pdfFilePath;
                        process.StartInfo.UseShellExecute = true;
                        process.Start();


                        // Attach the generated PDF to the email
                        string attachmentUri = Uri.EscapeDataString(pdfFilePath);

                        string mailtoUri = $"mailto:{request.Email}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(emailBody)}&attach={attachmentUri}";

                        // Start the default mail client
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = mailtoUri,
                            UseShellExecute = true
                        });
                    }
                }
                catch(NullReferenceException e)
                {
                    Console.WriteLine(e.ToString());
                }                    // Set the file path for the generated PDF
                
                 
                }
                }
            }
        }
