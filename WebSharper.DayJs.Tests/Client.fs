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
                fromObj.Years <- 2010
                fromObj.Months <- 3
                fromObj.Date <- 5
                fromObj.Hours <- 15
                fromObj.Minutes <- 10
                fromObj.Seconds <- 3
                fromObj.Milliseconds <- 123
                p [] [text ((DayJs(fromObj)).ToString())]
            )
            .Doc()
        |> Doc.RunById "main"
