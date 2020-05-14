@Html.DevExpress().UploadControl(
                            Sub(settings)
                                settings.Name = "uc"
                                settings.CallbackRouteValues = New With {.Controller = "DevExpress", .Action = "UploadControlCallbackAction"}
                                settings.ClientSideEvents.FileUploadComplete = "OnFileUploadComplete"
                                settings.ValidationSettings.Assign(compliancecg.Controllers.UploadControlDemosHelper.ValidationSettings)
                            End Sub).GetHtml()

