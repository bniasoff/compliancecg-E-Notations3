  @Html.DevExpress().CallbackPanel(Sub(settings)
                                          settings.Name = "CallbackPanel"
                                          settings.CallbackRouteValues = New With {.Controller = "DevExpress", .Action = "CallbackPanelPartial"}
                                          settings.SetContent(Sub()
                                                                  Html.RenderAction("RichEditPartial", New With {.path = ViewData("path")})
                                                              End Sub)
                                      End Sub).GetHtml()
