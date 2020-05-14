

@modeltype compliancecg.Models.EFile


@Html.DevExpress().RichEdit(Sub(settings)
                                     settings.Name = "RichEdit"
                                     settings.CallbackRouteValues = New With {.Controller = "DevExpress", .Action = "RichEditPartial"}
                                 End Sub).Open(Model.FriendlyName, DevExpress.XtraRichEdit.DocumentFormat.OpenXml, Function() Model.Data).GetHtml()







