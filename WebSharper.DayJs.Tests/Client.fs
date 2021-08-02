namespace WebSharper.DayJs.Tests

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI
open WebSharper.UI.Html
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.DayJs

[<JavaScript>]
module Client =

    type IndexTemplate = Template<"index.html", ClientLoad.FromDocument>

    [<SPAEntryPoint>]
    let Main () =

        let someDate = DayJs("2019-01-25").ToString()
        let formattedDate = DayJs("12-25-1995", "assdfsdfsdf").ToString()

        IndexTemplate.Main()
            .DatesAsString(
                Doc.Concat [
                    p [] [text someDate]
                    p [] [text formattedDate]
                ]
            )
            .ObjectSupp(
                let fromObj = FromStandardObject()
                fromObj.Year <- 2010
                fromObj.Month <- 3
                fromObj.Day <- 5
                fromObj.Hour <- 15
                fromObj.Minute <- 10
                fromObj.Second <- 3
                fromObj.Millisecond <- 123
                p [] [text ((DayJs(fromObj)).ToString())]
            )
            .Doc()
        |> Doc.RunById "main"
