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
        let formattedDate = DayJs("12-25-1995", "MM-DD-YYYY").ToString()

        IndexTemplate.Main()
            .CustomParseFormat(
                Doc.Concat [
                    p [] [text someDate]
                    p [] [text formattedDate]
                ]
            )
            .Doc()
        |> Doc.RunById "main"
