Imports System.IO
Imports System.Web.Mvc
Imports CCGData
Imports CCGData.CCGData
Imports CCGData.CCGData.CCGDataEntities
Imports System.IO.Compression
Imports Newtonsoft.Json
Imports System.Net.Sockets

Namespace Controllers
    Public Class FormsController
        Inherits Controller
        Private DataRepository As New DataRepository
        ' GET: Forms
        ' Function Index(Folder As String) As ActionResult
        '     Dim viewModel As DocFiles = DisplayFilesForDownload(Folder)
        '     Dim SubFolder As String
        '     Select Case Folder
        '         Case "generalforms"
        '             SubFolder = "General Forms"
        '         Case "acknowledgmentforms"
        '             SubFolder = "Acknowledgment Forms"
        '         Case "complianceofficer"
        '             SubFolder = "Compliance Officer"
        '         Case "posters"
        '             SubFolder = "Posters"
        '         'Case "statespecific"
        '         '    SubFolder = "State Specific"
        '         Case "training"
        '             SubFolder = "Training"
        '             'Case "misc"
        '             '    SubFolder = "Misc"
        '     End Select
        '
        '     ViewBag.Folder = SubFolder
        '
        '     Return PartialView("Index", viewModel)
        ' End Function
        '
        '
        ' Public Function DisplayFilesForDownload(Folder As String) As DocFiles
        '     Dim SubFolder As String = String.Empty
        '     Select Case Folder
        '         Case "generalforms"
        '             SubFolder = "General Forms"
        '         Case "acknowledgmentforms"
        '             SubFolder = "Acknowledgment Forms"
        '         Case "complianceofficer"
        '             SubFolder = "Compliance Officer"
        '         Case "posters"
        '             SubFolder = "Posters"
        '         'Case "statespecific"
        '         '    SubFolder = "State Specific"
        '         Case "training"
        '             SubFolder = "Training"
        '             'Case "misc"
        '             '    SubFolder = "Misc"
        '     End Select
        '
        '     Dim viewModel = New DocFiles With {.Path = Server.MapPath("../App_Data/Forms/") + SubFolder, .Files = New List(Of DocFile)()}
        '     'Dim paths = Directory.GetFiles(viewModel.Path).ToList()
        '
        '
        '
        '     'For Each path In paths
        '     '    Dim fileInfo = New FileInfo(path)
        '     '    Dim File = New DocFile With {
        '     '        .Path = path,
        '     '        .Name = fileInfo.Name,
        '     '        .Ext = fileInfo.Extension
        '     '    }
        '     '    viewModel.Files.Add(File)
        '     'Next
        '
        '     Return viewModel
        ' End Function
        ' Public Function DisplayRescourceFileForDownload(ResourceFile As String) As DocFiles
        '     Dim SubFolder As String = String.Empty
        '     Dim File2 As String = ""
        '     Dim viewModel = New DocFiles
        '     Select Case ResourceFile
        '         Case "exclusionlist"
        '             SubFolder = "Resources"
        '             File2 = "Exclusion List.pdf"
        '             viewModel = New DocFiles With {.Path = Server.MapPath("../App_Data/Resources/"), .Files = New List(Of DocFile)()}
        '         Case "hipaa"
        '             SubFolder = "Resources"
        '             viewModel = New DocFiles With {.Path = Server.MapPath("../App_Data/Resources/"), .Files = New List(Of DocFile)()}
        '
        '         Case "complianceofficer"
        '             SubFolder = "Compliance Officer"
        '             viewModel = New DocFiles With {.Path = Server.MapPath("../App_Data/Forms/Compliance Officer/"), .Files = New List(Of DocFile)()}
        '     End Select
        '
        '     Dim paths = Directory.GetFiles(viewModel.Path).ToList()
        '
        '     For Each path In paths
        '         Dim fileInfo = New FileInfo(path)
        '         Dim File = New DocFile With {
        '             .Path = path,
        '             .Name = fileInfo.Name
        '         }
        '         viewModel.Files.Add(File)
        '     Next
        '
        '
        '
        '
        '
        '     Return viewModel
        ' End Function


        Function Resources(File As String) As ActionResult
            Dim viewModel As DocFiles = DisplayRescourceFileForDownload2(File)

            Dim ViewForm As String = Nothing
            ViewForm = "Resources"
            Return PartialView(ViewForm, viewModel)
        End Function

        Function CovidResources(Page As String, Optional SMemoID As Integer = 0) As ActionResult
            Dim CovidTools As List(Of CovidTool) = DataRepository.GetCovidTools()
            Dim CovidMemos As List(Of CovidMemo) = DataRepository.GetCovidMemos()

            Dim viewModel = New FormsViewModel()
            viewModel.Page = Page

            If Page = "memos" Then
                viewModel.SelectedMemo = CovidMemos(0)
                If SMemoID > 0 Then
                    viewModel.SelectedMemo = CovidMemos.Where(Function(t) t.MemoID = SMemoID)(0)
                End If
            End If
            'Dim CovidJoin = CovidTools.Join(CovidMemos,
            '                        Function(c) c.MemoID,
            '                        Function(m) m.MemoID,
            '                        Function(c, m) New With {.ToolID = c.ToolID, .ToolName = c.ToolName, .ToolPath = c.ToolPath, .MemoID = c.MemoID, .MemoName = m.MemoName})
            viewModel.CovidMemos = CovidMemos
            viewModel.CovidTools = CovidTools

            Dim ViewForm As String = Nothing
            ViewForm = "covid19"
            Return PartialView(ViewForm, viewModel)
        End Function

        Public Function DisplayRescourceFileForDownload2(ResourceFile As String) As DocFiles
            Dim SubFolder As String = String.Empty
            Dim viewModel = New DocFiles
            Select Case ResourceFile
                Case "acknowledgmentforms"
                    SubFolder = "Acknowledgment Forms"
                Case "packets"
                    SubFolder = "Acknowledgment Forms/Packets"
                Case "complianceofficer"
                    SubFolder = "Compliance Officer"
                Case "exclusionlist"
                    SubFolder = "Exclusion List"
                Case "generalformsinformation"
                    SubFolder = "General Forms and Information"
                Case "hipaa"
                    SubFolder = "HIPAA"
                Case "humanresources"
                    SubFolder = "Human Resources"
                Case "requiredposters"
                    SubFolder = "Required Posters"
            End Select

            viewModel = GetResourceFiles(SubFolder)

            Return viewModel
        End Function

        Private Function GetResourceFiles(SubFolder As String) As DocFiles
            Dim viewModel = New DocFiles
            viewModel = New DocFiles With {.Path = Server.MapPath("../App_Data/Resources/" + SubFolder), .Files = New List(Of DocFile)()}

            Dim Paths = Directory.GetFiles(viewModel.Path)

            For Each Path In Paths
                Dim fileInfo = New FileInfo(Path)
                Dim File = New DocFile With {
                    .Path = Path,
                    .Name = fileInfo.Name,
                    .Ext = fileInfo.Extension
                }
                viewModel.Files.Add(File)
            Next

            Select Case SubFolder
                Case = "Acknowledgment Forms"
                    GetAcknowledgmentForms(viewModel)
                    viewModel.Title = "Individual Forms"
                Case = "Acknowledgment Forms/Packets"
                    GetAcknowledgmentForms(viewModel)
                    viewModel.Title = "Packets"
                Case = "Compliance Officer"
                    GetComplianceOfficer(viewModel)
                    viewModel.Title = SubFolder
                Case = "Exclusion List"
                    GetExclusionList(viewModel)
                    viewModel.Title = SubFolder
                Case = "General Forms and Information"
                    GetGeneralFormsandInformation(viewModel)
                    viewModel.Title = SubFolder
                Case = "HIPAA"
                    GetHIPAA(viewModel)
                    viewModel.Title = SubFolder
                Case = "Human Resources"
                    GetHumanResources(viewModel)
                    viewModel.Title = SubFolder
                Case = "Required Posters"
                    GetRequiredPosters(viewModel)
                    viewModel.Title = SubFolder
            End Select



            Return viewModel
        End Function

        Private Function GetAcknowledgmentForms(viewModel)
            Try
                For Each file As DocFile In viewModel.Files
                    Select Case file.Name
                        Case = "CCG 00102b Code of Conduct Acknowledgment Form.pdf"
                            file.Description = "Form to be signed stating individual has received a copy of the Facility's Code of Conduct, Corporate Compliance and Ethics plan"
                        Case = "CCG 00201a DRA acknowledgement form.pdf"
                            file.Description = "Form to be signed stating individual has received and read, understands and agrees to follow the Deficit Reduction Act of 2005 Policy and Procedure"
                        Case = "CCG 00202a Acknowledgement of Receipt of training in FWA.pdf"
                            file.Description = "Form to be signed stating individual has been provided with a copy of and training in the Facility’s Fraud, Waste and Abuse Policies and Procedures"
                        Case = "CCG 00207b Overpayment Acknowledgement Form.pdf"
                            file.Description = "Form to be signed stating individual has received a copy of and will comply with the facility’s Overpayment Self-Disclosure Policy and Procedure"
                        Case = "CCG 00209a Annual Conflicts of Interest Disclosure Form.pdf"
                            file.Description = "A form for employees and contractors in positions to influence the Facility’s decision-making to annually disclose potential conflicts of interest and to execute an acknowledgment confirming that he or she has complied with the Facility’s polices regarding conflicts of interest"
                        Case = "CCG 00304b EJA Acknowledgement form.pdf"
                            file.Description = "Form to be signed stating individual has read and agrees to the facility’s Policy and Procedure Regarding Resident Freedom from Abuse, Neglect, and Exploitation and the Elder Justice Act"
                        Case = "CCG 00307a Substance Abuse Acknowledgement of Receipt and Review.pdf"
                            file.Description = "Form to be signed stating individual has read, understands and agrees to comply with the Facility’s Substance Abuse in Workplace Policy and Procedure"
                        Case = "CCG 00308a Workplace Searches Acknowledgment of Receipt and Review.pdf"
                            file.Description = "Form to be signed stating individual has read, understands and agrees to comply with the Facility’s Workplace Searches Policy and Procedure"
                        Case = "CCG 00312a Disability and Pregnancy Accommodations Acknowledgement.pdf"
                            file.Description = "Form to be signed stating individual has read, understands and agrees to comply with the Facility’s Disability Accommodations Policy and Procedure"
                        Case = "CCG 00447a Acknowledgement Form.pdf"
                            file.Description = "Form to be signed stating individual has read, understands and agrees to comply with the Facility’s Policy and Procedure regarding Resident Privacy"
                        Case = "CCG 00516a Acknowledgement Form.pdf"
                            file.Description = "Form to be signed stating individual has read, understands and agrees to comply with the Facility’s Policy and Procedure regarding Resident Rights"
                    End Select
                Next
            Catch ex As Exception

            End Try
        End Function
        Private Function GetComplianceOfficer(viewModel)
            Try
                For Each file As DocFile In viewModel.Files
                    Select Case file.Name
                        Case = "CCG 00105 Resolution Designating a Compliance Officer.pdf"
                            file.Description = "Resolution documenting the appointment of a Compliance and Ethics Officer"
                        Case = "CCG 00106 Resolution Designating a Compliance Committee.pdf"
                            file.Description = "Resolution documenting the appointment of a Compliance And Ethics Committee"
                        Case = "CCG 00113a Compliance Reporting Form.pdf"
                            file.Description = "Form to document reports of compliance and ethics violations"
                        Case = "CCG 00117a Corrective Action Plan Template.pdf"
                            file.Description = "Plan to address a weakness in system and prevention of problem recurrence"
                        Case = "CCG 00505a Grievance Form.pdf"
                            file.Description = "Form for residents, family members, or others to use for filing a grievance"
                        Case = "CCG 00505c Grievance Decision.pdf"
                            file.Description = "Form for the Grievance Officer to provide his/her conclusion as to grievances"
                        Case = "Compliance Committee Meeting Minutes.pdf"
                            file.Description = "Template to record compliance committee meeting minutes"
                        Case = "Compliance Officer checklist.pdf"
                            file.Description = "Comprehensive document highlighting the Compliance and Ethics Officers tasks and responsibilities"
                        Case = "Compliance Officer Evaluation.pdf"
                            file.Description = "Form used for annual evaluation of Compliance and Ethics Officer"
                        Case = "Compliance Officer Quarterly Report to the Governing Body.pdf"
                            file.Description = "Report to be filled out by the Compliance and Ethics officer quarterly for the Governing Board"
                        Case = "Designating a Governing Body.pdf"
                            file.Description = "Document documenting the members of the Facility’s Governing Body"
                        Case = "Facility Assessment Tool.pdf"
                            file.Description = "Facility-wide assessment designed to determine what resources are necessary to care for the Facility’s residents"
                    End Select
                Next
            Catch ex As Exception

            End Try
        End Function

        Private Function GetExclusionList(viewModel)
            Try
                For Each file As DocFile In viewModel.Files
                    Select Case file.Name
                        Case = "Exclusion List.pdf"
                            file.Description = "Document indicating how to check federal and state exclusion lists on a monthly basis"
                    End Select
                Next
            Catch ex As Exception

            End Try
        End Function

        Private Function GetGeneralFormsandInformation(viewModel)
            Try
                For Each file As DocFile In viewModel.Files
                    Select Case file.Name
                        Case = "CCG 00508 Resident Smoking Agreement.pdf"
                            file.Description = "Agreement to be executed by residents who are smokers"
                        Case = "CCG Form 00215a Identity Theft Victim's Complaint and Affidavit.pdf"
                            file.Description = "A form for filing an identity theft report with law enforcement and credit reporting agencies"
                        Case = "Resident Fund Authorization.pdf"
                            file.Description = "Document to authorize the Facility to handle a resident’s funds"
                        Case = "Trauma-Informed Care Explained.pdf"
                            file.Description = "Comprehensive document highlighting all aspects of trauma and how to care for those suffering from trauma"
                    End Select
                Next
            Catch ex As Exception

            End Try
        End Function

        Private Function GetHIPAA(viewModel)
            Try
                For Each file As DocFile In viewModel.Files
                    Select Case file.Name
                        Case = "CCG 00107 Resolution Appointing HIPAA Privacy And Security Officer.pdf"
                            file.Description = "Resolution documenting the appointment of a HIPAA Privacy and Security Officer"
                        Case = "CCG 00214 Business Associate Agreement.pdf"
                            file.Description = "A contract between the Facility and a Business Associates that has access to Protected Health Information (""PHI"")"
                        Case = "CCG 00214a Business Associate Agreement Cover Lettter.pdf"
                            file.Description = ""
                        Case = "CCG 00402a Notice of Privacy Practices and Acknowledgement Form.pdf"
                            file.Description = "Form to be executed by all residents upon admission acknowledging the receipt of a Notice of Privacy Practices"
                        Case = "CCG 00408b Inventory of Locations, Phy Systems, Devices,  Media Containing PHI.pdf"
                            file.Description = "Form to list locations where PHI is stored"
                        Case = "CCG 00412a Fax Cover Page.pdf"
                            file.Description = "Cover sheet to be used when faxing PHI"
                        Case = "CCG 00423a Request for Correction of Protected Health Information Form.pdf"
                            file.Description = "Form for residents to request the amendment of their PHI"
                        Case = "CCG 00424a Right to Accounting of Disclosures of Protected Health Information Form.pdf"
                            file.Description = "Document for residents to use when requesting an accounting of disclosures of their PHI"
                        Case = "CCG 00425a Request to Restrict Use and Disclosure of Protected Health Information Form.pdf"
                            file.Description = "Document for residents to use when requesting a restriction on the use or disclosure of their PHI"
                        Case = "CCG 00428a HIPAA authorization.pdf"
                            file.Description = "Form authorizing the use and disclosure of a resident’s PHI"
                        Case = "CCG 00447a Acknowledgement Form.pdf"
                            file.Description = "A form for residents to execute that permits the Facility to take photographs or other audio-visual images or materials of the resident"
                        Case = "CMS Guidance on Texting.pdf"
                            file.Description = "Guidance to assist in ensuring that text messages are HIPAA compliant"
                        Case = "Suspected HIPAA Breach Incident Packet.pdf"
                            file.Description = "Checklist for investigating any reported HIPAA-related incidents within the facility"
                    End Select
                Next
            Catch ex As Exception

            End Try
        End Function

        Private Function GetHumanResources(viewModel)
            Try
                For Each file As DocFile In viewModel.Files
                    Select Case file.Name
                        Case = "CCG 00305a FMLA Request Form.pdf"
                            file.Description = "Form for eligible employees to request Family and Medical Leave"
                        Case = "CCG 00311 Physician Credentialing Packet and Agreements.pdf"
                            file.Description = "Physician Credentialing checklist and agreements"
                        Case = "CCG 00512a Volunteer Screening Authorization.pdf"
                            file.Description = "Form for potential volunteers to authorize the release of their criminal background screening reports to the Facility"
                        Case = "CCG Form 00305c FMLA Designation Notice.pdf"
                            file.Description = ""
                        Case = "CCG Form 00305d Notice of Eligibility and Rights and Responsibilities.pdf"
                            file.Description = ""
                        Case = "CCG Form 00305e Certification for Qualifying Exigency for Military Family Leave.pdf"
                            file.Description = ""
                        Case = "CCG Form 00305f Certification for Serious Injury or Illness of a Current Service Member for Military Family Leave.pdf"
                            file.Description = ""
                        Case = "CCG Form 00305g Certification for Serious Injury or Illness of a Veteran for Military Caregiver Leave.pdf"
                            file.Description = ""
                        Case = "CCG Form 00305h Certification of Health Care Provider for Employee's Serious Health Condition.pdf"
                            file.Description = ""
                        Case = "CCG Form 00305i Certification of Health Care Provider for Family Member.pdf"
                            file.Description = ""

                    End Select
                Next
            Catch ex As Exception

            End Try
        End Function

        Private Function GetRequiredPosters(viewModel)
            Try
                For Each file As DocFile In viewModel.Files
                    Select Case file.Name
                        Case = "CCG 00304a Elder Justice Act Poster.pdf"
                            file.Description = "Poster stating the requirement for employees to report suspected crimes against residents as well as the rights of employees related to reporting"
                            file.PostingRequirement = "should be posted for employees in a conspicuous place such an employee breakroom"
                        Case = "CCG 00305b FMLA Notice.pdf"
                            file.Description = "Poster prepared by the U.S. Department of Labor that summarizes the major provisions of the Family and Medical Leave Act and telling employees how to file a complaint"
                            file.PostingRequirement = "should be posted for employees in a conspicuous place such an employee breakroom"
                        Case = "CCG 00402a Notice of Privacy Practices and Acknowledgement Form.pdf"
                            file.Description = "Notice that provides an explanation of residents’ rights with respect to their PHI and the privacy practices of the Facility"
                            file.PostingRequirement = "Should be posted at the entrance to the facility to ensure that all residents and family members have access to the notice"
                        Case = "CCG 00412b Fax Machine Poster.pdf"
                            file.Description = "Unauthorized persons prohibited from viewing sent or received faxed documents"
                            file.PostingRequirement = "Should be posted by the fax machine"
                        Case = "CCG 00446a Resident Privacy Poster, Media Device Poster.pdf"
                            file.Description = "Poster stating that policy prohibiting photographing, videoing, or recording any resident in the Facility."
                            file.PostingRequirement = "Should be posted throughout the facility to ensure that all staff, contractors, vendors, residents, and family members are aware of this prohibition."
                        Case = "CCG 00503a Nondiscrimination Notice.pdf"
                            file.Description = "Notice stating that the facility does not discriminate on the basis of race, color, national origin, sex, age, or disability, and no resident shall be denied admission or appropriate care and placement following admission because of race, creed, color, national origin, ancestry, age, sex, handicap, disability, or any other category prohibited by applicable federal, state, or local laws and regulations"
                            file.PostingRequirement = "Should be posted throughout the facility to ensure that all residents and family members have access to the notice"
                        Case = "CCG 00505b Grievance Poster.pdf"
                            file.Description = "Poster explaining the Grievance Policy and the steps to take to file a grievance"
                            file.PostingRequirement = "Should be posted throughout the facility to ensure that all"
                        Case = "Common Taglines.pdf"
                            file.Description = "Language assistance availability notice"
                            file.PostingRequirement = "Should be posted conspicuously at the front desk"
                        Case = "Compliance and Ethics Hotline Poster"
                            file.Description = ""
                            file.PostingRequirement = "Should be posted throughout the facility to ensure that all staff, contractors, vendors, residents, and family members are aware of their ability to make an anonymous and confidential report of noncompliance"
                        Case = "State specific contact information for state agencies and advocacy groups"
                            file.Description = ""
                            file.PostingRequirement = ""
                        Case = "Nurse staffing information"
                            file.Description = "1. Facility name<br/>2. Date<br/>3. Staffing data"
                            file.PostingRequirement = "Should be posted in a conspicuous location in the facility readily accessible to residents and visitors"
                        Case = "Most recent survey and notice of availability of reports"
                            file.Description = ""
                            file.PostingRequirement = "Should be posted in a conspicuous location in the facility readily accessible to residents and visitors"
                        Case = "Notice of Medicaid/Medicare on Admissions"
                            file.Description = ""
                            file.PostingRequirement = "Should be posted in a conspicuous location in the facility"
                        Case = "State and Federal Labor Laws"
                            file.Description = "Rights and duties for employees"
                            file.PostingRequirement = ""
                        Case = "Ombudsman information"
                            file.Description = "State officials appointed to investigate individuals' complaints against maladministration."
                            file.PostingRequirement = "Should be posted in a conspicuous location in the facility readily accessible to residents and visitors"
                        Case = "Fire Safety Diagrams/Evacuation Routes"
                            file.Description = "As per individual building codes"
                            file.PostingRequirement = ""
                        Case = "State specific Resident Bill of Rights"
                            file.Description = ""
                            file.PostingRequirement = "Should be posted throughout the facility to ensure that all residents and family members have access to the notice"
                        Case = "Smoking Area"
                            file.Description = "1. Designated: Inside and Outside of the facility<br/>2. No Smoking: if applicable"
                            file.PostingRequirement = "Should be posted by the smoking areas"
                    End Select
                Next
            Catch ex As Exception

            End Try
        End Function






        Public Function Download(ByVal FilePath As String, ByVal FileName As String) As FileResult
            Return File(FilePath, System.Net.Mime.MediaTypeNames.Text.Xml, FileName)
            'Dim fileBytes As Byte() = System.IO.File.ReadAllBytes("c:\folder\myfile.ext")
            ''Dim fileName As String = "myfile.ext"
            'Return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName)
        End Function

        'Private Sub SurroundingSub()
        '    Dim list As IList(Of MultimediaFile) = d.MultimediaFiles.Where(Function(l) l.MultmediaId = id).ToList()
        '    ViewData("Files") = list
        'End Sub



        '<HttpPost>
        'Public Function Download(ByVal files As List(Of String)) As FileResult
        '    Dim archive = Server.MapPath("~/archive.zip")
        '    Dim temp = Server.MapPath("~/temp")

        '    If System.IO.File.Exists(archive) Then
        '        System.IO.File.Delete(archive)
        '    End If

        '    Directory.EnumerateFiles(temp).ToList().ForEach(Function(f) System.IO.File.Delete(f))
        '    files.ForEach(Function(f) System.IO.File.Copy(f, Path.Combine(temp, Path.GetFileName(f))))
        '    ZipFile.CreateFromDirectory(temp, archive)
        '    Return File(archive, "application/zip", "archive.zip")
        'End Function

    End Class





    Public Class DocFile
        Public Property Name As String
        Public Property Path As String
        Public Property Ext As String
        Public Property Description As String
        Public Property PostingRequirement As String
    End Class

    Public Class DocFiles
        Public Property Title As String
        Public Property Files As List(Of DocFile)
        Public Property Path As String
    End Class

End Namespace